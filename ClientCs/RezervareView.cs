using lab8.Domain;
using services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace client
{
    public partial class RezervareView : Form,IObserver
    {

        //private ServiceExcursie serviceExcursie;
        //private ServiceRezervare serviceRezervare;
        private BindingSource bindingSourceExcursii = new BindingSource();
        private BindingSource bindingSourceExcursiiRezervari = new BindingSource();
        private IServices server;
        private Agentie currentUser;

        public RezervareView()
        {
            InitializeComponent();


        }

        //public void SetService(ServiceExcursie serviceExcursie, ServiceRezervare serviceRezervare)
        //{
        //    this.serviceExcursie = serviceExcursie;
        //    this.serviceRezervare = serviceRezervare;

        //    InitModelExcursii();
        //    SetCombo1();
        //    SetCombo2();
        //}

        public void setServer(IServices services)
        {
            this.server = services;
            InitModelExcursii();
            SetCombo1();
            SetCombo2();
        }

        public void setUser(Agentie agentie)
        {
            this.currentUser = agentie;
            
        }

        private void InitModelExcursii()
        {
            
            bindingSourceExcursii.Clear();
            var excursii = server.GetAllExcursii();
            bindingSourceExcursii.DataSource = excursii;
            dataGridView1.DataSource = bindingSourceExcursii;
        }

        private void ColorRowsByColumnValue(DataGridView dataGridView)
        {
            int columnIndex = 5; // Indicele coloanei după care doriți să colorați rândurile

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Obțineți valoarea din coloana specificată pentru rândul curent
                object cellValue = row.Cells[columnIndex].Value;

                // Verificați valoarea și colorați rândul în funcție de aceasta
                if (cellValue != null && (int)cellValue == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Red; // Schimbați culoarea după nevoie
                }
                else
                {
                    // Restabiliți culoarea implicită a rândului
                    row.DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor;
                }
            }
        }

        private void SetCombo1()
        {
            for (int i = 0; i <= 23; i++)
            {
                comboBox1.Items.Add(i);
            }
        }

        private void SetCombo2()
        {
            comboBox1.SelectedIndexChanged += (sender, e) =>
            {
                if (comboBox1.SelectedItem != null)
                {
                    comboBox2.Items.Clear();
                    for (int i = (int)comboBox1.SelectedItem + 1; i <= 24; i++)
                    {
                        comboBox2.Items.Add(i);
                    }
                }
            };
        }

        private void btnCauta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                bindingSourceExcursiiRezervari.Clear();
                string ora1 = comboBox1.SelectedItem.ToString() + ":00:00";
                string ora2 = comboBox2.SelectedItem.ToString() + ":00:00";

                TimeSpan ts = TimeSpan.Parse(ora1);
                long timestamp = (long)ts.TotalMilliseconds;

                TimeSpan ts2 = TimeSpan.Parse(ora2);
                long timestamp2 = (long)ts2.TotalMilliseconds;

                var excursii = server.GetExcursiiBetweenHours(textBox4.Text, timestamp.ToString(), timestamp2.ToString());
                bindingSourceExcursiiRezervari.DataSource = excursii;
                dataGridView2.DataSource = bindingSourceExcursiiRezervari;
                ColorRowsByColumnValue(dataGridView2);
            }
            else
            {
                MessageBox.Show("Nu ati completat toate campurile!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRezerva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNume.Text) || string.IsNullOrEmpty(textBoxTelefon.Text) || string.IsNullOrEmpty(textBoxLoc.Text))
            {
                MessageBox.Show("All fields must be filled!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteaza o excursie!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Excursie excursie = (Excursie)dataGridView2.SelectedRows[0].DataBoundItem;
            try
            {
                if (int.Parse(textBoxLoc.Text) > server.GetFreeSeats((int)excursie.Id))
                {
                    MessageBox.Show("Nu exista destule locuri!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                server.AddRezervare(1,excursie.Id, textBoxNume.Text, textBoxTelefon.Text, int.Parse(textBoxLoc.Text));
                bindingSourceExcursiiRezervari.Clear();
                string ora1 = comboBox1.SelectedItem.ToString() + ":00:00";
                string ora2 = comboBox2.SelectedItem.ToString() + ":00:00";

                TimeSpan ts = TimeSpan.Parse(ora1);
                long timestamp = (long)ts.TotalMilliseconds;

                TimeSpan ts2 = TimeSpan.Parse(ora2);
                long timestamp2 = (long)ts2.TotalMilliseconds;
                var excursii = server.GetExcursiiBetweenHours(textBox4.Text, timestamp.ToString(), timestamp2.ToString());
                //foreach(Excursie ex in excursii)
                //{
                //    MessageBox.Show(ex.LocuriLibere.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                bindingSourceExcursiiRezervari.DataSource = excursii;
                ColorRowsByColumnValue(dataGridView2);
               
                MessageBox.Show("Adaugare cu succes!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Delay(1000);
                ReservationMade();
                ReservationMade();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //InitModelExcursii();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            server.Logout(currentUser, this);
            this.Close();
        }

        public void ReservationMade()
        {
            InitModelExcursii();
            bindingSourceExcursiiRezervari.Clear();
        }
    }
}
