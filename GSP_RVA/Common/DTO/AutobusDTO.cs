using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class AutobusDTO
	{
		public int Id
		{
			get; set;
		}

		public string Oznaka
		{
			get; set;
		}

		public int IdLinije
		{
			get; set;
		}

		public List<LinijaDTO> Linije
		{
			get; set;
		}
	}
}
