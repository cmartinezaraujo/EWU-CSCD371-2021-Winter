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
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ContactViewModel> Contacts { get; } = new();
        public RelayCommand NewContactCommand { get; }

        public RelayCommand EditContactCommand { get; }

        public RelayCommand SaveContactCommand { get; }

        public RelayCommand DeleteContactCommand { get; }

        private ContactViewModel _SelectedContact;

        public ContactViewModel SelectedContact
        {
            get => _SelectedContact;
            set => SetProperty(ref _SelectedContact, value);
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
            DeleteContactCommand = new RelayCommand(DeleteContact, CanDeleteContact);

            Contacts.Add(new()
            {
                FirstName = "Goofy",
                LastName = "Martinez",
                PhoneNumber = "509-361-7840",
                EmailAddress = "Goofy@gmail.com",
                TwitterHandle = "@GoodBoy"
            });

            Contacts.Add(new()
            {
                FirstName = "Mister",
                LastName = "Orozco",
                PhoneNumber = "509-361-7839",
                EmailAddress = "Mister@gmail.com",
                TwitterHandle = "@Mistery"
            });

        }

        private void OnNewContact()
        {
            ContactViewModel NewContact = new ContactViewModel() { FirstName = "Jane", LastName = "Doe" };

            Contacts.Add(NewContact);

            SelectedContact = NewContact;

            IsBeingEdited = true;
        }

        private void EditContact()
        {
            IsBeingEdited = true;
        }

        private void SaveContact()
        {
            IsBeingEdited = false;
        }

        public void DeleteContact()
        {
            Contacts.Remove(SelectedContact);

            if(Contacts.Count > 0)
            {
                SelectedContact = Contacts.First();
            }
        }

        public bool CanDeleteContact()
        {
            if(Contacts.Count > 0)
            {
                return true;
            }

            return false;
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
