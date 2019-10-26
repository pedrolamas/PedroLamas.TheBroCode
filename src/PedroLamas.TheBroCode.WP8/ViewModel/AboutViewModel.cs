using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace PedroLamas.TheBroCode.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly IWebBrowserService _webBrowserService;
        private readonly IMarketplaceReviewService _marketplaceReviewService;
        private readonly IMarketplaceSearchService _marketplaceSearchService;
        private readonly IShareLinkService _shareLinkService;

        #region Properties

        public RelayCommand OpenHomepageCommand { get; private set; }

        public RelayCommand OpenTwitterCommand { get; private set; }

        public RelayCommand RateApplicationCommand { get; private set; }

        public RelayCommand ShareApplicationCommand { get; private set; }

        public RelayCommand MarketplaceSearchCommand { get; private set; }

        #endregion

        public AboutViewModel(IWebBrowserService webBrowserService, IMarketplaceReviewService marketplaceReviewService, IMarketplaceSearchService marketplaceSearchService, IShareLinkService shareLinkService)
        {
            _webBrowserService = webBrowserService;
            _marketplaceReviewService = marketplaceReviewService;
            _marketplaceSearchService = marketplaceSearchService;
            _shareLinkService = shareLinkService;

            OpenHomepageCommand = new RelayCommand(() =>
            {
                _webBrowserService.Show("http://www.pedrolamas.com");
            });

            OpenTwitterCommand = new RelayCommand(() =>
            {
                _webBrowserService.Show("http://twitter.com/pedrolamas");
            });

            RateApplicationCommand = new RelayCommand(() =>
            {
                _marketplaceReviewService.Show();
            });

            ShareApplicationCommand = new RelayCommand(() =>
            {
                _shareLinkService.Show("The Bro Code", "The Bro Code: Legen- wait for it -dary, legendary! via @pedrolamas", "http://windowsphone.com/s?appid=2f575845-7f15-4de2-acc8-cb68bcf954a7");
            });

            MarketplaceSearchCommand = new RelayCommand(() =>
            {
                _marketplaceSearchService.Show("Pedro Lamas");
            });
        }
    }
}