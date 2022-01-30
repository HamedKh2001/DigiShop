using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public UserRole Role { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ActiveCode { get; set; }
        public bool IsActive { get; set; } = false;


        #region Relations
        public virtual ICollection<Productcomment> Productcomments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
        public enum UserRole
        {
            Admin,
            Customer,
            shop
        }
    }
}
