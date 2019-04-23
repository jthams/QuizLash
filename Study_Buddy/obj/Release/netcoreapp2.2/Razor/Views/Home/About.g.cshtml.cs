#pragma checksum "C:\Users\hamel\source\Repos\jthams\StudyBuddy\Study_Buddy\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79e328f75279aae4e135d76a1743f49e5437e051"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
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
#line 1 "C:\Users\hamel\source\Repos\jthams\StudyBuddy\Study_Buddy\Views\_ViewImports.cshtml"
using WebUI;

#line default
#line hidden
#line 2 "C:\Users\hamel\source\Repos\jthams\StudyBuddy\Study_Buddy\Views\_ViewImports.cshtml"
using Domain.Entities;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79e328f75279aae4e135d76a1743f49e5437e051", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"563b61fc0e4554a0b6c71230790c021970413200", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\hamel\source\Repos\jthams\StudyBuddy\Study_Buddy\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(90, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(94, 498, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79e328f75279aae4e135d76a1743f49e5437e0513634", async() => {
                BeginContext(100, 485, true);
                WriteLiteral(@"
    <title class=""alert-info"">This application is currently under development</title>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js""></script>
    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js""></script>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(592, 251, true);
            WriteLiteral("\r\n<div>\r\n    <h2 class=\"text-center\">Currently Implemented Technologies</h2>\r\n    <h6 class=\"alert-info text-secondary text-center italic\">This was developed for learning purposes only and is not intended to become a trademarked product</h6>\r\n</div>\r\n");
            EndContext();
            BeginContext(843, 3893, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79e328f75279aae4e135d76a1743f49e5437e0515577", async() => {
                BeginContext(849, 3880, true);
                WriteLiteral(@"
    <div class=""flex-container"">
        <table class=""table table-hover border-top-0"">
            <thead>
                <tr>
                    <th>Technology</th>
                    <th>Implementation</th>
                    <th>Documentation</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class=""text-info font-weight-bold"">Two Azure SQL Databases</td>
                    <td>Used as the applications database context, data at rest is encrypted to safeguard application data and user information. Authentication database is seperate from the buisness logic database, this is to limit the amount of queries the sensitive database recieves.</td>
                    <td><a href=""https://theblobthestorageaccount.blob.core.windows.net/myblob/AzureSQLDocumentation.pdf"">Azure SQL</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">Entity Framework Core</td>
               ");
                WriteLiteral(@"     <td>Code first model scaffolding to keep precise entity relationships. Grants users permission to utilize CRUD operations in order to store their customized questions and quizzes through the use of forms.</td>
                    <td><a href=""https://theblobthestorageaccount.blob.core.windows.net/myblob/EFCoreDocumentation.pdf"">EF Core</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">User-Secrets Manager</td>
                    <td>Protect application secrets in the development environment</td>
                    <td><a href=""https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2"">User Secrets</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">Azure Key Vault</td>
                    <td>Protect application secrets in the production environment</td>
                    <td><a href=""https://docs.microsoft.com/en-us/azure/key-vault/"">Azure Key");
                WriteLiteral(@" Vault</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">ASP.Net Identity</td>
                    <td>Full UI scaffolded to add further constraints to account behavior</td>
                    <td><a href=""https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity"">ASP.Net Identity</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">SendGrid API</td>
                    <td>Allows accounts to be confirmed through email confirmation</td>
                    <td><a href=""https://sendgrid.com/docs/API_Reference/api_v3.html""> SendGrid</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">Two Factor Authentication</td>
                    <td>Optional 2FA with TOTP Authenticator apps</td>
                    <td><a href=""https://docs.microsoft.com/en-us/aspnet/core/security/auth");
                WriteLiteral(@"entication/identity-enable-qrcodes?view=aspnetcore-2.2""> QR Codes</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">Azure BLOB Storage</td>
                    <td>Provide links directly to PDF's</td>
                    <td><a href=""https://theblobthestorageaccount.blob.core.windows.net/myblob/BLOBDocumentation.pdf"">Azure BLOB Storage</a></td>
                </tr>
                <tr>
                    <td class=""text-info font-weight-bold"">Bootstrap 4</td>
                    <td>Front end sytling and animated features</td>
                    <td><a href=""https://getbootstrap.com/docs/4.0/getting-started/introduction/"">Bootstrap Homepage</a></td>
                </tr>
            </tbody>
        </table>
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4736, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
