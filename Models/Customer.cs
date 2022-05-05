using System.ComponentModel.DataAnnotations;

namespace PolygonBank.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(80)]
        [MinLength(3)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(80)]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        public double Balance { get; set; }

        [MaxLength(512)]
        public string AvatarSrc { get; set; } = string.Empty;

        public static Customer Empty => new Customer();
    }
}
