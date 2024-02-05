using System;
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
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer(); 
        }
        #region EVENTOS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDialog();  
            
        }
        //Cuando carga la pagina se cargara los datos
        private void Main_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }
        //actualiza los datos de la grilla
        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = (gridContacts.Rows[e.RowIndex].Cells[1]).Value.ToString(),
                    LastName = (gridContacts.Rows[e.RowIndex].Cells[2]).Value.ToString(),
                    Phone = (gridContacts.Rows[e.RowIndex]).Cells[3].Value.ToString(),
                    Address = (gridContacts.Rows[e.RowIndex]).Cells[4].Value.ToString(),
                });
                contactDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContacts(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
        }
        //Metodo Buscar
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(textSearch.Text);
            textSearch.Text = string.Empty;
        }
        #endregion

        #region PRIVATE METHODS
        private void OpenContactDialog()
        {
            //Esto significa que va abrir nuestro segundo formulario
            ContactDetails contactDetails = new ContactDetails(); 
            contactDetails.ShowDialog(this);
        }
        private void DeleteContacts(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }
        #endregion

        #region PUBLIC METHODS
        //Funcion para obtener los datos de nuestra base de datos
        //string search =null es un parametro opcional
        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }
        #endregion

    }
}
