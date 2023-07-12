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
    public partial class ClaimDonal : Form
    {
        int index = 0;
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        List<Account> listAccount = new List<Account>();
        public ClaimDonal()
        {
            InitializeComponent();
            var account = new Account();
            account.id = "nlduong";
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
        private int RamdomTime(int begin, int end)
        {
            Random rnd = new Random();
            int ramNumber = rnd.Next(begin, end);
            return ramNumber;
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

                driver.Navigate().GoToUrl("https://donaldco.in/index.php?view=login&ref=&");

                //  await viewYoutube(e.RowIndex);
                demnguoc(RamdomTime(4, 7), RowIndex, "Load page");
                var query = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div[2]/h2/div/form/table/tbody/tr[2]/td/input"));
                query.SendKeys(listAccount[RowIndex].id);
                demnguoc(RamdomTime(4, 7), RowIndex, "Nhập Username");
                query = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div[2]/h2/div/form/table/tbody/tr[3]/td/input"));
                demnguoc(RamdomTime(1, 3), RowIndex, "Nhập Pass");
                query.SendKeys(listAccount[RowIndex].pass);
                demnguoc(RamdomTime(30, 35), RowIndex, "Chờ Nhập capchar");

              
                while (true)
                {
                    var querys = driver.FindElements(By.XPath("/html/body/div[2]/div[3]/div/div[2]/h2/center/center[1]/table/tbody/tr/th[1]/center/a[1]"));
                    if(querys.Count > 0)
                    {
                        if (querys[0].GetAttribute("href").Contains("index.php?view=account&ac=btc-profile&action=work&")) 
                            {
                                querys[0].Click();
                                
                            }
                        demnguoc(RamdomTime(1000, 1005), RowIndex, "Chờ nhấp lại đào");
                        driver.Navigate().Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                demnguoc(RamdomTime(3, 6), RowIndex, ex.Message);
                driver.Close();
                openCamle(RowIndex);
            }
        }
    }
}
