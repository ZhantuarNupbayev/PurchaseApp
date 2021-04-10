using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    /**
    * Cущность Пользователь
    * Использует ASP.NET Core Identity (наследуется от класса Identity User)
    * @FirstName - имя пользователя
    * @LastName - фамилия пользователя
    * */
    public class User : IdentityUser
    { 
        public string FirstName { get; set; }

        public string LastName { get; set; }
       
    }
}
