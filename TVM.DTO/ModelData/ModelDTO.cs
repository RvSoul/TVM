using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVM.DTO.ModelData
{
    public class ModelDTO
    {
        private int pageIndex = 1;
        private int pageSize = 10;

        /// <summary>
        /// 页码
        /// </summary> 
        public int PageIndex { get { return pageIndex; } set { pageIndex = value; } }

        /// <summary>
        /// 页大小
        /// </summary> 
        public int PageSize { get { return pageSize; } set { pageSize = value; } }

    }
}
