using System.Collections.Generic;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PedroLamas.TheBroCode.Model;

namespace PedroLamas.TheBroCode.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMainModel _mainModel;
        private readonly INavigationService _navigationService;
        private readonly IEmailComposeService _emailComposeService;
        private readonly ISmsComposeService _smsComposeService;
        private readonly IShareStatusService _shareStatusService;

        #region Properties

        public IEnumerable<QuoteModel> Quotes
        {
            get
            {
                return _mainModel.Quotes;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return _mainModel.SelectedIndex;
            }
            set
            {
                _mainModel.SelectedIndex = value;

                RaisePropertyChanged(() => SelectedIndex);
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => Description);

                PreviousQuoteCommand.RaiseCanExecuteChanged();
                NextQuoteCommand.RaiseCanExecuteChanged();
            }
        }

        public string Title
        {
            get
            {
                return _mainModel.SelectedQuote.Title;
            }
        }

        public string Description
        {
            get
            {
                return _mainModel.SelectedQuote.Description;
            }
        }

        public RelayCommand PreviousQuoteCommand { get; private set; }

        public RelayCommand NextQuoteCommand { get; private set; }

        public RelayCommand RandomQuoteCommand { get; private set; }

        public RelayCommand ShareQuoteCommand { get; private set; }

        public RelayCommand TodaysQuoteCommand { get; private set; }

        public RelayCommand ShowAboutCommand { get; private set; }

        public RelayCommand ShareByEmailCommand { get; private set; }

        public RelayCommand ShareBySmsCommand { get; private set; }

        public RelayCommand ShareOnSocialNetworkCommand { get; private set; }

        #endregion

        public MainViewModel(IMainModel mainModel, INavigationService navigationService, IEmailComposeService emailComposeService, ISmsComposeService smsComposeService, IShareStatusService shareStatusService)
        {
            _mainModel = mainModel;
            _navigationService = navigationService;
            _emailComposeService = emailComposeService;
            _smsComposeService = smsComposeService;
            _shareStatusService = shareStatusService;

            PreviousQuoteCommand = new RelayCommand(OnPreviousQuoteCommand,
                () => _mainModel.SelectedIndex > 0);

            NextQuoteCommand = new RelayCommand(OnNextQuoteCommand,
                () => _mainModel.SelectedIndex < _mainModel.Quotes.Length - 1);

            RandomQuoteCommand = new RelayCommand(OnRandomQuoteCommand);

            TodaysQuoteCommand = new RelayCommand(OnTodaysQuoteCommand);

            ShowAboutCommand = new RelayCommand(OnShowAboutCommand);

            ShareByEmailCommand = new RelayCommand(OnShareByEmailCommand);

            ShareBySmsCommand = new RelayCommand(OnShareBySmsCommand);

            ShareOnSocialNetworkCommand = new RelayCommand(OnShareOnSocialNetworkCommand);
        }

        private void OnPreviousQuoteCommand()
        {
            SelectedIndex--;

            PreviousQuoteCommand.RaiseCanExecuteChanged();
            NextQuoteCommand.RaiseCanExecuteChanged();
        }

        private void OnNextQuoteCommand()
        {
            SelectedIndex++;

            PreviousQuoteCommand.RaiseCanExecuteChanged();
            NextQuoteCommand.RaiseCanExecuteChanged();
        }

        private void OnRandomQuoteCommand()
        {
            SelectedIndex = _mainModel.GetRandomQuoteIndex();
        }

        private void OnTodaysQuoteCommand()
        {
            SelectedIndex = _mainModel.GetTodaysQuoteIndex();
        }

        private void OnShowAboutCommand()
        {
            _navigationService.NavigateTo("/View/AboutPage.xaml");
        }

        private void OnShareByEmailCommand()
        {
            _emailComposeService.Show("The Bro Code", Title + "\n\n" + Description);
        }

        private void OnShareBySmsCommand()
        {
            _smsComposeService.Show(string.Empty, Title + ": " + Description);
        }

        private void OnShareOnSocialNetworkCommand()
        {
            _shareStatusService.Show(Title + ": " + Description);
        }
    }
}