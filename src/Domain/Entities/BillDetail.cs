using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class BillDetail : BaseEntity
{   
    public string BillNumber { get; set; }
    public string? BillReference { get; set; }
    public string? CarNumber { get; set; }
    public DateTime? SendDate { get; set; }     
    public int CaneTypeId { get; set; }
    public int AccountId { get; set; } 
    public Account? Account { get; set; }
    public double? Weight { get; set; }
    public double? UnitPrice { get; set; }
    public double? NetPrice { get; set; }
    public double? GasPrice { get; set; }
    public double? NetBalance { get; set; }
    public string? BillFlag { get; set; }
    public string? Remark { get; set; }
    public int? CUser { get; set; }
    public int? MUser { get; set; } 
   
    public BillDetail(string billNumber,int caneTypeId,int accountId)
    {
        BillNumber = billNumber;
        CaneTypeId = caneTypeId;
        AccountId = accountId;
    }
}
