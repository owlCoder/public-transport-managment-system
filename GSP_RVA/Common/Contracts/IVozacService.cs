using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
	[ServiceContract]
	public interface IVozacService
	{
		[OperationContract]
		bool DodajVozaca(VozacDTO data);

		[OperationContract]
		bool IzmeniVozaca(int id, VozacDTO data);

		[OperationContract]
		bool ObrisiVozaca(int id);

		[OperationContract]
		VozacDTO Procitaj(int id);

		[OperationContract]
		List<VozacDTO> ProcitajSve();
	}
}