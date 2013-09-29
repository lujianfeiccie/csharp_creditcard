using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace creditcard
{
    public class BOCHtmlParser: IHtmlParser
    {
        string html;
        public BOCHtmlParser()
        {
           
        }
        public override List<GoodList> parse()
        {
            List<GoodList> mList = new List<GoodList>();
            MatchCollection mcTitle = Regex.Matches(html, @"(?<=\bonclick=""return false"" href=""#"">).*(?=\b*</A></TD>\r\n<TD class=group_text)");
            MatchCollection mcIntegral = Regex.Matches(html, @"(?<=\bA></TD>\r\n<TD class=group_text align=middle>).*(?=\b*</TD>)");
            MatchCollection mcNo = Regex.Matches(html, @"(?<=\bonclick=""return false"" href=""#"">).*(?=\b*</A></TD>\r\n<TD class=group_link)");
            MatchCollection mcImgUrl = Regex.Matches(html, @"(?<=\b*<TR>\r\n<TD class=group_link><A onmouseover=""onMouseOverShowImg\(').*(?=\b*')");
            int count = mcTitle.Count;
            for (int i = 0; i < count;i++)
            {
                //解析title
                string title = mcTitle[i].ToString();

                //解析积分
                string integral = mcIntegral[i].ToString();
                
                //解析编号
                string no = mcNo[i].ToString();

                //解析ImgUrl
                string imgUrl = mcImgUrl[i].ToString();
                imgUrl = "https://iservice.boccc.com.hk" + imgUrl;

                GoodList mGoodList = new GoodList();
                mGoodList.Title = title;
                mGoodList.Integral = integral;
                mGoodList.No = no;
                mGoodList.ImgUrl = imgUrl;
                mGoodList.DetailedUrl = "";
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
