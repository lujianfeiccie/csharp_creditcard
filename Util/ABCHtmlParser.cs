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

            MyRegex mcTitle = new MyRegex(html, "<DIV class=Name><A title=","href=");
            MyRegex mcIntegral = new MyRegex(html, "<SPAN class=GPriceRed>","</SPAN>");
            MyRegex mcNo = new MyRegex(html, "<DIV class=GPrice>���:","</DIV>");
            MyRegex mcImgUrl = new MyRegex(html, @"alt="""" src=""",@"""");
            MyRegex mcDetailedUrl = new MyRegex(html,
                @"<DIV class=GImage><A href=""", @"""");
            int count = mcTitle.Count;
            for (int i = 0; i < count;i++)
            {
                //����title
                string title = mcTitle.getValue(i);

                //��������
                string integral = mcIntegral.getValue(i);
                
                //�������
                string no = mcNo.getValue(i);

                //����ImgUrl
                string imgUrl = mcImgUrl.getValue(i);

                //����DetailedUrl
                string detailedUrl = mcDetailedUrl.getValue(i);
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
