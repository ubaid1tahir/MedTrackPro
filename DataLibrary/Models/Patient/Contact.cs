
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibrary.Models.Patient;

public class Contact
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Message
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    public string ReceiverId { get; set; }
    public IdentityUser Receiver { get; set; }
    public string SenderId { get; set; }
    public IdentityUser Sender { get; set; }

    public DateTime SendTime { get; set; }

}