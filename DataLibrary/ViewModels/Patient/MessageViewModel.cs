

using DataLibrary.Models.Patient;

namespace DataLibrary.ViewModels.Patient;

public class MessageViewModel
{
    public Contact contact { get; set; }    

    public List<Message> messages = new List<Message>();
}
