using System;

namespace network
{
    [Serializable]
    public class AgentieDTO
    {
        private long id;
        private string username;
        private string password;

        public AgentieDTO(long id, string user, string pass)
        {
            this.id = id;
            this.username = user;
            this.password = pass;
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public override string ToString()
        {
            return "AgentieDTO[" + username + "]";
        }
    }
}
