using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class CaneType : BaseEntity
{
    public string Description { get; set; }
    public string? Remark { get; set; }
    public CaneType(string description)
    {
        Description = description;        
    }
}
