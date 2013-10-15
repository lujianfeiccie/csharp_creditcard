using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using creditcard.Util;

namespace creditcard
{
    /// <summary>
    /// 招商银行
    /// </summary>
    public class CMBCHtmlParser: IHtmlParser
    {
        string html;
        public CMBCHtmlParser()
        {
           
        }
        public override List<GoodList> parse()
        {
            List<GoodList> mList = new List<GoodList>();
            MatchCollection mcTitle = Regex.Matches(html, @"(?<=<DT><A title=).*(?=.href)");
            MatchCollection mcIntegral = Regex.Matches(html, @"(?<=&nbsp;<SPAN>).*(?=</SPAN>)|(?<=兑换积分：<SPAN>).*(?=</SPAN>分)");
            
            MatchCollection mcImgUrl = Regex.Matches(html, @"(?<=src="").*(?="".width=120)");
            MatchCollection mcDetailedUrl = Regex.Matches(html, @"(?<=href="").*(?="".target=_blank><IMG.alt)");
           
            int count = mcTitle.Count;
            int count_for_integral = mcIntegral.Count;
            string title = "";
            string detailed_url = "";
            string integral = "";
            string no = "";
            string imgUrl = "";
            for (int i = 0,j=0; i < count;i++,j++)
            {
                //解析title
                title = mcTitle[i].ToString();

                //解析detailed url
                detailed_url = "http://ccclub.cmbchina.com/ccclubnew/"+mcDetailedUrl[i].ToString();

                string htmlText = CommonHttp.HttpGet(detailed_url);
                MatchCollection mcNo = Regex.Matches(htmlText, @"(?<=<td><b>)\d*(?=</b>)");
                //解析编号
                 no = mcNo[0].ToString();
                  
                //解析积分
                integral = mcIntegral[j].ToString();
                if (j + 1 < count_for_integral)
                { //如果有现金叠加条件，则加入,￥123等
                    string next_integral = mcIntegral[j + 1].ToString();
                    if (next_integral.Trim().StartsWith("￥"))
                    {
                        integral += "," + next_integral;
                        ++j; 
                    }
                }
                integral = integral.Trim();
               
                //解析ImgUrl
                imgUrl = mcImgUrl[i].ToString().Trim();

                GoodList mGoodList = new GoodList();
                mGoodList.Title = StringHelper.StringProc(title);
                mGoodList.Integral = integral;
                mGoodList.No = no;
                mGoodList.ImgUrl = imgUrl;
                mGoodList.DetailedUrl = detailed_url;
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
