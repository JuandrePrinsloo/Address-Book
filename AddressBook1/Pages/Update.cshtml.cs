using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook1.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook1.Pages
{
    public class UpdateModel : PageModel
    {
        ApplicationDBContext _context;
        public IEnumerable<Models.ContactEmailAddress> Emails { get; set; }

        public ICollection<Models.Contact> Contacts { get; set; }

        public ICollection<Models.PhoneNumbers> Numbers { get; set; }

        public UpdateModel(ApplicationDBContext context)
        {
            
            _context = context;
        }
        public void OnGet()
        {
            Contacts = _context.Contacts?.ToList();
            Numbers = _context.Numbers?.ToList();
            Emails = _context.Emails?.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateAsync(Guid id, Guid contactId, string firstname, string lastname,
            ICollection<Models.ContactEmailAddress> email, ICollection<Models.PhoneNumbers> numbers)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.ContactId == contactId);

            Models.Contact contacts = new Models.Contact() { ContactId = contactId, FirstName = firstname, LastName = lastname, ContactEmailAddress = email, PhoneNumbers = numbers };

            _context.Update(contacts);

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}