using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Addresstype { get; set; }
        public int UserId { get; set; }

    }
}
