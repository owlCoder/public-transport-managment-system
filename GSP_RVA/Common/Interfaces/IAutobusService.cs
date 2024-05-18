using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
	public interface IAutobusService
	{
		bool DodajAutobus(string oznaka);

		bool IzmeniAutobus(int id, AutobusDTO data);

		bool ObrisiAutobus(int id);

		AutobusDTO Procitaj(int id);

		List<AutobusDTO> ProcitajSve();
	}
}
