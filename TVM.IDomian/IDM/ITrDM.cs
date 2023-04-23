using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.Authorization;
using TVM.DTO.Car;
using TVM.DTO.Tr;
using TVM.Model.CM;

namespace TVM.IDomian.IDM
{
    public interface ITrDM
    {

        List<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto, out int count);
        bool AddTransportationRecords(TransportationRecordsModel model);

        bool UpTransportationRecords(TransportationRecordsModel model);

        bool DeTransportationRecords(string id);
        bool GetDataInfo(AuthorizationDTO dto );
    }
}
