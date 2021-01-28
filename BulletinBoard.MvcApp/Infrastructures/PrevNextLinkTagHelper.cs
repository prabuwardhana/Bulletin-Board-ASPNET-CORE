using BulletinBoard.Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BulletinBoard.MvcApp.Infrastructures
{
    [HtmlTargetElement("a", Attributes = "page-metadata")]
    public class PrevNextLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PrevNextLinkTagHelper(IUrlHelperFactory helperFactory)
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
        [HtmlAttributeName("is-previous")]
        public bool IsPrev { get; set; } = false;
        [HtmlAttributeName("is-next")]
        public bool IsNext { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder prevNextAnchor = new TagBuilder("a");

            if (IsPrev && PageMetaData.HasPrevious)
            {                
                var prevPage = PageMetaData.CurrentPage - 1;
                output.Attributes.SetAttribute("href", urlHelper.Action(PageAction, new { pageNumber = prevPage }));
            }

            if (IsNext && PageMetaData.HasNext)
            {
                var nextPage = PageMetaData.CurrentPage + 1;
                output.Attributes.SetAttribute("href", urlHelper.Action(PageAction, new { pageNumber = nextPage }));
            }
        }
    }
}