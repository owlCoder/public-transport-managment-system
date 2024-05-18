using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
	public interface ILinijaService
	{
		bool DodajLiniju(LinijaDTO data);

		bool IzmeniLiniju(int id, LinijaDTO data);

		bool ObrisiLiniju(int id);

		LinijaDTO Procitaj(int id);

		List<LinijaDTO> ProcitajSve();

		LinijaDTO Pretraga(bool poOdredistu, string unos);
	}
}
