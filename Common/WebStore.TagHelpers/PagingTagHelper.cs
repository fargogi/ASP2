using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebStore.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public PagingTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var url_helper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            for (var i = 1; i < PageViewModel.TotalPages; i++)
            {
                ul.InnerHtml.AppendHtml(CreateItem(i, url_helper));
            }

            output.Content.AppendHtml(ul);



            base.Process(context, output);
        }

        private TagBuilder CreateItem(int pageNumber, IUrlHelper urlHelper)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            if (pageNumber == PageViewModel.PageNumber)
            {
                li.MergeAttribute("data-page", PageViewModel.PageNumber.ToString());
                li.AddCssClass("active");
            }

            else
            {
                PageUrlValues["page"] = pageNumber;
                a.Attributes["href"] = "#";

                foreach (var page_url_value in PageUrlValues.Where(v=>v.Value != null))
                {
                    a.MergeAttribute($"data-{page_url_value.Key}", page_url_value.Value.ToString());
                }
            }

            a.InnerHtml.AppendHtml(pageNumber.ToString());

            li.InnerHtml.AppendHtml(a);

            return li;
        }
    }
}
