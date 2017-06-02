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
            //"http://www.msn.com/en-ca/lifestyle/style/the-atlas-of-beauty-%e2%80%93-the-next-steps/ss-BBBGS5Z?ocid=spartanntp";

        public string SelectionPolicy { get; set; } = CoreResources.DefaultPolicy;
    }
}