namespace Kondongpu.Domain.Entities
{
    public class ReportGroup: BaseEntity
    {
        public string Name { get; set; }
        public int Sort { get; set; }     
        public string? CreateUser { get; set; }
        public string? ModifiedUser { get; set; }

        public ReportGroup(string name,int sort) 
        { 
            Name = name;
            Sort = sort;
        }
    }
}
