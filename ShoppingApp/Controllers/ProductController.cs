﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingApp.Data;
using ShoppingApp.Models;
using X.PagedList;

namespace ShoppingApp.Controllers
{
    public class ProductController : Controller
    {
        //每個分頁最多顯示9筆
        private readonly int pageSize = 9;

        // 使用 DI 注入會用到的工具
        private readonly ApplicationDbContext _context;
        private static IMemoryCache _memoryCache;
        private readonly ILogger _logger;

        public ProductController(
            ApplicationDbContext context, 
            IMemoryCache memoryCache, 
            ILogger<OrderFormController> logger)
        {
            _context = context;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name)) return NotFound();
            
            // 按照產品的日期排序(新->舊)
            return View(await _context.Product.OrderByDescending(p => p.PublishDate).ToPagedListAsync(page, 10));
        }

        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // 取得此產品的所有留言
            var TheComments = _context.Comment.Where(c => c.ProductId == id).OrderByDescending(c => c.CreateTime).ToList();

            // 封裝此產品和此產品的留言
            List<ProductAndComment> productAndComments = new List<ProductAndComment>();

            if (TheComments.Count > 0)
            {
                foreach (var comment in TheComments)
                {
                    productAndComments.Add(new ProductAndComment
                    {
                        TheProduct = product,
                        TheComment = comment
                    });
                };
            }
            else
            {
                // 若此產品沒有留言，則添加產品資訊即可
                productAndComments.Add(new ProductAndComment
                {
                    TheProduct = product,
                    TheComment = null
                });
            }

            // 傳送封裝的類別，每頁顯示10筆留言
            return View(await productAndComments.ToPagedListAsync(page, 10));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,PublishDate,Quantity,DefaultImageURL,FromProduct2")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.PublishDate = DateTime.Now;
                product.FromProduct2 = false;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name)) return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,PublishDate,Quantity,DefaultImageURL,FromProduct2")] Product product)
        {
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name)) return NotFound();

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.FromProduct2 = false;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name)) return NotFound();

            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            _logger.LogWarning($"[{User.Identity.Name}]刪除了產品[{product.Name}]");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAll()
        {
            if (User.Identity.Name != AuthorizeManager.SuperAdmin) return NotFound();

            _context.RemoveRange(_context.Product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name))
            {
                return false;
            }

            return _context.Product.Any(e => e.Id == id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(int id, string comment)
        {
            // 檢查這個IP的留言次數
            string ClientIP = HttpContext.Connection.RemoteIpAddress.ToString();
            if (CommentManager.GetCommentCountByIP(ClientIP) > 4)
            {
                TempData["ProductDetail"] = "您的留言次數已達上限，請聯絡網站的管理員!";
                return RedirectToAction("Details", new { id });
            }
            else
            {
                CommentManager.IncrementCount(ClientIP);
            }

            // 檢查留言長度
            if (string.IsNullOrEmpty(comment) || comment.Length < 2 || comment.Length > 100)
            {
                TempData["ProductDetail"] = "請檢查您的留言內容!";
            }
            else
            {
                await _context.Comment.AddAsync(new Comment
                {
                    UserName = User.Identity.Name,
                    ProductId = id,
                    CreateTime = DateTime.Now,
                    Content = comment
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id });
        }

        public ActionResult ResetProducts()
        {
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name)) return NotFound();

            // 刪除所有產品
            _context.RemoveRange(_context.Product);
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Product', RESEED, 0)");

            // 從設定檔取得產品的網址
            List<string> ImageUrlList = new List<string>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build().AsEnumerable();

            foreach (KeyValuePair<string, string> pair in config)
            {
                if(pair.Key.StartsWith("WallPaper") && pair.Value != null)
                {
                    ImageUrlList.Add(pair.Value);
                }
            }

            // 重新創建所有產品
            List<Product> productList = new List<Product>();

            for (int i = 0; i < ImageUrlList.Count; i++)
            {
                Random random = new Random();

                productList.Add(
                    new Product
                    {
                        Name = "萌妹壁紙" + (i + 1).ToString("D2"),
                        Description = "可愛的萌妹子壁紙",
                        Price = random.Next(100, 200),
                        PublishDate = DateTime.Now.AddSeconds(i),
                        Quantity = 200,
                        DefaultImageURL = ImageUrlList[i],
                        FromProduct2 = false
                    }
                ); ;
            }

            _context.AddRange(productList);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowProducts(int page = 1)
        {
            // 從 Cache 取出這一頁的圖片資訊
            if (_memoryCache.Get($"ProductPage{page}") != null)
            {
                return View(_memoryCache.Get($"ProductPage{page}"));
            }
            // 將這一頁的圖片資訊存入 Cache
            else
            {
                _memoryCache.Set(
                    $"ProductPage{page}",
                    await _context.Product.OrderByDescending(p => p.PublishDate).ToPagedListAsync(page, pageSize)
                );

                return View(_memoryCache.Get($"ProductPage{page}"));
            }
        }

        public IActionResult ClearCache()
        {
            // 清除所有購物分頁的快取
            int PageAmount = _context.Product.Count() / 9 + 1;

            for (int Page = 1; Page <= PageAmount; Page++)
            {
                _memoryCache.Remove($"ProductPage{Page}");
            }

            return RedirectToAction("ShowProducts");
        }
    }
}