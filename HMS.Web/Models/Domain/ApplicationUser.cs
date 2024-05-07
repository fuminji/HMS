using Microsoft.AspNetCore.Identity;

namespace HMS.Web.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public decimal? BillsAmout { get; set; }
        public bool HasBalance { get; set; }
    }
}
