using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using ResilITApp.Control;
using ResilITApp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResilITApp
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AssociationModel> associations;
        public ObservableCollection<AssociationModel> Associations
        {
            get
            {
                return associations;
            }
            set
            {
                associations = value;
            }
        }

        private string ticketCode;
        public string TicketCode
        {
            get
            {
                return ticketCode;
            }
            set
            {
                ticketCode = value;
                TicketCodeError = false;
                NotifyPropertyChanged();
            }
        }

        private bool ticketCodeError;
        public bool TicketCodeError
        {
            get
            {
                return ticketCodeError;
            }
            set
            {
                ticketCodeError = value;
                NotifyPropertyChanged();
            }
        }

        private bool isFirstNameEmpty;
        public bool IsFirstNameEmpty
        {
            get
            {
                return isFirstNameEmpty;
            }
            set
            {
                isFirstNameEmpty = value;
                NotifyPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                IsFirstNameEmpty = false;
                NotifyPropertyChanged();
            }
        }

        private bool isSurnameEmpty;
        public bool IsSurnameEmpty
        {
            get
            {
                return isSurnameEmpty;
            }
            set
            {
                isSurnameEmpty = value;
                NotifyPropertyChanged();
            }
        }

        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                IsSurnameEmpty = false;
                NotifyPropertyChanged();
            }
        }

        private bool associationError;
        public bool AssociationError
        {
            get
            {
                return associationError;
            }
            set
            {
                associationError = value;
                NotifyPropertyChanged();
            }
        }

        private AssociationModel association;
        public AssociationModel Association
        {
            get
            {
                return association;
            }
            set
            {
                association = value;
                AssociationError = false;
                NotifyPropertyChanged();
            }
        }

        private bool useBus;
        public bool UseBus
        {
            get
            {
                return useBus;
            }
            set
            {
                useBus = value;
                NotifyPropertyChanged();
            }
        }

        private bool studyProgrammeError;
        public bool StudyProgrammeError
        {
            get
            {
                return studyProgrammeError;
            }
            set
            {
                studyProgrammeError = value;
                NotifyPropertyChanged();
            }
        }

        private string studyProgramme;
        public string StudyProgramme
        {
            get
            {
                return studyProgramme;
            }
            set
            {
                studyProgramme = value;
                StudyProgrammeError = false;
                NotifyPropertyChanged();
            }
        }

        private string studyProgrammeOther;
        public string StudyProgrammeOther
        {
            get
            {
                return studyProgrammeOther;
            }
            set
            {
                studyProgrammeOther = value;
                NotifyPropertyChanged();
            }
        }

        private string companyName;
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
                NotifyPropertyChanged();
            }
        }

        private bool vegetarian;
        public bool Vegetarian
        {
            get
            {
                return vegetarian;
            }
            set
            {
                vegetarian = value;
                NotifyPropertyChanged();
            }
        }

        private string otherRemarks;
        public string OtherRemarks
        {
            get
            {
                return otherRemarks;
            }
            set
            {
                otherRemarks = value;
                NotifyPropertyChanged();
            }
        }

        private string mail;

        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
                MailHasError = false;
                NotifyPropertyChanged();
            }
        }

        private bool mailHasError;
        public bool MailHasError
        {
            get
            {
                return mailHasError;
            }
            set
            {
                mailHasError = value;
                NotifyPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                IsPasswordEmpty = false;
                NotifyPropertyChanged();
            }
        }

        private bool isPasswordEmpty;
        public bool IsPasswordEmpty
        {
            get
            {
                return isPasswordEmpty;
            }
            set
            {
                isPasswordEmpty = value;
                NotifyPropertyChanged();
            }
        }

        private bool confirmPasswordError;
        public bool ConfirmPasswordError
        {
            get
            {
                return confirmPasswordError;
            }
            set
            {
                confirmPasswordError = value;
                NotifyPropertyChanged();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value;
                ConfirmPasswordError = false;
                NotifyPropertyChanged();
            }
        }

        private bool agreePolicy;
        public bool AgreePolicy
        {
            get
            {
                return agreePolicy;
            }
            set
            {
                agreePolicy = value;
                NotifyPropertyChanged();
            }
        }

        private bool mailSubscribe;
        public bool MailSubscribe
        {
            get
            {
                return mailSubscribe;
            }
            set
            {
                mailSubscribe = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; private set; }

        public ICommand ShowPrivacy { get; set; }
        public ICommand ShowRequirements { get; set; }

        public RegisterViewModel()
        {
            Associations = new ObservableCollection<AssociationModel>();
            _ = FillAssociations();
            SubmitCommand = new Command<string>(SubmitButtonClicked);
            ShowPrivacy = new Command(ShowPrivacyClicked);
            ShowRequirements = new Command(ShowRequirementsClicked);
        }

        private async void ShowPrivacyClicked()
        {
            await Launcher.OpenAsync(Login.URL + "Privacy_Policy.pdf");
        }

        private async void ShowRequirementsClicked()
        {
            await Launcher.OpenAsync(Login.URL + "Voorwaarden.pdf");
        }

        private async Task FillAssociations()
        {
            HttpMessage associationMesssage = await Login.Instance.DoGet("api/associations");
            if (associationMesssage.Success)
            {
                string json = await associationMesssage.Response.Content.ReadAsStringAsync();
                AssociationModel[] associations = JsonConvert.DeserializeObject<AssociationModel[]>(json);
                foreach(AssociationModel associationObj in associations)
                {
                    Associations.Add(associationObj);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Could not load associations. Are you sure you are connected?", "OK");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SubmitButtonClicked(object obj)
        {
            if(!AgreePolicy)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Please agree to the privacy policy.", "OK");
                return;
            }

            AppController.AddBusy(this);
            IsPasswordEmpty = string.IsNullOrEmpty(Password);
            MailHasError = string.IsNullOrEmpty(Mail) || !mail.Contains("@") || !mail.Contains(".");
            ConfirmPasswordError = string.IsNullOrEmpty(ConfirmPassword) || !string.Equals(ConfirmPassword, Password);
            IsFirstNameEmpty = string.IsNullOrEmpty(FirstName);
            IsSurnameEmpty = string.IsNullOrEmpty(Surname);
            AssociationError = Association != null && string.IsNullOrEmpty(Association.name);
            StudyProgrammeError = string.IsNullOrEmpty(StudyProgramme);
            TicketCodeError = string.IsNullOrEmpty(TicketCode);
            int associationIndex = -1;

            if(!AssociationError)
            {
                for (int i = 0; i < Associations.Count; i++)
                {
                    if (Associations[i].name.Equals(Association.name))
                    {
                        associationIndex = i;
                        break;
                    }
                }
                if (associationIndex < 0)
                {
                    AssociationError = true;
                }
            }
            

            if (!isPasswordEmpty && !MailHasError && !ConfirmPasswordError && !IsFirstNameEmpty && !IsSurnameEmpty && !AssociationError && !StudyProgrammeError && !TicketCodeError)
            {
                // TODO: set TicketCodeError to false if it doesn't work and we get a false.
                // TODO: check for connection instead of crash
                HttpMessage httpMessage = await Login.Instance.DoRegisterAsync(new Model.RegisterModel {
                    code=TicketCode,
                    firstname=FirstName,
                    surname=Surname,
                    vereniging=associationIndex,
                    bus=UseBus,
                    programme=StudyProgramme,
                    programmeOther= StudyProgrammeOther,
                    companyName= CompanyName,
                    vegetarian=Vegetarian,
                    specialNeeds=OtherRemarks,
                    email=Mail,
                    password=Password,
                    confirm=ConfirmPassword,
                    privacyPolicyAgree=AgreePolicy,
                    subscribe=MailSubscribe});
                AppController.RemoveBusy(this);
                if (httpMessage.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "You are logged in.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", httpMessage.Message, "OK");
                }
            }
            AppController.RemoveBusy(this);
        }
    }
}
