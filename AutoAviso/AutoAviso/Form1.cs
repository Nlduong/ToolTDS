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
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        private Dictionary<string, Thread> threadDictionarySEO = new Dictionary<string, Thread>();
        private Dictionary<string, Thread> threadDictionaryPro = new Dictionary<string, Thread>();

        IWebDriver driverSEO;
        IWebDriver driverPro;
        List<Account> listAccount = new List<Account>();
        List<Account> listAccountSEO = new List<Account>();
        List<Account> listAccountPro = new List<Account>();
        public Form1()
        {
            InitializeComponent();
            StreamReader r = new StreamReader("Account.json");
            string jsonString = r.ReadToEnd();
            listAccount = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            StreamReader rSeo = new StreamReader("AccountSEO.json");
            jsonString = rSeo.ReadToEnd();
            listAccountSEO = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            StreamReader rPro = new StreamReader("AccountProfit.json");
            jsonString = rPro.ReadToEnd();
            listAccountPro = JsonConvert.DeserializeObject<List<Account>>(jsonString);

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

            DataTable dataTablePro = new DataTable();
            dataTablePro.Columns.Add("#", typeof(int));
            dataTablePro.Columns.Add("ID Google", typeof(string));
            dataTablePro.Columns.Add("Job đang làm", typeof(string));
            dataTablePro.Columns.Add("Credits Thêm", typeof(string));
            dataTablePro.Columns.Add("Tổng Credits", typeof(string));
            dataTablePro.Columns.Add("Tổng Job", typeof(string));
            dataTablePro.Columns.Add("Trạng Thái", typeof(string));
            index = 0;
            foreach (var entry in listAccountPro)
            {
                index++;
                dataTablePro.Rows.Add(index, entry.id, "", "", "", "", "");

            }

            dataGridProfitcent.DataSource = dataTablePro;
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
            this.dataGridProfitcent.Columns.Add(buttonColumn);
            this.dataGridProfitcent.Columns[6].Width = 300;
        }
        private Task viewYoutubeSEO(int rowIndex, IWebDriver driver)
        {
            var checkRuvideos = false;
            var querys = driver.FindElements(By.XPath("//*[@id=\"echoall\"]/table/tbody/tr"));
            var xu = driver.FindElements(By.XPath("//*[@id=\"ajax_load\"]/div/div[3]/span"));
            int maxJob = RamdomTime(40, 55);
            int indexClick = 0;
            int currentJob = 0;


            var ramdoclick = RamdomTime(7, 9);


            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,800)");
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
                        if (style.IndexOf("display:") > -1)
                        {
                            continue;
                        }

                        var linkClick = querys[i].FindElements(By.TagName("a"));
                        if (linkClick.Count > 2)
                        {
                            var tabhandel = driver.WindowHandles;

                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", linkClick[2]);
                        }
                        indexClick = indexClick + 1;
                        if (i == (querys.Count - 4))
                        {
                            ramdoclick = indexClick;
                        }
                        driverSEO = driver;
                        if (checkRuvideos == true)
                        {
                            break;
                        }
                        if (indexClick == ramdoclick)
                        {
                            demnguocSEO(RamdomTime(15, 20), rowIndex, "Load Video");
                            var tabhandel = driver.WindowHandles;

                            var timeLarge = 0;

                            for (int j = 1; j < tabhandel.Count; j++)
                            {
                                driver.SwitchTo().Window(tabhandel[j]);

                                var time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                                int second = int.Parse(time[0].Text);
                                if (second > timeLarge)
                                {
                                    timeLarge = second;
                                }
                                driver.SwitchTo().Frame(0);


                                var unavailable = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span"));
                                var privateVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span/span"));
                                //*[@id="movie_player"]/div[15]/div[1]/div[2]/div[1]/span
                                if (unavailable.Count > 0 || privateVideo.Count > 0)
                                {
                                    //if (unavailable[0].Text == "Video unavailable" || privateVideo[0].Text =="This video is age-restricted and only available on YouTube. ")
                                    //{
                                    //if (tabhandel.Count > 1)
                                    //{
                                    //    driver.SwitchTo().Window(tabhandel[j]);
                                    //    driver.Close();
                                    //}
                                    continue;
                                    //}
                                }

                                var playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                                if (playVideo.Count > 0)
                                {
                                    playVideo[0].Click();
                                    listAccountSEO[rowIndex].totalJob = listAccountSEO[rowIndex].totalJob + 1;
                                }
                                var playRuVideo = driver.FindElements(By.XPath("//*[@id=\"app\"]/div/div/div[4]/div[2]/div[1]/div[2]/button"));
                                if (playRuVideo.Count > 0)
                                {
                                    checkRuvideos = true;
                                    playRuVideo[0].Click();
                                    listAccountSEO[rowIndex].totalJob = listAccountSEO[rowIndex].totalJob + 1;
                                }
                                demnguocSEO(RamdomTime(2, 3), rowIndex, "Play Video");
                            }


                            if (timeLarge > 0)
                            {
                                demnguocSEO(RamdomTime(timeLarge + 1, timeLarge + 3), rowIndex, "Xem View Video");
                            }
                            else
                            {
                                demnguocSEO(60, rowIndex, "Xem View Video");
                            }
                            for (int j = 1; j < tabhandel.Count; j++)
                            {
                                demnguocSEO(3, rowIndex, "Đóng tab Video");
                                if (tabhandel.Count > 1)
                                {
                                    driver.SwitchTo().Window(tabhandel[j]);
                                    var title = driver.Title.ToString();
                                    int myInt;
                                    var Result = int.TryParse(title, out myInt);
                                    if (!Result)
                                    {
                                        driver.Close();
                                    }
                                    else
                                    {
                                        demnguocSEO(RamdomTime(myInt + 1, myInt + 3), rowIndex, "Xem View Video");
                                        driver.Close();
                                    }
                                }
                            }
                            if (tabhandel.Count == 1)
                            {
                                continue;
                            }
                            demnguocSEO(1, rowIndex, "Chuyển về tab chính");
                            driver.SwitchTo().Window(tabhandel[0]);
                            demnguocSEO(5, rowIndex, "Nhận tiền");

                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,500)");
                            currentJob = currentJob + 1;
                            //if ((currentJob % 5) == 0)
                            //{

                            //}
                            //if (currentJob == maxJob)
                            //{
                            //    demnguocSEO(8, rowIndex, "Đã đủ job hôm nay" + maxJob + ".");
                            //    break;
                            //}
                            demnguocSEO(8, rowIndex, "Chuyển nv kế tiếp");
                            indexClick = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                demnguocSEO(8, rowIndex, ex.Message);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);
                //var tabclose = driver.WindowHandles;
                //for (int i = 0; i < tabclose.Count; i++)
                //{
                //    driver.SwitchTo().Window(tabclose[i]);
                //    driver.Close();
                //}
            }

            return Task.CompletedTask;
        }

        private Task viewRutubeSEO(int rowIndex, IWebDriver driver)
        {
            var querys = driver.FindElements(By.XPath("//*[@id=\"echoall\"]/table/tbody/tr"));
            var xu = driver.FindElements(By.XPath("//*[@id=\"ajax_load\"]/div/div[3]/span"));

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
                        if (style.IndexOf("display:") > -1)
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
                        if (tabhandel.Count == 1)
                        {
                            continue;
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguocSEO(RamdomTime(10, 14), rowIndex, "Load Video");

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
            return Task.CompletedTask;
        }
        private Task LikeYoutube(int rowIndex, IWebDriver driver)
        {
            var checkRuvideos = false;
            var querys = driver.FindElements(By.XPath("//*[@id=\"echoall\"]/table/tbody/tr"));
            var xu = driver.FindElements(By.XPath("//*[@id=\"ajax_load\"]/div/div[3]/span"));
            int maxJob = RamdomTime(40, 55);
            int indexClick = 0;
            int currentJob = 0;
            var ramdoclick = 1;

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,800)");
            try
            {
                if (querys.Count > 0)
                {
                    for (int i = 1; i < querys.Count; i++)
                    {
                        dataGridSEO.Rows[rowIndex].Cells[3].Value = "Like Youtube";
                        dataGridSEO.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridSEO.Rows[rowIndex].Cells[6].Value = listAccountSEO[rowIndex].totalJob;
                        var style = querys[i].GetAttribute("style");
                        if (style.IndexOf("display:") > -1)
                        {
                            continue;
                        }

                        var linkClick = querys[i].FindElements(By.TagName("a"));
                        if (linkClick.Count > 2)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", linkClick[1]);

                        }
                        demnguocSEO(RamdomTime(1, 2), rowIndex, "Tìm Link");
                        var btnStart = querys[i].FindElements(By.ClassName("start_link_youtube"));

                        if (btnStart.Count > 0)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", btnStart[0]);
                            // demnguocSEO(RamdomTime(6, 7), rowIndex, "Change tab");
                        }



                        var tabhandel = driver.WindowHandles;
                        if (tabhandel.Count == 1)
                        {
                            continue;
                        }

                        var timeLarge = 0;
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguocSEO(RamdomTime(15, 20), rowIndex, "Load Video");

                        var btnLike = driver.FindElements(By.XPath("/html /body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[2]/div[2]/div/div/ytd-menu-renderer/div[1]/ytd-segmented-like-dislike-button-renderer/div[1]/ytd-toggle-button-renderer/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]"));
                        if (btnLike.Count > 0)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", btnLike[0]);
                        }

                        demnguocSEO(3, rowIndex, "Đóng tab Video");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            var title = driver.Title.ToString();
                            int myInt;
                            var Result = int.TryParse(title, out myInt);
                            if (!Result)
                            {
                                driver.Close();
                            }
                            else
                            {
                                demnguocSEO(RamdomTime(myInt + 1, myInt + 3), rowIndex, "Xem View Video");
                                driver.Close();
                            }
                        }

                        if (tabhandel.Count == 1)
                        {
                            continue;
                        }
                        demnguocSEO(1, rowIndex, "Chuyển về tab chính");
                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguocSEO(5, rowIndex, "Nhận tiền");
                        var btnResult = querys[i].FindElements(By.ClassName("status_link_youtube"));

                        if (btnResult.Count > 0)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", btnResult[0]);
                        }
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,50)");
                        currentJob = currentJob + 1;
                        listAccountSEO[rowIndex].totalJob = listAccountSEO[rowIndex].totalJob + 1;
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
                        var time = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/span"));

                        demnguoc(RamdomTime(3, 4), rowIndex, "Check time");
                        var button = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/a"));
                        if (button.Count() > 0)
                        {
                            executor.ExecuteScript("arguments[0].click();", button[0]);
                            listAccount[rowIndex].totalJob = listAccount[rowIndex].totalJob + 1;
                        }
                        else
                        {

                            for (int k = 0; k < 5; k++)
                            {
                                time = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/span"));
                                if (time.Count > 0 && time[0].Text != "")
                                {
                                    demnguoc(RamdomTime(int.Parse(time[0].Text), int.Parse(time[0].Text) + 5), rowIndex, "Change tab suffiz");
                                }
                                else
                                {
                                    break;
                                }
                            }

                            button = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/a"));
                            if (button.Count() > 0)
                            {
                                executor.ExecuteScript("arguments[0].click();", button[0]);
                                listAccount[rowIndex].totalJob = listAccount[rowIndex].totalJob + 1;
                            }
                            else
                            {
                                button = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td[2]/a"));
                            }
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
                var tabclose = driver.WindowHandles;
                for (int i = 0; i < tabclose.Count; i++)
                {
                    driver.SwitchTo().Window(tabclose[i]);
                    driver.Close();
                }
            }

            return Task.CompletedTask;

        }
        private Task viewYoutubeAiso(int rowIndex)
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
            driver.Navigate().GoToUrl("https://aviso.bz/work-youtube");
            demnguoc(2, rowIndex, "Duyệt nhiệm vụ");
            var querys = driver.FindElements(By.XPath("/html/body/table/tbody/tr[3]/td[2]/div"));
            int currentJob = 0;
            try
            {
                if (querys.Count > 1)
                {
                    for (int i = 1; i < querys.Count; i++)
                    {
                        var link = querys[i].FindElements(By.TagName("span"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", link[0]);

                        demnguoc(RamdomTime(5, 7), rowIndex, "Click link");
                        link = querys[i].FindElements(By.TagName("span"));
                        executor.ExecuteScript("arguments[0].click();", link[0]);
                        var xu = driver.FindElements(By.XPath("//*[@id=\"new-money-ballans\"]"));
                        //dataGrid.Rows[rowIndex].Cells[4].Value = xuthem;
                        dataGridAviso.Rows[rowIndex].Cells[3].Value = "View Video";
                        dataGridAviso.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridAviso.Rows[rowIndex].Cells[6].Value = listAccount[rowIndex].totalJob;
                        var tabhandel = driver.WindowHandles;
                        if (tabhandel.Count == 1)
                        {
                            continue;
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguoc(RamdomTime(7, 10), rowIndex, "Load Video");
                        var time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                        int second = int.Parse(time[0].Text);
                        driver.SwitchTo().Frame(0);

                        var unavailable = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span"));
                        var privateVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span/span"));

                        if (unavailable.Count > 0 || privateVideo.Count > 0)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                            continue;
                        }
                        var playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                        if (playVideo.Count > 0)
                        {
                            playVideo[0].Click();
                            listAccount[rowIndex].totalJob = listAccount[rowIndex].totalJob + 1;
                        }
                        demnguoc(RamdomTime(2, 3), rowIndex, "Play Video");

                        if (time.Count > 0)
                        {
                            demnguoc(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                        }
                        else
                        {
                            demnguoc(60, rowIndex, "Xem View Video");
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));

                        if (time.Count > 0 && time[0].Text != "")
                        {
                            second = int.Parse(time[0].Text);
                            driver.SwitchTo().Frame(0);

                            playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                            if (playVideo.Count > 0)
                            {
                                playVideo[0].Click();
                            }
                            demnguoc(RamdomTime(2, 3), rowIndex, "Play Video");
                            demnguoc(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                        }

                        demnguoc(5, rowIndex, "Đóng tab Video");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                            if (time.Count > 0 && time[0].Text != "")
                            {
                                second = int.Parse(time[0].Text);
                                demnguoc(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                            }
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }
                        demnguoc(1, rowIndex, "Chuyển về tab chính");

                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguoc(5, rowIndex, "Nhận tiền");
                        currentJob = currentJob + 1;
                        if ((currentJob % 5) == 0)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,350)");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                demnguoc(8, rowIndex, ex.Message);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);
                var tabclose = driver.WindowHandles;
                for (int i = 0; i < tabclose.Count; i++)
                {
                    driver.SwitchTo().Window(tabclose[i]);
                    driver.Close();
                }
            }

            return Task.CompletedTask;
        }

        private Task viewYoutubePro(int rowIndex, IWebDriver driver)
        {
            var querys = driver.FindElements(By.XPath("//*[@id=\"work-youtube\"]/div"));

            int currentJob = 0;
            try
            {
                if (querys.Count > 1)
                {
                    for (int i = 0; i < querys.Count; i++)
                    {
                        var link = querys[i].FindElements(By.TagName("span"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", link[0]);
                        demnguocPro(RamdomTime(5, 7), rowIndex, "Click link");
                        link = querys[i].FindElements(By.TagName("span"));
                        executor.ExecuteScript("arguments[0].click();", link[0]);
                        var xu = driver.FindElements(By.XPath("//*[@id=\"new-money-ballans\"]/font/font"));
                        //dataGrid.Rows[rowIndex].Cells[4].Value = xuthem;
                        dataGridProfitcent.Rows[rowIndex].Cells[3].Value = "View Ru Video";
                        dataGridProfitcent.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridProfitcent.Rows[rowIndex].Cells[6].Value = listAccountPro[rowIndex].totalJob;
                        var tabhandel = driver.WindowHandles;
                        if (tabhandel.Count == 1)
                        {
                            continue;
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguocPro(RamdomTime(10, 14), rowIndex, "Load Video");

                        var time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                        int second = int.Parse(time[0].Text);
                        if (second >= 20)
                        {
                            while (second > 0)
                            {
                                
                                driver.SwitchTo().Window(tabhandel[1]);
                                time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                                var second1 = time[0].Text == "" ? 0 : int.Parse(time[0].Text);
                                demnguocPro(RamdomTime(3,4), rowIndex, "Check Time");
                                var time1 = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                                second = time1[0].Text == "" ? 0 : int.Parse(time1[0].Text);
                               
                                if (second == second1)
                                {
                                    driver.SwitchTo().Frame(0);
                                    var index = 27;
                                    var checkRelay = false;
                                    while(index <35)
                                    {
                                        var replayVideo = driver.FindElements(By.XPath("/html/body/div[1]/div/div["+ index + "]/div[2]/div[1]/button"));
                                        if (replayVideo.Count > 0)
                                        {
                                            checkRelay = true;
                                            replayVideo[0].Click();
                                            demnguocPro(RamdomTime(2, 3), rowIndex, "Play Video");
                                            break;
                                        }
                                        index++;
                                    }
                                  
                                    if(checkRelay==false)
                                    {
                                            var playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                                            if (playVideo.Count > 0)
                                            {
                                                playVideo[0].Click();
                                                listAccountPro[rowIndex].totalJob = listAccountPro[rowIndex].totalJob + 1;
                                            }
                                            demnguocPro(RamdomTime(2, 3), rowIndex, "Play Video");
                                      
                                    }  
                                }
                                demnguocPro(RamdomTime(10, 15), rowIndex, "Xem View Video");
                                if (second == 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            driver.SwitchTo().Frame(0);
                            var unavailable = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span"));
                            var privateVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[15]/div[1]/div[2]/div[1]/span/span"));

                            if (unavailable.Count > 0 || privateVideo.Count > 0)
                            {
                                driver.SwitchTo().Window(tabhandel[1]);
                                driver.Close();
                                continue;
                            }
                            var playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                            if (playVideo.Count > 0)
                            {
                                playVideo[0].Click();
                                listAccountPro[rowIndex].totalJob = listAccountPro[rowIndex].totalJob + 1;
                            }
                            demnguocPro(RamdomTime(2, 3), rowIndex, "Play Video");

                            if (time.Count > 0)
                            {
                                demnguocPro(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                            }
                            else
                            {
                                demnguocPro(60, rowIndex, "Xem View Video");
                            }
                            driver.SwitchTo().Window(tabhandel[1]);
                            time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));

                            if (time.Count > 0 && time[0].Text != "")
                            {
                                second = int.Parse(time[0].Text);
                                driver.SwitchTo().Frame(0);

                                playVideo = driver.FindElements(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));
                                if (playVideo.Count > 0)
                                {
                                    playVideo[0].Click();
                                }
                                demnguocPro(RamdomTime(2, 3), rowIndex, "Play Video");
                                demnguocPro(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                            }
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));

                        demnguocPro(5, rowIndex, "Đóng tab Video");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            time = driver.FindElements(By.XPath("//*[@id=\"tmr\"]"));
                            if (time.Count > 0 && time[0].Text != "")
                            {
                                second = int.Parse(time[0].Text);
                                demnguocPro(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                            }
                            var button = driver.FindElements(By.XPath("/html/body/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/button"));

                            if (button.Count > 0)
                            {
                                executor.ExecuteScript("arguments[0].click();", button[0]);
                            }
                            demnguocPro(3, rowIndex, "Đóng tab Video");
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }
                        demnguocPro(1, rowIndex, "Chuyển về tab chính");

                        

                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguocPro(5, rowIndex, "Nhận tiền");
                        //currentJob = currentJob + 1;
                        //if ((currentJob % 5) == 0)
                        //{
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,55)");
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                demnguocPro(8, rowIndex, ex.Message);
                var tabclose = driver.WindowHandles;
                for (int i = 1; i < tabclose.Count; i++)
                {
                    driver.SwitchTo().Window(tabclose[i]);
                    driver.Close();
                }
                driver.SwitchTo().Window(tabclose[0]);
                driver.Navigate().Refresh();
                demnguocPro(8, rowIndex, "Load lại page");
                viewYoutubePro(rowIndex, driver);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);

            }

            return Task.CompletedTask;
        }

        private Task viewRutubePro(int rowIndex, IWebDriver driver)
        {
            var querys = driver.FindElements(By.XPath("//*[@id=\"work-rutube\"]/div"));
            try
            {
                if (querys.Count > 1)
                {
                    for (int i = 0; i < querys.Count; i++)
                    {
                        var link = querys[i].FindElements(By.TagName("span"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", link[0]);
                        demnguocPro(RamdomTime(5, 7), rowIndex, "Click link");
                        link = querys[i].FindElements(By.TagName("span"));
                        executor.ExecuteScript("arguments[0].click();", link[0]);
                        var xu = driver.FindElements(By.XPath("//*[@id=\"new-money-ballans\"]/font/font"));
                        //dataGrid.Rows[rowIndex].Cells[4].Value = xuthem;
                        dataGridProfitcent.Rows[rowIndex].Cells[3].Value = "View Rub Video";
                        dataGridProfitcent.Rows[rowIndex].Cells[5].Value = xu[0].Text;
                        dataGridProfitcent.Rows[rowIndex].Cells[6].Value = listAccountPro[rowIndex].totalJob;
                        var tabhandel = driver.WindowHandles;
                        if (tabhandel.Count == 1)
                        {
                            continue;
                        }
                        driver.SwitchTo().Window(tabhandel[1]);
                        demnguocPro(RamdomTime(15, 20), rowIndex, "Load Video");
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
                                demnguocPro(1, rowIndex, "Chuyển về tab chính");
                                driver.SwitchTo().Window(tabhandel[0]);
                                continue;
                            }
                        }

                        var playRuVideo = driver.FindElements(By.XPath("//*[@id=\"app\"]/div/div/div[4]/div[2]/div[1]/div[2]/button"));
                        if (playRuVideo.Count > 0)
                        {
                            playRuVideo[0].Click();
                        }

                        if (time.Count > 0)
                        {
                            demnguocPro(RamdomTime(second + 1, second + 3), rowIndex, "Xem View Video");
                        }
                        else
                        {
                            demnguocPro(60, rowIndex, "Xem View Video");
                        }


                        demnguocPro(7, rowIndex, "Đóng tab Video");
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }

                        demnguocPro(1, rowIndex, "Chuyển về tab chính");


                        driver.SwitchTo().Window(tabhandel[0]);
                        demnguocPro(5, rowIndex, "Nhận tiền");
                        //currentJob = currentJob + 1;
                        //if ((currentJob % 5) == 0)
                        //{
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,55)");
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                demnguocPro(8, rowIndex, ex.Message);
                var tabclose = driver.WindowHandles;
                for (int i = 1; i < tabclose.Count; i++)
                {
                    driver.SwitchTo().Window(tabclose[i]);
                    driver.Close();
                }
                driver.SwitchTo().Window(tabclose[0]);
                driver.Navigate().Refresh();
                demnguocPro(8, rowIndex, "Load lại page");
                viewYoutubePro(rowIndex, driver);
                strError = strError + ";" + ex.Message;
                System.IO.File.WriteAllText("error.json", strError);
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
                    if (!this.threadDictionarySEO.ContainsKey("SEO" + this.dataGridSEO[1, rowIndex].Value.ToString()))
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
        public void demnguocPro(int time, int rowIndex, string job)
        {
            try
            {
                while (time > 0)
                {
                    if (!this.threadDictionaryPro.ContainsKey("PRO" + this.dataGridProfitcent[1, rowIndex].Value.ToString()))
                    {
                        this.dataGridProfitcent.Invoke(new Action(() =>
                        {
                            this.dataGridProfitcent[7, rowIndex].Value = (object)"Đã dừng";
                        }));
                    }
                    this.dataGridProfitcent.Invoke(new Action(() =>
                    {
                        this.dataGridProfitcent.Rows[rowIndex].Cells[7].Value = string.Format("{0} - {1} {2} s...", job, "Vui lòng chờ", time);
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
                            if (chkViewYTBAviso.Checked)
                            {
                                await viewYoutubeAiso(e.RowIndex);
                            }
                            if (chkTrafficAviso.Checked)
                            {
                                await traffic(e.RowIndex);
                            }

                            // await LikeYoutube(e.RowIndex);

                            //  await viewYoutube(e.RowIndex);

                            i++;
                            dataGridAviso.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                            demnguoc(RamdomTime(200, 360), e.RowIndex, "Thời gian xoay vòng nhiệm vụ");
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
                        demnguocSEO(5, e.RowIndex, "Load page");
                        var query = driver.FindElement(By.XPath("//*[@id=\"logusername\"]"));
                        query.SendKeys(listAccountSEO[e.RowIndex].id);
                        demnguocSEO(3, e.RowIndex, "Nhập Username");
                        query = driver.FindElement(By.XPath("//*[@id=\"logpassword\"]"));
                        query.SendKeys(listAccountSEO[e.RowIndex].pass);
                        demnguocSEO(3, e.RowIndex, "Nhập Password");
                        // 
                        // query.Click();
                        demnguocSEO(5, e.RowIndex, "Đăng Nhập");


                        demnguocSEO(15, e.RowIndex, "Load page");
                        reloadPageTask(driver, e.RowIndex);
                        demnguocSEO(RamdomTime(10, 15), e.RowIndex, "Load page");
                        int oldNV = 0;
                        while (i < 15)
                        {
                            //var querys = driver.FindElements(By.XPath("/html/body/div[9]/div/div/table[2]/tbody/tr/td[2]/div[15]/div/div/div[2]/center/a)"));
                            //if (querys.Count() > 0)
                            //{
                            //    reloadPageTask(driver, e.RowIndex);
                            //}
                            if (i > 0)
                            {
                                driver = driverSEO;

                                demnguocSEO(RamdomTime(5, 10), e.RowIndex, "Load page");
                                var nv = RamdomTime(0, 2);
                                for (int k = 0; k < 10; k++)
                                {
                                    if (oldNV == nv)
                                    {
                                        nv = RamdomTime(0, 3);
                                    }
                                    else
                                    {
                                        oldNV = nv;
                                        break;
                                    }
                                }

                                if (nv == 3)
                                {
                                    driver.Navigate().GoToUrl("https://seo-fast.ru/work_youtube?rutube_video");
                                }
                                else if (nv == 0)
                                {
                                    driver.Navigate().GoToUrl("https://seo-fast.ru/work_youtube?youtube_expensive");
                                }
                                else if (nv == 1)
                                {
                                    driver.Navigate().GoToUrl("https://seo-fast.ru/work_youtube?youtube_video_simple");
                                }
                                else
                                {
                                    driver.Navigate().GoToUrl("https://seo-fast.ru/work_youtube?youtube_video_bonus");
                                }
                                var querys = driver.FindElements(By.XPath("//*[@id=\"echoall\"]/table/tbody/tr"));
                                if (querys.Count == 0)
                                {
                                    reloadPageTask(driver, e.RowIndex);
                                    demnguocSEO(RamdomTime(10, 15), e.RowIndex, "Load page");
                                    query = driver.FindElement(By.XPath("/html/body/div[9]/div/div/table[2]/tbody/tr/td[1]/div[1]/nav/div[3]/ul/li[1]/a[2]/div[9]"));
                                    query.Click();
                                    demnguocSEO(10, e.RowIndex, "Load page nv YouTube");
                                    querys = driver.FindElements(By.XPath("/html/body/div[10]/div/div/table[2]/tbody/tr/td[2]/div[15]/div/div/div[5]/a"));
                                    nv = RamdomTime(0, 2);
                                    oldNV = nv;
                                    if (chkViewRuVideo.Checked)
                                    {
                                        querys[6].Click();
                                        demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                        await viewRutubeSEO(e.RowIndex, driver);
                                    }
                                    if (chkLikeSeoVideo.Checked)
                                    {
                                        querys[4].Click();
                                        demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                        await LikeYoutube(e.RowIndex, driver);
                                    }
                                    if (chkViewYoutube.Checked)
                                    {
                                        querys[nv].Click();
                                        demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                        await viewYoutubeSEO(e.RowIndex, driver);

                                    }
                                }
                            }
                            else
                            {
                                query = driver.FindElement(By.XPath("/html/body/div[9]/div/div/table[2]/tbody/tr/td[1]/div[1]/nav/div[3]/ul/li[1]/a[2]/div[9]"));
                                query.Click();
                                demnguocSEO(10, e.RowIndex, "Load page nv YouTube");
                                var querys = driver.FindElements(By.XPath("/html/body/div[10]/div/div/table[2]/tbody/tr/td[2]/div[15]/div/div/div[5]/a"));
                                if (chkViewRuVideo.Checked)
                                {
                                    querys[6].Click();
                                    demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                    await viewRutubeSEO(e.RowIndex, driver);
                                }
                                if (chkLikeSeoVideo.Checked)
                                {
                                    querys[3].Click();
                                    demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                    await LikeYoutube(e.RowIndex, driver);
                                }
                                if (chkViewYoutube.Checked)
                                {
                                    for (int j = 2; j >= 0; j--)
                                    {
                                        if (j < 2)
                                        {
                                            querys = driver.FindElements(By.XPath("/html/body/div[8]/div/div/table[2]/tbody/tr/td[2]/div[15]/div/div/div[5]/a"));
                                        }
                                        querys[j].Click();
                                        demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                        await viewYoutubeSEO(e.RowIndex, driver);
                                        driver.Navigate().Refresh();
                                        demnguocSEO(10, e.RowIndex, "Chờ load nv YouTube");
                                    }
                                }
                                //querys[3].Click();
                            }

                            // await traffic(e.RowIndex);

                            //await LikeYoutube(e.RowIndex, driver);

                            //  await viewYoutubeSEO(e.RowIndex, driver);

                            i++;
                            dataGridSEO.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                            demnguocSEO(RamdomTime(100, 120), e.RowIndex, "Thời gian xoay vòng nhiệm vụ");
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
        private void reloadPageTask(IWebDriver driver, int rowIndex)
        {

            var query = driver.FindElement(By.XPath("/html/body/div[8]/div/div/table[2]/tbody/tr/td[1]/div[1]/nav/div[3]/ul/li[1]/a[2]/div[1]"));
            query.Click();
            demnguocSEO(10, rowIndex, "Click task");

            var querys = driver.FindElements(By.XPath("/html/body/div[9]/div/div/table[2]/tbody/tr/td[2]/div[15]/div/div/center/table[2]/tbody/tr"));
            int indexran = 0;
            var ran = RamdomTime(1, 10);
            if (querys.Count > 0)
            {
                for (int j = 1; j < querys.Count; j++)
                {
                    var style = querys[j].GetAttribute("style");
                    if (style.IndexOf("display:") > -1)
                    {
                        continue;
                    }
                    else
                    {
                        indexran++;
                    }

                    var linkClick = querys[j].FindElements(By.TagName("a"));
                    if (linkClick.Count > 2 && indexran == ran)
                    {
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].click();", linkClick[0]);
                        demnguocSEO(RamdomTime(6, 7), rowIndex, "Change tab");

                        var tabhandel = driver.WindowHandles;
                        if (tabhandel.Count > 1)
                        {
                            driver.SwitchTo().Window(tabhandel[1]);
                            driver.Close();
                        }
                        driver.SwitchTo().Window(tabhandel[0]);
                        break;
                    }

                }
            }
        }

        private void dataGridSEO_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                int currentMouseOverRow = dataGridSEO.HitTest(e.X, e.Y).RowIndex;
                RowIndexGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGridSEO, new System.Drawing.Point(e.X, e.Y));

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

        private void dataGridProfitcent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridProfitcent[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        var i = 0;
                        demnguocPro(5, e.RowIndex, "Mở Chorme selenium");
                        ChromeOptions chromeOptions = new ChromeOptions();
                        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
                        chromeDriverService.HideCommandPromptWindow = true;
                        if (!Directory.Exists(ProfileFolderPath))
                        {
                            Directory.CreateDirectory(ProfileFolderPath);
                        }

                        if (Directory.Exists(ProfileFolderPath))
                        {
                            string nameProfile = "PRO" + dataGridProfitcent.Rows[e.RowIndex].Cells[1].Value.ToString();

                            chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
                        }

                        chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=850,850", "--disable-gpu");
                        IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
                        driver.Navigate().GoToUrl("https://profitcentr.com/login");
                        demnguocPro(15, e.RowIndex, "Load page");
                        var query = driver.FindElement(By.XPath("//*[@id=\"login-form\"]/table/tbody/tr[1]/td[2]/input"));
                        query.SendKeys(listAccountPro[e.RowIndex].id);
                        demnguocPro(3, e.RowIndex, "Nhập Username");
                        query = driver.FindElement(By.XPath("//*[@id=\"login-form\"]/table/tbody/tr[2]/td[2]/input"));
                        query.SendKeys(listAccountPro[e.RowIndex].pass);
                        demnguocPro(3, e.RowIndex, "Nhập Password");
                        // 
                        // query.Click();

                        demnguocPro(50, e.RowIndex, "Load page");


                        if (chkRuVideoPro.Checked)
                        {
                            await viewRutubePro(e.RowIndex, driver);
                        }

                        if (chkviewVideoPro.Checked)
                        {
                            await viewYoutubePro(e.RowIndex, driver);
                        }


                        // await traffic(e.RowIndex);

                        //await LikeYoutube(e.RowIndex, driver);

                        //  await viewYoutubeSEO(e.RowIndex, driver);


                        dataGridProfitcent.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                        //  demnguocPro(RamdomTime(200, 220), e.RowIndex, "Thời gian xoay vòng nhiệm vụ");


                    }));
                    thread.Name = "PRO" + dataGridProfitcent.Rows[e.RowIndex].Cells[1].Value.ToString();
                    thread.Start();
                    this.threadDictionaryPro.Add("PRO" + dataGridProfitcent.Rows[e.RowIndex].Cells[1].Value.ToString(), thread);
                }
                else
                {
                    cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    this.threadDictionaryPro["PRO" + dataGridProfitcent.Rows[e.RowIndex].Cells[1].Value.ToString()].Abort();
                    this.threadDictionaryPro.Remove("PRO" + dataGridProfitcent.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }

        private void dataGridProfitcent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                int currentMouseOverRow = dataGridProfitcent.HitTest(e.X, e.Y).RowIndex;
                RowIndexGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGridProfitcent, new System.Drawing.Point(e.X, e.Y));

                m.ItemClicked += M_ItemClicked1; ;
            }
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
