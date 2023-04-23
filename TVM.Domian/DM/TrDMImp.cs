using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.ModelData;
using TVM.IDomian.IDM;
using TVM.Model;
using TVM.Model.CM;
using TVM.DTO.Tr;
using TVM.DTO.Car;
using TVM.DTO.Authorization;
using System.ComponentModel.DataAnnotations;

namespace TVM.Domian.DM
{
    public class TrDMImp : ITrDM
    {
        private readonly TVMContext c;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public TrDMImp(TVMContext context)
        {
            c = context;
        }

        public bool GetDataInfo(AuthorizationDTO dto)
        {
            Car car = c.Car.Where(w => w.PlateNumber == dto.PlateNumber).FirstOrDefault();
            if (car == null)
            {
                throw new Exception("车牌号不存在！");
            }
            Scale scale = c.Scale.Where(w => w.Id == dto.ClassName).FirstOrDefault();
            if (scale == null)
            {
                throw new Exception("衡不存在！");
            }

            int scaleNum = c.Scale.Where(w => w.Type == 1 && w.State == 1).Count();
            List<DateTime> timeLi = c.TransportationRecords.OrderByDescending(px => px.STime).Take(scaleNum).Select(x => x.STime).ToList();


            TransportationRecords oldTr = c.TransportationRecords.Where(w => timeLi.Contains(w.STime) && w.CarId == car.Id && w.CollieryCode == dto.CollieryCode).FirstOrDefault();
            if (oldTr != null)
            {
                #region 有上一趟数据
                if (oldTr.ETime == null && oldTr.TareWeight == null && oldTr.NetWeight == null)
                {
                    #region 未记录皮重（未轻衡称重）
                    if (scale.Type == 1)
                    {
                        #region 重衡
                        throw new Exception("有上一趟数据/未轻衡称重/重衡：异常还是重新称重！");
                        #endregion
                    }
                    else
                    {
                        #region 轻衡
                        ScalageRecords sr = new ScalageRecords();
                        sr.Id = Guid.NewGuid().ToString();
                        sr.ScaleId = dto.ClassName;
                        sr.TId = oldTr.Id;
                        sr.Weigh = dto.Weight;
                        sr.AddTime = DateTime.Now;
                        c.ScalageRecords.Add(sr);
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 已记录皮重（已轻衡称重）
                    if (scale.Type == 1)
                    {
                        #region 重衡
                        throw new Exception("有上一趟数据/已轻衡称重/重衡：异常！");
                        #endregion
                    }
                    else
                    {
                        #region 轻衡
                        throw new Exception("有上一趟数据/已轻衡称重/轻衡：异常还是重新称重！");
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region 没有有上一趟数据
                if (scale.Type == 1)
                {
                    #region 重衡
                    TransportationRecords tr = new TransportationRecords();
                    tr.Id = Guid.NewGuid().ToString();
                    tr.CarId = car.Id;
                    tr.CollieryCode = dto.CollieryCode;
                    tr.RoughWeight = dto.Weight;
                    tr.STime = DateTime.Now;
                    tr.IsUpload = 1;
                    tr.State = 1;

                    ScalageRecords sr = new ScalageRecords();
                    sr.Id = Guid.NewGuid().ToString();
                    sr.ScaleId = dto.ClassName;
                    sr.TId = tr.Id;
                    sr.Weigh = dto.Weight;
                    sr.AddTime = DateTime.Now;

                    if (dto.inX == 1)
                    {
                        tr.State = 2;
                        AbnormalRecords ar = new AbnormalRecords();
                        ar.Id = Guid.NewGuid().ToString();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "入口光幕阻挡";
                        c.AbnormalRecords.Add(ar);
                    }
                    if (dto.outX == 1)
                    {
                        tr.State = 2;
                        AbnormalRecords ar = new AbnormalRecords();
                        ar.Id = Guid.NewGuid().ToString();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "出口光幕阻挡";
                        c.AbnormalRecords.Add(ar);
                    }
                    if (dto.Error != 0)
                    {
                        tr.State = 2;
                        AbnormalRecords ar = new AbnormalRecords();
                        ar.Id = Guid.NewGuid().ToString();
                        ar.TId = tr.Id;
                        ar.AbnormalCause = "错误码错误";
                        c.AbnormalRecords.Add(ar);
                    }

                    c.TransportationRecords.Add(tr);
                    c.ScalageRecords.Add(sr);
                    #endregion
                }
                else
                {
                    #region 轻衡
                    throw new Exception("没有有上一趟数据/轻衡：异常！");
                    #endregion
                }
                #endregion
            }

            c.SaveChanges();
            return true;
        }

        public List<TransportationRecordsDTO> GetTransportationRecordsList(Request_TransportationRecords dto, out int count)
        {
            Expression<Func<TransportationRecords, bool>> expr = AutoAssemble.Splice<TransportationRecords, Request_TransportationRecords>(dto);
            if (dto.State != null)
            {
                expr = expr.And2(w => w.State == (int)dto.State);
            }


            count = c.TransportationRecords.Where(expr).Count();
            List<TransportationRecords> li = c.TransportationRecords.Where(expr).OrderByDescending(px => px.STime).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return GetMapperDTO.GetDTOList<TransportationRecords, TransportationRecordsDTO>(li);
        }


        public bool AddTransportationRecords(TransportationRecordsModel model)
        {
            TransportationRecords pt = GetMapperDTO.SetModel<TransportationRecords, TransportationRecordsModel>(model);


            return true;

        }

        public bool UpTransportationRecords(TransportationRecordsModel model)
        {
            TransportationRecords pt = c.TransportationRecords.FirstOrDefault(n => n.Id == model.Id);

            c.SaveChanges();
            return true;
        }

        public bool DeTransportationRecords(string id)
        {
            TransportationRecords pt = c.TransportationRecords.FirstOrDefault(n => n.Id == id);
            c.TransportationRecords.Remove(pt);
            c.SaveChanges();
            return true;

        }


    }
}
