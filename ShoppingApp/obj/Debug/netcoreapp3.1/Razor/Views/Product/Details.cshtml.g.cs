#pragma checksum "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f4814bc102b1c5665f38c661a1f839a57c5ae077"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Details), @"mvc.1.0.view", @"/Views/Product/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\_ViewImports.cshtml"
using ShoppingApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\_ViewImports.cshtml"
using ShoppingApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4814bc102b1c5665f38c661a1f839a57c5ae077", @"/Views/Product/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9650a1976e0c12ce9f77745181f01694dd4d0bd3", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<X.PagedList.IPagedList<ShoppingApp.Models.ProductAndComment>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
  
    ViewData["Title"] = "產品資訊與留言";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>產品資訊</h1>\r\n\r\n<div>\r\n    <div><img style=\"max-width:100%; height:auto;\"");
            BeginWriteAttribute("src", " src=\"", 219, "\"", 301, 1);
#nullable restore
#line 11 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
WriteAttributeValue("", 225, Html.DisplayFor(model => model.FirstOrDefault().TheProduct.DefaultImageURL), 225, 76, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 15 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstOrDefault().TheProduct.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 18 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayFor(model => model.FirstOrDefault().TheProduct.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 21 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstOrDefault().TheProduct.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayFor(model => model.FirstOrDefault().TheProduct.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 27 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstOrDefault().TheProduct.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayFor(model => model.FirstOrDefault().TheProduct.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 33 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstOrDefault().TheProduct.PublishDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayFor(model => model.FirstOrDefault().TheProduct.PublishDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 39 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstOrDefault().TheProduct.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.DisplayFor(model => model.FirstOrDefault().TheProduct.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n\r\n<br>\r\n<h1>訪客留言</h1>\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n");
#nullable restore
#line 53 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
         if (Model.FirstOrDefault().TheComment != null)
        {
            foreach (var pc in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 58 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
               Write(pc.TheComment.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("<span style=\"margin-left:4px;\">:</span>\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 61 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
               Write(pc.TheComment.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ( ");
#nullable restore
#line 61 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
                                         Write(pc.TheComment.CreateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" )\r\n                </dd>\r\n");
#nullable restore
#line 63 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </dl>\r\n</div>\r\n<div>\r\n");
#nullable restore
#line 68 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
     if (string.IsNullOrEmpty(User.Identity.Name))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>登入會員後才能留言!</p>\r\n");
#nullable restore
#line 71 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
    }
    else
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
         using (Html.BeginForm("AddComment", "Product", FormMethod.Post, new { onsubmit = "return checkLen();" }))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span style=\"display:none;\">");
#nullable restore
#line 77 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
                                   Write(Html.TextBox("id", Model.FirstOrDefault().TheProduct.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 78 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
       Write(Html.TextBox("comment", null, new { id = "PostComment", @class = "text-field" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("            <input type=\"submit\" value=\"留言\" class=\"btn btn-primary\" />\r\n");
#nullable restore
#line 80 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <a href=\"javascript:\" onclick=\"self.location=document.referrer;\">回上一頁</a>\r\n</div>\r\n<br>\r\n\r\n");
            WriteLiteral("Page ");
#nullable restore
#line 88 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
 Write(Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral(" of ");
#nullable restore
#line 88 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
                                                                Write(Model.PageCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 89 "D:\Desktop\Project\ShoppingApp_CoreMVC\ShoppingApp\Views\Product\Details.cshtml"
Write(Html.PagedListPager(Model, page => Url.Action("Details", new { id = Model.FirstOrDefault().TheProduct.Id, page })));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<script>
    //將捲軸自動滾到底以方便查看留言
    window.scrollTo(0, document.body.scrollHeight);

    //檢查留言框的內容長度
    function checkLen() {

        var commentLen = document.getElementById(""PostComment"").value.length;

        if (commentLen < 2 || commentLen > 100) {
            alert(""留言長度必須為2~100字!"");
            return false;
        }
        else {
            return true;
        }
    }
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<X.PagedList.IPagedList<ShoppingApp.Models.ProductAndComment>> Html { get; private set; }
    }
}
#pragma warning restore 1591
