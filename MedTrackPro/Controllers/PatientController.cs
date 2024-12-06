using DataLibrary.Models.Patient;
using DataLibrary.ViewModels.Patient;
using MedTrackPro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedTrackPro.Controllers;

//[Authorize(Roles = "admin,patient")]
public class PatientController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public PatientController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task<IActionResult> Message(string id)
    {
        if(id != null)
        {
            var user = await _userManager.FindByIdAsync(id);
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(user != null)
            {
                var contact = new Contact
                {
                    Id = id,
                    Name = user.UserName
                };
                var chat = _context.Messages.FromSqlInterpolated
                    ($"exec dbo.spMessage_GetMessageByIds @SenderId = {senderId}, @ReceiverId = {id};")
                    .AsEnumerable().Select(message => new Message
                    {
                        Id=message.Id,
                        ReceiverId=message.ReceiverId,
                        SenderId=message.SenderId,
                        Text=message.Text,
                    }).ToList();

                var viewModel = new MessageViewModel
                {
                    contact = contact,
                    messages = chat
                };
                return View(viewModel);
            }
        }
        return NotFound();
    }

    public IActionResult Contacts()
    {
        var users = _userManager.Users.ToList();
        List<Contact> contacts = new List<Contact>();
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        foreach (var user in users)
        {
            if(user.Id != currentUserId)
            {
                contacts.Add(new Contact
                {
                    Id = user.Id,
                    Name = user.UserName
                });
            }
            
        }
        return View(contacts);
    }

    public IActionResult SelectDoctor()
    {
        return View();
    }
}
