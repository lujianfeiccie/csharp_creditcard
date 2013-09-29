using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using creditcard.Util;

namespace creditcard
{
    public partial class FormMain : Form
    {
        string tag = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
        FormProgressDialog mFormProgressDialog = null;
        Context mContext = new Context();
        List<GoodList> mList = null;
        System.Threading.Timer timer = null;
        string selectedValue = "";
        int count_add_new = 0;
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            mFormProgressDialog = new FormProgressDialog();
            initData();
            initEvent();
        }

        private void initData()
        {
           comboBox1.DisplayMember = "type_name";
            comboBox1.ValueMember = "type_id";
            comboBox1.DataSource = DBHelper.GetDataSet("SELECT * FROM tb_good_type");
        }

       
        private void initEvent()
        {
            webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            log("webBrowser1_DocumentCompleted");
           // MessageBox.Show(webBrowser1.DocumentText);
            string html = webBrowser1.Document.Body.OuterHtml;
            richTextBox1.Text = html;
         
        }
        private void saveData(string html){
            mContext.Html = html;
            mList = mContext.parse();

            if (mList == null || mList.Count <= 0) return;
            log("showData");
            count_add_new = 0;
            this.BeginInvoke(new UIHandler1(showProgressDialog), new string[] { "正在录入数据,请稍候..."});
            foreach (GoodList good in mList)
            {

                string queryString = string.Format("SELECT good_id FROM tb_good WHERE good_name = '{0}' and good_no = '{1}'", good.Title,good.No);

                DataTable dt = DBHelper.GetDataSet(queryString);
                if (dt.Rows.Count > 0)
                {
                    continue;
                }
                ++count_add_new;
                add(good);
            }

            this.Invoke(new UIHandler1(closeProgressDialog), new string[] { string.Format("数据录入完毕,新增{0}条数据",count_add_new)});
            Console.WriteLine(string.Format("{0} in total", mList.Count));
        }
         
         
        
        
        public delegate void UIHandler1(string msg);
        public delegate void UIHandler2(int total,int current);

        void showProgressDialog(string msg){
            log("showProgressDialog"+msg);
            lblState.Text = msg; 
            if (mFormProgressDialog == null)
            {
                mFormProgressDialog = new FormProgressDialog();
            }
            mFormProgressDialog.Title = "提示";
            mFormProgressDialog.Message = msg;
            mFormProgressDialog.StartPosition = FormStartPosition.Manual;
            mFormProgressDialog.Location = new Point(this.Location.X + this.Width / 2 - mFormProgressDialog.Width / 2, this.Location.Y + this.Height / 2 + mFormProgressDialog.Height/2);
            mFormProgressDialog.ShowDialog();
             
        }
         
        void closeProgressDialog(string msg) {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
            if (mFormProgressDialog != null)
            {
                mFormProgressDialog.Message = msg;
              
                mFormProgressDialog.Close();
                mFormProgressDialog = null;
            }
            log("closeProgressDialog " + msg);
            lblState.Text = msg ;
        }
        void timeout(object obj)
        {
            this.Invoke(new UIHandler1(closeProgressDialog), new string[] { obj.ToString()});
            
        }
       
      
        private void add(GoodList good) {
            good.Typeid = selectedValue;
            string executeString = string.Format("INSERT INTO `tb_good`(good_name,good_integral,good_no,good_imgurl,good_detailedurl,good_typeid) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5})",
            good.Title, good.Integral, good.No, good.ImgUrl, good.DetailedUrl, good.Typeid);
            DBHelper.ExecuteCommand(executeString);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            log("Navigate");
            selectedValue = comboBox1.SelectedValue.ToString();
            switch (Convert.ToInt32(comboBox1.SelectedValue.ToString()))
            {
                case 1:
                    mContext.Parser = new PingAnHtmlParser();
                    webBrowser1.Navigate(CommonHttp.URL_PING_AN);
                    break;
                case 2:
                    mContext.Parser = new ABCHtmlParser();
                    webBrowser1.Navigate(CommonHttp.URL_ABC);
                    break;
                case 3:
                    mContext.Parser = new BOCHtmlParser();
                    webBrowser1.Navigate(CommonHttp.URL_BOC);
                    break;
            }

            showProgressDialog("正在载入页面,请稍候...");
            if (timer == null)
            {
                timer = new System.Threading.Timer(new TimerCallback(timeout), "请求已超时", 5000, 1000);
            }
        }

        void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            log("webBrowser1_Navigating");
            mFormProgressDialog.Message = "正在载入中，请稍后...";
           
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            log("webBrowser1_Navigated");
            closeProgressDialog("页面载入完毕!");
            txtSearch.Text = e.Url.ToString();
        }
        void log(String msg)
        {
            Console.WriteLine(tag+"|"+msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string html = "";
            HtmlDocument obj =webBrowser1.Document;
            if (obj == null)
            {
                MessageBox.Show("数据为空,请先载入页面");
                return;
            }

            html = obj.Body.OuterHtml;
            
            richTextBox1.Text = html;
            saveData(html);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Context context = new Context();
          
            string html = webBrowser1.Document.Body.OuterHtml;
            richTextBox1.Text = html;

            context.Html = html;
            context.Parser = new ABCHtmlParser();
            context.parse();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Equals("")) {
                MessageBox.Show("地址栏不能为空!");
                return;
            }
            webBrowser1.Navigate(txtSearch.Text.Trim());
        }
    }
}