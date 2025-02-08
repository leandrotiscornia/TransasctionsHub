using _011Global.Shared.JobsServiceDBContext.Entities;
using _011Global.Shared.JobsServiceDBContext.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using _011Global.CustomerAPIService.DTO_s;


namespace _011Global.CustomerAPIService.Controllers
{
    [ApiController]
    [Route("Customer")]
    public class GlobalCustomerController : ControllerBase
    {
        
        private readonly ILogger<GlobalCustomerController> _logger;
        private readonly IJobsServiceRepository _jobsServiceRepository;
        public GlobalCustomerController
            (ILogger<GlobalCustomerController> logger, IJobsServiceRepository jobsServiceRepository)
        {
            _logger = logger;
            _jobsServiceRepository = jobsServiceRepository;
        }
        /// <summary>
        /// Http method to enrol a new non existent customer, defining a subscription plan
        /// </summary>
        /// <param name="globalCustomer">json containing the customer superclass</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Enrol")]
        public async Task<ActionResult> EnrolCustomer([FromBody] GlobalCustomerDTO globalCustomer)
        {
            try
            {
                GlobalCustomerMapper mapper = new GlobalCustomerMapper(globalCustomer);
                GlobalCustomer gc = await mapper.Map();
                
                await _jobsServiceRepository.EnrolCustomer(gc);
                _logger.LogInformation("Customer successfully enrolled");
                return Ok(globalCustomer);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Http method to unsubscribe a selected customer, changing it's state. This does not delete customer's data.
        /// </summary>
        /// <param name="customerId">Id of the customer to be unsubscribed</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Unsubscribe")]
        public ActionResult UnsubscribeCustomer(int customerId)
        {
            try
            {
                _jobsServiceRepository.UnsubscribeCustomer(customerId);
                _logger.LogInformation($"Customer {customerId} successfully unsubscribed from service");
                return Ok($"Customer {customerId} successfully unsubscribed from service");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
    }
}
