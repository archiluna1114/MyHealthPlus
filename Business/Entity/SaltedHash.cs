using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entity
{
    public class SaltedHash
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
