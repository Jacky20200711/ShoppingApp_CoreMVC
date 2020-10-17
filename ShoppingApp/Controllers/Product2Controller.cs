﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    [Authorize]
    public class Product2Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Product2Controller(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (AuthorizeManager.InAdminGroup(User.Identity.Name))
            {
                // 返回所有產品
                return View(await _context.Product2.OrderBy(m => m.SellerEmail).ToListAsync());
            }
            else if(AuthorizeManager.InSellerGroup(User.Identity.Name))
            {
                // 返回符合上架者 Id 的產品
                return View(await _context.Product2.Where(m => m.SellerId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var product2 = await _context.Product2
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product2 == null)
            {
                return NotFound();
            }

            // 令沒有管理權限的 Seller 只能查看自己上架的產品
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name))
            {
                if (product2.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();
            }

            return View(product2);
        }

        public IActionResult Create()
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return NotFound();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,PublishDate,Quantity,DefaultImageURL,SellerEmail,SellerId")] Product2 product2)
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return NotFound();

            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ProductList = _context.Product2.Where(m => m.SellerId == UserId).ToList();

            // 檢查該使用者上架的產品數量
            if (ProductList != null && ProductList.Count > 5)
            {
                TempData["ReachLimit"] = "建立失敗，您的產品數量已達上限!";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                product2.PublishDate = DateTime.Now;
                product2.SellerEmail = User.Identity.Name;
                product2.SellerId = UserId;

                _context.Add(product2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product2);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var product2 = await _context.Product2.FindAsync(id);
            if (product2 == null)
            {
                return NotFound();
            }

            // 令沒有管理權限的 Seller 只能編輯自己上架的產品
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name))
            {
                if (product2.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();
            }

            return View(product2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,PublishDate,Quantity,DefaultImageURL,SellerEmail,SellerId")] Product2 product2)
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return NotFound();

            if (id != product2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product2.SellerEmail = User.Identity.Name;
                    product2.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // 令沒有管理權限的 Seller 只能編輯自己上架的產品
                    if (!AuthorizeManager.InAdminGroup(User.Identity.Name))
                    {
                        if (product2.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();
                    }

                    _context.Update(product2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product2Exists(product2.Id))
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
            return View(product2);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var product2 = await _context.Product2
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product2 == null)
            {
                return NotFound();
            }

            // 令沒有管理權限的 Seller 只能刪除自己上架的產品
            if (!AuthorizeManager.InAdminGroup(User.Identity.Name))
            {
                if (product2.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier)) return NotFound();
            }

            _context.Product2.Remove(product2);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool Product2Exists(int id)
        {
            if (!AuthorizeManager.InAuthorizedMember(User.Identity.Name)) return false;

            return _context.Product2.Any(e => e.Id == id);
        }
    }
}
