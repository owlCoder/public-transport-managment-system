using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class LinijaDTO
	{
		public int Id
		{
			get; set;
		}

		public string Oznaka
		{
			get; set;
		}

		public string Polaziste
		{
			get; set;
		}

		public string Odrediste
		{
			get; set;
		}

		public List<VozacDTO> Vozaci
		{
			get; set;
		}

		public List<AutobusDTO> Autobusi
		{
			get; set;
		}
	}
}
