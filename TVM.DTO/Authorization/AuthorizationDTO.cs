using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVM.DTO.Authorization
{
    /// <summary>
    /// 衡传过来的数据
    /// </summary>
    public class AuthorizationDTO
    {
        /// <summary>
        /// 下位机设备ID
        /// </summary>
        [DefaultValue("下位机设备ID")]
        public string ID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        [DefaultValue("流水号")]
        public string Sn { get; set; }
        /// <summary>
        /// 衡器名称
        /// </summary>
        [DefaultValue("衡器名称")]
        public int ClassName { get; set; }
        /// <summary>
        /// 衡器入口光幕-0:入口光幕正常;1:阻挡
        /// </summary>
        [DefaultValue("衡器入口光幕")]
        public int inX { get; set; }
        /// <summary>
        /// 衡器出口光幕-0:出口光幕正常;1:阻挡;
        /// </summary>
        [DefaultValue("衡器出口光幕")]
        public int outX { get; set; }

        /// <summary>
        /// 衡器入衡道闸状态-0:关门;1:开门;
        /// </summary>
        public int EntranceGate { get; set; }

        /// <summary>
        /// 衡器出衡道闸状态-0:关门;1:开门
        /// </summary>
        public int ExitGater { get; set; }
        /// <summary>
        /// 表示下列数据有效状态-0:无效;1:有效;
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 表示该车辆当日入厂次数
        /// </summary>
        public int TrainNumber { get; set; }

        /// <summary>
        /// 衡器上面车辆正确停靠-0:无效;1:正确停靠;
        /// </summary>
        public int Inside { get; set; }
        /// <summary>
        /// 衡器准备锁定重量-0:无效;1:正确停靠;
        /// </summary>
        public int StartWeigh { get; set; }
        /// <summary>
        /// 衡器锁定重量成功-0:无效;1:锁定重量成功;
        /// </summary>
        public int Finish { get; set; }
        /// <summary>
        /// 衡器称重流程正常结束-0:无效;1:通知车辆出衡;
        /// </summary>
        public int GoOut { get; set; }
        /// <summary>
        /// 衡器车辆完全出衡-0:无效;1:表示结束;
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// 衡器车辆称重出现错误
        /// </summary>
        [DefaultValue("衡器车辆称重出现错误")]
        public int Error { get; set; }
        /// <summary>
        /// 衡器上面车牌
        /// </summary>
        [DefaultValue("衡器上面车牌")]
        public string PlateNumber { get; set; }
        /// <summary>
        /// 衡器上面车辆运的那个矿号
        /// </summary>
        [DefaultValue("衡器上面车辆运的那个矿号")]
        public string CollieryCode { get; set; }
        /// <summary>
        /// 衡器上面车辆运煤的重量
        /// </summary>
        [DefaultValue("衡器上面车辆运煤的重量")]
        public int Weight { get; set; }

    }
}
