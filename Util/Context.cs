using System;
using System.Collections.Generic;
using System.Text;

namespace creditcard
{
    public class Context
    {
        private string html;

        public string Html
        {
            get { return html; }
            set { html = value; }
        }
        private IHtmlParser parser;

        public IHtmlParser Parser
        {
            get { return parser; }
            set { parser = value; }
        }
        public Context() {
            html = "";
        }
        public Context(IHtmlParser parser) {
            this.parser = parser;
            html = "";
        }
        public List<GoodList> parse()
        {
            if (parser != null && !html.Equals(""))
            {
                parser.HTML = html;
                return parser.parse();
            }
            return null;
        }
    }
}
