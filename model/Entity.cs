using System;

namespace lab8.Domain
{
	[Serializable]
	public class Entity<ID>
	{
		private ID id;

		// Constructor
		public Entity(ID id)
		{
            this.id = id;
        }

		public ID Id
		{
			get { return id; }
			set { id = value; }
		}

		
	}
}