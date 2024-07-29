using lab8.Domain;
using services;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;





namespace client
{
    public partial class Login : Form
    {
        //private ServiceAgentie serviceAgentie;
        //private ServiceExcursie serviceExcursie;
        //private ServiceRezervare serviceRezervare;
        private ClientCtrl clientCtrl;
        private IServices server;
        

        public Login(ClientCtrl clientCtrl)
        {
            InitializeComponent();
            this.clientCtrl = clientCtrl;
        }

        //public void SetService(ServiceAgentie serviceAgentie, ServiceExcursie serviceExcursie, ServiceRezervare serviceRezervare)
        //{
        //    this.serviceAgentie = serviceAgentie;
        //    this.serviceExcursie = serviceExcursie;
        //    this.serviceRezervare = serviceRezervare;
        //}
        public void setServer(IServices server)
        {
            this.server = server;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                string user = textBox1.Text ?? "numele utilizatorului lipsește";
                string pass = textBox2.Text ?? "parola lipsește";
                
                if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                {
                    throw new Exception("Numele utilizatorului sau parola nu poate fi gol.");
                }
                if (clientCtrl.login(user, pass))
                {
                    int id = (int)server.GetId(user, pass);
                    Agentie agentie = new Agentie(id, user);
                    RezervareView rezervarecontroller = new RezervareView();
                    rezervarecontroller.setServer(server);
                    rezervarecontroller.setUser(agentie);
                    
                    //rezervarecontroller.setservice(serviceexcursie, servicerezervare);
                    rezervarecontroller.Show();
                }
                else
                {
                    MessageBox.Show("Username sau parola incorecte!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la logare!" + ex.Message);
                //throw new Exception( ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
