using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace creditcard
{
    public class ABCHtmlParser: IHtmlParser
    {
        string html;
        public ABCHtmlParser()
        {
           
        }
        public override List<GoodList> parse()
        {
            List<GoodList> mList = new List<GoodList>();
            MatchCollection mcTitle = Regex.Matches(html, @"(?<=\bclass=Name><A title=).*(?=\bhref=)");
            MatchCollection mcIntegral = Regex.Matches(html, @"(?<=\bSPAN class=GPriceRed>).*(?=\b</SPAN>)");
            MatchCollection mcNo = Regex.Matches(html, @"(?<=\b编号: ).*(?=\b</DIV>)");
            MatchCollection mcImgUrl = Regex.Matches(html, @"(?<=\bsrc="").*(?=""></A></DIV>)");
            MatchCollection mcDetailedUrl = Regex.Matches(html, @"(?<=\bhref="").*(?=""\s+target=_blank><IMG)");
            
            int count = mcTitle.Count;
            for (int i = 0; i < count;i++)
            {
                //解析title
                string title = mcTitle[i].ToString();

                //解析积分
                string integral = mcIntegral[i].ToString();
                integral = integral.Replace(",", "");
                //解析编号
                string no = mcNo[i].ToString();

                //解析ImgUrl
                string imgUrl = mcImgUrl[i].ToString();

                //解析DetailedUrl
                string detailedUrl = mcDetailedUrl[i].ToString();
                GoodList mGoodList = new GoodList();
                mGoodList.Title = title;
                mGoodList.Integral = integral;
                mGoodList.No = no;
                mGoodList.ImgUrl = imgUrl;
                mGoodList.DetailedUrl = detailedUrl;
                mList.Add(mGoodList);
            }
            return mList;
        }

        public override string HTML
        {
            get
            {
                return html;
            }
            set
            {
                this.html = value;
            }
        }
    }
    
}
