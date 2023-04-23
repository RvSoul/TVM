using log4net.Config;
using log4net.Repository;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TVM.Domian.DM.Base;
using TVM.Web.ApiAttribute;
using TVM.Model;
using TVM.DTO;
using System.Reflection;

namespace TVM.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置日志
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //配置日志
            Repository = LogManager.CreateRepository("AprilLog");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }


        ///This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services)
        {
            #region 权限
            services.AddMemoryCache();  // 内存缓存

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDFSDSDFSDFSDFFSDF")), //参数配置在下边
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:5000", //发行人
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:5000/", //订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    RequireExpirationTime = true,
                };
            });

            // JWT 认证授权
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    RSA rsa = RSA.Create();
            //    rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(RsaKeys.RsaPublicKey8), out int reads);   // pkcs 8
            //    //rsa.ImportRSAPublicKey(Convert.FromBase64String(RsaKeys.RsaPublicKey), out int reads);  // pkcs 1

            //    SecurityKey securityKey = new RsaSecurityKey(rsa);  // 非对称签名
            //    //SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]));   // 对称签名
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Audience"],
            //        ValidateAudience = true,
            //        ValidateLifetime = true, //是否验证失效时间
            //        ClockSkew = TimeSpan.FromSeconds(5), // 允许5秒偏差,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = securityKey
            //    };
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = (context) =>
            //        {
            //            return Task.CompletedTask;
            //        },
            //        OnTokenValidated = (context) =>
            //        {
            //            var jtiClaims = context.Principal.Claims.FirstOrDefault(item => item.Type == "UserId");
            //            if (jtiClaims == null)
            //            {
            //                throw new Exception("没有授权");
            //            }
            //            else
            //            {
            //                var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            //                if (cache.TryGetValue(jtiClaims.Value, out object value))   // 缓存中存在，说明token 已注销，已失效
            //                {
            //                    context.Fail("授权已失效");
            //                }
            //                return Task.CompletedTask;
            //            }

            //        }
            //    };
            //});
            #endregion

            #region 错误拦截 
            //全局错误拦截
            services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); });

            //模型绑定 特性验证，自定义返回格式
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //获取验证失败的模型字段 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //设置返回内容
                    var result = new ResultEntity<bool>()
                    {
                        Msg = str
                    };
                    return new BadRequestObjectResult(result);
                };
            });
            #endregion

            #region 配置API接口说明文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "TVM.Web.xml");
                c.IncludeXmlComments(xmlPath);

                /********JWT 授权请求头*******/
                var security = new OpenApiSecurityRequirement();
                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "jwt",
                        Type = ReferenceType.SecurityScheme
                    },
                    Description = "json web token授权请求头，在下方输入Bearer {token} 即可，注意两者之间有空格",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                security.Add(scheme, Array.Empty<string>());
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("jwt", scheme);
            });
            #endregion   
            #region 业务层注入方式1
            Assembly BLL = Assembly.Load("TVM.Domian");
            Assembly IBLL = Assembly.Load("TVM.IDomian");
            var typesIBLL = IBLL.GetTypes();
            var typesBLL = BLL.GetTypes();
            foreach (var item in typesIBLL)
            {
                var name = item.Name.Substring(1);

                string implBLLImpName = name + "Imp";
                var impl = typesBLL.Where(w => w.Name == implBLLImpName).FirstOrDefault();

                if (impl != null)
                {
                    services.AddScoped(item, impl);
                }
            }

            #endregion

            #region 数据层EF连接注入
            //builder.Services.AddDbContext<TVMContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TVMContext"))); 
            services.AddDbContext<TVMContext>(options => options.UseMySql(Configuration.GetConnectionString("TVMContext"), MySqlServerVersion.LatestSupportedServerVersion));
            #endregion

            services.AddControllers();
        }

        ///This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //认证
            app.UseAuthentication();
            //授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            #region 配置API接口说明文档
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "test V1");
                //c.RoutePrefix = string.Empty;//如果不设置这个，浏览器的链接为https://localhost:44334/swagger/index.html，设置了就是https://localhost:44334/index.html
            });
            #endregion


            IdentityModelEventSource.ShowPII = true;

            #region 业务层不需要注入，使用实例化实现调用方式
            BaseDM.service = app.ApplicationServices;
            #endregion
        }
    }
}
