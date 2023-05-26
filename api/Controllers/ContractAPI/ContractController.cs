using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.Controllers.ContractAPI;
using Api.Controllers.Common;
using Api.Models;
using Api.Data;
using Api.Helper;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/contract")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _ContractService;

        private readonly IEmailService _emailService;

        public ContractController(IContractService ContractService ,  IEmailService emailService )
        {
            _ContractService = ContractService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> MakeContract([FromBody] RequestContract contractRequest)
        {
            Console.WriteLine("MakeContract start");
            Contract contract = MapRequestContractToContract(contractRequest);

            try
            {
                // Validate the request model and perform necessary business logic
                // Check if the customer has enough cash balance for conversion
                bool hasEnoughBalance = await _ContractService.CheckUserCashBalance(contract);

                Console.WriteLine("MakeContract checked CheckUserCashBalance");

                if (hasEnoughBalance)
                { 

                    bool result = await _ContractService.SubmitContract(contract);

                    Console.WriteLine("MakeContract checked SubmitContract");

                    if (result)
                        // Send the contract submission email
                        await _emailService.SendContractSubmissionEmail(contract);

                    Console.WriteLine("MakeContract checked SendContractSubmissionEmail");

                    return Ok("Contract submission processed successfully");
                }
                else
                {
                    return BadRequest("Insufficient cash balance for conversion");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ExecuteContract([FromBody] RequestContract contractRequest)
        {

            Contract contract = MapRequestContractToContract(contractRequest);

            try 
            {
                
                if (contract == null)
                {
                    throw new Exception("Contract Resquest cannot be found.");
                }

                bool result = await _ContractService.ApproveCashBalance(contract);
                 
                if (result) 
                {
                    bool markExecuted  = await _ContractService.MarkContractAsExecuted(contract.ContractID);

                    if (!markExecuted)
                        throw new Exception("MarkContractAsExecuted has an error");
                }

                return Ok("Contract execution processed successfully");

            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }                
        }

        
        private Contract MapRequestContractToContract(RequestContract requestContract)
        {
            Console.WriteLine("MapRequestContractToContract start" );
            var contract = new Contract
            {                
                ContractDate = DateTime.Now, // Set the ContractDate to the current date and time
                Status = Constant.SubmittedStatus, // Set the Status to "SUBMITTED"
                FundFromAmount = requestContract.FundFromAmount,
                ConvertToAmount = requestContract.ConvertToAmount,
                ExchangeRate = requestContract.ExchangeRate
            };

            // Create the User object
            var user = new User
            {
                UserId = requestContract.UserID
            };


            // Set the related objects in the Contract
            contract.User = user;
            contract.ConvertToCurrency = requestContract.ConvertToCurrency;
            contract.FundFromCurrency = requestContract.FundFromCurrency;

            return contract;
        }
    }


}
