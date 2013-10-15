using System;
using System.Collections.Generic;
using System.Text;

namespace creditcard.Util
{
    public class StringHelper
    {
        /// <summary>
        /// 处理转义的情况
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringProc(string str)
        { 
            string goodname = str;
            int position  = goodname.IndexOf('\'');
            while (position != -1)
            {
                goodname = goodname.Insert(position, "'");
                position = goodname.IndexOf('\'', position+2);
            }
            return goodname;
        }
    }
}
