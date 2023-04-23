using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;

namespace TVM.DTO.Scale
{
    public class Request_Scale : ModelDTO
    {

    }

    public class ScaleModel
    {
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
    }
    public class ScaleDTO
    {
    }
}
