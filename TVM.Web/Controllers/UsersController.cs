using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TVM.DTO;
using TVM.DTO.Users;
using TVM.IDomian.IDM;
using TVM.Model.CM;
using TVM.Web.Controllers.Base;
using Utility;

namespace TVM.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUsersDM dm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DM"></param>
        public UsersController(IUsersDM DM)
        {
            dm = DM;
        }

        #region 用户
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsersList")]
        public ResultEntity<List<UsersDTO>> GetUsersList([FromQuery] Request_Users dto)
        {
            return new ResultEntityUtil<List<UsersDTO>>().Success(dm.GetUsersList(dto, out int count), count);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddUsers")]
        public ResultEntity<bool> AddUsers([FromQuery] UsersModel model)
        {
            if (CurrentUserId != 1)
            {
                throw new Exception("该用户没有权限！");
            }
            return new ResultEntityUtil<bool>().Success(dm.AddUsers(model));
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpUsers")]
        public ResultEntity<bool> UpUsers([FromQuery] UsersModel model)
        {
            if (CurrentUserId != 1)
            {
                throw new Exception("该用户没有权限！");
            }
            return new ResultEntityUtil<bool>().Success(dm.UpUsers(model));
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeUsers")]
        public ResultEntity<bool> DeUsers(int Id)
        {
            if (CurrentUserId != 1)
            {
                throw new Exception("该用户没有权限！");
            }
            return new ResultEntityUtil<bool>().Success(dm.DeUsers(Id));
        }
        #endregion

    }

}
