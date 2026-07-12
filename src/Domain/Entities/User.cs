using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Passwords { get; set; }
    public string? DisplayName { get; set; }
    public string? PositionName { get; set; }
    public DateTime? LastLog { get; set; }
    public int RoleId { get; set; }

    public User( string username, string passwords,string displayName,string positionName,int roleId)
    {
        Username = username;
        Passwords = passwords;
        DisplayName = displayName;
        PositionName = positionName;
        RoleId = roleId;
    }

}
