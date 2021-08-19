using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/customerGroups")]
    [ApiController]
    public class CustomerGroupsController : BaseController<CustomerGroup>
    {
        #region Constructors
        public CustomerGroupsController(ICustomerGroupService customerGroupService):base(customerGroupService)
        {

        }
        #endregion
    }
}
