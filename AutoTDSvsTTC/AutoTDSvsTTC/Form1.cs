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
using static AutoTDSvsTTC.Common;

namespace AutoTDSvsTTC
{
    public partial class Form1 : Form
    {
        int index = 0;
       
        private Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
        int RowIndexTDSGrid = 0;
        public Form1()
        {
            InitializeComponent();
            StreamReader r = new StreamReader("account.json");
            string jsonString = r.ReadToEnd();
            var listAccount = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("#", typeof(int));
            //dataTable.Columns.Add("ID Google", typeof(string));
            //dataTable.Columns.Add("Pass Google", typeof(string));
            dataTable.Columns.Add("ID TDS", typeof(string));
            dataTable.Columns.Add("Pass TDS", typeof(string));
            dataTable.Columns.Add("Token", typeof(string));
            dataTable.Columns.Add("Xu Thêm", typeof(string));
            dataTable.Columns.Add("Tổng Xu", typeof(string));
            dataTable.Columns.Add("Trạng Thái", typeof(string));
           
            foreach (var entry in listAccount)
            {
                index++;             
                dataTable.Rows.Add(index,entry.idtds, entry.passtds, entry.tokentds,"0", "0");
               
            }           
            dataGridTDS.DataSource = dataTable;
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
            this.dataGridTDS.Columns.Add(buttonColumn);
            this.dataGridTDS.Columns[6].Width = 300;
        }

        private void dataGridTDS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridTDS[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
            {
                if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                {
                    cell.Value = "Kết thúc";
                    Thread thread = new Thread((ThreadStart)(async () =>
                    {
                        commentTikTok(e.RowIndex);
                    }));
                    thread.Name = dataGridTDS.Rows[e.RowIndex].Cells[4].Value.ToString();
                    thread.Start();
                    this.threadDictionary.Add(dataGridTDS.Rows[e.RowIndex].Cells[4].Value.ToString(), thread);
                }
                else
                {
                    cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    this.threadDictionary[dataGridTDS.Rows[e.RowIndex].Cells[4].Value.ToString()].Abort();
                    this.threadDictionary.Remove(dataGridTDS.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
            }
           // dataGridTDS.Rows[e.RowIndex].Cells["action"].Value = (object)"Bắt đầu";
           // dataGridTDS.Rows[e.RowIndex].Cells["trangthaichay"].Value = (object)"Đang dừng, vui lòng đợi giây lát...";
        }
             

        private void btnThemTKTDS_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridTDS.DataSource as DataTable;
            index++;
            dt.Rows.Add(index, txtIdGoogle.Text,txtpassGoogle.Text, txtIdTDS.Text, txtPassTDS.Text,"fff","");           
            dataGridTDS.DataSource = dt;
        }

        private void dataGridTDS_MouseClick(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();            

                int currentMouseOverRow = dataGridTDS.HitTest(e.X, e.Y).RowIndex;
                RowIndexTDSGrid = currentMouseOverRow;
                if (currentMouseOverRow >= 0)
                {
                    m.Items.Add("CongfigChrome").Name = "Config Chome";
                }

                m.Show(dataGridTDS, new Point(e.X, e.Y));
               
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
                string nameProfile = dataGridTDS.Rows[RowIndexTDSGrid].Cells[2].Value.ToString() ;

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }
            //chromeOptions2.AddArguments("profile-directory=" + UID);
            //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=1200,850", "--disable-gpu");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Navigate().GoToUrl("https://www.tiktok.com/en/");
        }

        private  async Task commentTikTok(int index)
        {
            this.demnguoc(5, index,"Mở Chorme selenium");
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("Chrome");
            chromeDriverService.HideCommandPromptWindow = true;
            if (!Directory.Exists(ProfileFolderPath))
            {
                Directory.CreateDirectory(ProfileFolderPath);
            }

            if (Directory.Exists(ProfileFolderPath))
            {
                string nameProfile = dataGridTDS.Rows[index].Cells[2].Value.ToString();

                chromeOptions.AddArguments("user-data-dir=" + ProfileFolderPath + "/" + nameProfile);
            }
            //chromeOptions2.AddArguments("profile-directory=" + UID);
            //chromeOptions.EnableMobileEmulation("iPhone 12 Pro");
            chromeOptions.AddArguments("--disable-blink-features=AutomationControlled", "--disable-notifications", "--disable-popup-blocking", "--disable-geolocation", "--no-sandbox", "--window-size=1200,850", "--disable-gpu");
            IWebDriver driver = (IWebDriver)new ChromeDriver(chromeDriverService, chromeOptions);

            string responseContent;         
            HttpClient httpClient;
            CookieContainer cookies;
            HttpClientHandler handler;

            httpClient = new HttpClient();
            cookies = new CookieContainer();
            handler = new HttpClientHandler();
            var token = dataGridTDS.Rows[index].Cells[4].Value.ToString();
            responseContent = await PostData(httpClient, cookies, handler, "https://traodoisub.com/api/?fields=tiktok_comment&access_token=" + token);

            List<ListCommnentTiktok> lisNV = JsonConvert.DeserializeObject<List<ListCommnentTiktok>>(responseContent);
            this.demnguoc(3, index, "Lấy list nhiệm vụ");
            foreach (var item in lisNV)
            {
                //dataGridTDS.Invoke(new Action(() =>
                //{
                //    dataGridTDS.Rows[index].Cells[7].Value = "Xử lý nội dung";
                //}));
                this.demnguoc(3, index, "Xử lý nội dung");
                var nd = item.noidung;
                if (checkEmoji(nd) || nd == "")
                {
                    continue;
                }
                driver.Navigate().GoToUrl(item.link);
                this.demnguoc(10, index,"Load Page");
                //dataGridTDS.Rows[index].Cells[8].Value = "Vui lòng cho" + lblTime.Text;
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,800)");
                this.demnguoc(2, index, "Scoll page 800");

                var commentbt = driver.FindElements(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/div/div/div[1]/div/div[1]"));
                if (commentbt.Count == 0)
                {
                    continue;
                }
                this.demnguoc(2, index, "Click comment");
                commentbt[0].Click();

                // dataGridTDS.Rows[index].Cells[8].Value = "Click ô comment";

                // sendKey.SendKeys(nd);
                this.demnguoc(5, index, "Nhập comment");
                var element = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/div/div/div[1]/div/div[1]"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                actions.Click();
                actions.SendKeys(nd);
                actions.Build().Perform();

                
                this.demnguoc(2, index, "Post comment");
                var commment = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[1]/div[2]/div[1]/div[3]/div[1]/div/div/div[2]"));
                commment.Click();
                this.demnguoc(15, index, "Nhận Xu");
                string url = "https://traodoisub.com/api/coin/?type=TIKTOK_COMMENT&id=" + item.id + "&access_token=" + token;
                responseContent = await PostData(httpClient, cookies, handler, url);
                XuCommnentTiktok xuCommnentTiktok = JsonConvert.DeserializeObject<XuCommnentTiktok>(responseContent);
                if(xuCommnentTiktok.data== null)
                {
                    dataGridTDS.Rows[index].Cells[5].Value = xuCommnentTiktok.error;                    
                }
                if(xuCommnentTiktok.success =="200")
                {
                   
                    dataGridTDS.Rows[index].Cells[5].Value = xuCommnentTiktok.data.msg;
                    dataGridTDS.Rows[index].Cells[6].Value = xuCommnentTiktok.data.xu;
                }
                this.demnguoc(3, index, "Chuyển nhiệm vụ");               
            }

        }       
        public void demnguoc(int time, int rowIndex,string job)
        {
            while (time > 0)
            {
                this.dataGridTDS[7, rowIndex].Value = (object)string.Format("{0} - {1} {2} s...", job, (object)"Vui lòng chờ", (object)time);
                --time;
                Thread.Sleep(1000);
                if (!this.threadDictionary.ContainsKey(this.dataGridTDS[4, rowIndex].Value.ToString()))
                {
                    this.dataGridTDS[7, rowIndex].Value = (object)"Đã dừng";
                    break;
                }
            }
        }
    }
}
