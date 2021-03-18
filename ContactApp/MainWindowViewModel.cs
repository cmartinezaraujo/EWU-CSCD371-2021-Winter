using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
//using System.Text;

namespace ContactApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ContactViewModel> Contacts { get; } = new();
        public RelayCommand NewContactCommand { get; }

        public RelayCommand EditContactCommand { get; }

        public RelayCommand SaveContactCommand { get; }

        public RelayCommand DeleteContactCommand { get; }

        private ContactViewModel? _SelectedContact;

        public ContactViewModel SelectedContact
        {
            get => _SelectedContact;
            set => SetProperty(ref _SelectedContact, value);
        }

        private bool _IsListEmpty;

        public bool IsListEmpty
        {
            get => _IsListEmpty;
            set => SetProperty(ref _IsListEmpty, value);
        }

        private bool _IsBeingEdited;
        public bool IsBeingEdited 
        { 
            get => _IsBeingEdited; 
            set => SetProperty(ref _IsBeingEdited, value);
        }

        public MainWindowViewModel()
        {
            NewContactCommand = new RelayCommand(OnNewContact, () => true);
            EditContactCommand = new RelayCommand(EditContact, () => true);
            SaveContactCommand = new RelayCommand(SaveContact, () => true);
            DeleteContactCommand = new RelayCommand(DeleteContact, () => !IsListEmpty);

            Contacts.Add(new()
            {
                FirstName = "Goofy",
                LastName = "Martinez",
                PhoneNumber = "509-111-1111",
                EmailAddress = "Goofy@gmail.com",
                TwitterHandle = "@GoodBoy",
                LastModified = DateTime.Now
            });

            Contacts.Add(new()
            {
                FirstName = "Mister",
                LastName = "Orozco",
                PhoneNumber = "509-111-1111",
                EmailAddress = "Mister@gmail.com",
                TwitterHandle = "@Mistery",
                LastModified = DateTime.Now
            });
            SelectedContact = Contacts.First();
            IsListEmpty = false;
        }

        private void OnNewContact()
        {
            ContactViewModel NewContact = new ContactViewModel() { FirstName = "Jane", LastName = "Doe", LastModified = DateTime.Now };

            Contacts.Add(NewContact);

            SelectedContact = NewContact;

            IsBeingEdited = true;

            IsListEmpty = false;
        }

        private void EditContact()
        {
            IsBeingEdited = true;
        }

        private void SaveContact()
        {
            IsBeingEdited = false;
            SelectedContact.LastModified = DateTime.Now;
        }

        private void DeleteContact()
        {
            Contacts.Remove(SelectedContact);

            if(Contacts.Count == 0)
            {
                IsListEmpty = true;
            }

            if(!IsListEmpty)
            {
                SelectedContact = Contacts.First();
            }
        
        }

        private bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

    }
}
