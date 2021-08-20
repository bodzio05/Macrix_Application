using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Macrix_Application.Model.Common;
using Macrix_Application.Model.Interfaces;

namespace Macrix_Application.Model
{
    [XmlRoot(ElementName = "Person")]
    public class Person : IPerson, INotifyPropertyChanged
    {
        #region Properties
        [XmlElement("First_Name")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        [XmlElement("Last_Name")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        [XmlElement("Street_Name")]
        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;
                OnPropertyChanged(nameof(StreetName));
            }
        }

        [XmlElement("House_Number")]
        public int HouseNumber
        {
            get { return _houseNumber; }
            set
            {
                _houseNumber = value;
                OnPropertyChanged(nameof(HouseNumber));
            }
        }

        [XmlElement("Apartment_Number", IsNullable = true)]
        public int? ApartmentNumber
        {
            get { return _apartmentNumber; }
            set
            {
                _apartmentNumber = value;
                OnPropertyChanged(nameof(ApartmentNumber));
            }
        }

        [XmlElement("Postal_Code")]
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        [XmlElement("Town")]
        public string Town
        {
            get { return _town; }
            set
            {
                _town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        [XmlElement("Phone_Number")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        [XmlElement("Date_Of_sBirth")]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                OnPropertyChanged(nameof(Age));
            }
        }

        [XmlElement("Age")]
        public string Age
        {
            get 
            {
                if (DateOfBirth == DateTime.MinValue)
                {
                    return String.Empty;
                }
                return (DateTime.Now.Year - DateOfBirth.Year).ToString(); 
            }
            set
            {
                OnPropertyChanged(nameof(Age));
            }
        }
        #endregion

        #region Fields
        private string _firstName;
        private string _lastName;
        private string _streetName;
        private int _houseNumber;
        private int? _apartmentNumber;
        private string _postalCode;
        private string _town;
        private string _phoneNumber;
        private DateTime _dateOfBirth;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public Person()
        {

        }
        #endregion

        #region Public_Methods
        public Person Clone(IPerson person)
        {
            return new Person()
            {
                ApartmentNumber = person.ApartmentNumber,
                DateOfBirth = person.DateOfBirth,
                FirstName = person.FirstName,
                HouseNumber = person.HouseNumber,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                PostalCode = person.PostalCode,
                StreetName = person.StreetName,
                Town = person.Town
            };
        }
        #endregion

        #region Private_Methods
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
