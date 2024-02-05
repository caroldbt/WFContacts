using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    public class BusinessLogicLayer
    {
        private readonly DataAccessLayer _dataAccesslayer;
        public BusinessLogicLayer() { 
            _dataAccesslayer = new DataAccessLayer();
        }
        public Contact SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                _dataAccesslayer.InsertContact(contact);
            }
            else
            {
                _dataAccesslayer.UpdateContact(contact);
            }
            return contact;
        }
        public List<Contact> GetContacts(string searchText = null)
        {
            return _dataAccesslayer.GetContacts(searchText);
        }

        public void DeleteContact(int id)
        {
            _dataAccesslayer.DeleteContact(id);
        }
    }
}
