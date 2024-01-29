using System.ComponentModel.DataAnnotations;

namespace Cine.Models
{
    public class Login
    {
        [Required]
        public string Usuario
        {
        get; set; }
        [Required]
        public string Password
        { get; set; }
            
    }
}
