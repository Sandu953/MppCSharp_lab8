using lab8.Domain;
using System;


namespace lab8.Domain
{
	public class Agentie : Entity<long>
	{
        
        private string username;
		

		public Agentie(long id,string username) : base(id)
		{
			this.username = username;
		
		}

		public string Username
		{
			get { return username; }
			set { username = value; }
		}

        public string Password { get; set; }
    }
}