using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class Bill : BaseEntity
{
    public string BillNumber { get; set; }
    public DateTime BillDate { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public string CompanyCode { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public double? TotalWeight { get; set; }

    public double? TotalNetPrice { get; set; }

    public double? TotalDeduct { get; set; }

    public double? Balance { get; set; }

    public int? CUser { get; set; }

    public int? MUser { get; set; }

    public string? Remark { get; set; }
    public Bill(string billNumber,DateTime billDate,int companyId,string companyCode,int customerId)
    {
        BillNumber = billNumber;
        BillDate = billDate;
        CompanyId = companyId;
        CompanyCode = companyCode;
        CustomerId = customerId;        
    }
}
