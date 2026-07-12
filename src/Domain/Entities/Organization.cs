using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class Organization
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string? LanguageCode { get; set; }
    public string Name { get; set; }
    public string? VatId { get; set; }
    public string? AddressNumber { get; set; }
    public string? Moo { get; set; }
    public string? Village { get; set; }
    public string? SubDistrict { get; set; }
    public string? District { get; set; }
    public string? Province { get; set; }
    public string? ZipCode { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? LogoPath { get; set; }
    public string? AuthCode { get; set; }
   
    public Organization(int id,string code,string languageCode, string name,string vatId,string addressNumber,string moo,string village,string subDistrict,string district,string province,string zipCode,string telephone,string fax ,string email,string website,string logoPath,string authCode)
    {
        Id = id;
        Code = code;
        LanguageCode = languageCode;
        Name = name;
        VatId = vatId;
        AddressNumber = addressNumber;
        Moo = moo;
        Village = village;
        SubDistrict = subDistrict;
        District = district;
        Province = province;
        ZipCode = zipCode;
        Telephone = telephone;
        Fax = fax;
        Email = email;
        Website = website;
        LogoPath = logoPath;
        AuthCode = authCode;        
    }
}
