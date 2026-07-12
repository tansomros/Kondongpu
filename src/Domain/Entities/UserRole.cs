using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class UserRole 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRole(int id,string name)
    {
        Id = id;
        Name = name;
    }
}
