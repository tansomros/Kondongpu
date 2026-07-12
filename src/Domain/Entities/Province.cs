using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kondongpu.Domain.Entities;

public class Province
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NameEnglish { get; set; }
    public Province(string id,string name,string nameEnglish)
    {
        Id = id;
        Name = name;
        NameEnglish = nameEnglish;
    }
}
