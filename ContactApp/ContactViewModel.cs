using System;
using System.ComponentModel;

namespace ContactApp
{
    public class ContactViewModel : INotifyPropertyChanged
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterHandle { get; set; }

        private DateTime _LastModified;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime LastModified 
        { 
            get => _LastModified;
            set
            {
                _LastModified = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastModified)));
            }
        }

    }

}
