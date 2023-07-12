using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamelBTC
{
    public partial class Form1 : Form
    {
        int index = 0;
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        List<Account> listAccount = new List<Account>();
        public Form1()
        {
            InitializeComponent();
            var account = new Account();
            account.id = "nlduong2007@gmail.com";
            account.pass = "123qwe!@#";
            listAccount.Add(account);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("#", typeof(int));
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Tài nguyên", typeof(string));
            dataTable.Columns.Add("Tài nguyên claim", typeof(string));
            dataTable.Columns.Add("Trạng Thái", typeof(string));

            foreach (var entry in listAccount)
            {
                index++;
                dataTable.Rows.Add(index, entry.id, "", "", "");

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
            this.dataGrid.Columns[4].Width = 300;
            this.dataGrid.Columns[2].Width = 250;
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
                            this.dataGrid[5, rowIndex].Value = (object)"Đã dừng";
                        }));
                    }
                    this.dataGrid.Invoke(new Action(() =>
                    {
                        this.dataGrid.Rows[rowIndex].Cells[5].Value = string.Format("{0} - {1} {2} s...", job, "Vui lòng chờ", time);
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
        private void claimGold(IWebDriver driver, int rowIndex)
        {
            try
            {
                var query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td/center/table[1]/tbody/tr[1]/td[1]/center/table/tbody/tr/td/center/a"));
                if (query.Count() > 0)
                {
                    foreach (var item in query)
                    {
                        if (item.GetAttribute("href").IndexOf("claim=1&type=gold") > 0)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", item);
                            break;
                        }
                    }
                }
                demnguoc(10, rowIndex, "Claim Gold");
                query = driver.FindElements(By.XPath("/html/body/center/center/table[1]/tbody/tr/td[1]/center/font"));
                dataGrid.Rows[rowIndex].Cells[4].Value = query[0].Text;
                query = driver.FindElements(By.XPath(" //*[@id=\"button1\"]"));
                if (query.Count() > 0)
                {
                    query[0].Click();
                }
                demnguoc(10, rowIndex, "Về trang chính");
                query = driver.FindElements(By.XPath("//*[@id=\"button1\"]"));
                if (query.Count() > 0)
                {
                    query[0].Click();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void claimLog(IWebDriver driver, int rowIndex)
        {
            try
            {
                var query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td/center/table[1]/tbody/tr[1]/td[2]/table/tbody/tr/td/center/a"));
                if (query.Count() > 0)
                {
                    foreach (var item in query)
                    {
                        if (item.GetAttribute("href").IndexOf("claim=1&type=wood") > 0)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", item);
                            break;
                        }
                    }

                }
                demnguoc(10, rowIndex, "Claim Log");
                query = driver.FindElements(By.XPath("/html/body/center/center/table[1]/tbody/tr/td[1]/center/font"));
                dataGrid.Rows[rowIndex].Cells[4].Value = query[0].Text;

                query = driver.FindElements(By.XPath(" //*[@id=\"button1\"]"));
                if (query.Count() > 0)
                {
                    query[0].Click();
                }
                demnguoc(10, rowIndex, "Về trang chính");
                query = driver.FindElements(By.XPath(" //*[@id=\"button1\"]"));
                if (query.Count() > 0)
                {
                    query[0].Click();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void claimRock(IWebDriver driver, int rowIndex)
        {
            try
            {
                var query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td/center/table[1]/tbody/tr[2]/td[1]/center/table/tbody/tr/td/center/a"));                
                if (query.Count() > 0)
                {
                    foreach (var item in query)
                    {
                        if (item.GetAttribute("href").IndexOf("claim=1&type=rock") > 0)
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", item);
                            break;
                        }
                    }

                }
                demnguoc(10, rowIndex, "Claim Rock");
                query = driver.FindElements(By.XPath("/html/body/center/center/table[1]/tbody/tr/td[1]/center/font"));
                dataGrid.Rows[rowIndex].Cells[4].Value = query[0].Text;

                query = driver.FindElements(By.XPath(" //*[@id=\"button1\"]"));
                if (query.Count() > 0)
                {
                    query[0].Click();
                }
                demnguoc(10, rowIndex, "Về trang chính");
                query = driver.FindElements(By.XPath(" //*[@id=\"button1\"]"));
                if (query.Count() > 0)
                {
                    query[0].Click();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void getDataAccount(IWebDriver driver, int rowIndex)
        {
            listAccount[rowIndex].Gold = int.Parse(driver.FindElements(By.XPath("/html/body/center/center/font/b[1]"))[0].Text);
            listAccount[rowIndex].Log = int.Parse(driver.FindElements(By.XPath("/html/body/center/center/font/b[2]"))[0].Text);
            listAccount[rowIndex].Rock = int.Parse(driver.FindElements(By.XPath("/html/body/center/center/font/b[3]"))[0].Text);
            listAccount[rowIndex].Steel = int.Parse(driver.FindElements(By.XPath("/html/body/center/center/font/b[4]"))[0].Text);
            listAccount[rowIndex].Food = int.Parse(driver.FindElements(By.XPath("/html/body/center/center/font/b[5]"))[0].Text);
        }
        private void checkMarket(IWebDriver driver, int rowIndex)
        {
            try
            {              
               var query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td/center/table[1]/tbody/tr[1]/td[3]/table[1]/tbody/tr/td/center/a/input"));


                if (query.Count() > 0)
                {
                    query[0].Click();
                }
                else
                {
                    query = driver.FindElements(By.XPath("/ html/body/center/center/table/tbody/tr/td/center/table[1]/tbody/tr[2]/td[1]/table/tbody/tr/td/center/a/input"));
                    if (query.Count() > 0)
                    {
                        query[0].Click();
                    }

                }
                demnguoc(RamdomTime(4, 7), rowIndex, "Claim Coin");
                query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td[2]/center/table/tbody/tr[1]/td/center/a/input"));
                if (query.Count() == 1)
                {
                    query[0].Click();
                }
                demnguoc(RamdomTime(4, 7), rowIndex, "Check Request");
                query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td[2]/center/table/tbody/tr[2]/td/center/table/tbody/tr/td[1]/center/font[2]"));
                if (query.Count() > 0)
                {
                    var request = query[0].Text;

                    if (request.IndexOf('\r') < 0)
                    {
                        var image = query[0].FindElement(By.TagName("img"));
                        var src = image.GetAttribute("src").ToString();
                        if (src.IndexOf("wood") > 0)
                        {
                            var log = int.Parse(request);
                            if (listAccount[rowIndex].Log >= log)
                            {
                                demnguoc(RamdomTime(4, 7), rowIndex, "Delivery");
                                query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td[2]/center/table/tbody/tr[1]/td/center/a[1]/input"));
                                if (query.Count() > 0)
                                {
                                    query[0].Click();
                                }
                            }
                        }
                        if (src.IndexOf("gold") > 0)
                        {
                            var gold = int.Parse(request);
                            if (listAccount[rowIndex].Gold >= gold)
                            {
                                demnguoc(RamdomTime(4, 7), rowIndex, "Delivery");
                                query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td[2]/center/table/tbody/tr[1]/td/center/a[1]/input"));
                                if (query.Count() > 0)
                                {
                                    query[0].Click();
                                }
                            }
                        }
                        if (src.IndexOf("rock") > 0)
                        {
                            var gold = int.Parse(request);
                            if (listAccount[rowIndex].Gold >= gold)
                            {
                                demnguoc(RamdomTime(4, 7), rowIndex, "Delivery");
                                query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td[2]/center/table/tbody/tr[1]/td/center/a[1]/input"));
                                if (query.Count() > 0)
                                {
                                    query[0].Click();
                                }
                            }
                        }
                    }
                    else
                    {
                        var str = request.Replace("\n", "").Split('\r');
                        var images = query[0].FindElements(By.TagName("img"));
                        List<Market> lstobj = new List<Market>();
                      
                        foreach (var image in images)
                        {
                            if (image.GetAttribute("src").IndexOf("img/gold.png") > 0)
                            {
                                Market mk = new Market();
                                mk.Name = "gold";
                                lstobj.Add(mk);


                            }
                            if (image.GetAttribute("src").IndexOf("img/wood.png") > 0)
                            {
                                Market mk = new Market();
                                mk.Name = "wood";
                                lstobj.Add(mk);
                            }
                            if (image.GetAttribute("src").IndexOf("img/rock.png") > 0)
                            {
                                Market mk = new Market();
                                mk.Name = "rock";
                                lstobj.Add(mk);
                            }
                        }
                      
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (lstobj[i].Name == "gold")
                            {
                                lstobj[i].Value = int.Parse(str[i]).ToString();
                            }
                            if (lstobj[i].Name == "wood")
                            {
                                lstobj[i].Value = int.Parse(str[i]).ToString();
                            }
                            if (lstobj[i].Name == "rock")
                            {
                                lstobj[i].Value = int.Parse(str[i]).ToString();
                            }
                        }
                         bool checkgold = false;
                        bool checkwood = false;
                        bool checkrock = false;
                        for (int i = 0; i < lstobj.Count;i++)
                        {
                            if(lstobj[i].Name == "gold" && listAccount[rowIndex].Gold >= int.Parse(lstobj[i].Value))
                            {
                                checkgold = true;
                            }  
                          

                            if (lstobj[i].Name == "wood" && listAccount[rowIndex].Gold >= int.Parse(lstobj[i].Value))
                            {
                                checkwood = true;
                            }
                           
                            if (lstobj[i].Name == "rock" && listAccount[rowIndex].Gold >= int.Parse(lstobj[i].Value))
                            {
                                checkrock = true;
                            }                          
                        }
                       if((checkgold&& checkwood && checkrock && lstobj.Count>2) || ((checkgold && checkwood || checkwood && checkrock || checkgold && checkrock) && lstobj.Count == 2))
                        {
                            demnguoc(5, rowIndex, "Delivery");
                            query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td[2]/center/table/tbody/tr[1]/td/center/a[1]/input"));
                            if (query.Count() > 0)
                            {
                                query[0].Click();
                            }
                        }                      
                    }
                    demnguoc(RamdomTime(4, 7), rowIndex, "Back home");
                    query = driver.FindElements(By.XPath("/html/body/center/center/a/input"));
                    if (query.Count() > 0)
                    {
                        query[0].Click();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void checkClaim(IWebDriver driver, int rowIndex)
        {
            var query = driver.FindElements(By.XPath("/html/body/center/center/table/tbody/tr/td/center/table[1]/tbody/tr[1]/td[1]/center/table/tbody/tr/td/center/font[1]"));
            if (query.Count() > 0)
            {
                var text = query[0].Text;
            }

        }
        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        var i = 0;  
                        while (i < 20)
                        {
                            openCamle(e.RowIndex);
                             i++;
                        }
                        // query.SendKeys(listAccountSEO[e.RowIndex].pass);

                        // dataGrid.Rows[e.RowIndex].Cells[7].Value = "Hết nhiệm vụ dừng";
                        // demnguoc(RamdomTime(200, 360), e.RowIndex, "Thời gian xoay vòng nhiệm vụ");



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
        private void openCamle(int RowIndex)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
            chromeDriverService.HideCommandPromptWindow = true;

            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=820,850", "--disable-gpu");
            // chromeOptions.EnableMobileEmulation("iPad Air");
            IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
            try
            {
                demnguoc(5, RowIndex, "Mở Chorme selenium");
               
                driver.Navigate().GoToUrl("https://camelbtc.com/index.php");

                //  await viewYoutube(e.RowIndex);
                demnguoc(RamdomTime(4, 7), RowIndex, "Load page");
                var query = driver.FindElement(By.XPath("/html/body/center/form/input[1]"));
                query.SendKeys(listAccount[RowIndex].id);
                demnguoc(RamdomTime(4, 7), RowIndex, "Nhập Username");
                query = driver.FindElement(By.XPath("/html/body/center/form/input[3]"));
                demnguoc(RamdomTime(1, 3), RowIndex, "Click Đăng nhập");
                query.Click();
                demnguoc(RamdomTime(4, 7), RowIndex, "Nhập Pass");

                var querys = driver.FindElements(By.XPath("/html/body/center/form/input[1]"));

                if (querys.Count > 0)
                {
                    querys[0].SendKeys(listAccount[RowIndex].pass);
                    demnguoc(RamdomTime(4, 7), RowIndex, "Nhập Pass");
                    query = driver.FindElement(By.XPath("/html/body/center/form/input[3]"));
                    demnguoc(RamdomTime(1, 3), RowIndex, "Click Đăng nhập");
                    query.Click();
                    var test = driver.FindElements(By.XPath("/html/body/center/form/input[3]"));
                    if (test.Count > 0)
                    {
                        test[0].Click();
                    }
                }
                while (true)
                {
                    getDataAccount(driver, RowIndex);
                    string str = "Gold:" + listAccount[RowIndex].Gold;
                    str = str + ",Log: " + listAccount[RowIndex].Log;
                    str = str + ",Rock: " + listAccount[RowIndex].Rock;
                    str = str + ",Steel: " + listAccount[RowIndex].Steel;
                    str = str + ",Food: " + listAccount[RowIndex].Food;

                    dataGrid.Rows[RowIndex].Cells[3].Value = str;
                    if (chkClaimGold.Checked)
                    {
                        demnguoc(RamdomTime(1, 3), RowIndex, "Chuẩn bị claim Gold");
                        claimGold(driver, RowIndex);
                        demnguoc(RamdomTime(1, 3), RowIndex, "Check robot");
                        var robot = driver.FindElements(By.XPath("/html/body/center/form/input[3]"));
                        if (robot.Count > 0)
                        {
                            robot[0].Click();
                        }
                    }
                    if (chkClaimLog.Checked)
                    {
                        demnguoc(RamdomTime(1, 3), RowIndex, "Chuẩn bị claim Log");
                        claimLog(driver, RowIndex);
                        demnguoc(RamdomTime(1, 3), RowIndex, "Check robot");
                        var robot = driver.FindElements(By.XPath("/html/body/center/form/input[3]"));
                        if (robot.Count > 0)
                        {
                            robot[0].Click();
                        }

                    }
                    if (chkClaimRock.Checked)
                    {
                        demnguoc(RamdomTime(1, 3), RowIndex, "Chuẩn bị claim Rock");
                        claimRock(driver, RowIndex);
                        demnguoc(RamdomTime(1, 3), RowIndex, "Check robot");
                        var robot = driver.FindElements(By.XPath("/html/body/center/form/input[3]"));
                        if (robot.Count > 0)
                        {
                            robot[0].Click();
                        }
                    }
                    demnguoc(RamdomTime(1, 3), RowIndex, "Load lại tài nguyên");
                    getDataAccount(driver, RowIndex);
                    str = "Gold:" + listAccount[RowIndex].Gold;
                    str = str + ",Log: " + listAccount[RowIndex].Log;
                    str = str + ",Rock: " + listAccount[RowIndex].Rock;
                    str = str + ",Steel: " + listAccount[RowIndex].Steel;
                    str = str + ",Food: " + listAccount[RowIndex].Food;

                    dataGrid.Rows[RowIndex].Cells[3].Value = str;
                    if (chkCheckMarket.Checked)
                    {
                        demnguoc(RamdomTime(1, 3), RowIndex, "Check market");
                        checkMarket(driver, RowIndex);
                    }
                    demnguoc(int.Parse(txtWaitTime.Text), RowIndex, "Chờ lần kế tiếp");
                    driver.Navigate().Refresh();
                    demnguoc(RamdomTime(3, 6), RowIndex, "Load page");
                }
            }
            catch (Exception ex)
            {
                demnguoc(RamdomTime(3, 6), RowIndex, ex.Message);
                driver.Close();
                openCamle(RowIndex);
            }
            }
        private int RamdomTime(int begin, int end)
        {
            Random rnd = new Random();
            int ramNumber = rnd.Next(begin, end);
            return ramNumber;
        }
        public class Account
        {
            public string id { get; set; }
            public string pass { get; set; }
            public int totalJob { get; set; }
            public int totalFailJob { get; set; }
            public string Money { get; set; }
            public string Gems { get; set; }
            public string Worker { get; set; }
            public int Gold { get; set; }
            public int Log { get; set; }
            public int Rock { get; set; }
            public int Steel { get; set; }
            public int Food { get; set; }
            public string Star { get; set; }
        }
        public class Market
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }

}
