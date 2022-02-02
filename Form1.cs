using System;
using System.Linq;
using System.Windows.Forms;
using xNet;

namespace FakeIP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void AddCookie(HttpRequest http, string cookie)
        {
            var temp = cookie.Split(';');
            foreach (var item in temp)
            {
                var temp2 = item.Split('=');
                if (temp2.Count() > 1)
                {
                    http.Cookies.Add(temp2[0], temp2[1]);
                }
            }
        }
        string GetData(string url, HttpRequest http = null, string userArgent = "", string cookie = null)
        {
            if (http == null)
            {
                http = new HttpRequest();
                http.Cookies = new CookieDictionary();
            }

            if (!string.IsNullOrEmpty(cookie))
            {
                AddCookie(http, cookie);
            }

            if (!string.IsNullOrEmpty(userArgent))
            {
                http.UserAgent = userArgent;
            }
            string html = http.Get(url).ToString();
            return html;
        }
        private void btnFake_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.Cookies = new CookieDictionary();
                httpRequest.UserAgent = Http.ChromeUserAgent();
                httpRequest.Proxy = Socks5ProxyClient.Parse(txtIP.Text.Trim());

                var html = GetData("https://whoer.net/", httpRequest);
                webBrowser1.DocumentText = html;
               
                
            }
            catch (Exception ex)
            {

                webBrowser1.DocumentText = ex.Message;
            }
        }
    }
}
