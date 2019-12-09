using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook1.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook1.Pages
{
    public class CreateModel : PageModel
    {
        ApplicationDBContext _context;
        public IEnumerable<Models.ContactEmailAddress> Emails { get; set; }

        public ICollection<Models.Contact> Contacts { get; set; }

        public ICollection<Models.PhoneNumbers> Numbers { get; set; }

        public CreateModel(ApplicationDBContext context)
        {

            _context = context;
        }

        [HttpGet]
        public void OnGet()
        {
            Contacts = _context.Contacts?.ToList();
            Numbers = _context.Numbers?.ToList();
            Emails = _context.Emails?.ToList();
        }


        [HttpPost]
        public async Task<IActionResult> OnPostAsync( Guid contactId, string firstname, string lastname, string[] email, int[] number)
        {
            var contacts = _context.Contacts.FirstOrDefault(x => x.ContactId == contactId);


            var emails = email.Select(e =>
            {
                return new Models.ContactEmailAddress() { Email = e, EmailId = Guid.NewGuid() };
            });

            var numbers = number.Select(e =>
            {
                return new Models.PhoneNumbers() { Numbers = e, NumberId = Guid.NewGuid() };
            });

            Models.Contact contact = new Models.Contact() { ContactId = Guid.NewGuid(), FirstName = firstname, LastName = lastname, 
                ContactEmailAddress = emails.ToList(), PhoneNumbers = numbers.ToList() };

            _context.Add(contact);

            await _context.SaveChangesAsync();

            return RedirectToPage("./UserDetails");
        }
    }
}
