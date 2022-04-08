using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Intex2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        //dynamically create the page links for us
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        //different than view context
        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");
            if (PageModel.CurrentPage > 1)
            {
                //this is an arrow that will send you back to page one.
                
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = 1 , city=PageModel.CurrentCity, county=PageModel.CurrentCounty, severity=PageModel.CurrentSeverity});
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                }
                tb.InnerHtml.Append("<<");

                final.InnerHtml.AppendHtml(tb);
            }
            if (PageModel.CurrentPage > 1)
            {
                //this has a previous page button so you can go backwards easily
                TagBuilder tb1 = new TagBuilder("a");
                tb1.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.CurrentPage - 1 , city = PageModel.CurrentCity, county = PageModel.CurrentCounty, severity = PageModel.CurrentSeverity });
                if (PageClassesEnabled)
                {
                    tb1.AddCssClass(PageClass);
                    tb1.AddCssClass(PageClassNormal);
                }
                tb1.InnerHtml.Append("Previous Page");

                final.InnerHtml.AppendHtml(tb1);
            }
            if (PageModel.CurrentPage < PageModel.TotalPages-10)
            {
                for (int i = PageModel.CurrentPage; i <= PageModel.CurrentPage + 10; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i , city = PageModel.CurrentCity, county = PageModel.CurrentCounty, severity = PageModel.CurrentSeverity });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageModel.CurrentPage
                            ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }
            else
            {
                //this says that the pagination should not go past the the Total Pages if it is at the end.
                for (int i = PageModel.CurrentPage; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i , city = PageModel.CurrentCity, county = PageModel.CurrentCounty, severity = PageModel.CurrentSeverity });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageModel.CurrentPage
                            ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }
            //this sets an arrow to head to the last page
            if (PageModel.CurrentPage > 0)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.TotalPages, city = PageModel.CurrentCity, county = PageModel.CurrentCounty, severity = PageModel.CurrentSeverity });
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                }
                tb.InnerHtml.Append(">>");

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
