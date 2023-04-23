using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TVM.DTO;

namespace TVM.Web.ApiAttribute
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            ResultEntity<bool> result = new();


            result.Msg = context.Exception.Message;

            context.Result = new OkObjectResult(result);

            //设置异常已经处理,否则会被其他异常过滤器覆盖
            context.ExceptionHandled = true;
        }
    }
}
