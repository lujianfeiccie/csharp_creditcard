using System;
using System.Collections.Generic;
using System.Text;

namespace creditcard
{
    public abstract class IHtmlParser
    {
        public abstract List<GoodList> parse();
        public abstract string HTML {
            get ;
            set ;
        }
    }
}
