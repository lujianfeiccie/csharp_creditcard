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
            MatchCollection mcTitle = Regex.Matches(html, @"(?<=\bIMG alt=).*(?=\b*src)");
            MatchCollection mcIntegral = Regex.Matches(html, @"(?<=\btxt_bg_jinbi\>\<SPAN class=c_e\>\<SPAN class=fwb>).*(?=\b\<\/SPAN\>��)");
            MatchCollection mcNo = Regex.Matches(html, @"(?<=\btitle="""" href=""../product_).*(?=\b.html)");
            MatchCollection mcImgUrl = Regex.Matches(html, @"(?<=\bsrc="").*(?=\b"">\s+</A></DIV>\r\n<DIV class=pro_name)");
            MatchCollection mcDetailedUrl = Regex.Matches(html, @"(?<=\btitle="""" href=""..).*(?=\b=""\s+target=_blank)");
            int count = mcTitle.Count;
            for (int i = 0; i < count;i++)
            {
               
                //����title
                string title = mcTitle[i].ToString().Replace("\"","");

                //��������
                string integral = mcIntegral[i].ToString();
                
                //�������
                string no = mcNo[i].ToString();

                //����ImgUrl
                string imgUrl = mcImgUrl[i].ToString();

                 //����DetailedUrl
                string detailedUrl = "http://www.wanlitong.com"+mcDetailedUrl[i].ToString();
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
