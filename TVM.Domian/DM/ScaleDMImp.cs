using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;
using TVM.DTO.Scale;
using TVM.IDomian.IDM;
using TVM.Model;
using TVM.Model.CM;

namespace TVM.Domian.DM
{
    public class ScaleDMImp : IScaleDM
    {
        private readonly TVMContext c;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public ScaleDMImp(TVMContext context)
        {
            c = context;
        }

        public List<ScaleDTO> GetScaleList(Request_Scale dto, out int count)
        {
            Expression<Func<Scale, bool>> expr = AutoAssemble.Splice<Scale, Request_Scale>(dto);

            count = c.Scale.Where(expr).Count();
            List<Scale> li = c.Scale.Where(expr).OrderBy(px => px.State).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();


            return GetMapperDTO.GetDTOList<Scale, ScaleDTO>(li);
        }

        public bool AddScale(ScaleModel model)
        {
            if (c.Scale.Where(w => w.Name == model.Name).FirstOrDefault() != null)
            {
                throw new Exception("该用户已经存在");
            }

            Scale pt = GetMapperDTO.SetModel<Scale, ScaleModel>(model);
            pt.UpTime = DateTime.Now;

            c.Scale.Add(pt);
            c.SaveChanges();
            return true;

        }

        public bool UpScale(ScaleModel model)
        {
            Scale pt = c.Scale.FirstOrDefault(n => n.Id == model.Id);
            pt.Name = model.Name;
            pt.Type = model.Type;
            pt.UpTime = DateTime.Now;

            c.SaveChanges();
            return true;
        }

        public bool QtScale(int id)
        {
            Scale pt = c.Scale.FirstOrDefault(n => n.Id == id);
            if (pt.State == 1)
            {
                pt.State = 2;
            }
            else
            {
                pt.State = 1;
            }
            pt.UpTime = DateTime.Now;

            c.SaveChanges();
            return true;

        }
    }
}
