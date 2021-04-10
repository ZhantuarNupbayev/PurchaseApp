using System.Collections.Generic;

namespace DataTransfer.DTO.Users
{
    public class UserForRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
