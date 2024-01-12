using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Responses;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    public class BaseService<T> : IBaseService<T> where T : DomainObject
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public BaseService(IDbContextFactory<EvaDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public virtual async Task<PagedResultViewModel<T>> GetAllAsync(BaseRequestViewModel request)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var rawData = await context.Set<T>().ToListAsync();
                var filteredData = rawData.ApplyBaseRequest(request);
                var totalRecords = rawData.Count();
                return new PagedResultViewModel<T>()
                {
                    Data = filteredData,
                    Pagination = request.PaginationRequest.ToPagination(totalRecords)
                };
            }
        }

        public virtual async Task<SingleResultViewModel<T>> GetByIdAsync(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                    throw new CrudException<T>(string.Format("{0} not found", typeof(T).Name), BaseOperations.GetById);
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("Id: {0}, {1}", id.ToString(), entity.ToString()))
                };
            }
        }

        public virtual async Task<SingleResultViewModel<T>> InsertAsync(T entity)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                if (entity == null)
                    throw new CrudException<T>(string.Format("Sorry, can not insert null {0} entity", typeof(T).Name), BaseOperations.Insert);
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} inserted successfully", typeof(T).Name))
                };
            }
        }

        public virtual async Task<SingleResultViewModel<T>> UpdateAsync(T entity)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var currentEntity = await context.Set<T>().Where(e => e.Id == entity.Id).FirstOrDefaultAsync();
                if (currentEntity == null)
                    throw new CrudException<T>(string.Format("{0} not found", typeof(T).Name), BaseOperations.Update);
                context.Entry(currentEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} updated successfully", typeof(T).Name))
                };
            }
        }
        public virtual async Task<SingleResultViewModel<T>> DeleteAsync(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                    throw new CrudException<T>(string.Format("{0} not found", typeof(T).Name), BaseOperations.Delete);
                context.Remove(entity);
                await context.SaveChangesAsync();
                return new SingleResultViewModel<T>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} deleted successfully", typeof(T).Name))
                };
            }
        }
    }
}
