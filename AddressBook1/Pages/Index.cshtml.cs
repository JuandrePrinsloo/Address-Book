using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook1.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook1.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationDBContext _context;
        public IEnumerable<Models.ContactEmailAddress> Emails { get; set; }

        public ICollection<Models.Contact> Contacts { get; set; }

        public ICollection<Models.PhoneNumbers> Numbers { get; set; }

        public IndexModel(ApplicationDBContext context)
        {

            _context = context;
        }
        public void OnGet()
        {
            Contacts = _context.Contacts?.ToList();
            Numbers = _context.Numbers?.ToList();
            Emails = _context.Emails?.ToList();
        }


        public async Task<IActionResult> OnPostAsync(Guid id, Guid contactId, string firstname, string lastname, string email, int numbers)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.ContactId == contactId);

            var number = _context.Numbers.FirstOrDefault(x => x.NumberId == id);

            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}