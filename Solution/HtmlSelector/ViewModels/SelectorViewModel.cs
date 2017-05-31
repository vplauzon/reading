using HtmlSelector.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlSelector.ViewModels
{
    public class SelectorViewModel
    {
        public string SampleUrl { get; set; } =
            "https://azure.microsoft.com/en-us/blog/visual-studio-team-services-may-2017-digest/";

        public string SelectionPolicy { get; set; } = CoreResources.DefaultPolicy;
    }
}