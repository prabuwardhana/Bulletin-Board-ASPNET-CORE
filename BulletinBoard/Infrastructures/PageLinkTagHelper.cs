using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BulletinBoard.Infrastructures
{
    [HtmlTargetElement("ul", Attributes = "page-metadata")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        [HtmlAttributeName("page-metadata")]
        public MetaData PageMetaData { get; set; }
        [HtmlAttributeName("page-action")]
        public string PageAction { get; set; }
        [HtmlAttributeName("page-classes-enabled")]
        public bool PageClassesEnabled { get; set; } = false;
        [HtmlAttributeName("page-class-normal")]
        public string PageClassNormal { get; set; }
        [HtmlAttributeName("page-class-selected")]
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder unorderedList = new TagBuilder("ul");

            for (int i = 1; i <= PageMetaData.TotalPages; i++)
            {
                TagBuilder list = new TagBuilder("li");

                TagBuilder anchor = new TagBuilder("a");
                anchor.Attributes["href"] = urlHelper.Action(PageAction, new { pageNumber = i });

                if (PageClassesEnabled)
                {
                    list.AddCssClass(i == PageMetaData.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                anchor.InnerHtml.Append(i.ToString());
                list.InnerHtml.AppendHtml(anchor);
                unorderedList.InnerHtml.AppendHtml(list);
            }

            output.Content.AppendHtml(unorderedList.InnerHtml);
        }
    }
}