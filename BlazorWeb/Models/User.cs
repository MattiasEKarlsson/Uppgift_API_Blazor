using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace BlazorWeb.Models
{
    public partial class User
    {
        public User()
        {
            Cases = new HashSet<Case>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] UserHash { get; set; }
        public byte[] UserSalt { get; set; }

        public virtual ICollection<Case> Cases { get; set; }

       
    }
}
