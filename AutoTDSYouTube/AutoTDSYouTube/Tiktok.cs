using AutoTTCYouTube;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTDSYouTube
{
    public partial class Tiktok : Form
    {
        int TongJob;
        bool clickRun = false;
        string ProfileFolderPath = @"D:\Production\ToolManual\AutoTDSYouTube\AutoTDSYouTube\bin\Debug\Profile";
        string ExtentionFolderPath = "Extention";
        List<Account> listAccount = new List<Account>();
        bool isStop = false;
        List<ReponseYoutube> lisNV = new List<ReponseYoutube>();
        List<ReponseTiktok> lisNVTiktok = new List<ReponseTiktok>();
        ReponseNhantien ListXu = new ReponseNhantien();
        List<KeyValuePair<string, string>> listToken = new List<KeyValuePair<string, string>>();
        IWebDriver driver;
        public Tiktok()
        {

            InitializeComponent();
            StreamReader r = new StreamReader("account.json");
            string jsonString = r.ReadToEnd();
            listAccount = JsonConvert.DeserializeObject<List<Account>>(jsonString);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Auto();
            });

            if (clickRun == false)
            {
                button1.Text = "Running";
                clickRun = true;
                isStop = false;
                t.Start();
            }
            else
            {
                button1.Text = "Run Job";
                clickRun = false;
                isStop = true;
            }
        }

        void Auto()
        {
            var i = 0;
            delay(1);
          
                Task t = new Task(async () =>
            {
                foreach (Account account in listAccount)
                {
                    ChromeOptions chromeOptions = new ChromeOptions();
                    ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
                    chromeDriverService.HideCommandPromptWindow = true;
                    if (!Directory.Exists(ProfileFolderPath))
                    {
                        Directory.CreateDirectory(ProfileFolderPath);
                    }

                    if (Directory.Exists(ProfileFolderPath))
                    {
                        string nameProfile = account.idtds;

                        chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
                    }
                    //chromeOptions2.AddArguments("profile-directory=" + UID);
                    //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
                    chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=1200,850", "--disable-gpu");
                    driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);
                    //driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30.0));
                    //driver.Navigate().GoToUrl("https://accounts.google.com/ServiceLogin/signinchooser?service=youtube&uilel=3&passive=true&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Faction_handle_signin%3Dtrue%26app%3Ddesktop%26hl%3Den%26next%3Dhttps%253A%252F%252Fwww.youtube.com%252F&hl=en&ec=65620&ifkv=ARgdvAtbsFtyS9LIFGa8UA8vf9lcW6Dzgi01LlhKYhrArQri81_FA3S8LwVqUFUmMpLwDteQP4SXbw&flowName=GlifWebSignIn&flowEntry=ServiceLogin");
                    //IWebElement query = driver.FindElement(By.CssSelector("input[type='email']"));
                    //query.SendKeys(account.idgg);
                    //query = driver.FindElement(By.CssSelector("div#identifierNext"));
                    //query.Click();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    //query = driver.FindElement(By.CssSelector("input[name='Passwd']"));
                    //query.SendKeys(account.passgg);
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    //Thread.Sleep(500);

                    //query = driver.FindElement(By.XPath(".//*[@id='passwordNext']"));
                    //query.Click();
                    //String oldTab = driver.CurrentWindowHandle.ToString();
                    //Thread.Sleep(5000);

                    //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    //js.ExecuteScript("window.open('_blank', 'tab2');");
                    //driver.SwitchTo().Window("tab2");
                    driver.Navigate().GoToUrl("https://traodoisub.com/");
                    IWebElement query = driver.FindElement(By.XPath("//*[@id=\'username\']"));
                    query.SendKeys(account.idtds);
                    Thread.Sleep(2000);
                    query = driver.FindElement(By.XPath("//*[@id=\'password\']"));
                    query.SendKeys(account.passtds);
                    Thread.Sleep(2000);
                    query = driver.FindElement(By.XPath("//*[@id=\"loginclick\"]"));
                    Thread.Sleep(2000);
                    query.Click();
                    //var tabs = driver.WindowHandles;
                    //if (tabs.Count > 1)
                    //{
                    //    driver.SwitchTo().Window(oldTab);
                    //    Thread.Sleep(4000);
                    //    driver.Close();
                    //    driver.SwitchTo().Window(tabs[1]);
                    //}
                    //js.ExecuteScript("window.open('_blank', 'tab3');");
                    //driver.SwitchTo().Window("tab3");
                    //driver.Navigate().GoToUrl("https://www.tiktok.com/en/");
                    delay(5);
                    if (chkTKsub.Checked)
                    {
                        await WorkSubscribeTikTok();
                    }

                    if (chkCommentTT.Checked)
                    {
                        await WorkCommentTikTok();
                    }
                    // WorkSubscribeTikTok();
                    //();
                    //await (device);
                    //if (isStop)
                    //    break;
                    if (chkLikeTT.Checked)
                    {
                        await WorkTimTikTok();
                    }

                    delay(30);
                }

            });
                delay(1);
                t.Start();
            
            //  }
        }

        private async Task WorkSubscribeTikTok()
        {            
            int i = 0;
            int j = 0;
            var querys = driver.FindElements(By.XPath("//*[@id=\"top\"]/div/div[1]/nav/button"));
            querys[0].Click();
            delay(2);
            querys = driver.FindElements(By.XPath("//*[@id=\"home\"]/li[2]/a"));
            querys[0].Click();
            delay(2);
            querys = driver.FindElements(By.XPath("//*[@id=\"tiktok-coin\"]/li[1]/a/div/span"));
            querys[0].Click();
            delay(2);
            var lisNV = driver.FindElement(By.Id("load")).FindElements(By.TagName("button"));

            foreach (var item in lisNV)
            {
                item.Click();
                string link = item.GetAttribute("title");
                dataGridView1.Invoke(new Action(() =>
                {
                    dataGridView1.Rows.Add(TongJob + 1, "Subscribe Tiktok", link, "Mở Link");
                    dataGridView1.CurrentCell = dataGridView1.Rows[TongJob].Cells[2];
                }));
                delay(10);
                var tabs = driver.WindowHandles;
                driver.SwitchTo().Window(tabs[1]);
                var sbubt = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div/div[1]/div[1]/div[2]/div/div[1]/button"));

                if (sbubt.Count == 0)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Lỗi Video";
                    Thread.Sleep(5000);
                }
                else
                {
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Click Subscribe";
                    sbubt[0].Click();
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Hoàn Thành";
                }
                Thread.Sleep(4000);
                driver.Close();
                Thread.Sleep(4000);
                driver.SwitchTo().Window(tabs[0]);
                //Subscribe


                TongJob++;
                i++;
                j++;
                lblNumber.Invoke((MethodInvoker)(() => lblNumber.Text = j + "/" + lisNV.Count));

                if (i == lisNV.Count)
                {
                    querys = driver.FindElements(By.XPath(" //*[@id=\"nhanall\"]"));
                    querys[0].Click();
                    var sxu = driver.FindElement(By.XPath("//*[@id='soduchinh']"));
                    lblXu.Invoke((MethodInvoker)(() => lblXu.Text = sxu.Text));

                }
                if (isStop)
                    break;
            }
        }
        private async Task WorkTimTikTok()
        {
          
            int i = 0;
            int j = 0;
            var querys = driver.FindElements(By.XPath("//*[@id=\"top\"]/div/div[1]/nav/button"));
            querys[0].Click();
            delay(2);
            querys = driver.FindElements(By.XPath("//*[@id=\"home\"]/li[2]/a"));
            querys[0].Click();
            delay(2);
            querys = driver.FindElements(By.XPath("//*[@id=\"tiktok-coin\"]/li[2]/a"));
            querys[0].Click();
            delay(2);
            var lisNV = driver.FindElement(By.Id("load")).FindElements(By.TagName("button"));

            foreach (var item in lisNV)
            {
                item.Click();
                string link = item.GetAttribute("title");
                dataGridView1.Invoke(new Action(() =>
                {
                    dataGridView1.Rows.Add(TongJob + 1, "Subscribe Tiktok", link, "Mở Link");
                    dataGridView1.CurrentCell = dataGridView1.Rows[TongJob].Cells[2];
                }));
                delay(10);
                var tabs = driver.WindowHandles;
                driver.SwitchTo().Window(tabs[1]);
                var sbubt = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[3]/div[1]/div[1]/div[1]/div[4]/button[1]/span"));

                if (sbubt.Count == 0)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Lỗi Video";
                    Thread.Sleep(5000);
                }
                else
                {
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Click Subscribe";
                    sbubt[0].Click();
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Hoàn Thành";
                }
                Thread.Sleep(4000);
                driver.Close();
                Thread.Sleep(4000);
                driver.SwitchTo().Window(tabs[0]);

                //Subscribe


                TongJob++;
                i++;
                j++;
                lblNumber.Invoke((MethodInvoker)(() => lblNumber.Text = j + "/" + lisNV.Count));

                delay(10);
                if (i == lisNV.Count)
                {
                    querys = driver.FindElements(By.XPath(" //*[@id=\"nhanall\"]"));
                    querys[0].Click();
                    var sxu = driver.FindElement(By.XPath("//*[@id='soduchinh']"));
                    lblXu.Invoke((MethodInvoker)(() => lblXu.Text = sxu.Text));

                }
                if (isStop)
                    break;
            }
        }
        private async Task WorkCommentTikTok()
        {

            try
            {              
               
                int j = 0;
                var querys = driver.FindElements(By.XPath("//*[@id=\"top\"]/div/div[1]/nav/button"));
                querys[0].Click();
                delay(2);
                querys = driver.FindElements(By.XPath("//*[@id=\"home\"]/li[2]/a"));
                querys[0].Click();
                delay(2);
                querys = driver.FindElements(By.XPath("//*[@id=\"tiktok-coin\"]/li[3]/a/div/span"));
                querys[0].Click();
                delay(5);
                var lisNV = driver.FindElement(By.Id("load")).FindElements(By.TagName("button"));
                // lblCurrentJob.Invoke((MethodInvoker)(() => lblCurrentJob.Text = "Comment Youtube"));
                for (int i = 0; i < lisNV.Count; i++)
                {
                    string linktt = lisNV[i].GetAttribute("title");
                    var listnd = driver.FindElements(By.ClassName("card-text"));
                    var nd = listnd[i].Text;
                    if (checkEmoji(nd) || nd == "")
                    {
                        continue;
                    }
                    dataGridView1.Invoke(new Action(() =>
                    {
                        dataGridView1.Rows.Add(TongJob + 1, "Comment Tiktok", linktt, "Xử lý nội dung");
                        dataGridView1.CurrentCell = dataGridView1.Rows[TongJob].Cells[2];
                    }));

                    lisNV[i].Click();                    
                    delay(12);
                    //var notnow = driver.FindElement(By.XPath("/html/body/div[6]/div[2]/div/div/div[3]/button"));
                    //notnow.Click();
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    delay(5);
                    //Subscribe
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,800)");
                    Thread.Sleep(5000);

                    var commentbt = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/div/div/div[1]/div/div[1]"));
                    if(commentbt.Count==0)
                    {
                        continue;
                    }
                    commentbt[0].Click();
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Click ô comment";

                    dataGridView1.Rows[TongJob].Cells[3].Value = "Nhập comment";

                    //var sendKey = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[3]/div[1]/div[3]/div[1]/div/div/div[1]/div/div[1]/div/div/div"));

                    //sendKey.SendKeys(nd);
                    var element = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/div/div/div[1]/div/div[1]"));
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(element);
                    actions.Click();
                    //var a =  nd.Substring(0, 1);
                    //if (a.All(x => char.IsUpper(x)))
                    //{                       
                    actions.SendKeys(" " + nd);
                    actions.Build().Perform();
                    //}
                    //else
                    //{

                    //    actions.SendKeys(nd);
                    //    actions.Build().Perform();
                    //}                   

                    delay(4);
                    var commment = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/div/div/div[2]"));
                    commment.Click();

                    Thread.Sleep(4000);
                    driver.Close();
                    Thread.Sleep(4000);
                    driver.SwitchTo().Window(tabs[0]);
                    dataGridView1.Rows[TongJob].Cells[3].Value = "Nhận xu";
                  
                    delay(5);
                    j++;
                    delay(2);
                    var btbNhantien = driver.FindElement(By.Id("load")).FindElements(By.ClassName("btn-success"));
                    btbNhantien[0].Click();
                    var sxu = driver.FindElement(By.XPath("//*[@id='soduchinh']"));
                    var oldxu = lblXu.Text;
                    delay(5);
                    if (nd != "" && sxu.Text != oldxu)
                    {
                        dataGridView1.Rows[TongJob].Cells[3].Value = "Hoàn thành";
                    }
                    else
                    {
                        dataGridView1.Rows[TongJob].Cells[3].Value = "Thất bại";
                    }

                    TongJob++;
                    lblNumber.Invoke((MethodInvoker)(() => lblNumber.Text = j + "/" + lisNV.Count));
                    delay(5);
                    lblXu.Invoke((MethodInvoker)(() => lblXu.Text = sxu.Text));
                    if (isStop)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void delay(int delay)
        {
            while (delay > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delay--;
                lblWaitingTime.Invoke((MethodInvoker)(() => lblWaitingTime.Text = delay.ToString()));
                if (isStop)
                    break;
            }

        }
        public async Task<string> PostData(
      HttpClient httpClient,
      CookieContainer cookies,
      HttpClientHandler handler,
      string url,
      IEnumerable<KeyValuePair<string, string>> data = null)
        {
            handler.UseCookies = true;
            handler.CookieContainer = cookies;
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Headers.Add("x-requested-with", "XMLHttpRequest");
            if (data != null)
            {
                request.Method = HttpMethod.Post;
                request.Content = (HttpContent)new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(responseBody);
                return responseBody;
            }
            request.Method = HttpMethod.Get;
            HttpResponseMessage response1 = await httpClient.SendAsync(request);
            response1.EnsureSuccessStatusCode();
            string responseBody1 = await response1.Content.ReadAsStringAsync();
            //Console.WriteLine(responseBody1);
            return responseBody1;
        }
        public class ReponseYoutube
        {
            public string idpost;
            public string link;
            public string nd;
        }
        public class ReponseNhantien
        {
            public string mess;
            public int sodu;
            public string error;
            public string error2;
        }

        public class ReponseTiktok
        {
            public string idpost;
            public string link;
        }
        public class ReponseAccount
        {
            public string status;
            public User data;
        }
        public class User
        {
            public string user;
            public int sodu;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }
        public bool checkEmoji(string input)
        {
            string regex = "(?:0\x20E3|1\x20E3|2\x20E3|3\x20E3|4\x20E3|5\x20E3|6\x20E3|7\x20E3|8\x20E3|9\x20E3|#\x20E3|\\*\x20E3|\xD83C(?:\xDDE6\xD83C(?:\xDDE8|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDEE|\xDDF1|\xDDF2|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFC|\xDDFD|\xDDFF)|\xDDE7\xD83C(?:\xDDE6|\xDDE7|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDEF|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFB|\xDDFC|\xDDFE|\xDDFF)|\xDDE8\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF5|\xDDF7|\xDDFA|\xDDFB|\xDDFC|\xDDFD|\xDDFE|\xDDFF)|\xDDE9\xD83C(?:\xDDEA|\xDDEC|\xDDEF|\xDDF0|\xDDF2|\xDDF4|\xDDFF)|\xDDEA\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEC|\xDDED|\xDDF7|\xDDF8|\xDDF9|\xDDFA)|\xDDEB\xD83C(?:\xDDEE|\xDDEF|\xDDF0|\xDDF2|\xDDF4|\xDDF7)|\xDDEC\xD83C(?:\xDDE6|\xDDE7|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDF1|\xDDF2|\xDDF3|\xDDF5|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFC|\xDDFE)|\xDDED\xD83C(?:\xDDF0|\xDDF2|\xDDF3|\xDDF7|\xDDF9|\xDDFA)|\xDDEE\xD83C(?:\xDDE8|\xDDE9|\xDDEA|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9)|\xDDEF\xD83C(?:\xDDEA|\xDDF2|\xDDF4|\xDDF5)|\xDDF0\xD83C(?:\xDDEA|\xDDEC|\xDDED|\xDDEE|\xDDF2|\xDDF3|\xDDF5|\xDDF7|\xDDFC|\xDDFE|\xDDFF)|\xDDF1\xD83C(?:\xDDE6|\xDDE7|\xDDE8|\xDDEE|\xDDF0|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFB|\xDDFE)|\xDDF2\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF5|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFB|\xDDFC|\xDDFD|\xDDFE|\xDDFF)|\xDDF3\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEB|\xDDEC|\xDDEE|\xDDF1|\xDDF4|\xDDF5|\xDDF7|\xDDFA|\xDDFF)|\xDDF4\xD83C\xDDF2|\xDDF5\xD83C(?:\xDDE6|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF7|\xDDF8|\xDDF9|\xDDFC|\xDDFE)|\xDDF6\xD83C\xDDE6|\xDDF7\xD83C(?:\xDDEA|\xDDF4|\xDDF8|\xDDFA|\xDDFC)|\xDDF8\xD83C(?:\xDDE6|\xDDE7|\xDDE8|\xDDE9|\xDDEA|\xDDEC|\xDDED|\xDDEE|\xDDEF|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF7|\xDDF8|\xDDF9|\xDDFB|\xDDFD|\xDDFE|\xDDFF)|\xDDF9\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEB|\xDDEC|\xDDED|\xDDEF|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF7|\xDDF9|\xDDFB|\xDDFC|\xDDFF)|\xDDFA\xD83C(?:\xDDE6|\xDDEC|\xDDF2|\xDDF8|\xDDFE|\xDDFF)|\xDDFB\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEC|\xDDEE|\xDDF3|\xDDFA)|\xDDFC\xD83C(?:\xDDEB|\xDDF8)|\xDDFD\xD83C\xDDF0|\xDDFE\xD83C(?:\xDDEA|\xDDF9)|\xDDFF\xD83C(?:\xDDE6|\xDDF2|\xDDFC)))|[\xA9\xAE\x203C\x2049\x2122\x2139\x2194-\x2199\x21A9\x21AA\x231A\x231B\x2328\x23CF\x23E9-\x23F3\x23F8-\x23FA\x24C2\x25AA\x25AB\x25B6\x25C0\x25FB-\x25FE\x2600-\x2604\x260E\x2611\x2614\x2615\x2618\x261D\x2620\x2622\x2623\x2626\x262A\x262E\x262F\x2638-\x263A\x2648-\x2653\x2660\x2663\x2665\x2666\x2668\x267B\x267F\x2692-\x2694\x2696\x2697\x2699\x269B\x269C\x26A0\x26A1\x26AA\x26AB\x26B0\x26B1\x26BD\x26BE\x26C4\x26C5\x26C8\x26CE\x26CF\x26D1\x26D3\x26D4\x26E9\x26EA\x26F0-\x26F5\x26F7-\x26FA\x26FD\x2702\x2705\x2708-\x270D\x270F\x2712\x2714\x2716\x271D\x2721\x2728\x2733\x2734\x2744\x2747\x274C\x274E\x2753-\x2755\x2757\x2763\x2764\x2795-\x2797\x27A1\x27B0\x27BF\x2934\x2935\x2B05-\x2B07\x2B1B\x2B1C\x2B50\x2B55\x3030\x303D\x3297\x3299]|\xD83C[\xDC04\xDCCF\xDD70\xDD71\xDD7E\xDD7F\xDD8E\xDD91-\xDD9A\xDE01\xDE02\xDE1A\xDE2F\xDE32-\xDE3A\xDE50\xDE51\xDF00-\xDF21\xDF24-\xDF93\xDF96\xDF97\xDF99-\xDF9B\xDF9E-\xDFF0\xDFF3-\xDFF5\xDFF7-\xDFFF]|\xD83D[\xDC00-\xDCFD\xDCFF-\xDD3D\xDD49-\xDD4E\xDD50-\xDD67\xDD6F\xDD70\xDD73-\xDD79\xDD87\xDD8A-\xDD8D\xDD90\xDD95\xDD96\xDDA5\xDDA8\xDDB1\xDDB2\xDDBC\xDDC2-\xDDC4\xDDD1-\xDDD3\xDDDC-\xDDDE\xDDE1\xDDE3\xDDEF\xDDF3\xDDFA-\xDE4F\xDE80-\xDEC5\xDECB-\xDED0\xDEE0-\xDEE5\xDEE9\xDEEB\xDEEC\xDEF0\xDEF3]|\xD83E[\xDD10-\xDD18\xDD80-\xDD84\xDDC0\U0001f970\U0001f923]";
            var result = Regex.Match(input, regex);
            if (result.Length > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
