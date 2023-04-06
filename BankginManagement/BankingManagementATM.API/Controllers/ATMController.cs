using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementATM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        #region Private Members and CTOR
        private readonly IATMService _service;

        public ATMController(IATMService service)
        {
            _service = service;
        } 
        #endregion
    }
}
