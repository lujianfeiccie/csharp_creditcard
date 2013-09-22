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

            MyRegex mcTitle = new MyRegex(html, @"onclick=""return false"" href=""#"">", "</A></TD>\r\n<TD class=group_text");
            MyRegex mcIntegral = new MyRegex(html, "</A></TD>\r\n<TD class=group_text align=middle>", "</TD>");
            MyRegex mcNo = new MyRegex(html, @"onclick=""return false"" href=""#"">", "</A></TD>\r\n<TD class=group_link");
            MyRegex mcImgUrl = new MyRegex(html, "<TR>\r\n<TD class=group_link><A onmouseover=\"onMouseOverShowImg", "onmouseout");
             
            int count = mcTitle.Count;
            for (int i = 0; i < count;i++)
            {
                //解析title
                string title = mcTitle.getValue(i);

                //解析积分
                string integral = mcIntegral.getValue(i);
                
                //解析编号
                string no = mcNo.getValue(i);

                //解析ImgUrl
                string imgUrl = mcImgUrl.getValue(i);
                imgUrl = imgUrl.Replace("'", "");
                imgUrl = imgUrl.Replace("(", "");
                imgUrl = imgUrl.Replace(")", "");
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
