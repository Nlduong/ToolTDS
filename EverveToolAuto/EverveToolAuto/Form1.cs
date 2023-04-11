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

namespace EverveToolAuto
{
    public partial class Form1 : Form
    {
        int index = 0;
        string strError = "";
        int RowIndexGrid = 0;
        public static string ProfileFolderPath = @"D:\ToolAuto\ToolTDS\EverveToolAuto\EverveToolAuto\bin\Debug\Profile";
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        List<Account> listAccount = new List<Account>();
        public Form1()
        {
            InitializeComponent();
            StreamReader r = new StreamReader("Account.json");
            string jsonString = r.ReadToEnd();
            listAccount = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("#", typeof(int));
            dataTable.Columns.Add("ID Google", typeof(string));
            //dataTable.Columns.Add("Pass Google", typeof(string));
            //dataTable.Columns.Add("ID TDS", typeof(string));
            //dataTable.Columns.Add("Pass TDS", typeof(string));
            dataTable.Columns.Add("Job đang làm", typeof(string));
            dataTable.Columns.Add("Credits Thêm", typeof(string));
            dataTable.Columns.Add("Tổng Credits", typeof(string));
            dataTable.Columns.Add("Tổng Job", typeof(string));
            dataTable.Columns.Add("Trạng Thái", typeof(string));

            foreach (var entry in listAccount)
            {
                index++;
                dataTable.Rows.Add(index, entry.idgg, "", "", "", "", "");

            }
            dataGrid.DataSource = dataTable;       
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
            this.dataGrid.Columns.Add(buttonColumn);
            this.dataGrid.Columns[6].Width = 300;           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        var i = 0;
                        while (i<15)
                        {
                            if (chkTrafficView.Checked)
                            {
                                await traffic(e.RowIndex);
                            }
                            if (chkLikeYouTube.Checked)
                            {
                                await LikeYoutube(e.RowIndex);
                            }
                            if (chkViewYoutube.Checked)
                            {
                                await viewYoutube(e.RowIndex);
                            }
                            i++;
                            dataGrid.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                            demnguoc(360, e.RowIndex, "Thời gian xoay vòng nhiệm vụ");                           
                        }
                       

                    }));
                    thread.Name = dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                    thread.Start();
                    this.threadDictionary.Add(dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString(), thread);
                }
                else
                {
                    cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    this.threadDictionary[dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString()].Abort();
                    this.threadDictionary.Remove(dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }
        private Task viewYoutube(int rowIndex)
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
                string nameProfile = dataGrid.Rows[rowIndex].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }

            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl(" https://everve.net/tasks/youtube-views/");


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

                        dataGrid.Rows[rowIndex].Cells[3].Value = "View Youtube";
                        dataGrid.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGrid.Rows[rowIndex].Cells[6].Value = listAccount[rowIndex].totalJob;
                        var query1 = querys[i].FindElements(By.XPath("/html/body/main/div/div/div[1]/div[2]/table/tbody/tr[" + (i + 1) + "]/td[3]/div/a[1]"));
                        if (query1.Count > 0)
                        {
                            query1[0].Click();
                        }
                        else
                        {
                            break;
                        }
                        demnguoc(RamdomTime(3, 7), rowIndex, "change tab");
                        var tabhandel = driver.WindowHandles;
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguoc(5, rowIndex, "Load Video");
                        var unavalibVideo = driver.FindElements(By.XPath("/html/body/div/div/div[16]/div[1]/div[2]/div[1]"));
                        if (unavalibVideo.Count == 0)
                        {
                           var  query = driver.FindElement(By.XPath("/html/body/div/div/div[4]/button"));
                            if (query != null)
                            {
                                query.Click();
                            }
                        }

                        demnguoc(RamdomTime(60, 66), rowIndex, "Xem View Video");

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
                        demnguoc(8, rowIndex, "Chuyển nv kế tiếp");
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
                string nameProfile = dataGrid.Rows[rowIndex].Cells[1].Value.ToString();

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
                        dataGrid.Rows[rowIndex].Cells[3].Value = "Like Youtube";
                        dataGrid.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGrid.Rows[rowIndex].Cells[6].Value = listAccount[rowIndex].totalJob;
                        var query1 = querys[i].FindElements(By.XPath("/html/body/main/div/div/div[1]/div[2]/table/tbody/tr[" + (i + 1) + "]/td[3]/div/a[1]"));
                        if(query1.Count>0)
                        {
                            query1[0].Click();
                        }
                        else
                        {
                            break;
                        }                         
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
                        demnguoc(RamdomTime(8,10), rowIndex, "Chuyển nv kế tiếp");
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
                string nameProfile = dataGrid.Rows[rowIndex].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }

            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://everve.net/tasks/traffic-exchange/");


            demnguoc(2, rowIndex, "Duyệt nhiệm vụ");
            var querys = driver.FindElements(By.XPath("/html/body/main/div/div/div[1]/div[2]/table/tbody/tr"));
            //int index1 = 1;
           // int index2 = 2;
            try
            {
                if (querys.Count > 1)
                {
                    for (int i = 0; i < querys.Count; i++)
                    {                       
                        var xu = driver.FindElements(By.XPath("/html/body/header/div/div[1]/div[1]/div/button/span[2]/span"));
                        //dataGrid.Rows[rowIndex].Cells[4].Value = xuthem;
                        dataGrid.Rows[rowIndex].Cells[3].Value = "Traffic Web";
                        dataGrid.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGrid.Rows[rowIndex].Cells[6].Value = listAccount[rowIndex].totalJob;
                             
                        demnguoc(2, rowIndex, "Click traffic");
                        //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                        var query1 = querys[i].FindElements(By.XPath("/html/body/main/div/div/div[1]/div[2]/table/tbody/tr[" + (i + 1) + "]/td[3]/div/a[1]"));
                        if (query1.Count > 0)
                        {
                            query1[0].Click();
                        }
                        else
                        {
                            break;
                        }
                        demnguoc(RamdomTime(5, 8), rowIndex, "Change tab traffic");
                        var tabhandel = driver.WindowHandles;
                        driver.SwitchTo().Window(tabhandel[1]);
                        for (int k = 0; k < 7;k++)
                        {
                            demnguoc(RamdomTime(5, 8), rowIndex, "scroll traffic web");
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,500)");
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
                        demnguoc(RamdomTime(10, 15), rowIndex, "Chuyển nv kế tiếp");
                       // index1 = index1 + 2;
                       // index2 = index2 + 2;
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
                    if (!this.threadDictionary.ContainsKey(this.dataGrid[1, rowIndex].Value.ToString()))
                    {
                        this.dataGrid.Invoke(new Action(() =>
                        {
                            this.dataGrid[7, rowIndex].Value = (object)"Đã dừng";
                        }));
                    }
                    this.dataGrid.Invoke(new Action(() =>
                    {
                        this.dataGrid.Rows[rowIndex].Cells[7].Value = string.Format("{0} - {1} {2} s...", job, "Vui lòng chờ", time);
                    }));
                    --time;
                    Thread.Sleep(1000);
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }
        private int RamdomTime(int begin,int end)
        {
            Random rnd = new Random();
            int ramNumber = rnd.Next(begin, end);
            return ramNumber;
        }

        private void dataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                int currentMouseOverRow = dataGrid.HitTest(e.X, e.Y).RowIndex;
                RowIndexGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGrid, new System.Drawing.Point(e.X, e.Y));

                m.ItemClicked += M_ItemClicked;
            }
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
                string nameProfile = dataGrid.Rows[RowIndexGrid].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }
            //chromeOptions2.AddArguments("profile-directory=" + UID);
            //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://everve.net/ref/1308344/");
        }
    }
    public class Account
    {
        public string idgg { get; set; }
        public string passgg { get; set; }
        public string idtds { get; set; }
        public string passtds { get; set; }
        public string tokentds { get; set; }
        public int totalJob { get; set; }
        public int totalFailJob { get; set; }
    }
}
