using System;
using System.Collections.Generic;

namespace aspCoreEmpty.Models
{
    public partial class Users
    {
        public int Idusers { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public int? Isadminordispatcher { get; set; }
    }
}
