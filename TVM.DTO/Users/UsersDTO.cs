using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;

namespace TVM.DTO.Users
{
    public class Request_Users : ModelDTO
    {
        //[Required(ErrorMessage = "BiaoduanId字段必填！")]
        //[SelectField("and", "=", "string")]
        //public string? BiaoduanId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        [SelectField("and", "like", "string")]
        public string? Name { get; set; }

    }
    public class UsersModel
    {
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
    }

    public class UsersDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DefaultValue("用户名称")]
        public string Name { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        [DefaultValue("注册时间")]
        public DateTime AddTime { get; set; }
    }
}
