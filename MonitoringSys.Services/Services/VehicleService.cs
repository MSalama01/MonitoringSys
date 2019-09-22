using MonitoringSys.Models;
using MonitoringSys.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringSys.Services
{
    public interface IVehicleService:IService<Vehicle>
    {
        Task<bool> UpdateVehicleStatus(int id, bool IsResponse);
        Task<VehicleStatusUpdate> GetLastVehicleStatusUpdate(int id);
    }
    class VehicleService : Service<Vehicle>, IVehicleService
    {
        private readonly IRepository<VehicleStatusUpdate> _VehicleStatusUpdateRepository;
        public VehicleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _VehicleStatusUpdateRepository = unitOfWork.GetRepository<VehicleStatusUpdate>();
        }

        public override Task<bool> Add(Vehicle entity)
        {
            // Initial Status NULL
            entity.VehicleStatus = new VehicleStatus()
            {
                LastVehicleStatusUpdateId = null,
            };

            return base.Add(entity);
        }

        public async Task<VehicleStatusUpdate> GetLastVehicleStatusUpdate(int id)
        {
            return await _VehicleStatusUpdateRepository.Get(a => a.VehicleStatusId == id);
        }

        bool RequestPingVehicle(int id)
        {
            return true;
        }


        public async Task<bool> UpdateVehicleStatus(int id, bool IsResponse)
        {
            if (!Any(id))
                return false;


            var _Vehicle = await Get(id);

            //Check Valid Response From The Vehicle Ping Request  ...
            IsResponse = RequestPingVehicle(_Vehicle.Id);

            //Get Last Status ...
            var LastStatus = _Vehicle.VehicleStatus.LastVehicleStatusUpdateId is null ? null :
                await GetLastVehicleStatusUpdate((int)_Vehicle.VehicleStatus.LastVehicleStatusUpdateId);

            // New Status For Vehicle ...
            if (LastStatus == null || LastStatus.IsResponse != IsResponse)
            {
                _Vehicle.VehicleStatus.VehicleStatusUpdates.Add(new VehicleStatusUpdate()
                {
                    IsResponse = IsResponse,
                    UpdatedTime = DateTime.Now,
                    VehicleStatusId = _Vehicle.VehicleStatus.Id
                });

                _Vehicle.VehicleStatus.LastVehicleStatusUpdateId = _Vehicle.VehicleStatus.VehicleStatusUpdates.LastOrDefault().Id;
            }

            return true;

        }
    }
}
