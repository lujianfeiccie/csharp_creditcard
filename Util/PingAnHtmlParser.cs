using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace creditcard
{
    public class PingAnHtmlParser: IHtmlParser
    {
        string html;
        public PingAnHtmlParser()
        {
           
        }
        public override List<GoodList> parse()
        {
            List<GoodList> mList = new List<GoodList>();

            MyRegex mcTitle = new MyRegex(html, @"target=_blank><IMG alt=", "src=");
            MyRegex mcIntegral = new MyRegex(html, "<DIV class=txt_bg_jinbi><SPAN class=c_e><SPAN class=fwb>", "</SPAN>");
            MyRegex mcNo = new MyRegex(html, "product_", ".html");
            MyRegex mcImgUrl = new MyRegex(html, "src=\"","\"> </A></DIV>\r\n<DIV class=pro_name>");
             MyRegex mcDetailedUrl = new MyRegex(html,
                 @"href=""../", @"""");
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
                 string detailedUrl = "http://www.wanlitong.com/"+mcDetailedUrl.getValue(i);
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
