namespace Kondongpu.Domain.Entities;
public class Account : BaseEntity
{
    public string Name { get; set; }
    public string? Remark { get; set; }
    public Account(string name)
    {
        Name = name;
    }
}
