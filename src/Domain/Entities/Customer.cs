using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class Customer : BaseEntity
{
    public string Code { get; set; }

    public int PrefixId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? NickName { get; set; }

    public int? Age { get; set; }

    public string? Sex { get; set; }

    public string? CardId { get; set; }

    /// <summary>
    /// เลขทะเบียนชาวไร่
    /// </summary>
    public string? FamerId { get; set; }

    public string? Tel { get; set; }

    public string? AddressNo { get; set; }

    public string? Moo { get; set; }

    public string? Village { get; set; }

    public string? SubDistrict { get; set; }

    public string? District { get; set; }

    public string? ProvinceId { get; set; }
    public Province? Province { get; set; }
    public string? Zipcode { get; set; }

    public int? CUser { get; set; }

    public int? MUser { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountName { get; set; }

    public int? BankId { get; set; }
    public Bank? Bank { get; set; }

    public Customer(string code,int prefixId,string firstName,string lastName)
    {
        Code = code;
        PrefixId = prefixId;
        FirstName = firstName;
        LastName = lastName;
        
    }
}
