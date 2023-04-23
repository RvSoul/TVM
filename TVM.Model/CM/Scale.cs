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

    [Table("Scale")]
    public partial class Scale
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 衡名称
        /// </summary>
        [DefaultValue("衡名称")]
        public string Name { get; set; }

        /// <summary>
        /// 衡类型
        /// </summary>
        [DefaultValue("衡类型")]
        public int Type { get; set; }

        /// <summary>
        /// 衡状态
        /// </summary>
        [DefaultValue("衡状态")]
        public int State { get; set; }

        /// <summary>
        /// 启停用时间
        /// </summary>
        [DefaultValue("启停用时间")]
        public DateTime UpTime { get; set; }
    }
}
