using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TVM.DTO;
using log4net.Repository;
using log4net;
using log4net.Config;
using TVM.Model;
using TVM.Web.ApiAttribute;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Caching.Memory;
using System.Buffers.Text;
using TVM.Domian.DM.Base;
using System.Reflection;
using System.Text;

namespace TVM.Web
{
    public class Program
    {

        /// <summary>
        /// ������־
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        public static void Main(string[] args)
        {
            //������־
            Repository = LogManager.CreateRepository("AprilLog");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            #region ����API�ӿ�˵���ĵ�
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello", Version = "v1" });
                                
                var xmlPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                //Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);

                /********JWT ��Ȩ����ͷ*******/
                var security = new OpenApiSecurityRequirement();
                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "jwt",
                        Type = ReferenceType.SecurityScheme
                    },
                    Description = "json web token��Ȩ����ͷ�����·�����Bearer {token} ���ɣ�ע������֮���пո�",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                security.Add(scheme, Array.Empty<string>());
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("jwt", scheme);
            });
            //builder.Services.AddSwaggerGenNewtonsoftSupport();
            #endregion

            #region Ȩ��
            builder.Services.AddMemoryCache();  // �ڴ滺��

            // JWT ��֤��Ȩ
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateAudience = true,
                    ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
                    ClockSkew = TimeSpan.FromSeconds(5), // ����5��ƫ��,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async context =>
                    {
                        await Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var jtiClaims = context.Principal.Claims.FirstOrDefault(item => item.Type == "UserId");

                        var cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
                        if (cache.TryGetValue(jtiClaims.Value, out object value))   // �����д��ڣ�˵��token ��ע������ʧЧ
                        {
                            //context.Fail("��Ȩ��ʧЧ");

                            throw new Exception("��Ȩ��ʧЧ��");
                        }

                        await Task.CompletedTask;
                    }
                };
            });
            #endregion


            #region �������� 
            //ȫ�ִ�������
            builder.Services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });

            //ģ�Ͱ� ������֤���Զ��巵�ظ�ʽ
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //��ȡ��֤ʧ�ܵ�ģ���ֶ� 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //���÷�������
                    var result = new ResultEntity<bool>()
                    {
                        Msg = str
                    };
                    return new BadRequestObjectResult(result);
                };
            });
            #endregion

            #region ҵ���ע�뷽ʽ1
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
                    builder.Services.AddScoped(item, impl);
                }
            }

            #endregion

            #region ���ݲ�EF����ע�� 
            //builder.Services.AddDbContext<TVMContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TVMContext"))); 
            builder.Services.AddDbContext<TVMContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("TVMContext"), MySqlServerVersion.LatestSupportedServerVersion));
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (true)//app.Environment.IsDevelopment()
            {
          
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    //c.RoutePrefix = string.Empty;
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    c.DefaultModelsExpandDepth(-1);
                });
                 

            }

            //app.UseHttpsRedirection();



            //��֤
            app.UseAuthentication();
            //��Ȩ
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void Main1111(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}