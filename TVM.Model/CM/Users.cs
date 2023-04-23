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

    [Table("Users")]
    public partial class Users
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        public string Name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [DefaultValue("用户密码")]
        public string Pwd { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DefaultValue("注册时间")]
        public DateTime AddTime { get; set; }
    }
}
