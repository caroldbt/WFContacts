﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class ContactDetails : Form
    {
        private readonly BusinessLogicLayer _businessLogicLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }
        #region EVENTS
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();
        }
        #endregion

        #region PRIVATE METHODS
        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FirstName = textFirstName.Text;
            contact.LastName = textLastName.Text;
            contact.Phone = textPhone.Text;
            contact.Address = textAddress.Text;

            contact.Id = _contact != null ? _contact.Id : 0;

            _businessLogicLayer.SaveContact(contact);
        }
        private void ClearForm()
        {
            textFirstName.Text = string.Empty;
            textLastName.Text = string.Empty;
            textPhone.Text = string.Empty;
            textAddress.Text = string.Empty;

        }
        #endregion

        #region PUBLIC METHODS
        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm();
                textFirstName.Text = contact.FirstName;
                textLastName.Text = contact.LastName;
                textPhone.Text = contact.Phone;
                textAddress.Text = contact.Address;

            }
        }
        #endregion
    }
}
