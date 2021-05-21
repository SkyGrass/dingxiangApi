using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class ReqModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string apiId { get; set; } = "500000";
        /// <summary>
        /// 
        /// </summary>
        public string customId { get; set; } = "oyqjgbpSTmP8e";
        /// <summary>
        /// 
        /// </summary>
        public string timestamp { get; set; } = "1621559954295";
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; } = "V1";
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string clinicId { get; set; } = "7";
        /// <summary>
        /// 
        /// </summary>
        public string outpatientStatus { get; set; } = "4";
        /// <summary>
        /// 
        /// </summary>
        public string doctorId { get; set; } = "8931";
        /// <summary>
        /// 
        /// </summary>
        public string departmentId { get; set; } = "116";
        /// <summary>
        /// 
        /// </summary>
        public string startDate { get; set; } = "2021-05-21";
        /// <summary>
        /// 
        /// </summary>
        public string endDate { get; set; } = "2021-05-21";
        /// <summary>
        /// 
        /// </summary>
        public string chargeStatus { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string pageIndex { get; set; } = "1";
        /// <summary>
        /// 
        /// </summary>
        public string pageSize { get; set; } = "10";
    }
}
