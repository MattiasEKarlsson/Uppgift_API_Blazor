using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorWeb.Models
{
    public partial class Client
    {
        public Client()
        {
            Cases = new HashSet<Case>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Case> Cases { get; set; }
    }
}
