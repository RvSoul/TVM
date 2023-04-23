using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.Car;
using TVM.Model.CM;

namespace TVM.IDomian.IDM
{
    public interface ICarDM
    {
        bool AddCar(CarModel model);

        List<CarDTO> GetCarList(Request_Car dto, out int count);

        bool UpCar(CarModel model);

        bool DeCar(int id);
    }
}
