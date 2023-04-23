using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;

namespace TVM.DTO.Tr
{
     
    public class Request_TransportationRecords : ModelDTO
    { 
        [DefaultValue("状态")]
        public TransportationRecordsState? State { get; set; }

    }
    public enum TransportationRecordsState
    {
        [DefaultValue("正常")]
        正常 = 1,
        [DefaultValue("告警")]
        告警 = 2,
        [DefaultValue("异常")]
        异常 = 3,
    }
    public class TransportationRecordsModel
    { 
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
        public TransportationRecordsState State { get; set; }

        /// <summary>
        /// 异常原因
        /// </summary>
        [DefaultValue("异常原因")]
        public string AbnormalCause { get; set; }
        /// <summary>
        /// 操作用户id
        /// </summary>
        [DefaultValue("操作用户id")]
        public int? Userid { get; set; }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        [DefaultValue("异常处理方式")]
        public string Disposal { get; set; }
         
         
    }

    public class TransportationRecordsDTO
    {
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
        /// 数据记录时间
        /// </summary>
        [DefaultValue("数据记录时间")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DefaultValue("状态")]
        public TransportationRecordsState State { get; set; }

        /// <summary>
        /// 异常原因
        /// </summary>
        [DefaultValue("异常原因")]
        public string AbnormalCause { get; set; }

        /// <summary>
        /// 操作用户id
        /// </summary>
        [DefaultValue("操作用户id")]
        public int? Userid { get; set; }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        [DefaultValue("异常处理方式")]
        public string Disposal { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DefaultValue("操作时间")]
        public DateTime DisposalTime { get; set; }

        /// <summary>
        /// 是否上传
        /// </summary>
        [DefaultValue("是否上传")]
        public int IsUpload { get; set; }
    }
}
