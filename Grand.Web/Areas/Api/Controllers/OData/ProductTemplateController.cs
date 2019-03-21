﻿using Grand.Api.Interfaces;
using Grand.Services.Security;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Grand.Web.Areas.Api.Controllers.OData
{
    public partial class ProductTemplateController : BaseODataController
    {
        private readonly ICommonApiService _commonApiService;
        private readonly IPermissionService _permissionService;

        public ProductTemplateController(ICommonApiService commonApiService, IPermissionService permissionService)
        {
            _commonApiService = commonApiService;
            _permissionService = permissionService;
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            if (!_permissionService.Authorize(PermissionSystemName.Maintenance))
                return Forbid();

            var store = _commonApiService.GetProductMessageTemplate().FirstOrDefault(x => x.Id == key);
            if (store == null)
                return NotFound();

            return Ok(store);
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            if (!_permissionService.Authorize(PermissionSystemName.Maintenance))
                return Forbid();

            return Ok(_commonApiService.GetProductMessageTemplate());
        }
    }
}
