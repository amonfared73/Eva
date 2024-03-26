﻿using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries.Inv
{
    [RegistrationRequired]
    public class InventoryDocumentHeaderService : BaseService<InventoryDocumentHeader, InventoryDocumentHeaderViewModel>, IInventoryDocumentHeaderService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public InventoryDocumentHeaderService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
