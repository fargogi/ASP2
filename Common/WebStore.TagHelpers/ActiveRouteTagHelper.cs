using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = ACTIVE_ROUTE_ATTRIBUTE_NAME)]
    public class ActiveRouteTagHelper : TagHelper
    {
        public const string ACTIVE_ROUTE_ATTRIBUTE_NAME = "is-active-route";
        public const string IGNORE_ACTION_ATTRIBUTE_NAME = "ignore-action";

        private Dictionary<string, string> _RouteValues;

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues
        {
            get => _RouteValues ?? (_RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            set => _RouteValues = value;
        }

        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var ignore_action = context.AllAttributes.ContainsName(IGNORE_ACTION_ATTRIBUTE_NAME);
            if (ShouldBeActive(ignore_action))
                MakeActive(output);

            output.Attributes.RemoveAll(ACTIVE_ROUTE_ATTRIBUTE_NAME);
        }


        private bool ShouldBeActive(bool ignoreAction)
        {
            var route_values = ViewContext.RouteData.Values;

            var current_controller = route_values["Controller"].ToString();
            var current_action = route_values["Action"].ToString();

            const StringComparison STR_COMP = StringComparison.CurrentCultureIgnoreCase;

            if (Controller?.Equals(current_controller, STR_COMP) == false) return false;
            if (!ignoreAction || Action?.Equals(current_action, STR_COMP) == false) return false;

            foreach (var (key, value) in RouteValues)
                if (!route_values.ContainsKey(key) || route_values[key].ToString() != value) return false;

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            const string CLASS_ATTRIBUTE_NAME = "class";
            const string ACTIVE_STATE = "active";

            var class_attribute = output.Attributes.FirstOrDefault(a => a.Name == CLASS_ATTRIBUTE_NAME);

            if (class_attribute is null)
            {
                class_attribute = new TagHelperAttribute(CLASS_ATTRIBUTE_NAME, ACTIVE_STATE);
                output.Attributes.Add(class_attribute);
            }
            else if (class_attribute.Value?.ToString().ToLower().Contains(ACTIVE_STATE) != true)
            {
                output.Attributes.SetAttribute(
                    CLASS_ATTRIBUTE_NAME,
                    class_attribute.Value is null
                    ? ACTIVE_STATE:
                    $"{ class_attribute.Value} { ACTIVE_STATE}");
            }

        }
    }
}
