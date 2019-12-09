using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook1.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AddressBook1.Pages
{
    public class UserDetailsModel : PageModel
    {
        ApplicationDBContext _context;

        public IEnumerable<Models.ContactEmailAddress> Emails { get; set; }

        public IEnumerable<Models.Contact> Contacts { get; set; }
        
        public IEnumerable<Models.PhoneNumbers> Numbers { get; set; }

        public UserDetailsModel(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task OnGet(string search = null)
        {
            if (search == null || string.IsNullOrWhiteSpace(search))
            {
                Contacts = await _context.Contacts?.ToListAsync();
                Numbers = await _context.Numbers?.ToListAsync();
                Emails = await _context.Emails?.ToListAsync();
            }
            else
            {
                var terms = search.Split();

                Contacts = _context.Contacts.Where(x => terms.Contains(x.FirstName) || terms.Contains(x.LastName));

                Numbers = _context.Numbers?.ToList();
                Emails = _context.Emails?.ToList();
            }
        }

        [HttpPost]
        public IActionResult OnPostSearch(string search)
        {
            return RedirectToPage(new { search = search });
        }

        [HttpPost]
        public async Task<IActionResult> OnPostDeleteAsync(Guid? id)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);

            var numbers = _context.Numbers.Where(e => e.Contact.ContactId == contact.ContactId);
            foreach (var number in numbers)
            {
                _context.Remove(number);
            }

            var emails = _context.Emails.Where(e => e.Contact.ContactId == contact.ContactId);
            foreach (var email in emails)
            {
                _context.Remove(email);
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./UserDetails");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateAsync(Guid contactId, string firstname, string lastname,
            string[] email, int[] number)
        {
            var contacts = _context.Contacts.FirstOrDefault(x => x.ContactId == contactId);

            contacts.ContactId = contactId;
            contacts.FirstName = firstname;
            contacts.LastName = lastname;
            contacts.ContactEmailAddress = email.Select(x => new Models.ContactEmailAddress { Email = x }).ToList();
            contacts.PhoneNumbers = number.Select(x => new Models.PhoneNumbers { Numbers = x }).ToList();

            _context.Contacts.Update(contacts);

            await _context.SaveChangesAsync();

            return RedirectToPage("./UserDetails");
        }
    }
}