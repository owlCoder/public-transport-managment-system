using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface defining operations for managing drivers.
    /// </summary>
    [ServiceContract]
    public interface IVozacService
    {
        /// <summary>
        /// Adds a new driver.
        /// </summary>
        /// <param name="data">The data of the driver to add.</param>
        /// <returns>True if the driver is successfully added; otherwise, false.</returns>
        [OperationContract]
        bool DodajVozaca(VozacDTO data);

        /// <summary>
        /// Updates an existing driver with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the driver to update.</param>
        /// <param name="data">The data to update the driver with.</param>
        /// <returns>The updated driver data.</returns>
        [OperationContract]
        VozacDTO IzmeniVozaca(int id, VozacDTO data);

        /// <summary>
        /// Deletes an existing driver with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the driver to delete.</param>
        /// <returns>True if the driver is successfully deleted; otherwise, false.</returns>
        [OperationContract]
        bool ObrisiVozaca(int id);

        /// <summary>
        /// Retrieves the driver with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the driver to retrieve.</param>
        /// <returns>The driver with the specified ID.</returns>
        [OperationContract]
        VozacDTO Procitaj(int id);

        /// <summary>
        /// Retrieves all drivers.
        /// </summary>
        /// <returns>A list of all drivers.</returns>
        [OperationContract]
        List<VozacDTO> ProcitajSve();

        /// <summary>
        /// Authenticates a driver with the specified username and password.
        /// </summary>
        /// <param name="username">The username of the driver.</param>
        /// <param name="password">The password of the driver.</param>
        /// <returns>The ID of the authenticated driver, or -1 if authentication fails.</returns>
        [OperationContract]
        int Prijava(string username, string password);
    }
}
