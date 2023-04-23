using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.Scale;

namespace TVM.IDomian.IDM
{
    public interface IScaleDM
    {
        List<ScaleDTO> GetScaleList(Request_Scale dto, out int count);
        bool AddScale(ScaleModel model);

        bool UpScale(ScaleModel model);

        bool QtScale(int id);
    }
}
