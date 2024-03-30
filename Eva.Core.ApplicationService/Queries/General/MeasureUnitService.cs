﻿using Eva.Core.ApplicationService.Services.General;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.ViewModels.General;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries.General
{
    [RegistrationRequired]
    public class MeasureUnitService : BaseService<MeasureUnit, MeasureUnitViewModel>, IMeasureUnitService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public MeasureUnitService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
