

using Domain.Attributes;

namespace Domain.Enums
{
    public enum Roles
    {
        [StringValue("System Admin")]
        Admin,

        [StringValue("School Archivist")]
        Archivist,

        [StringValue("Data Entry Clerk")]
        EntryClerk,

        [StringValue("Requestor")]
        Requestor
    }
}