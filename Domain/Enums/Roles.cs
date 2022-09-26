

using Domain.Attributes;

namespace Domain.Enums
{
    public enum Roles
    {
        [StringValue("Super Admin")]
        SuperAdmin,
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