using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonitoringSys.Models;
using MonitoringSys.Services;

namespace MonitoringSys.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBaseController<TEntity> : ControllerBase where TEntity : class, IBaseEntity
    {

        private readonly IBaseService<TEntity> _baseService;

        protected ApiBaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        #region APIs
        [HttpGet, Route("GetAll")]
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _baseService.GetAll();
        }

        [HttpGet, Route("Get")]
        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await _baseService.Get(id);
        }

        [HttpPost, Route("Insert")]
        public virtual async Task<bool> PostAsync([FromBody]TEntity entity)
        {
            return await _baseService.Add(entity);
        }

        [HttpPut, Route("Update")]
        public virtual async Task<bool> PutAsync([FromBody]TEntity entity)
        {
            return await _baseService.Update(entity);
        }

        [HttpDelete, Route("Delete")]
        public virtual async Task<bool> DeleteAsync([FromBody]TEntity entity)
        {
            return await _baseService.Delete(entity);
        }

        [HttpPost, Route("IsExisted")]
        public virtual bool CheckIfExist([FromQuery]object id)
        {
            return _baseService.Any(id);
        }
        #endregion
    }
}
