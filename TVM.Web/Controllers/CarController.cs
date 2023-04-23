using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TVM.DTO.Car;
using TVM.DTO;
using TVM.IDomian.IDM;
using TVM.Model.CM;
using TVM.Web.Controllers.Base;

namespace TVM.Web.Controllers
{
    [Route("api/[controller]")]
    public class CarController : ApiControllerBase
    {
        private readonly ICarDM dm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DM"></param>
        public CarController(ICarDM DM)
        {
            dm = DM;
        }


        #region 车辆
        /// <summary>
        /// 获取车辆列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCarList")]
        public ResultEntity<List<CarDTO>> GetCarList([FromQuery] Request_Car dto)
        {
            return new ResultEntityUtil<List<CarDTO>>().Success(dm.GetCarList(dto, out int count), count);
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddCar")]
        public ResultEntity<bool> AddCar([FromQuery] CarModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddCar(model));
        }
        /// <summary>
        /// 修改车辆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpCar")]
        public ResultEntity<bool> UpCar([FromQuery] CarModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpCar(model));
        }


        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeCar")]
        public ResultEntity<bool> DeCar(int Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeCar(Id));
        }
        #endregion
    }
}
