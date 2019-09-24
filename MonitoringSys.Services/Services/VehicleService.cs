using Microsoft.AspNetCore.Mvc.Rendering;
using MonitoringSys.Models;
using MonitoringSys.Models.ModelsDTO;
using MonitoringSys.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MonitoringSys.Services
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        Task<VehicleListDTO> GetVehiclesDTO(int? custId);
        Task<bool> UpdateVehicleStatus(int id, bool IsResponse);
    }
    public class VehicleService : BaseService<Vehicle>, IVehicleService
    {

        private readonly IBaseService<Customer> _CustomerService; 
        private readonly IBaseService<VehicleLog> _vehicleLogService; 

        public VehicleService(IUnitOfWork unitOfWork,IBaseService<Customer> customerService,IBaseService<VehicleLog> vehicleLogService) : base(unitOfWork)
        {
            _CustomerService = customerService;
            _vehicleLogService = vehicleLogService;
        }


        public override async Task<bool> Add(Vehicle entity)
        {
            return await base.Add(entity);
        }

        public async Task<VehicleListDTO> GetVehiclesDTO(int? custId)
        {
            var _Vehicles = GetAll(a =>
                    custId == null ? true : a.CustomerId == custId, // Filter
                    a => a.Customer, a => a.LastVehicleLog ) //Include
                .Result.ToList();

            return _ = new VehicleListDTO()
            {
                FilterCustomerId = custId,
                FilterCustomers = new SelectList(await _CustomerService.GetAll(), "Id", "Name"),
                Vehicles = new List<VehicleDTO>(_Vehicles.Select(a=>
                        new VehicleDTO()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Number = a.Number,
                            LastVehicleLogId = a.LastVehicleLogId,

                            LastVehicleLogDateTime = a.LastVehicleLog?.UpdatedTime ?? null,
                            LastVehicleLogResponse = a.LastVehicleLog?.IsResponse ?? null,

                            CustomerId = a.CustomerId,
                            CustomerName = a.Customer.Name,
                        })),
            };
        }


        public async Task<bool> UpdateVehicleStatus(int id, bool IsResponse)
        {
            if (!Any(id))
                return false;


            var _Vehicle = await Get(id, a => a.LastVehicleLog);

            //Check Valid Response From The Vehicle Ping Request  ...
            //IsResponse = RequestPingVehicle(_Vehicle.Id);

            //Get Last Status ...


            // New Status For Vehicle if no logs or Different Status ...
            if (_Vehicle.LastVehicleLogId == null || _Vehicle.LastVehicleLog.IsResponse != IsResponse)
            {
                var _NewStatus = new VehicleLog()
                {
                    IsResponse = IsResponse,
                    UpdatedTime = DateTime.Now,
                    VehicleId = _Vehicle.Id,
                };
                await _vehicleLogService.Add(_NewStatus);

                //Ref to last Id
                _Vehicle.LastVehicleLogId = _NewStatus.Id;
                return await Update(_Vehicle);
            }
            else // Update time ...
            {
                var _LastLog = await _vehicleLogService.Get(_Vehicle.LastVehicleLogId);
                _LastLog.UpdatedTime = DateTime.Now;
                return await _vehicleLogService.Update(_LastLog);
            }

        }

        private bool RequestPingVehicle(int id)
        {
            /// Logic Ping For Vehicles ...........
            if (id != -1)
                return true;

            return false;
        }


    }
}
