using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AddressBook1.Models
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<ContactEmailAddress> ContactEmailAddress { get; set; }

        public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
    }

    public class ContactEmailAddress
    {
        [Key]
        public Guid EmailId { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public virtual Contact Contact { get; set; }
    }

    public class PhoneNumbers
    {
        [Key]
        public Guid NumberId { get; set; }

        public int Numbers { get; set; }

        [JsonIgnore]
        public virtual Contact Contact { get; set; }
    }

}
