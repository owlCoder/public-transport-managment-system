using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
	public class VozacDTO
	{
		public int Id
		{
			get; set;
		}

		public string Username
		{
			get; set;
		}

		public string Password
		{
			get; set;
		}

		public string Ime
		{
			get; set;
		}

		public string Prezime
		{
			get; set;
		}

		public UserRoles Role
		{
			get; set;
		}

		public string Oznaka
		{
			get; set;
		}

		public List<LinijaDTO> Linije
		{
			get; set;
		}
	}
}
