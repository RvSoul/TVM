using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TVM.Model.CM
{

    [Table("Car")]
    public partial class Car
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DefaultValue("车牌号")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车重
        /// </summary>
        [DefaultValue("车重")]
        public int CarWeight { get; set; }

        /// <summary>
        /// 浮动重量
        /// </summary>
        [DefaultValue("浮动重量")]
        public int FloatWeight { get; set; }

        /// <summary>
        /// 驾驶员名称
        /// </summary>
        [DefaultValue("驾驶员名称")]
        public string DriverName { get; set; }

        /// <summary>
        /// 驾照编号
        /// </summary>
        [DefaultValue("驾照编号")]
        public string DrivingCode { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DefaultValue("注册时间")]
        public DateTime AddTime { get; set; }


    }
}
