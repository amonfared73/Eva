﻿using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class RoleController : EvaControllerBase<Role>
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service) : base(service)
        {
            _service = service;
        }
        [NonAction]
        public override Task<ActionResultViewModel<Role>> InsertAsync(Role entity)
        {
            return base.InsertAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Role>> CreateRole(string name)
        {
            return await _service.CreateRole(name);
        }
    }
}
