﻿using AllPay.Payment.Integration;
using Newtonsoft.Json;
using OpayApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpayApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SendToOpay(int OrderId, string JsonString)
        {
            // 將 JsonString 轉回購物車
            Cart currentCart = JsonConvert.DeserializeObject<Cart>(JsonString);

            List<string> enErrors = new List<string>();
            string szHtml = string.Empty;
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    string MyApiDomain = ConfigurationManager.AppSettings["MyApiDomain"];

                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;
                    oPayment.ServiceURL = ConfigurationManager.AppSettings["ServiceURL"];
                    oPayment.HashKey = ConfigurationManager.AppSettings["HashKey"];
                    oPayment.HashIV = ConfigurationManager.AppSettings["HashIV"];
                    oPayment.MerchantID = ConfigurationManager.AppSettings["MerchantID"];

                    /* 基本參數 */
                    string hostname = Request.Url.Authority;
                    oPayment.Send.ReturnURL = $"{MyApiDomain}/Home/GetPayResult/?OrderId={OrderId}";
                    oPayment.Send.OrderResultURL = $"{MyApiDomain}/Home/GetPayResult/?OrderId={OrderId}";
                    oPayment.Send.MerchantTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                    oPayment.Send.MerchantTradeDate = DateTime.Now;
                    oPayment.Send.TotalAmount = currentCart.TotalAmount;
                    oPayment.Send.TradeDesc = "串接測試";

                    foreach (var cartItem in currentCart)
                    {
                        oPayment.Send.Items.Add(new Item()
                        {
                            Name = cartItem.Name,
                            Price = cartItem.Price,
                            Currency = "元",
                            Quantity = cartItem.Quantity
                        });
                    }

                    /* 產生歐付寶的訂單 */
                    enErrors.AddRange(oPayment.CheckOut());

                    /* 產生 Html Code */
                    enErrors.AddRange(oPayment.CheckOutString(ref szHtml));
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。 
                enErrors.Add(ex.Message);
            }
            finally
            {
                // 顯示錯誤訊息。 
                if (enErrors.Count() > 0)
                {
                    szHtml = string.Join("\\r\\n", enErrors);
                }
            }
            return Content(szHtml);
        }

        public ActionResult GetPayResult(AllInOne oPayment, int OrderId)
        {
            string MyAppDomain = ConfigurationManager.AppSettings["MyAppDomain"];

            try
            {
                List<string> enErrors = new List<string>();
                Hashtable htFeedback = null;

                oPayment.HashKey = ConfigurationManager.AppSettings["HashKey"];
                oPayment.HashIV = ConfigurationManager.AppSettings["HashIV"];
                enErrors.AddRange(oPayment.CheckOutFeedback(ref htFeedback));

                if (enErrors.Count() == 0)
                {
                    return Redirect($"{MyAppDomain}/OrderForm/CheckPayResult/?OrderId={OrderId}&PaySuccess=true");
                }
                else
                {
                    return Redirect($"{MyAppDomain}/OrderForm/CheckPayResult/?OrderId={OrderId}&PaySuccess=false");
                }
            }
            catch (Exception e)
            {
                return Redirect($"{MyAppDomain}/OrderForm/CheckPayResult/?OrderId={OrderId}&Exception={e}");
            }
        }
    }
}
