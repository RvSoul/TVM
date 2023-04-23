using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TVM.Web.Controllers.Base
{
    /// <summary>
    /// 父类控制器
    /// </summary>
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 当前登录的系统用户
        /// </summary>
        public int CurrentUserId
        {
            get
            {
                //return 1;
                var jtiClaims = User.Claims.FirstOrDefault(item => item.Type == "UserId");
                if (jtiClaims != null)
                {
                    return Convert.ToInt32(jtiClaims.Value);
                }
                throw new Exception("用户未登录");
            }
        }
    }
}
