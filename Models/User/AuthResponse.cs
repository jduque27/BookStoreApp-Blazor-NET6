using System.Drawing;

namespace BookStoreApp.API.Models.User
{
    public class AuthResponse
    {
        public String UserId { get; set; }  
        public String Token { get; set; }  
        public String Email { get; set; }  
    }
}
