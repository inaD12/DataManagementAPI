using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Domain.Entities.Base
{
	public abstract class BaseEntity : IBaseEntity
	{
		public BaseEntity()
		{
			Id = Guid.NewGuid().ToString();
			CreatedAt = DateTime.Now;
			DeletedAt = null;
		}

		public string Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? DeletedAt { get; set;}
	}
}
