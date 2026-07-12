using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class Price : BaseEntity
{
    public int PriceYear { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public int CaneTypeId { get; set; }
    public CaneType? CaneType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double UnitPrice { get; set; }
    public int? CUser { get; set; }
    public int? MUser { get; set; }

    public Price(int priceYear,int companyId,int caneTypeId,double unitPrice)
    {
        PriceYear = priceYear;
        CompanyId = companyId;
        CaneTypeId = caneTypeId;
        UnitPrice = unitPrice;        
    }
}
