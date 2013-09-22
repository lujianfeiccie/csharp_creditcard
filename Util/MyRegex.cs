using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace creditcard
{
    public class MyRegex
    {
        private string start;
        private string end;
        MatchCollection mMatchCollection;

        public string Start
        {
            get { return start; }
            set { start = value; }
        }
        public string End
        {
            get { return end; }
            set { end = value; }
        }
        public MatchCollection MMatchCollection
        {
            get { return mMatchCollection; }
        }
        public MyRegex(string html, string start, string end)
        {
            this.start = start;
            this.end = end;
            mMatchCollection = Regex.Matches(html, string.Format("{0}.*?{1}", start, end));
        }
        public string getValue(int index)
        {
            string value = "";
            try
            {
                string str = mMatchCollection[index].Value;
                value = str.Replace(start, "");
                value = value.Replace(end, "");
                value = value.Replace(@"""","");
                value = value.Trim();
            }
            catch
            {
            }
            return value;
        }
        public int Count
        {
            get { return mMatchCollection.Count; }
        }
    }
}
