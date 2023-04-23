using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;
using TVM.DTO.Car;
using TVM.IDomian.IDM;
using TVM.Model.CM;
using TVM.Model;

namespace TVM.Domian.DM
{
    public class CarDMImp : ICarDM
    {
        private readonly TVMContext c;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public CarDMImp(TVMContext context)
        {
            c = context;
        }
         
        public List<CarDTO> GetCarList(Request_Car dto, out int count)
        {
            Expression<Func<Car, bool>> expr = AutoAssemble.Splice<Car, Request_Car>(dto);

            count = c.Car.Where(expr).Count();
            List<Car> li = c.Car.Where(expr).OrderByDescending(px => px.AddTime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return GetMapperDTO.GetDTOList<Car, CarDTO>(li);
        }
        public bool AddCar(CarModel model)
        {
            Car pt = GetMapperDTO.SetModel<Car, CarModel>(model);
            pt.AddTime = DateTime.Now;

            c.Car.Add(pt);
            c.SaveChanges();
            return true;
        }

        public bool UpCar(CarModel model)
        {
            Car pt = c.Car.FirstOrDefault(n => n.Id == model.Id);


            c.SaveChanges();
            return true;
        }

        public bool DeCar(int id)
        {
            Car pt = c.Car.FirstOrDefault(n => n.Id == id);
            c.Car.Remove(pt);
            c.SaveChanges();
            return true;

        }
    }
}
