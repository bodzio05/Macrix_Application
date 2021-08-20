using System;

namespace Macrix_Application.Model.Interfaces
{
    public interface IPerson
    {
        #region Properties
        string FirstName { get; set; }
        string LastName { get; set; }
        string StreetName { get; set; }
        int HouseNumber { get; set; }
        int? ApartmentNumber { get; set; }
        string PostalCode { get; set; }
        string Town { get; set; }
        string PhoneNumber { get; set; }
        DateTime DateOfBirth { get; set; }
        string Age { get; }
        #endregion

        #region Methods
        Person Clone(IPerson person);
        #endregion
    }
}
