#pragma checksum "C:\Github_repo\micro_services\micro_services_all\ZaMvcV03\Views\Home\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "60f66834ccee5ca683406f8e089c0ecc03ae0ba0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Login), @"mvc.1.0.view", @"/Views/Home/Login.cshtml")]
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
#line 1 "C:\Github_repo\micro_services\micro_services_all\ZaMvcV03\Views\_ViewImports.cshtml"
using ZaMvcV03;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Github_repo\micro_services\micro_services_all\ZaMvcV03\Views\_ViewImports.cshtml"
using ZaMvcV03.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60f66834ccee5ca683406f8e089c0ecc03ae0ba0", @"/Views/Home/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c5551b74a190a5a30136319653b05ebe671dffaa", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("loginform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("signupform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    <div class=""container"">    
        <div id=""loginbox"" style=""margin-top:50px;"" class=""mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2"">                    
            <div class=""panel panel-info"" >
                    <div class=""panel-heading"">
                        <div class=""panel-title"">Kullanıcı Girişi</div>
                        <div style=""float:right; font-size: 80%; position: relative; top:-10px""><a href=""#"">Şifremi Unuttum </a></div>
                    </div>     

                    <div style=""padding-top:30px"" class=""panel-body"" >

                        <div style=""display:none"" id=""login-alert"" class=""alert alert-danger col-sm-12""></div>
                            
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "60f66834ccee5ca683406f8e089c0ecc03ae0ba05386", async() => {
                WriteLiteral(@"
                                    
                            <div style=""margin-bottom: 25px"" class=""input-group"">
                                        <span class=""input-group-addon""><i class=""glyphicon glyphicon-user""></i></span>
                                        <input id=""login-username"" type=""text"" class=""form-control"" name=""username""");
                BeginWriteAttribute("value", " value=\"", 1167, "\"", 1175, 0);
                EndWriteAttribute();
                WriteLiteral(@" placeholder=""email"">                                        
                                    </div>
                                
                            <div style=""margin-bottom: 25px"" class=""input-group"">
                                        <span class=""input-group-addon""><i class=""glyphicon glyphicon-lock""></i></span>
                                        <input id=""login-password"" type=""password"" class=""form-control"" name=""password"" placeholder=""şifre"">
                                    </div>
                                    

                                
                            <div class=""input-group"">
                                      <div class=""checkbox"">
                                        <label>
                                          <input id=""login-remember"" type=""checkbox"" name=""remember"" value=""1""> Beni Hatırla
                                        </label>
                                      </div>
                            </d");
                WriteLiteral(@"iv>


                                <div style=""margin-top:10px"" class=""form-group"">
                                    <!-- Button -->

                                    <div class=""col-sm-12 controls"">
                                      <a id=""btn-login"" href=""#"" class=""btn btn-success"">Giriş  </a>

                                    </div>
                                </div>


                                <div class=""form-group"">
                                    <div class=""col-md-12 control"">
                                        <div style=""border-top: 1px solid#888; padding-top:15px; font-size:85%"" >
                                            Kullanıcı kaydınız yok ise  
                                        <a href=""#"" onClick=""$('#loginbox').hide(); $('#signupbox').show()"">
                                            tıklayınız ...
                                        </a>
                                        </div>
                                    ");
                WriteLiteral("</div>\r\n                                </div>    \r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"     



                        </div>                     
                    </div>  
        </div>
        <div id=""signupbox"" style=""display:none; margin-top:50px"" class=""mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2"">
                    <div class=""panel panel-info"">
                        <div class=""panel-heading"">
                            <div class=""panel-title"">Kullanıcı Kayıt</div>
                            <div style=""float:right; font-size: 85%; position: relative; top:-10px""><a id=""signinlink"" href=""#"" onclick=""$('#signupbox').hide(); $('#loginbox').show()"">Kullanıcı Giriş</a></div>
                        </div>  
                        <div class=""panel-body"" >
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "60f66834ccee5ca683406f8e089c0ecc03ae0ba010420", async() => {
                WriteLiteral(@"
                                
                                <div id=""signupalert"" style=""display:none"" class=""alert alert-danger"">
                                    <p>Error:</p>
                                    <span></span>
                                </div>
                                    
                                
                                  
                                <div class=""form-group"">
                                    <label for=""email"" class=""col-md-3 control-label"">Email</label>
                                    <div class=""col-md-9"">
                                        <input type=""text"" class=""form-control"" name=""email"" placeholder=""Email - Kullanıcı Kodu"">
                                    </div>
                                </div>
                                    
                                <div class=""form-group"">
                                    <label for=""firstname"" class=""col-md-3 control-label"">Adınız</label");
                WriteLiteral(@">
                                    <div class=""col-md-9"">
                                        <input type=""text"" class=""form-control"" name=""firstname"" placeholder=""Adınız"">
                                    </div>
                                </div>
                                <div class=""form-group"">
                                    <label for=""lastname"" class=""col-md-3 control-label"">Soyadınız</label>
                                    <div class=""col-md-9"">
                                        <input type=""text"" class=""form-control"" name=""lastname"" placeholder=""Soyadınız"">
                                    </div>
                                </div>
                                <div class=""form-group"">
                                    <label for=""password"" class=""col-md-3 control-label"">Şifre</label>
                                    <div class=""col-md-9"">
                                        <input type=""password"" class=""form-control"" name=""passwd"" place");
                WriteLiteral(@"holder=""Şifre"">
                                    </div>
                                </div>

                                <div class=""form-group"">
                                    <!-- Button -->                                        
                                    <div class=""col-md-offset-3 col-md-9"">
                                        <button id=""btn-signup"" type=""button"" class=""btn btn-info""><i class=""icon-hand-right""></i> &nbsp Kaydet</button>
                                    </div>
                                </div>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                         </div>\r\n                    </div>\r\n\r\n               \r\n               \r\n                \r\n         </div> \r\n    </div>");
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
