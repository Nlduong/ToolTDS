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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AutoLike4Like.Common;

namespace AutoLike4Like
{
    public partial class Form1 : Form
    {
        int index = 0;
        string strError = "";
        List<Account> listAccount = new List<Account>();
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        int RowIndexTDSGrid = 0;
        int tongJob = 0;
        int tongFailJobTT = 0;
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
                dataTable.Rows.Add(index, entry.idgg,"", "", "", "", "");

            }
            dataGrid.DataSource = dataTable;
            dataGridViewControl.DataSource = dataTable;
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
            var buttonColumn1 = new DataGridViewButtonColumn()
            {
                Name = "statusButton",
                HeaderText = "Action",
                UseColumnTextForButtonValue = false,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "Bắt đầu"
                }
            };
            this.dataGridViewControl.Columns.Add(buttonColumn1);
            this.dataGridViewControl.Columns[6].Width = 300;
        }
        private void dataGridViewControl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewControl[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        demnguocView(5, e.RowIndex, "Mở Chorme selenium");
                        ChromeOptions chromeOptions = new ChromeOptions();
                        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
                        chromeDriverService.HideCommandPromptWindow = true;
                        if (!Directory.Exists(ProfileFolderPath))
                        {
                            Directory.CreateDirectory(ProfileFolderPath);
                        }

                        if (Directory.Exists(ProfileFolderPath))
                        {
                            string nameProfile = dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();

                            chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
                        }
                        //chromeOptions2.AddArguments("profile-directory=" + UID);
                        //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
                        chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=600,850", "--disable-gpu");
                        IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
                        driver.Navigate().GoToUrl("https://www.like4like.org/");
                        //*[@id="top-header"]/div[3]/a
                        var logintbt = driver.FindElements(By.XPath("//*[@id=\"top-header\"]/div[3]/a"));
                        if (logintbt.Count > 0)
                        {
                            logintbt[0].Click();
                        }

                        var query = driver.FindElement(By.XPath("//*[@id=\"username\"]"));
                        query.SendKeys(listAccount[e.RowIndex].idtds);
                        demnguocView(3, e.RowIndex, "Nhập Username");
                        query = driver.FindElement(By.XPath("//*[@id=\"password\"]"));
                        query.SendKeys(listAccount[e.RowIndex].passtds);
                        demnguocView(3, e.RowIndex, "Nhập Password");
                        query = driver.FindElement(By.XPath("//*[@id=\"login\"]/fieldset/table/tbody/tr[8]/td/span"));
                        query.Click();
                        demnguocView(3, e.RowIndex, "Đăng Nhập");
                        if (chkLikeTiktok.Checked)
                        {
                            dataGridViewControl.Rows[e.RowIndex].Cells[3].Value = "Repost Tiktok";
                            await RepostSoundCloud(e.RowIndex, driver);
                        }
                        //if (chkLikeTiktok.Checked)
                        //{
                        //    dataGridViewControl.Rows[e.RowIndex].Cells[3].Value = "Like Tiktok";
                        //    await LikeSoundCloud(e.RowIndex, driver);
                        //}
                        if (chkLikeTiktok.Checked)
                        {
                            dataGridViewControl.Rows[e.RowIndex].Cells[3].Value = "Follow Tiktok";
                            await FollowTikTok(e.RowIndex, driver);
                        }
                        if (chkLikeTiktok.Checked)
                        {
                            dataGridViewControl.Rows[e.RowIndex].Cells[3].Value = "Like Tiktok";
                            await LikeTikTok(e.RowIndex, driver);
                        }

                        var tabclose = driver.WindowHandles;
                        for (int i = 0; i < tabclose.Count; i++)
                        {
                            driver.SwitchTo().Window(tabclose[i]);
                            driver.Close();
                        }
                    }));
                    thread.Name = dataGridViewControl.Rows[e.RowIndex].Cells[1].Value.ToString();
                    thread.Start();
                    this.threadDictionary.Add(dataGridViewControl.Rows[e.RowIndex].Cells[1].Value.ToString(), thread);
                }
                else
                {
                    cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    this.threadDictionary[dataGridViewControl.Rows[e.RowIndex].Cells[1].Value.ToString()].Abort();
                    this.threadDictionary.Remove(dataGridViewControl.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }

        private void dataGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        demnguoc(5, e.RowIndex, "Mở Chorme selenium");
                        ChromeOptions chromeOptions = new ChromeOptions();
                        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
                        chromeDriverService.HideCommandPromptWindow = true;
                        if (!Directory.Exists(ProfileFolderPath))
                        {
                            Directory.CreateDirectory(ProfileFolderPath);
                        }

                        if (Directory.Exists(ProfileFolderPath))
                        {
                            string nameProfile = dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();

                            chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
                        }
                        //chromeOptions2.AddArguments("profile-directory=" + UID);
                        //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
                        chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=400,850", "--disable-gpu");
                        chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
                        IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
                        driver.Navigate().GoToUrl("https://www.like4like.org/");
                        //*[@id="top-header"]/div[3]/a
                        var logintbt = driver.FindElements(By.XPath("//*[@id=\"top-header\"]/div[3]/a"));
                        if (logintbt.Count > 0)
                        {
                            logintbt[0].Click();
                        }

                        demnguoc(3, e.RowIndex, "Click Login");

                        var query = driver.FindElement(By.XPath("//*[@id=\"username\"]"));
                        query.SendKeys(listAccount[e.RowIndex].idtds);
                        demnguoc(3, e.RowIndex, "Nhập Username");
                        query = driver.FindElement(By.XPath("//*[@id=\"password\"]"));
                        query.SendKeys(listAccount[e.RowIndex].passtds);
                        demnguoc(3, e.RowIndex, "Nhập Password");
                        query = driver.FindElement(By.XPath("//*[@id=\"login\"]/fieldset/table/tbody/tr[8]/td/span"));
                        query.Click();
                        demnguoc(3, e.RowIndex, "Đăng Nhập");
                       
                        if (chkLikeFace.Checked)
                        {
                            dataGrid.Rows[e.RowIndex].Cells[3].Value = "Like FaceBook";
                            await LikeFacebook(e.RowIndex, driver);
                        }
                        if (chkFollowFace.Checked)
                        {
                            dataGrid.Rows[e.RowIndex].Cells[3].Value = "Follow Page FaceBook";
                            await FollowFacebookPage(e.RowIndex, driver);
                        }
                        if (chkLikeYouTube.Checked)
                        {
                            dataGrid.Rows[e.RowIndex].Cells[3].Value = "Like Youtube";
                            await LikeYoutube(e.RowIndex, driver);
                        }

                        if (chkSubYoutube.Checked)
                        {
                            dataGrid.Rows[e.RowIndex].Cells[3].Value = "Subcribed Youtube";
                            await SubsribeYoutube(e.RowIndex, driver);
                        }

                        if (chkCommentYouTube.Checked)
                        {
                            dataGrid.Rows[e.RowIndex].Cells[3].Value = "Comment Youtube";
                            await CommentYoutube(e.RowIndex, driver);
                        }

                        var tabclose = driver.WindowHandles;
                        for (int i = 0; i < tabclose.Count; i++)
                        {
                            driver.SwitchTo().Window(tabclose[i]);
                            driver.Close();
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

        private void dataGrid_MouseClick_1(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                int currentMouseOverRow = dataGrid.HitTest(e.X, e.Y).RowIndex;
                RowIndexTDSGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGrid, new Point(e.X, e.Y));

                m.ItemClicked += M_ItemClicked;
            }
        }
        private void M_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           // test();
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
            chromeDriverService.HideCommandPromptWindow = true;
            if (!Directory.Exists(ProfileFolderPath))
            {
                Directory.CreateDirectory(ProfileFolderPath);
            }

            if (Directory.Exists(ProfileFolderPath))
            {
                string nameProfile = dataGrid.Rows[RowIndexTDSGrid].Cells[1].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }
            //chromeOptions2.AddArguments("profile-directory=" + UID);
            //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=800,850", "--disable-gpu");
            //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://www.youtube.com/");
        }
        #region Youtube
        private Task LikeYoutube(int index, IWebDriver driver)
        {            
            try
            {
                demnguoc(5, index, "Click chọn Menu");
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguoc(5, index, "Click Menu");
                 query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguoc(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguoc(3, index, "Chọn loại nhiệm vụ");               
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[32]"));
                query.Click();
                demnguoc(3, index, "Lấy nhiệm vụ");
                int i = 0;
                int j = 0;
                while (i < int.Parse(txtLikeYTBMaxPer.Text) && j < 5)
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGrid.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        j++;
                        continue;
                    }
                   
                 
                    demnguoc(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguoc(15, index, "Chuyển tab Youtube");                  
                   
                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {
                       
                        var likebtn = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[1]/ytm-watch/ytm-single-column-watch-next-results-renderer/div/ytm-slim-video-metadata-section-renderer/ytm-slim-video-action-bar-renderer/div/ytm-segmented-like-dislike-button-renderer/div/toggle-button-with-animated-icon/ytm-toggle-button-renderer/button/yt-touch-feedback-shape/div/div[2]"));

                        if (likebtn.Count>0)
                        {
                            likebtn[0].Click();
                            checkClick=true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            likebtn = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[1]/ytm-watch/ytm-single-column-watch-next-results-renderer/ytm-slim-video-metadata-section-renderer/ytm-slim-video-action-bar-renderer/div/ytm-segmented-like-dislike-button-renderer/div/toggle-button-with-animated-icon/ytm-toggle-button-renderer/button/yt-touch-feedback-shape/div/div[2]"));
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                            //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                            //demnguoc(2, index, "Scoll page");
                            //demnguoc(3, index, "Load lại page");
                        }
                        time = time+1;
                    }
                    demnguoc(1, index, "Chuẩn bị chuyển về tab chính");
                    driver.SwitchTo().Window(tabs[0]);                                      
                    demnguoc(6, index, "Chuyển tab chính");
                 
                   
                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img")); 
                     
                    if(querys.Count==0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                    }
                  
                    if(querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        j++;
                        continue;
                    }
                   
                    demnguoc(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));
                   
                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;                   
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguoc(3, index, "Chuyển nhiệm vụ");
                }
             
            }
            catch (Exception ex)
            {
                //var tabs = driver.WindowHandles;
                //driver.SwitchTo().Window(tabs[1]);
                //driver.Close();
                //driver.SwitchTo().Window(tabs[0]);
                //driver.Close();
                demnguoc(10, index, ex.Message);
                //await commentTikTok(index);
            }

            return Task.CompletedTask;
        }
        private Task SubsribeYoutube(int index, IWebDriver driver)
        {
            
            try
            {               
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguoc(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguoc(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguoc(3, index, "Chọn loại nhiệm vụ");
                //*[@id="select-feature"]/option[33]
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[33]"));
                query.Click();
                demnguoc(3, index, "Lấy nhiệm vụ");
                int i = 0;
                int j = 0;
                while (i < int.Parse(txtSubYTBMaxPer.Text) && j <5)
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGrid.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        j++;
                        continue;
                    }
                    demnguoc(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguoc(15, index, "Chuyển tab Youtube");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {
                        var likebtn = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[1]/ytm-browse/ytm-c4-tabbed-header-renderer/div/div/div/ytm-subscribe-button-renderer/div/div/button/yt-touch-feedback-shape/div/div[2]"));                      
                        if (likebtn.Count > 0)
                        {
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,400)");
                            demnguoc(2, index, "Scoll page");
                        }
                        time = time + 1;
                    }

                    demnguoc(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.SwitchTo().Window(tabs[0]);
                    demnguoc(3, index, "Chuyển tab chính");
                  
                   
                   querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));
                      
                   if(querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                       
                    }                  
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        j++;
                        continue;
                    }
                       
                    demnguoc(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));
                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguoc(3, index, "Chuyển nhiệm vụ");
                }               
            }
            catch (Exception ex)
            {
                //var tabs = driver.WindowHandles;
                //driver.SwitchTo().Window(tabs[1]);
                //driver.Close();
                //driver.SwitchTo().Window(tabs[0]);
                //driver.Close();
                demnguoc(10, index, ex.Message);
                //await commentTikTok(index);
            }

            return Task.CompletedTask;
        }
        private Task CommentYoutube(int index, IWebDriver driver)
        {
            var stri = "";
            try
            {
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguoc(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguoc(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguoc(3, index, "Chọn loại nhiệm vụ");
                //*[@id="select-feature"]/option[33]
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[31]"));
                query.Click();
                demnguoc(3, index, "Lấy nhiệm vụ");
                int i = 0;
                int j = 0;
                while (i < int.Parse(TxtCommentYTBMaxPer.Text) && j < 5)
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGrid.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        j++;
                        continue;
                    }
                    demnguoc(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguoc(3, index, "Chuyển tab Youtube");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {

                        demnguoc(5, index, "Click thẻ comment");
                        var showCommentbtn = driver.FindElements(By.XPath("/html/body/ytm-app/div[1]/ytm-watch/ytm-single-column-watch-next-results-renderer/div/ytm-item-section-renderer/lazy-list/ytm-comments-entry-point-header-renderer/button/div"));
                        if (showCommentbtn.Count > 0)
                        {
                            showCommentbtn[0].Click();
                            demnguoc(5, index, "Load thẻ comment");                         
                            var allconment = driver.FindElements(By.TagName("ytm-comment-thread-renderer"));
                            Random rnd = new Random();
                            var nd = "i love your video.";
                            if (allconment.Count>0)
                            {
                                int indexcoment = rnd.Next(0, allconment.Count);
                                nd = allconment[indexcoment].FindElement(By.ClassName("comment-text")).Text;
                                while (checkEmoji(nd))
                                {
                                    indexcoment = rnd.Next(0, allconment.Count);
                                    nd = allconment[indexcoment].FindElement(By.ClassName("comment-text")).Text;
                                    break;
                                }
                            }
                            stri = nd;
                            demnguoc(5, index, "Click ô commmnet - "+ nd);
                            var b = driver.FindElements(By.XPath("//*[@id='app']/panel-container/ytm-engagement-panel/ytm-engagement-panel-section-list-renderer/div/div/div[2]/ytm-section-list-renderer/lazy-list/ytm-item-section-renderer/ytm-comments-header-renderer/ytm-comment-simplebox-renderer/div"));
                            b[0].Click();
                            demnguoc(5, index, "Nhập comment");
                            var c = driver.FindElement(By.XPath("//*[@id='app']/panel-container/ytm-engagement-panel/ytm-engagement-panel-section-list-renderer/div/div/div[2]/ytm-section-list-renderer/lazy-list/ytm-item-section-renderer/ytm-comments-header-renderer/ytm-comment-simplebox-renderer/div/textarea"));
                            c.SendKeys(nd);

                            demnguoc(5, index, "Send commmnet");
                            var commment = driver.FindElement(By.XPath("//*[@id='app']/panel-container/ytm-engagement-panel/ytm-engagement-panel-section-list-renderer/div/div/div[2]/ytm-section-list-renderer/lazy-list/ytm-item-section-renderer/ytm-comments-header-renderer/ytm-comment-simplebox-renderer/div/div/ytm-button-renderer[2]/button/yt-touch-feedback-shape/div/div[2]"));
                            commment.Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,400)");
                            demnguoc(2, index, "Scoll page");
                        }
                        time = time + 1;
                    }

                    demnguoc(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.SwitchTo().Window(tabs[0]);
                    demnguoc(3, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));

                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        j++;
                        continue;
                    }

                    demnguoc(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));
                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguoc(3, index, "Chuyển nhiệm vụ");
                }
            }
            catch (Exception ex)
            {
                //var tabs = driver.WindowHandles;
                //driver.SwitchTo().Window(tabs[1]);
                //driver.Close();
                //driver.SwitchTo().Window(tabs[0]);
                //driver.Close();
                demnguoc(10, index, ex.Message);
                strError = strError + ";" + stri;
                System.IO.File.WriteAllText("error.json", strError);
                //await commentTikTok(index);
            }

            return Task.CompletedTask;
        }
        private Task ViewYoutube(int index, IWebDriver driver)
        {

            try
            {
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguocView(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguocView(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguocView(3, index, "Chọn loại nhiệm vụ");
                //*[@id="select-feature"]/option[34]
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[34]"));
                query.Click();
                demnguocView(3, index, "Lấy nhiệm vụ");

                demnguocView(50, index, "Nhập capchar");

                for (int i = 0; i < 50; i++)
                {
                    if (i % 2 == 0)
                    {
                        query = driver.FindElement(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    }
                    else
                    {
                        query = driver.FindElement(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                    }

                    query.Click();
                    demnguocView(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    var timeseen = driver.FindElement(By.XPath("//*[@id=\"full-time\"]")).Text;
                    int seenTime = int.Parse(timeseen) + 2;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguocView(1, index, "Chuyển tab Youtube ");

                    demnguocView(seenTime, index, "Đang xem Youtube ");
                  
                    demnguocView(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.SwitchTo().Window(tabs[0]);
                    demnguocView(3, index, "Chuyển tab chính");
                    var xuthem = "";
                    if (i % 2 == 0)
                    {
                        query = driver.FindElement(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));
                        xuthem = driver.FindElement(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[2]/div/span[2]")).Text;
                    }
                    else
                    {
                        query = driver.FindElement(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                        xuthem = driver.FindElement(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[2]/div/span[2]")).Text;
                    }

                    query.Click();
                    demnguocView(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));
                    dataGrid.Rows[index].Cells[4].Value = xuthem;
                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguocView(3, index, "Chuyển nhiệm vụ");
                }
            }
            catch (Exception ex)
            {
                var tabs = driver.WindowHandles;
                driver.SwitchTo().Window(tabs[1]);
                driver.Close();
                driver.SwitchTo().Window(tabs[0]);
                driver.Close();
                demnguoc(10, index, ex.Message);
                //await commentTikTok(index);
            }

            return Task.CompletedTask;
        }
        #endregion
        #region Facebook
        private Task LikeFacebook(int index, IWebDriver driver)
        {
            try
            {
                demnguoc(5, index, "Click chọn Menu");
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguoc(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();                  
                demnguoc(3, index, "Lấy nhiệm vụ");
                int i = 0;
                while (i < int.Parse(txtMaxLikeFace.Text))
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[3]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[3]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[3]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[3]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGrid.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        continue;
                    }


                    demnguoc(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguoc(15, index, "Chuyển tab Facebook");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {

                        var likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[4]"));
                        var whatbtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[1]/div/div/div[2]"));
                       
                        if (likebtn.Count > 0 && (whatbtn[0].Text == "WhatsApp" || whatbtn[0].Text == "Learn More"))
                        {
                            likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[3]"));
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;

                        }
                        else
                        {
                            likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[2]"));
                            if (likebtn.Count > 0)
                            {                              
                                likebtn[0].Click();
                                checkClick = true;
                                listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                i++;

                            }
                            else
                            {
                                likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[3]/div[4]/div[1]/div/div[1]"));
                                if (likebtn.Count > 0)
                                {
                                    likebtn[0].Click();
                                    checkClick = true;
                                    listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                    i++;
                                }
                                else
                                {
                                    break;
                                }
                            
                                //likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[6]/div[1]"));
                                //if (likebtn.Count > 0)
                                //{
                                //    likebtn[0].Click();
                                //    checkClick = true;
                                //    listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                //    i++;
                                //}
                                //else
                                //{
                                
                                //    likebtn = driver.FindElements(By.XPath("/html/body/div[2]/div/div[2]/div[7]/div[1]/div/ button/span[1]"));
                                //    if (likebtn.Count > 0)
                                //    {
                                //        likebtn[0].Click();
                                //        checkClick = true;
                                //        listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                //        i++;
                                //    }
                                //}
                            }
                        }
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                        demnguoc(2, index, "Scoll page");
                        time = time + 1;
                        
                    }
                    demnguoc(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    demnguoc(6, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[3]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[3]/div[2]/div/div[1]/a/img"));
                    }

                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        continue;
                    }

                    demnguoc(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));

                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguoc(3, index, "Chuyển nhiệm vụ");
                }

            }
            catch (Exception ex)
            {
                demnguoc(10, index, ex.Message);
            }

            return Task.CompletedTask;
        }
        private Task FollowFacebookPage(int index, IWebDriver driver)
        {

            try
            {
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguoc(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguoc(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguoc(3, index, "Chọn loại nhiệm vụ");
                //*[@id="select-feature"]/option[33]
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[4]"));
                query.Click();
                demnguoc(3, index, "Lấy nhiệm vụ");
                int i = 0;
                while(i < int.Parse(TxtMaxFollowFace.Text))
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGrid.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        continue;
                    }
                    demnguoc(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguoc(15, index, "Chuyển tab FaceBook");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {
                        
                        var likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[4]"));
                        if (likebtn.Count > 0)
                        {
                            likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[3]"));
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;

                        }
                        else
                        {
                             likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[4]/div[2]"));
                            if (likebtn.Count > 0)
                            {
                                likebtn[0].Click();
                                checkClick = true;
                                listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                i++;
                            }
                            else
                            {
                                likebtn = driver.FindElements(By.XPath("//*[@id=\"screen-root\"]/div/div[2]/div[3]/div[4]/div[1]/div/div[1]"));
                                if (likebtn.Count > 0)
                                {
                                    likebtn[0].Click();
                                    checkClick = true;
                                    listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                    i++;
                                }
                            }
                           
                        }
                        time = time + 1;                       
                    }
                    demnguoc(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    demnguoc(3, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));

                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        continue;
                    }

                    demnguoc(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));
                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguoc(3, index, "Chuyển nhiệm vụ");
                }
            }
            catch (Exception ex)
            {
                //var tabs = driver.WindowHandles;
                //driver.SwitchTo().Window(tabs[1]);
                //driver.Close();
                //driver.SwitchTo().Window(tabs[0]);
                //driver.Close();
                demnguoc(10, index, ex.Message);
                //await commentTikTok(index);
            }

            return Task.CompletedTask;
        }
        #endregion
        #region Tiktok
        private Task LikeTikTok(int index, IWebDriver driver)
        {
            try
            {
                demnguocView(5, index, "Click chọn Menu");
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguocView(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguocView(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguocView(3, index, "Chọn loại nhiệm vụ");
                //*[@id="select-feature"]/option[33]
                //*[@id="select-feature"]/option[22]
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[22]"));
                query.Click();
                demnguocView(3, index, "Lấy nhiệm vụ");
                int i = 0;              
                while (i < int.Parse(txtLikeTiktok.Text))
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGridViewControl.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        continue;
                    }


                    demnguocView(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguocView(15, index, "Chuyển tab Tik Tok");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {

                        var likebtn = driver.FindElements(By.XPath("/html/body/div[2]/div[3]/div[2]/div[1]/div[2]/div/div[1]/div[1]/div[4]/button[1]/span"));                     
                        if (likebtn.Count > 0)
                        {                           
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                            demnguocView(2, index, "Scoll page");
                        }
                      
                        time = time + 1;

                    }
                    demnguocView(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    demnguocView(6, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                    }

                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        continue;
                    }

                    demnguocView(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));

                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguocView(3, index, "Chuyển nhiệm vụ");
                }

            }
            catch (Exception ex)
            {
                demnguoc(10, index, ex.Message);
            }

            return Task.CompletedTask;
        }
        private Task FollowTikTok(int index, IWebDriver driver)
        {
            try
            {
                demnguocView(5, index, "Click chọn Menu");
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguocView(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguocView(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguocView(3, index, "Chọn loại nhiệm vụ");
                //*[@id="select-feature"]/option[33]
                //*[@id="select-feature"]/option[22]
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[21]"));
                query.Click();
                demnguocView(3, index, "Lấy nhiệm vụ");
                int i = 0;
                while (i < int.Parse(txtLikeTiktok.Text))
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGridViewControl.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        continue;
                    }


                    demnguocView(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguocView(15, index, "Chuyển tab Tik Tok");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {

                        var likebtn = driver.FindElements(By.XPath("/html/body/div[2]/div[3]/div[2]/div/div[1]/div[1]/div[2]/div/div[1]/button"));
                        if (likebtn.Count > 0)
                        {
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                            demnguocView(2, index, "Scoll page");
                        }

                        time = time + 1;

                    }
                    demnguocView(3, index, "Chuẩn bị chuyển về tab chính");
                   // driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    demnguocView(6, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                    }

                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        continue;
                    }

                    demnguocView(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));

                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguocView(3, index, "Chuyển nhiệm vụ");
                }

            }
            catch (Exception ex)
            {
                demnguoc(10, index, ex.Message);
            }

            return Task.CompletedTask;
        }

        private Task LikeSoundCloud(int index, IWebDriver driver)
        {
            try
            {
                demnguocView(5, index, "Click chọn Menu");
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguocView(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguocView(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguocView(3, index, "Chọn loại nhiệm vụ");               
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[18]"));
                query.Click();
                demnguocView(3, index, "Lấy nhiệm vụ");
                int i = 0;
                while (i < int.Parse(txtLikeTiktok.Text))
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGridViewControl.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        continue;
                    }


                    demnguocView(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguocView(15, index, "Chuyển tab Tik Tok");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {

                        var likebtn = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/div[3]/div[1]/div/div[1]/div/div/div[2]/div/div[1]/button[1]"));
                        if (likebtn.Count > 0)
                        {
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            likebtn = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/div[3]/div[1]/div/div[1]/div/div/div/div/div/button[1]"));
                            if (likebtn.Count > 0)
                            {
                                likebtn[0].Click();
                                checkClick = true;
                                listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                i++;
                            }
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                            demnguocView(2, index, "Scoll page");
                        }

                        time = time + 1;

                    }
                    demnguocView(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    demnguocView(6, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                    }

                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        continue;
                    }

                    demnguocView(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));

                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguocView(3, index, "Chuyển nhiệm vụ");
                }

            }
            catch (Exception ex)
            {
                demnguoc(10, index, ex.Message);
            }

            return Task.CompletedTask;
        }
        private Task RepostSoundCloud(int index, IWebDriver driver)
        {
            try
            {
                demnguocView(5, index, "Click chọn Menu");
                var query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu\"]/div"));
                query.Click();
                demnguocView(5, index, "Click Menu");
                query = driver.FindElement(By.XPath("//*[@id=\"hamburger-menu-sidebar\"]/li[3]"));
                query.Click();
                demnguocView(5, index, "Chọn loại kiếm tiền");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]"));
                query.Click();
                demnguocView(3, index, "Chọn loại nhiệm vụ");
                query = driver.FindElement(By.XPath("//*[@id=\"select-feature\"]/option[18]"));
                query.Click();
                demnguocView(3, index, "Lấy nhiệm vụ");
                int i = 0;
                while (i < int.Parse(txtLikeTiktok.Text))
                {

                    var querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[3]/div/div/a"));
                    var xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/span"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[3]/div/div/a"));
                        xuthem = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/span"));
                    }
                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                        dataGridViewControl.Rows[index].Cells[4].Value = xuthem[0].Text;
                    }
                    else
                    {
                        continue;
                    }
                    demnguocView(3, index, "Click nhiệm vụ");
                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);
                    demnguocView(15, index, "Chuyển tab Tik Tok");

                    bool checkClick = false;
                    int time = 0;
                    while (!checkClick && time < 4)
                    {

                        var likebtn = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/div[3]/div[1]/div/div[1]/div/div/div[2]/div/div/button[2]"));
                        if (likebtn.Count > 0)
                        {
                            likebtn[0].Click();
                            checkClick = true;
                            listAccount[index].totalJob = listAccount[index].totalJob + 1;
                            i++;
                        }
                        else
                        {
                            likebtn = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/div[3]/div[1]/div/div[1]/div/div/div/div/div/button[2]"));
                            if (likebtn.Count > 0)
                            {
                                likebtn[0].Click();
                                checkClick = true;
                                listAccount[index].totalJob = listAccount[index].totalJob + 1;
                                i++;
                            }
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,200)");
                            demnguocView(2, index, "Scoll page");
                        }

                        time = time + 1;

                    }
                    demnguocView(3, index, "Chuẩn bị chuyển về tab chính");
                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    demnguocView(6, index, "Chuyển tab chính");


                    querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[1]/div/div[1]/a/img"));

                    if (querys.Count == 0)
                    {
                        querys = driver.FindElements(By.XPath("/html/body/div[6]/div/div[1]/div[2]/div[2]/div[4]/div[1]/div[2]/div[2]/div/div[1]/a/img"));
                    }

                    if (querys.Count > 0)
                    {
                        querys[0].Click();
                    }
                    else
                    {
                        continue;
                    }

                    demnguocView(3, index, "Click nhận credit");

                    var sxu = driver.FindElement(By.XPath("//*[@id=\"top-header-credits-credits-total\"]"));

                    dataGrid.Rows[index].Cells[5].Value = sxu.Text;
                    dataGrid.Rows[index].Cells[6].Value = listAccount[index].totalJob;
                    //*[@id="top-header-credits-credits-total"]
                    demnguocView(3, index, "Chuyển nhiệm vụ");
                }

            }
            catch (Exception ex)
            {
                demnguoc(10, index, ex.Message);
            }

            return Task.CompletedTask;
        }
        #endregion
        public void demnguoc(int time, int rowIndex, string job)
        {           
            try
            {
                while (time > 0)
                {
                    this.dataGrid.Invoke(new Action(() =>
                    {
                        this.dataGrid.Rows[rowIndex].Cells[7].Value = string.Format("{0} - {1} {2} s...", job, "Vui lòng chờ", time);                      
                    }));
                    --time;
                    Thread.Sleep(1000);
                }               
                if (!this.threadDictionary.ContainsKey(this.dataGrid[1, rowIndex].Value.ToString()))
                {
                    this.dataGrid.Invoke(new Action(() =>
                    {
                        this.dataGrid[7, rowIndex].Value = (object)"Đã dừng";
                    }));
                }
            }
            catch(Exception ex)
            {             
                throw ex;
            }
           
        }
        public void demnguocView(int time, int rowIndex, string job)
        {
            while (time > 0)
            {
                this.dataGridViewControl[7, rowIndex].Value = (object)string.Format("{0} - {1} {2} s...", job, (object)"Vui lòng chờ", (object)time);
                --time;
                Thread.Sleep(1000);
                if (!this.threadDictionary.ContainsKey(this.dataGrid[1, rowIndex].Value.ToString()))
                {
                    this.dataGridViewControl[7, rowIndex].Value = (object)"Đã dừng";
                    break;
                }
            }
        }
       
        private void test()
        {
            var driverService = ChromeDriverService.CreateDefaultService();

            driverService.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
            options.AddArgument(@"--start-maximized");
           

            var driver = new ChromeDriver(driverService, options);
            // options.AddArguments("--incognito");


            driver.Navigate().GoToUrl(@"D:\ToolAuto\AutoLike4Like\AutoLike4Like\bin\Debug\pagetest\Free Credits - Youtube Views.html");
            Screenshot TakeScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
            TakeScreenshot.SaveAsFile(@"D:\ToolAuto\AutoLike4Like\AutoLike4Like\bin\Debug\pagetest\test1." + System.Drawing.Imaging.ImageFormat.Png);
            Screenshot screenshot = driver.GetScreenshot();
            driver.SwitchTo().Frame(driver.FindElement(By.TagName("iframe")));
            var  captchaImage = driver.FindElements(By.XPath("//*[@id=\"result\"]/div/a"));

           
            TakeScreenshot.SaveAsFile(@"D:\ToolAuto\AutoLike4Like\AutoLike4Like\bin\Debug\pagetest\test2." + System.Drawing.Imaging.ImageFormat.Png);

        }

    }
}
