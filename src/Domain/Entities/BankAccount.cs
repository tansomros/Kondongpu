using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class BankAccount : BaseEntity
{

    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public string? Branch { get; set; }
    public int? BankId { get; set; }
    public Bank? Bank { get; set; }

public BankAccount(string accountNumber, string accountName)
    {
        AccountNumber = accountNumber;
        AccountName = accountName;        
    }
}
