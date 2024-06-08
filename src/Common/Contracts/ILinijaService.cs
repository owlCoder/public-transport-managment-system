using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface defining operations for managing bus lines.
    /// </summary>
    [ServiceContract]
    public interface ILinijaService
    {
        /// <summary>
        /// Adds a new bus line.
        /// </summary>
        /// <param name="data">The data of the bus line to add.</param>
        /// <returns>The ID of the added bus line.</returns>
        [OperationContract]
        int DodajLiniju(LinijaDTO data);

        /// <summary>
        /// Updates an existing bus line with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the bus line to update.</param>
        /// <param name="data">The data to update the bus line with.</param>
        /// <returns>The ID of the updated bus line.</returns>
        [OperationContract]
        int IzmeniLiniju(int id, LinijaDTO data);

        /// <summary>
        /// Deletes an existing bus line with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the bus line to delete.</param>
        /// <returns>The ID of the deleted bus line.</returns>
        [OperationContract]
        int ObrisiLiniju(int id);

        /// <summary>
        /// Retrieves the bus line with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the bus line to retrieve.</param>
        /// <returns>The bus line with the specified ID.</returns>
        [OperationContract]
        LinijaDTO Procitaj(int id);

        /// <summary>
        /// Retrieves all bus lines.
        /// </summary>
        /// <returns>A list of all bus lines.</returns>
        [OperationContract]
        List<LinijaDTO> ProcitajSve();

        /// <summary>
        /// Searches for bus lines based on destination or other criteria.
        /// </summary>
        /// <param name="poOdredistu">Indicates whether to search by destination.</param>
        /// <param name="unos">The search term or criteria.</param>
        /// <returns>A list of bus lines matching the search criteria.</returns>
        [OperationContract]
        List<LinijaDTO> Pretraga(bool poOdredistu, string unos);
    }
}
