using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayInHistory.Model
{
    public class History
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public string _id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public int month { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public int day { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public string des { get; set; }
        /// <summary>
        /// 农历日期
        /// </summary>
        public string lunar { get; set; }

    }

    public class HistoryJson
    {
        /// <summary>
        /// 
        /// </summary>
        public List<History> result { get; set; }
        /// <summary>
        /// 请求成功！
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int error_code { get; set; }
    }
}
