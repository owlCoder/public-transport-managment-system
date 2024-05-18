using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
	public interface IVozacService
	{
		bool DodajVozaca(VozacDTO data);

		bool IzmeniVozaca(int id, VozacDTO data);

		bool ObrisiVozaca(int id);

		VozacDTO Procitaj(int id);

		List<VozacDTO> ProcitajSve();
	}
}
