namespace Api.Controllers.ContractAPI;

using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Controllers.Common;
using Api.Data;
using Api.Helper;

public interface IContractService
{

    Task<bool> MarkContractAsExecuted(int contractId);
    Task<bool> CheckUserCashBalance(Contract contractRequest);

    Task<bool> ApproveCashBalance(Contract contractRequest);
    Task<bool> SubmitContract(Contract contractRequest);
}

public class ContractService : IContractService
{
    private readonly ApplicationDbContext _dbContext;

    public ContractService(ApplicationDbContext dbContext)
    {     
        _dbContext = dbContext;
    }

    public async Task<bool> SubmitContract(Contract contract)
    {
        Console.WriteLine("SubmitContract start");
        try
        {
            // Retrieve the user's cash balance from the database based on the user ID and currency ID
            UserCashBalance userCashBalance = await _dbContext.UserCashBalance
            .FirstOrDefaultAsync(c => c.UserID == contract.User.UserId 
            && c.Currency == contract.FundFromCurrency);
            
            if (userCashBalance==null)
                throw new Exception("Error userCashBalance not found.");            

            userCashBalance.OnHold += contract.FundFromAmount;
            userCashBalance.CashBalance -= contract.FundFromAmount;

            // Retrieve the existing User from the database using UserID
            var existingUser = await _dbContext.Users.FindAsync(contract.User.UserId);

            if (existingUser==null)
                throw new Exception("Error existingUser not found.");

            // Assign the existing User to the Contract
            contract.User = existingUser;

            // Save the contract to the database
            _dbContext.Contracts.Add(contract);
            await _dbContext.SaveChangesAsync();

            return true; // Return true if the contract is successfully marked as executed
        }
        catch (Exception ex)
        {
            throw new Exception($"Error Contract not found.  {ex.Message}");
        }
    }

    public async Task<bool> MarkContractAsExecuted(int contractId)
    {
        try
        {
            // Retrieve the contract from the database
            Contract contract = await _dbContext.Contracts.FindAsync(contractId);

            if (contract != null)
            {
                // Update the contract status to "Executed"
                contract.Status = Constant.ExecutedStatus;

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();

                return true; // Return true if the contract is successfully marked as executed
            }
            else
            {
                throw new Exception("Contract not found.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error Contract not found.  {ex.Message}");
        }
}


    public async Task<bool> CheckUserCashBalance(Contract contract)
    {       

        Console.WriteLine("CheckUserCashBalance FundFromCurrency start=" + contract.FundFromCurrency);
        Console.WriteLine("CheckUserCashBalance UserID start=" + contract.User.UserId);

        // Retrieve the user's cash balance from the database based on the user ID and currency ID
        UserCashBalance userCashBalance = await _dbContext.UserCashBalance
            .FirstOrDefaultAsync(c => c.UserID == contract.User.UserId 
            && c.Currency == contract.FundFromCurrency);

        // Check if the user's cash balance is not null and if it is greater than or equal to the requested amount
        if (userCashBalance != null && userCashBalance.CashBalance >= contract.FundFromAmount)
        {
            return true; // User has enough cash balance
        }
        
        Console.WriteLine("CheckUserCashBalance has enough CashBalance=" );


        return false; // User does not have enough cash balance
    }    

    public async Task<bool> ApproveCashBalance(Contract contract)
    {
        try
        {
            UserCashBalance userCashBalanceFrom = await _dbContext.UserCashBalance
                .FirstOrDefaultAsync(c => c.UserID == contract.User.UserId && c.Currency == contract.FundFromCurrency);

            if (userCashBalanceFrom != null)
            {
                // Update the UserCashBalance's OnHold property
                userCashBalanceFrom.OnHold -= userCashBalanceFrom.OnHold - contract.FundFromAmount;
            }

            UserCashBalance userCashBalanceTo = await _dbContext.UserCashBalance
                .FirstOrDefaultAsync(c => c.UserID == contract.User.UserId && c.Currency == contract.ConvertToCurrency);

            if (userCashBalanceTo != null)
            {
                // Add the converted amount to the UserCashBalance
                userCashBalanceTo.CashBalance += contract.ConvertToAmount;
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"ApproveCashBalance   {ex.Message}");
        }
    }



}
