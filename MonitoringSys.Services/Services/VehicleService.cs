using MonitoringSys.Models;
using MonitoringSys.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringSys.Services
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        Task<bool> UpdateVehicleStatus(int id, bool IsResponse);
        VehicleStatusLog GetLastVehicleStatusUpdate(int id);
    }
    public class VehicleService : BaseService<Vehicle>, IVehicleService
    {
        private readonly IBaseRepository<VehicleStatusLog> _VehicleStatusUpdateRepository;
        public VehicleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _VehicleStatusUpdateRepository = unitOfWork.GetRepository<VehicleStatusLog>();
        }

        public override Task<bool> Add(Vehicle entity)
        {
            // Initial Status NULL
            entity.VehicleStatus = new VehicleStatus()
            {
                LastVehicleStatusLogId = null,
            };

            return base.Add(entity);
        }

        public VehicleStatusLog GetLastVehicleStatusUpdate(int id)
        {
            return _VehicleStatusUpdateRepository.Get(a => a.VehicleStatusId == id).Result;
        }

        bool RequestPingVehicle(int id)
        {
            return true;
        }


        public async Task<bool> UpdateVehicleStatus(int id, bool IsResponse)
        {
            if (!Any(id))
                return false;


            var _Vehicle = await Get(id, a => a.VehicleStatus);

            //Check Valid Response From The Vehicle Ping Request  ...
            IsResponse = RequestPingVehicle(_Vehicle.Id);

            //Get Last Status ...
            VehicleStatusLog LastStatus =
                _Vehicle.VehicleStatus.LastVehicleStatusLogId == null ? null :
                    GetLastVehicleStatusUpdate((int)_Vehicle.VehicleStatus.LastVehicleStatusLogId);

            // New Status For Vehicle ...
            if (LastStatus == null || LastStatus.IsResponse != IsResponse)
            {
                _Vehicle.VehicleStatus.VehicleStatusLogs.Add(new VehicleStatusLog()
                {
                    IsResponse = IsResponse,
                    UpdatedTime = DateTime.Now,
                    VehicleStatusId = _Vehicle.VehicleStatus.Id
                });

                _Vehicle.VehicleStatus.LastVehicleStatusLogId = _Vehicle.VehicleStatus.VehicleStatusLogs.LastOrDefault().Id;
            }

            return true;

        }
    }
}
