using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace creditcard
{
    public class CommonHttp
    {
        /// <summary>
        /// 平安银行信用卡
        /// </summary>
        public readonly static string URL_PING_AN = "http://www.wanlitong.com/spendpoints/productList.do";
        /// <summary>
        /// 农业银行信用卡
        /// </summary>
        public readonly static string URL_ABC = "http://www.abchina.com/cn/CreditCard/RewardsProgram/default.htm";
        /// <summary>
        /// 中国长城国际信用卡
        /// </summary>
        public readonly static string URL_BOC = "https://iservice.boccc.com.hk/iserv/index_gw.html";
        /// <summary>
        /// 中国招商银行
        /// </summary>
        public readonly static string URL_CMBC = "http://ccclub.cmbchina.com/ccclubnew/PointCategory.aspx?categoryId=1025";
        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string HttpGet(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}
