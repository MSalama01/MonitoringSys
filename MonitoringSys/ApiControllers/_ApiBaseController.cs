using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonitoringSys.Models;
using MonitoringSys.Services;

namespace MonitoringSys.ApiControllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiBaseController<TEntity> : ControllerBase where TEntity : class, IBaseEntity
    {

        private readonly IBaseService<TEntity> _baseService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseService"></param>
        protected ApiBaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        #region APIs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAll")]
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _baseService.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Get")]
        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await _baseService.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("Insert")]
        public virtual async Task<bool> PostAsync([FromBody]TEntity entity)
        {
            return await _baseService.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut, Route("Update")]
        public virtual async Task<bool> PutAsync([FromBody]TEntity entity)
        {
            return await _baseService.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete, Route("Delete")]
        public virtual async Task<bool> DeleteAsync([FromBody]TEntity entity)
        {
            return await _baseService.Delete(entity);
        }

        /// <summary>
        /// Check If the Entity Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("IsExisted")]
        public virtual bool CheckIfExist([FromQuery]object id)
        {
            return _baseService.Any(id);
        }
        #endregion
    }
}
