using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodayInHistory.Entity;
using TodayInHistory.Tools;
using Newtonsoft.Json;
using TodayInHistory.Model;
using System.Web;

namespace TodayInHistory
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();

            textBoxAppKey.Text = "b08c390990cf2006c9da3fd270f3f413";
            //获取接口数据
            //string Appkey = "b08c390990cf2006c9da3fd270f3f413";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //检查数据库
            ActionDo("CheckDb");

            if (!string.IsNullOrEmpty(textBoxAppKey.Text.Replace(" ", "")))
            {
                //获取数据
                ActionDo("GetJson");
            }
            else
            {
                MessageBox.Show("请填写Appkey");
            }
        }

        private void ActionDo(string Action)
        {
            switch (Action)
            {
                case "CheckDb":
                    ActionAsync checkDb = new ActionAsync();
                    checkDb.Do(() =>
                    {
                        CheckDb("正在检查数据库,请稍后...");
                        Thread.Sleep(0000);//延迟3秒
                    });
                    break;
                case "GetJson":
                    ActionAsync action = new ActionAsync();
                    action.Do(() =>
                    {
                        GetHistory(textBoxAppKey.Text);
                    });
                    break;
                case "DownImg":
                    ActionAsync Downimg = new ActionAsync();
                    Downimg.Do(() =>
                    {
                        DownImg();
                    });
                    break;
            }
        }

        /// <summary>
        /// 检查数据库
        /// </summary>
        private void CheckDb(object msg)
        {
            using (HistoryContext db = new HistoryContext())
            {
                textBoxContent.Text = "\r\n" + msg + textBoxContent.Text;
                try
                {
                    if (db.Database.CreateIfNotExists())
                    {
                        textBoxContent.Text = "\r\n正在创建数据库" + textBoxContent.Text;
                        Thread.Sleep(0000);//延迟3秒
                        textBoxContent.Text = "\r\n数据库创建成功" + textBoxContent.Text;
                        textBoxContent.Text = "\r\n开始获取数据..." + textBoxContent.Text;
                    }
                    else
                    {
                        textBoxContent.Text = "\r\n数据库已存在" + textBoxContent.Text;
                        textBoxContent.Text = "\r\n开始获取未完成数据..." + textBoxContent.Text;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(ex.Message);
                    textBoxContent.Text = "\r\n数据库初始化失败" + textBoxContent.Text;
                }
            }
        }

        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private void GetHistory(string Appkey)
        {
            try
            {
                using (HistoryContext db = new HistoryContext())
                {
                    History LastHistory = new History();
                    int month = 1;
                    int day = 0;
                    if (db.Historys.Count() > 0)
                    {
                        int MaxMonth = db.Historys.Max(h => h.month);
                        int MaxDay = db.Historys.Where(h => h.month == MaxMonth).Max(h => h.day);
                        LastHistory = db.Historys.Where(h => h.month == MaxMonth & h.day == MaxDay).FirstOrDefault();
                        month = LastHistory.month;
                    }
                    for (int m = month; m <= 12; m++)
                    {
                        int days = 0;
                        days = GetDay(m);

                        for (int d = 1; d <= days; d++)
                        {
                            if (m == LastHistory.month)
                            {
                                month = m;
                                day = d + LastHistory.day;
                                if (day >= days)
                                {
                                    d = days;
                                }
                            }
                            else
                            {
                                month = m;
                                day = d;
                            }
                            string url = "http://api.juheapi.com/japi/toh?key=" + Appkey + "&v=1.0&month=" + month + "&day=" + day;
                            WebRequest myWebRequest = WebRequest.Create(url.ToString());
                            WebResponse myWebResponse = myWebRequest.GetResponse();
                            Stream ReceiveStream = myWebResponse.GetResponseStream();
                            string responseStr = "";
                            if (ReceiveStream != null)
                            {
                                StreamReader reader = new StreamReader(ReceiveStream, Encoding.UTF8);
                                responseStr = reader.ReadToEnd();
                                reader.Close();
                            }
                            myWebResponse.Close();
                            LogHelper.InfoLog(responseStr);
                            HistoryJson historyjson = JsonConvert.DeserializeObject<HistoryJson>(responseStr);

                            if (historyjson.error_code == 0)
                            {
                                var historys = new List<History>();
                                foreach (var item in historyjson.result)
                                {
                                    History history = new History()
                                    {
                                        _id = item._id,
                                        title = item.title,
                                        pic = item.pic,
                                        year = item.year,
                                        month = item.month,
                                        day = item.day,
                                        des = item.des,
                                        lunar = item.lunar
                                    };
                                    historys.Add(history);
                                }
                                historys.ForEach(s => db.Historys.Add(s));
                                db.SaveChanges();

                                textBoxContent.Text = "\r\n" + month + "月" + day + "日数据获取成功" + textBoxContent.Text;
                            }
                            else
                            {
                                textBoxContent.Text = "\r\n" + month + "月" + day + "日数据失败,错误码:" + historyjson.error_code + ",错误信息:" + historyjson.reason + textBoxContent.Text;
                            }
                        }
                    }
                }
                textBoxContent.Text.Trim();
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(ex.Message);
            }
        }

        /// <summary>
        /// 发起一个HTTP请求（以GET方式）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string httpGetStr(string Appkey, string Month, string Day)
        {
            string url = "http://api.juheapi.com/japi/toh?key=" + Appkey + "&v=1.0&month=" + Month + "&day=" + Day;
            WebRequest myWebRequest = WebRequest.Create(url.ToString());
            WebResponse myWebResponse = myWebRequest.GetResponse();
            Stream ReceiveStream = myWebResponse.GetResponseStream();
            string responseStr = "";
            if (ReceiveStream != null)
            {
                StreamReader reader = new StreamReader(ReceiveStream, Encoding.UTF8);
                responseStr = reader.ReadToEnd();
                reader.Close();
            }
            myWebResponse.Close();
            return responseStr;
        }

        /// <summary>
        /// 获取月份天数
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetDay(int month)
        {
            int days = 0;
            switch (month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = 29;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }
            return days;
        }

        private void DownImg()
        {
            textBoxContent.Text = "\r\n开始下载图片" + textBoxContent.Text;
            //下载图书封面
            string local = "Images";
            string localPath = Path.Combine(Application.StartupPath, local);
            using (HistoryContext db = new HistoryContext())
            {
                var historys = db.Historys.AsNoTracking().ToList();
                historys = historys.OrderByDescending(h => h.month).ToList();
                foreach (var item in historys)
                {
                    string filePathName = DateTime.Now.ToFileTime() + ".jpg";
                    string localURL = Path.Combine(local, filePathName);
                    if (!System.IO.Directory.Exists(localPath))
                    {
                        System.IO.Directory.CreateDirectory(localPath);
                    }
                    if (item.pic.Length > 0)
                    {
                        HttpDldFile df = new HttpDldFile();
                        if (df.Download(item.pic, Path.Combine(localPath, filePathName)))
                        {
                            History history_ = db.Historys.Find(item._id);
                            history_.pic = filePathName;
                            //History history = new History()
                            //{
                            //    _id=item._id,
                            //    pic = filePathName
                            //};
                            db.Entry(history_).State = EntityState.Modified;
                            db.Entry(history_).Property(x => x.title).IsModified = false;
                            db.Entry(history_).Property(x => x.year).IsModified = false;
                            db.Entry(history_).Property(x => x.month).IsModified = false;
                            db.Entry(history_).Property(x => x.day).IsModified = false;
                            db.Entry(history_).Property(x => x.des).IsModified = false;
                            db.Entry(history_).Property(x => x.lunar).IsModified = false;
                            db.SaveChanges();

                            textBoxContent.Text = "\r\n" + item.month + "月" + item.day + "日,id:" + item._id + "图片下载成功;当前第" + historys.IndexOf(item) + "个文件" + textBoxContent.Text;
                        }
                        else
                        {
                            textBoxContent.Text = "\r\n" + item.month + "月" + item.day + "日,id:" + item._id + "图片下载失败;当前第" + historys.IndexOf(item) + "个文件" + textBoxContent.Text;
                        }
                    }
                    else
                    {
                        textBoxContent.Text = "\r\n" + item.month + "月" + item.day + "日,id:" + item._id + "没有图片;当前第" + historys.IndexOf(item) + "个文件" + textBoxContent.Text;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //下载图片
            ActionDo("DownImg");
        }
    }
}
