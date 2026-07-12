using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class Company : BaseEntity
{ 
    public string Code { get; set; }
    public string Name { get; set; }
    public string? AliasName { get; set; }
    public string OwnerName { get; set; }
    public string? VatId { get; set; }
    public string? AddressNo { get; set; }
    public string? Moo { get; set; }
    public string? Village { get; set; }
    public string? SubDistrict { get; set; }
    public string? District { get; set; }
    public string? ProvinceId { get; set; }
    public Province? Province { get; set; }
    public string? ZipCode { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public int? CUser { get; set; }
    public int? MUser { get; set; }

    public Company(string code, string name,string ownerName)
    {
        Code = code;
        Name = name;
        OwnerName = ownerName;
    }

}
