using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TVM.DTO.Scale;
using TVM.DTO;
using TVM.IDomian.IDM;
using TVM.Web.Controllers.Base;

namespace TVM.Web.Controllers
{
    [Route("api/[controller]")] 
    public class ScaleController : ApiControllerBase
    {
        private readonly IScaleDM dm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DM"></param>
        public ScaleController(IScaleDM DM)
        {
            dm = DM;
        }


        #region 衡（称）
        /// <summary>
        /// 获取衡（称）列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetScaleList")]
        public ResultEntity<List<ScaleDTO>> GetScaleList([FromQuery] Request_Scale dto)
        {
            return new ResultEntityUtil<List<ScaleDTO>>().Success(dm.GetScaleList(dto, out int count), count);
        }

        /// <summary>
        /// 添加衡（称）
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddScale")]
        public ResultEntity<bool> AddScale([FromQuery] ScaleModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddScale(model));
        }
        /// <summary>
        /// 修改衡（称）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpScale")]
        public ResultEntity<bool> UpScale([FromQuery] ScaleModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpScale(model));
        }


        /// <summary>
        /// 删除衡（称）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("QtScale")]
        public ResultEntity<bool> QtScale(int Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.QtScale(Id));
        }
        #endregion
    }
}
