#pragma checksum "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99eb014d0f457a2988b4685049d75c6f9aea0862"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_GetProductByCategoryName), @"mvc.1.0.view", @"/Views/Product/GetProductByCategoryName.cshtml")]
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
#line 1 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
using GettingData.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99eb014d0f457a2988b4685049d75c6f9aea0862", @"/Views/Product/GetProductByCategoryName.cshtml")]
    public class Views_Product_GetProductByCategoryName : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
     if (ViewData["Error"]!=null)
   {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2>");
#nullable restore
#line 6 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
   Write(ViewData["Error"].ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 7 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ul>\r\n");
#nullable restore
#line 11 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
         foreach(Product item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>");
#nullable restore
#line 13 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
           Write(item.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 14 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n");
#nullable restore
#line 16 "C:\Users\umuto\source\repos\Mvc\GettingData\GettingData\Views\Product\GetProductByCategoryName.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n   ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
