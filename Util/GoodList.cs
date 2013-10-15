using System;
using System.Collections.Generic;
using System.Text;

namespace creditcard
{
    public class GoodList
    {
        private string imgUrl;
        private string title;
        private string integral;
        private string no;
        private string detailedUrl;
        private string typeid;
        private string cash;
        public GoodList()
        {
            this.cash = "0";
        }
        public string Typeid
        {
            get { return typeid; }
            set { typeid = value; }
        }
        public string DetailedUrl
        {
            get { return detailedUrl; }
            set { detailedUrl = value; }
        }
        public string No
        {
            get { return no; }
            set { no = value; }
        }
        public string Integral
        {
            get { return integral; }
            set { integral = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string ImgUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }
        public string Cash
        {
            get { return cash; }
            set { cash = value; }
        }
    }
}
