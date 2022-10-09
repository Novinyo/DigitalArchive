namespace Domain
{
    public class SchoolType: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char CategoryId { get; set; }
    }
}