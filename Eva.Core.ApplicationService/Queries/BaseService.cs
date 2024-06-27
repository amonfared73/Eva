using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Responses;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.Tools.Extensions;
using Eva.Infra.Tools.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    public class BaseService<TModel, TViewModel> : IBaseService<TModel, TViewModel> where TModel : ModelBase where TViewModel : ViewModelBase
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public BaseService(IEvaDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public virtual async Task<PagedResultViewModel<TModel>> GetAllAsync(BaseRequestViewModel request)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Set<TModel>().ToPagedResultViewModelAsync(request);
            }
        }

        public virtual async Task<SingleResultViewModel<TModel>> GetByIdAsync(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<TModel>().Where(e => e.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                    throw new CrudException<TModel>(string.Format("{0} not found", typeof(TModel).Name), BaseOperations.GetById);
                return new SingleResultViewModel<TModel>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("Id: {0}, {1}", id.ToString(), entity.ToString()))
                };
            }
        }

        public virtual async Task<ActionResultViewModel<TModel>> InsertAsync(TModel entity)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                if (entity == null)
                    throw new CrudException<TModel>(string.Format("Sorry, can not insert null {0} entity", typeof(TModel).Name), BaseOperations.Insert);
                await context.Set<TModel>().AddAsync(entity);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<TModel>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} inserted successfully", typeof(TModel).Name))
                };
            }
        }

        public virtual async Task<ActionResultViewModel<TModel>> UpdateAsync(TModel entity)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var currentEntity = await context.Set<TModel>().Where(e => e.Id == entity.Id).FirstOrDefaultAsync();
                if (currentEntity == null)
                    throw new CrudException<TModel>(string.Format("{0} not found", typeof(TModel).Name), BaseOperations.Update);
                context.Entry(currentEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<TModel>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} updated successfully", typeof(TModel).Name))
                };
            }
        }
        public virtual async Task<ActionResultViewModel<TModel>> DeleteAsync(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<TModel>().Where(e => e.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                    throw new CrudException<TModel>(string.Format("{0} not found", typeof(TModel).Name), BaseOperations.Delete);
                context.Remove(entity);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<TModel>()
                {
                    Entity = entity,
                    ResponseMessage = new ResponseMessage(string.Format("{0} deleted successfully", typeof(TModel).Name))
                };
            }
        }

        public virtual async Task<CustomResultViewModel<byte[]>> ToByte(int id)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var entity = await context.Set<TModel>().Where(e => e.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                    throw new CrudException<TModel>(string.Format("{0} not found", typeof(TModel).Name), BaseOperations.GetById);
                return new CustomResultViewModel<byte[]>()
                {
                    Entity = entity.ToBytes(),
                    ResponseMessage = new ResponseMessage(string.Format("Id: {0}, {1}", id.ToString(), entity.ToString()))
                };
            }
        }

        public virtual Task<CustomResultViewModel<IEnumerable<TViewModel>>> ImportFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
