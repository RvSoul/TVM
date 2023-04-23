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

    [Table("TransportationRecords")]
    public partial class TransportationRecords
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        [DefaultValue("车辆Id")]
        public int CarId { get; set; }

        /// <summary>
        /// 矿号
        /// </summary>
        [DefaultValue("矿号")]
        public string CollieryCode { get; set; }

        /// <summary>
        /// 毛量
        /// </summary>
        [DefaultValue("毛量")]
        public int RoughWeight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary>
        [DefaultValue("皮重")]
        public int? TareWeight { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        [DefaultValue("净重")]
        public int? NetWeight { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        [DefaultValue("进厂时间")]
        public DateTime STime { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        [DefaultValue("出厂时间")]
        public DateTime? ETime { get; set; }

       
        /// <summary>
        /// 状态
        /// </summary>
        [DefaultValue("状态")]
        public int State { get; set; }

        
        /// <summary>
        /// 是否上传
        /// </summary>
        [DefaultValue("是否上传")]
        public int IsUpload { get; set; }
    }
}
