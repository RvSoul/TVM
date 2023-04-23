using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TVM.DTO;
using TVM.DTO.Authorization;
using TVM.DTO.Car;
using TVM.IDomian.IDM;
using TVM.Web.ApiAttribute;
using Utility;

namespace TVM.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IUsersDM userdm;
        private readonly IConfiguration configuration;
        private readonly ITrDM trDm;

        public AuthorizationController(ITrDM TrDM, IMemoryCache cache, IUsersDM UserdmDM, IConfiguration Configuration)
        {
            trDm = TrDM;
            _cache = cache;
            userdm = UserdmDM;
            configuration = Configuration;
        }

        #region 登录模块

        /// <summary>
        /// 登录  
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public ResultEntity<string> Login(string userName = "系统管理员", string password = "111111")
        {
            string UserId = userdm.Login(userName, password, out string message);

            if (UserId != null && UserId != "")
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddHours(2)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, UserId),
                    new Claim("UserId", UserId)
                };

                SigningCredentials credentials = BuildSymmetricCredentials();

                var jwttoken = new JwtSecurityToken(
                    //颁发者
                    issuer: configuration["Jwt:Issuer"],
                    //接收者
                    audience: configuration["Jwt:Audience"],
                    //参数
                    claims: claims,
                    notBefore: DateTime.Now,
                    //过期时间
                    expires: DateTime.Now.AddHours(2),
                    //证书签名
                    signingCredentials: credentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(jwttoken); //生成token

                return new ResultEntityUtil<string>().Success(tokenString);
            }
            else
            {
                return new ResultEntityUtil<string>().Failure("", message);
            }

        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("LogOut")]
        public ResultEntity<bool> LogOut()
        {
            var jtiClaims = User.Claims.FirstOrDefault(item => item.Type == "UserId");
            _cache.Set(jtiClaims.Value, jtiClaims.Value, TimeSpan.FromHours(2));  // 将token唯一标识存入缓存中， 用于判断当前Token已注销，即使token在有效期内

            return new ResultEntityUtil<bool>().Success(true);
        }


        /// <summary>
        /// hs256 对称签名
        /// </summary>
        /// <returns></returns>
        SigningCredentials BuildSymmetricCredentials()
        {
            var m5dkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]));
            var creds = new SigningCredentials(m5dkey, SecurityAlgorithms.HmacSha256); //生成签名
            return creds;
        }
        #endregion

        [HttpGet("GetDataInfo")]
        public ResultEntity<bool> GetDataInfo([FromQuery] string dto)
        {
            LogUtil.Info(dto);
            AuthorizationDTO model = JsonHelper.FromJSON<AuthorizationDTO>(dto);
            return new ResultEntityUtil<bool>().Success(true); //trDm.GetDataInfo(dto)
        }

    }
}
