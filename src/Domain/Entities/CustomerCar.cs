using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class CustomerCar : BaseEntity
{ 
    public int CustomerId { get; set; }
    public string CarNumber { get; set; }
    public int? CUser { get; set; }
    public CustomerCar(int customerId,string carNumber)
    {        
        CustomerId = customerId;
        CarNumber = carNumber;
    }
}
