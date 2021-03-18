using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactApp
{
    public class ContactViewModel : INotifyPropertyChanged
    {

        private string? _FirstName;
        public string? FirstName 
        { 
            get => _FirstName;
            set => SetProperty(ref _FirstName!, value!);
        }

        private string? _LastName;
        public string? LastName 
        {
            get => _LastName; 
            set => SetProperty(ref _LastName!, value!);
        }

        private string? _PhoneNumber;
        public string? PhoneNumber 
        {
            get => _PhoneNumber; 
            set => SetProperty(ref _PhoneNumber!, value!);
        }

        private string? _EmailAddress;
        public string? EmailAddress 
        {
            get => _EmailAddress; 
            set => SetProperty(ref _EmailAddress!, value!);
        }

        private string? _TwitterHandle;
        public string? TwitterHandle 
        {
            get => _TwitterHandle; 
            set => SetProperty(ref _TwitterHandle!, value!);
        }

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

        private void SetProperty(ref string field, string newValue, [CallerMemberName] string propertyName = "")
        {
            if(String.Equals("", newValue.Trim()))
            {
                newValue = "";
            }

            if (!String.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
