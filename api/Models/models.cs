using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
        
    // Contracts entity
    public class Contract
    {
        public int ContractID { get; set; }
        public DateTime ContractDate { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public string ConvertToCurrency { get; set; }
        public string FundFromCurrency { get; set; }

        public EmailNotifications EmailNotifications { get; set; }

        public decimal FundFromAmount { get; set; }
        public decimal ConvertToAmount { get; set; }
        public decimal ExchangeRate { get; set; }
    }

    public class RequestContract
    {

        public int UserID { get; set; }
        public string ConvertToCurrency { get; set; }
        public string FundFromCurrency { get; set; }

        public decimal FundFromAmount { get; set; }
        public decimal ConvertToAmount { get; set; }
        public decimal ExchangeRate { get; set; }
    }    

    public class UserBase 
    {
   

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

    }    

    public class Admin : UserBase
    {
        [Key]
        public int AdminID { get; set; }

        public string Role { get; set; }

        public DateTime AddedDate { get; set; }
        
    }


    public class User : UserBase
    {

        [Key]
        public int UserId { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<UserCashBalance> UserCashBalances { get; set; }
    }    

    // public class User1
    // {
    //     [Key]
    //     public int UserID { get; set; }
        
    //     [Required]
    //     public string FirstName { get; set; }
        
    //     [Required]
    //     public string LastName { get; set; }
        
    //     [Required]
    //     public string Email { get; set; }
        
    //     [Required]
    //     public string Password { get; set; }
        
    //     public bool IsActive { get; set; }
        
        
    //     // public virtual ICollection<UserVerification> UserVerifications { get; set; }
    //     // public virtual ICollection<UserRolesMapping> UserRolesMappings { get; set; }
    //     // public virtual ICollection<UserAuthenticationFactor> UserAuthenticationFactors { get; set; }

    // }
    
    // public class UserVerification
    // {
    //     [Key]
    //     public int UserVerificationID { get; set; }
        
    //     [ForeignKey("User")]
    //     public int UserID { get; set; }
        
    //     [Required]
    //     public string VerificationCode { get; set; }
        
    //     public bool IsVerified { get; set; }
        
    //     public virtual User User { get; set; }
    // }
    
    // public class UserRoles
    // {
    //     [Key]
    //     public int RoleID { get; set; }
        
    //     [Required]
    //     public string RoleName { get; set; }
        
    //     public virtual ICollection<UserRolesMapping> UserRolesMappings { get; set; }
    // }
    
    // public class UserRolesMapping
    // {
    //     [Key]
    //     public int UserRolesMappingID { get; set; }
        
    //     [ForeignKey("User")]
    //     public int UserID { get; set; }
        
    //     [ForeignKey("UserRoles")]
    //     public int RoleID { get; set; }
        
    //     public virtual User User { get; set; }
    //     public virtual UserRoles UserRoles { get; set; }
    // }
    
    // public class UserAuthenticationFactor
    // {
    //     [Key]
    //     public int UserAuthenticationFactorID { get; set; }
        
    //     [ForeignKey("User")]
    //     public int UserID { get; set; }
        
    //     [Required]
    //     public string FactorType { get; set; }
        
    //     [Required]
    //     public string FactorValue { get; set; }
        
    //     public bool IsVerified { get; set; }
        
    //     public virtual User User { get; set; }
    // }
    
    // public class Currencies
    // {
    //     [Key]
    //     public int CurrencyID { get; set; }
        
    //     [Required]
    //     public string CurrencyCode { get; set; }
        
    //     [Required]
    //     public string CurrencyName { get; set; }
        
    //     public string Symbol { get; set; }
        
    //     public string Country { get; set; }
        
    //     public bool IsActive { get; set; }
        
    //     public virtual ICollection<Contract> ContractsConvertTo { get; set; }
    //     public virtual ICollection<Contract> ContractsFundFrom { get; set; }
    //     public virtual ICollection<UserCashBalance> UserCashBalances { get; set; }
    // }

    // UserCashBalance entity
    public class UserCashBalance
    {
        public int BalanceID { get; set; }
        public int UserID { get; set; }
        // public int CurrencyID { get; set; }
        public decimal CashBalance { get; set; }
        public decimal OnHold { get; set; }
        
        public User User { get; set; }
        public string Currency { get; set; }

    }

    // EmailNotifications entity
    public class EmailNotifications
    {
        public int NotificationID { get; set; }
        public int ContractID { get; set; }
        public string RecipientEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public DateTime EmailSentDate { get; set; }
        public bool IsEmailDelivered { get; set; }
        public string SenderEmail { get; set; }
        public DateTime EmailDeliveredDate { get; set; }
        public string EmailStatus { get; set; }

        public Contract Contract { get; set; }
    }
}