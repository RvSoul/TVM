using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using TVM.DTO;
using TVM.Web.Controllers.Base;
using TVM.IDomian.IDM;
using TVM.DTO.Tr;

namespace TVM.Web.Controllers
{
    [Route("api/[controller]")] 
    public class TrController : ApiControllerBase
    {
        private readonly ITrDM dm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DM"></param>
        public TrController(ITrDM DM)
        {
            dm = DM;
        }

        #region 运输记录
        /// <summary>
        /// 获取运输记录列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTransportationRecordsList")]
        public ResultEntity<List<TransportationRecordsDTO>> GetTransportationRecordsList([FromQuery] Request_TransportationRecords dto)
        {
            return new ResultEntityUtil<List<TransportationRecordsDTO>>().Success(dm.GetTransportationRecordsList(dto, out int count), count);
        }

        /// <summary>
        /// 添加运输记录
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddTransportationRecords")]
        public ResultEntity<bool> AddTransportationRecords([FromQuery] TransportationRecordsModel model)
        {
            model.Userid = CurrentUserId;
            return new ResultEntityUtil<bool>().Success(dm.AddTransportationRecords(model));
        }
        /// <summary>
        /// 修改运输记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpTransportationRecords")]
        public ResultEntity<bool> UpTransportationRecords([FromQuery] TransportationRecordsModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpTransportationRecords(model));
        }


        /// <summary>
        /// 删除运输记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeTransportationRecords")]
        public ResultEntity<bool> DeTransportationRecords(string Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeTransportationRecords(Id));
        }
        #endregion
    }
}
