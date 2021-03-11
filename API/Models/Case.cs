using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Case
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public string Descrip { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LatestUpdate { get; set; }
        public string StatusCode { get; set; }

        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
    }
}
