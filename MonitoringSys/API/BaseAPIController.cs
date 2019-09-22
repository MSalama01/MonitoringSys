using Microsoft.AspNetCore.Mvc;
using MonitoringSys.Models;
using MonitoringSys.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSys.API
{
    [Route("api/[controller]")]
    public abstract class BaseAPIController<TEntity> : ControllerBase where TEntity : class, IBaseEntity
    {
        protected readonly IService<TEntity> _service;

        protected BaseAPIController(IService<TEntity> service)
        {
            _service = service;
        }

        [HttpGet, Route("GetAll")]
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet, Route("Get")]
        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await _service.Get(id);
        }

        [HttpPost, Route("Insert")]
        public virtual async Task<bool> PostAsync([FromBody]TEntity entity)
        {
            return await _service.Add(entity);
        }

        [HttpPut, Route("Update")]
        public virtual async Task<bool> PutAsync([FromBody]TEntity entity)
        {
            return await _service.Update(entity);
        }

        [HttpDelete, Route("Delete")]
        public virtual async Task<bool> Delete([FromBody]TEntity entity)
        {
            return await _service.Delete(entity);
        }

        [HttpPost, Route("IsExisted")]
        public virtual bool CheckIfExist([FromQuery]object id)
        {
            return _service.Any(id);
        }
    }
}
