using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAviso
{
    public partial class Form1 : Form
    {
        int index = 0;
        string strError = "";
        int RowIndexGrid = 0;
        public static string ProfileFolderPath = @"D:\ToolAuto\ToolTDS\AutoAviso\AutoAviso\bin\Debug\Profile";
        string ExtentionFolderPath = @"D:\ToolAuto\ToolTDS\AutoAviso\AutoAviso\bin\Debug\Extention";
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        private Dictionary<string, Thread> threadDictionarySEO = new Dictionary<string, Thread>();
        List<Account> listAccount = new List<Account>();
        List<Account> listAccountSEO = new List<Account>();
        public Form1()
        {
            InitializeComponent();
            StreamReader r = new StreamReader("Account.json");
            string jsonString = r.ReadToEnd();
            listAccount = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            StreamReader rSeo = new StreamReader("AccountSEO.json");
            jsonString = rSeo.ReadToEnd();
            listAccountSEO = JsonConvert.DeserializeObject<List<Account>>(jsonString);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("#", typeof(int));
            dataTable.Columns.Add("ID Google", typeof(string));          
            dataTable.Columns.Add("Job đang làm", typeof(string));
            dataTable.Columns.Add("Credits Thêm", typeof(string));
            dataTable.Columns.Add("Tổng Credits", typeof(string));
            dataTable.Columns.Add("Tổng Job", typeof(string));
            dataTable.Columns.Add("Trạng Thái", typeof(string));

            foreach (var entry in listAccount)
            {
                index++;
                dataTable.Rows.Add(index, entry.id, "", "", "", "", "");

            }
            dataGridAviso.DataSource = dataTable;
            var buttonColumn = new DataGridViewButtonColumn()
            {
                Name = "statusButton",
                HeaderText = "Action",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "Bắt đầu"
                }
            };
            this.dataGridAviso.Columns.Add(buttonColumn);
            this.dataGridAviso.Columns[6].Width = 300;


            DataTable dataTableSEO = new DataTable();
            dataTableSEO.Columns.Add("#", typeof(int));
            dataTableSEO.Columns.Add("ID Google", typeof(string));
            dataTableSEO.Columns.Add("Job đang làm", typeof(string));
            dataTableSEO.Columns.Add("Credits Thêm", typeof(string));
            dataTableSEO.Columns.Add("Tổng Credits", typeof(string));
            dataTableSEO.Columns.Add("Tổng Job", typeof(string));
            dataTableSEO.Columns.Add("Trạng Thái", typeof(string));
            index = 0;
            foreach (var entry in listAccountSEO)
            {
                index++;
                dataTableSEO.Rows.Add(index, entry.id, "", "", "", "", "");

            }

            dataGridSEO.DataSource = dataTableSEO;
            buttonColumn = new DataGridViewButtonColumn()
            {
                Name = "statusButton",
                HeaderText = "Action",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "Bắt đầu"
                }
            };
            this.dataGridSEO.Columns.Add(buttonColumn);
            this.dataGridSEO.Columns[6].Width = 300;
        }     
        private Task viewYoutubeSEO(int rowIndex, IWebDriver driver)
        {
            var querys = driver.FindElements(By.XPath("//*[@id=\"echoall\"]/table/tbody/tr"));
            var xu = driver.FindElements(By.XPath("//*[@id=\"ajax_load\"]/div/div[3]/span"));
            int maxJob = RamdomTime(40, 55);
            int currentJob = 0;
            try
            {
                if (querys.Count > 0)
                {
                    for (int i = 1; i < querys.Count; i++)
                    {
                        dataGridSEO.Rows[rowIndex].Cells[3].Value = "View Youtube";
                        dataGridSEO.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridSEO.Rows[rowIndex].Cells[6].Value = listAccountSEO[rowIndex].totalJob;
                       var style = querys[i].GetAttribute("style");
                        if (style.IndexOf("display:")> -1)
                        {
                            continue;
                        }                     

                        var linkClick = querys[i].FindElements(By.TagName("a"));
                        if (linkClick.Count > 2)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", linkClick[2]);
                            demnguocSEO(RamdomTime(6, 7), rowIndex, "Change tab");
                        }
                     
                        var tabhandel = driver.WindowHandles;
                        if(tabhandel.Count ==1)
                        {
                            continue;
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguocSEO(RamdomTime(10,14), rowIndex, "Load Video");

                        var time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                        int second = int.Parse(time[0].Text);
                        driver.SwitchTo().Frame(0);


                        var unavailable = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span"));
                        if (unavailable.Count > 0)
                        {
                            if (unavailable[0].Text == "Video unavailable")
                            {
                                if (tabhandel.Count > 1)
                                {
                                    driver.SwitchTo().Window(tabhandel[1]);
                                    driver.Close();
                                }
                                demnguocSEO(1, rowIndex, "Chuyển về tab chính");
                                driver.SwitchTo().Window(tabhandel[0]);
                                continue;
                            }
                        }
                        
                        var playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                        if (playVideo.Count > 0)
                        {                            
                             playVideo[0].Click();                            
                        }
                        var playRuVideo = driver.FindElements(By.XPath("//*[@id=\"app\"]/div/div/div[4]/div[2]/div[1]/div[2]/button"));
                        if (playRuVideo.Count > 0)
                        {
                            playRuVideo[0].Click();
                        }

                        if (time.Count > 0)
                        {
                            demnguocSEO(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                        }
                        else
                        {
                            demnguocSEO(60, rowIndex, "Xem View Video");
                        }
                        

                        demnguocSEO(7, rowIndex, "Đóng tab Video");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }
                        demnguocSEO(1, rowIndex, "Chuyển về tab chính");

                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguocSEO(5, rowIndex, "Nhận tiền");
                        listAccountSEO[rowIndex].totalJob = listAccountSEO[rowIndex].totalJob + 1;
                        currentJob = currentJob + 1;
                        if ((currentJob % 5) == 0)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,400)");
                        }
                       
                        if (currentJob == maxJob)
                        {
                            demnguocSEO(8, rowIndex, "Đã đủ job hôm nay" + maxJob +".");
                            break;
                        }
                        demnguocSEO(8, rowIndex, "Chuyển nv kế tiếp");    
                    }
                }

            }
            catch (Exception ex)
            {
                demnguocSEO(8, rowIndex, ex.Message);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);
            }
            var tabclose = driver.WindowHandles;
            for (int i = 0; i < tabclose.Count; i++)
            {
                driver.SwitchTo().Window(tabclose[i]);
                driver.Close();
            }
            return Task.CompletedTask;
        }
        private Task LikeYoutube(int rowIndex)
        {
            demnguoc(5, rowIndex, "Mở Chorme selenium");
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
            chromeDriverService.HideCommandPromptWindow = true;
            if (!Directory.Exists(ProfileFolderPath))
            {
                Directory.CreateDirectory(ProfileFolderPath);
            }

            if (Directory.Exists(ProfileFolderPath))
            {
                string nameProfile = dataGridAviso.Rows[rowIndex].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }

            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://everve.net/tasks/youtube-likes/");


            demnguoc(2, rowIndex, "Duyệt nhiệm vụ");
            var querys = driver.FindElements(By.XPath("/html/body/main/div/div/div[1]/div[2]/table/tbody/tr"));
            int index1 = 1;
            int index2 = 2;
            try
            {
                if (querys.Count > 0)
                {
                    for (int i = 0; i < querys.Count; i++)
                    {
                        var xu = driver.FindElements(By.XPath("/html/body/header/div/div[1]/div[1]/div/button/span[2]/span"));
                        dataGridAviso.Rows[rowIndex].Cells[3].Value = "Like Youtube";
                        dataGridAviso.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridAviso.Rows[rowIndex].Cells[6].Value = listAccount[rowIndex].totalJob;
                        IWebElement SplitCaseYes = driver.FindElement(By.XPath("/html/body/main/div/div/div[1]/div[2]/table/tbody/tr[" + (i + 1) + "]/td[3]/div/a[1]"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", SplitCaseYes);

                        demnguoc(RamdomTime(3, 7), rowIndex, "change tab");
                        var tabhandel = driver.WindowHandles;
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguoc(5, rowIndex, "Load Video");


                        demnguoc(RamdomTime(60, 65), rowIndex, "Xem View Video");
                        var unavalibVideo = driver.FindElements(By.XPath("/html/body/div/div/div[16]/div[1]/div[2]/div[1]"));
                        if (unavalibVideo.Count == 0)
                        {
                            querys = driver.FindElements(By.XPath("/html/body/ytm-app/div[1]/ytm-watch/div[2]/ytm-single-column-watch-next-results-renderer/div/ytm-slim-video-metadata-section-renderer/ytm-slim-video-action-bar-renderer/div/ytm-segmented-like-dislike-button-renderer/div/toggle-button-with-animated-icon/ytm-toggle-button-renderer/button/yt-touch-feedback-shape/div"));

                            if (querys.Count > 0)
                            {

                                querys[0].Click();
                            }
                            else
                            {
                                querys = driver.FindElements(By.XPath("/html/body/ytm-app/div[1]/ytm-watch/div[2]/ytm-single-column-watch-next-results-renderer/ytm-slim-video-metadata-section-renderer/ytm-slim-video-action-bar-renderer/div/ytm-segmented-like-dislike-button-renderer/div/toggle-button-with-animated-icon/ytm-toggle-button-renderer/button/yt-touch-feedback-shape/div"));
                                if (querys.Count > 0)
                                {
                                    querys[0].Click();
                                }
                            }
                        }
                        demnguoc(5, rowIndex, "Đóng tab Video");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }
                        demnguoc(1, rowIndex, "Chuyển về tab chính");

                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguoc(5, rowIndex, "Nhận tiền");

                        var element = driver.FindElement(By.XPath("//*[@id=\"next_report_button\"]"));

                        var strStyle = element.GetAttribute("style");
                        if (strStyle.IndexOf("display") >= 0)
                        {
                            var success = driver.FindElements(By.XPath("/html/body/main/div/div/div[3]/div/div/div/div/div/div[2]/div/div[7]/button"));
                            success[0].Click();
                            listAccount[rowIndex].totalJob = listAccount[rowIndex].totalJob + 1;
                        }
                        else
                        {
                            var warmning = driver.FindElements(By.XPath("/html/body/main/div/div/div[3]/div/div/div/div/div/div[2]/div/div[6]/div/button[1]"));
                            warmning[0].Click();
                        }
                        demnguoc(RamdomTime(8, 10), rowIndex, "Chuyển nv kế tiếp");
                        index1 = index1 + 2;
                        index2 = index2 + 2;
                    }
                }

            }
            catch (Exception ex)
            {
                demnguoc(8, rowIndex, ex.Message);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);
            }
            var tabclose = driver.WindowHandles;
            for (int i = 0; i < tabclose.Count; i++)
            {
                driver.SwitchTo().Window(tabclose[i]);
                driver.Close();
            }
            return Task.CompletedTask;

        }

        private Task traffic(int rowIndex)
        {
            demnguoc(5, rowIndex, "Mở Chorme selenium");
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
            chromeDriverService.HideCommandPromptWindow = true;
            if (!Directory.Exists(ProfileFolderPath))
            {
                Directory.CreateDirectory(ProfileFolderPath);
            }

            if (Directory.Exists(ProfileFolderPath))
            {
                string nameProfile = dataGridAviso.Rows[rowIndex].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }

            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            //chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://aviso.bz/work-serf");


            demnguoc(2, rowIndex, "Duyệt nhiệm vụ");
            var querys = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td/div/table"));
            int index1 = 3;
            // int index2 = 2;
            try
            {
                if (querys.Count > 1)
                {
                    for (int i = 0; i < querys.Count; i++)
                    {
                        var xu = driver.FindElements(By.XPath("//*[@id=\"new-money-ballans\"]"));
                        //dataGrid.Rows[rowIndex].Cells[4].Value = xuthem;
                        dataGridAviso.Rows[rowIndex].Cells[3].Value = "Traffic Web";
                        dataGridAviso.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridAviso.Rows[rowIndex].Cells[6].Value = listAccount[rowIndex].totalJob;
                        var idTable = querys[i].GetAttribute("id");                      
                        ///html/body/table/tbody/tr[3]/td[2]/div[4]/
                        ///html/body/table/tbody/tr[3]/td[2]/div[5]/table/tbody/tr/td[2]/div[1]/a
                        if (idTable == "serf-link-59867")
                        {
                            index1 = index1 + 1;
                            continue;
                        }
                        demnguoc(2, rowIndex, "Click suffix");
                        IWebElement link = driver.FindElement(By.XPath("/html/body/table/tbody/tr[3]/td[2]/div[" + index1 + "]/table/tbody/tr/td[2]/div[1]/a"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", link);
                      
                        demnguoc(RamdomTime(5, 7), rowIndex, "Click link");
                        var a = driver.FindElement(By.XPath("/html/body/table/tbody/tr[3]/td[2]/div[" + index1 + "]/table/tbody/tr/td[2]/div[1]/a"));
                        executor.ExecuteScript("arguments[0].click();", a);
                        demnguoc(RamdomTime(5, 10), rowIndex, "Change tab suffiz");
                        var tabhandel = driver.WindowHandles;
                        driver.SwitchTo().Window(tabhandel[1]);
                        driver.SwitchTo().Frame(0);
                        var time = driver.FindElement(By.XPath("/html/body/table/tbody/tr/td[2]/span")).Text;

                        demnguoc(RamdomTime(3, 4), rowIndex, "Check time");
                        var time1 = driver.FindElement(By.XPath("/html/body/table/tbody/tr/td[2]/span")).Text;
                        if(time1 == time)
                        {
                            driver.Navigate().Refresh();
                            demnguoc(RamdomTime(3, 4), rowIndex, "Load page");
                        }
                        
                        demnguoc(RamdomTime(int.Parse(time), int.Parse(time)+5), rowIndex, "Change tab suffiz");  
                       
                        var  button = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/a"));
                        if (button.Count() > 0)
                        {
                            executor.ExecuteScript("arguments[0].click();", button[0]);
                        }
                        else
                        {
                            button = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/a"));
                        }
                        

                        demnguoc(5, rowIndex, "Đóng tab");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }
                        demnguoc(1, rowIndex, "Chuyển về tab chính");

                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguoc(5, rowIndex, "Nhận tiền");
                        
                        demnguoc(RamdomTime(10, 15), rowIndex, "Chuyển nv kế tiếp");
                        index1 = index1 + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                demnguoc(8, rowIndex, ex.Message);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);
            }
            var tabclose = driver.WindowHandles;
            for (int i = 0; i < tabclose.Count; i++)
            {
                driver.SwitchTo().Window(tabclose[i]);
                driver.Close();
            }
            return Task.CompletedTask;

        }
        public void demnguoc(int time, int rowIndex, string job)
        {
            try
            {
                while (time > 0)
                {
                    if (!this.threadDictionary.ContainsKey(this.dataGridAviso[1, rowIndex].Value.ToString()))
                    {
                        this.dataGridAviso.Invoke(new Action(() =>
                        {
                            this.dataGridAviso[7, rowIndex].Value = (object)"Đã dừng";
                        }));
                    }
                    this.dataGridAviso.Invoke(new Action(() =>
                    {
                        this.dataGridAviso.Rows[rowIndex].Cells[7].Value = string.Format("{0} - {1} {2} s...", job, "Vui lòng chờ", time);
                    }));
                    --time;
                    Thread.Sleep(1000);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void demnguocSEO(int time, int rowIndex, string job)
        {
            try
            {
                while (time > 0)
                {
                    if (!this.threadDictionarySEO.ContainsKey("SEO" +this.dataGridSEO[1, rowIndex].Value.ToString()))
                    {
                        this.dataGridSEO.Invoke(new Action(() =>
                        {
                            this.dataGridSEO[7, rowIndex].Value = (object)"Đã dừng";
                        }));
                    }
                    this.dataGridSEO.Invoke(new Action(() =>
                    {
                        this.dataGridSEO.Rows[rowIndex].Cells[7].Value = string.Format("{0} - {1} {2} s...", job, "Vui lòng chờ", time);
                    }));
                    --time;
                    Thread.Sleep(1000);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private int RamdomTime(int begin, int end)
        {
            Random rnd = new Random();
            int ramNumber = rnd.Next(begin, end);
            return ramNumber;
        }
        private void M_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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
                string nameProfile = dataGridAviso.Rows[RowIndexGrid].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }
            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
           // chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://aviso.bz/?r=nlduong");

        }

        private void dataGridAviso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridAviso[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        var i = 0;
                        while (i < 15)
                        {                          
                          
                                await traffic(e.RowIndex);
                           
                               // await LikeYoutube(e.RowIndex);
                           
                              //  await viewYoutube(e.RowIndex);
                           
                            i++;
                            dataGridAviso.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                            demnguoc(360, e.RowIndex, "Thời gian xoay vòng nhiệm vụ");
                        }


                    }));
                    thread.Name = dataGridAviso.Rows[e.RowIndex].Cells[1].Value.ToString();
                    thread.Start();
                    this.threadDictionary.Add(dataGridAviso.Rows[e.RowIndex].Cells[1].Value.ToString(), thread);
                }
                else
                {
                    cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    this.threadDictionary[dataGridAviso.Rows[e.RowIndex].Cells[1].Value.ToString()].Abort();
                    this.threadDictionary.Remove(dataGridAviso.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }

        private void dataGridAviso_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                int currentMouseOverRow = dataGridAviso.HitTest(e.X, e.Y).RowIndex;
                RowIndexGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGridAviso, new System.Drawing.Point(e.X, e.Y));

                m.ItemClicked += M_ItemClicked;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AcceptInsecureCertificates = true;
            //chromeOptions.Proxy = new Proxy { HttpProxy = ":7738", SslProxy = "103.185.186.225:7738", Kind = ProxyKind.Manual };
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
            chromeDriverService.HideCommandPromptWindow = true;
            //if (!Directory.Exists(ProfileFolderPath))
            //{
            //    Directory.CreateDirectory(ProfileFolderPath);
            //}

            //if (Directory.Exists(ProfileFolderPath))
            //{
            //    string nameProfile = dataGridAviso.Rows[RowIndexGrid].Cells[1].Value.ToString();

            //    chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            //}          
            var proxy = new Proxy();
            // Thiết lập thông tin proxy
            proxy.Kind = ProxyKind.Manual;
            proxy.HttpProxy = "103.185.186.225:27738"; // địa chỉ proxy và port
            proxy.SslProxy = "103.185.186.225:27738";
           // proxy.ProxyAutoConfigUrl = null;
            // Thiết lập thông tin đăng nhập vào proxy
            proxy.SocksUserName = "nlduong2007";
            proxy.SocksPassword = "5e6ae8b9";
            // Khởi tạo đối tượng ChromeOptions và add Proxy vào          
            chromeOptions.Proxy = proxy;

           

            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);

            driver.Navigate().GoToUrl("https://whoer.net/");
           
        }

        private void dataGridSEO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridSEO[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        var i = 0;
                        demnguocSEO(5, e.RowIndex, "Mở Chorme selenium");
                        ChromeOptions chromeOptions = new ChromeOptions();
                        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
                        chromeDriverService.HideCommandPromptWindow = true;
                        if (!Directory.Exists(ProfileFolderPath))
                        {
                            Directory.CreateDirectory(ProfileFolderPath);
                        }

                        if (Directory.Exists(ProfileFolderPath))
                        {
                            string nameProfile = "SEO" + dataGridSEO.Rows[e.RowIndex].Cells[1].Value.ToString();

                            chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
                        }

                        chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=850,850", "--disable-gpu");
                        IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
                        driver.Navigate().GoToUrl("https://seo-fast.ru/login");


                        demnguocSEO(100, e.RowIndex, "Load page");

                        while (i < 15)
                        {

                           // await traffic(e.RowIndex);

                            // await LikeYoutube(e.RowIndex);

                             await viewYoutubeSEO(e.RowIndex, driver);

                            i++;
                            dataGridSEO.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                            demnguocSEO(360, e.RowIndex, "Thời gian xoay vòng nhiệm vụ");
                        }


                    }));
                    thread.Name = "SEO" + dataGridSEO.Rows[e.RowIndex].Cells[1].Value.ToString();
                    thread.Start();
                    this.threadDictionarySEO.Add("SEO" + dataGridSEO.Rows[e.RowIndex].Cells[1].Value.ToString(), thread);
                }
                else
                {
                    cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    this.threadDictionarySEO["SEO" + dataGridSEO.Rows[e.RowIndex].Cells[1].Value.ToString()].Abort();
                    this.threadDictionarySEO.Remove("SEO" + dataGridSEO.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }

        }

        private void dataGridSEO_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                int currentMouseOverRow = dataGridAviso.HitTest(e.X, e.Y).RowIndex;
                RowIndexGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGridAviso, new System.Drawing.Point(e.X, e.Y));

                m.ItemClicked += M_ItemClicked1; ;
            }
        }

        private void M_ItemClicked1(object sender, ToolStripItemClickedEventArgs e)
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
                string nameProfile = "SEO" + dataGridSEO.Rows[RowIndexGrid].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }
            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            // chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://seo-fast.ru/?r=2799414");
        }
    }
    public class Account
    {
        public string id { get; set; }
        public string pass { get; set; } 
        public int totalJob { get; set; }
        public int totalFailJob { get; set; }
    }
}
