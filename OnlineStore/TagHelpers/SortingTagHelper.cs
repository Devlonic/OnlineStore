using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineStore.Models;

namespace OnlineStore.TagHelpers {
    public class SortingTagHelper : TagHelper {
        public string Action { get; set; }
        public SortingType Value { get; set; }
        public SortingType CurrentSorting { get; set; }
        public bool IsUp { get; set; }

        [HtmlAttributeNotBound]
        public IUrlHelperFactory urlHelperFactory { get; set; }
        
        
        // dependency injection
        public SortingTagHelper(IUrlHelperFactory urlHelperFactory) {
            this.urlHelperFactory = urlHelperFactory;
        }

        // dependency injection
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";
            var url = urlHelper.Action(Action, new { sort = Value });

            output.Attributes.SetAttribute("href", url);

            if( CurrentSorting == Value) {
                TagBuilder tb = new TagBuilder("i");
                tb.AddCssClass("glyphicon");

                if ( this.IsUp )
                    tb.AddCssClass("glyphicon-chevron-up");
                else
                    tb.AddCssClass("glyphicon-chevron-down");

                output.PreContent.AppendHtml(tb);
            }
        }
    }
}
