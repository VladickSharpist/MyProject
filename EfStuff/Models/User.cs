namespace WebApplication.EfStuff.Models
{
    public class User:BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}