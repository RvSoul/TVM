using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVM.Model.CM
{

    [Table("ScalageRecords")]
    public partial class ScalageRecords
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 衡Id
        /// </summary>
        [DefaultValue("衡Id")]
        public int ScaleId { get; set; }

        /// <summary>
        /// 运输Id
        /// </summary>
        [DefaultValue("运输Id")]
        public string TId { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [DefaultValue("重量")]
        public int Weigh { get; set; }

        /// <summary>
        /// 称重时间
        /// </summary>
        [DefaultValue("称重时间")]
        public DateTime AddTime { get; set; }
    }
}
