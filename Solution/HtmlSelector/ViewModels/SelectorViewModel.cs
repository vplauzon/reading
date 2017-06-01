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
            "http://www.msn.com/en-ca/entertainment/celebrity/these-are-the-10-celebrity-scandals-people-just-cant-let-go-of/ss-BBBJeBd";

        public string SelectionPolicy { get; set; } = CoreResources.DefaultPolicy;
    }
}