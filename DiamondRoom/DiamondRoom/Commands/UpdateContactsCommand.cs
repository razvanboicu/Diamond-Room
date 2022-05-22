using DiamondRoom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Commands
{
    public class UpdateContactsCommand : CommandBase
    {
        List<Contact> _contacts = new List<Contact>();

        public UpdateContactsCommand(List<Contact> contacts)
        {
              _contacts = contacts; 
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
