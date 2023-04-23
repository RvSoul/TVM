using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;
using TVM.DTO.Users;
using TVM.IDomian.IDM;
using TVM.Model;
using TVM.Model.CM;

namespace TVM.Domian.DM
{
    public class UsersDMImp : IUsersDM
    {
        private readonly TVMContext c;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public UsersDMImp(TVMContext context)
        {
            c = context;
        }

        #region 登录
        public string Login(string userName, string password, out string message)
        {
            Users? data = c.Users.Where(w => w.Name == userName && w.Pwd == password).FirstOrDefault();
            if (data != null)
            {
                message = "登录成功。";
                return data.Id.ToString();
            }
            else
            {
                message = "登录失败，用户名或密码错误。";
                return "";
            }
        }
        #endregion


        public List<UsersDTO> GetUsersList(Request_Users dto, out int count)
        {
            Expression<Func<Users, bool>> expr = AutoAssemble.Splice<Users, Request_Users>(dto);
            //if (dto.BShiyongTime2 != null)
            //{
            //    expr = expr.And2(w => w.ShiyongTime2 >= dto.BShiyongTime2);
            //}
            //if (dto.EShiyongTime2 != null)
            //{
            //    expr = expr.And2(w => w.ShiyongTime2 <= dto.EShiyongTime2);
            //} 
            count = c.Users.Where(expr).Count();
            List<Users> li = c.Users.Where(expr).OrderByDescending(px => px.AddTime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            //dtoli = c.Users.Where(expr).OrderByDescending(px => px.AddTime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize)
            //    .Select(x => new UsersDTO
            //    {

            //        Id = x.Id 
            //    }).ToList();

            return GetMapperDTO.GetDTOList<Users, UsersDTO>(li);
        }

        public bool AddUsers(UsersModel model)
        {
            if (c.Users.Where(w => w.Name == model.Name).FirstOrDefault() != null)
            {
                throw new Exception("该用户已经存在");
            }

            Users pt = GetMapperDTO.SetModel<Users, UsersModel>(model);
            pt.AddTime = DateTime.Now;

            c.Users.Add(pt);
            c.SaveChanges();
            return true;

        }

        public bool UpUsers(UsersModel model)
        {
            Users pt = c.Users.FirstOrDefault(n => n.Id == model.Id);
            pt.Name = model.Name;
            pt.Pwd = model.Pwd;

            c.SaveChanges();
            return true;
        }

        public bool DeUsers(int id)
        {
            Users pt = c.Users.FirstOrDefault(n => n.Id == id);
            c.Users.Remove(pt);
            c.SaveChanges();
            return true;

        }
    }
}
