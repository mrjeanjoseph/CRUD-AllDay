using Caliburn.Micro;
using CCMS.DesktopService;
using CCMS.DesktopService.Api;
using CCMS.DesktopUI.Models;

namespace CCMS.DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SimpleContainer _container;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;

        public ShellViewModel(IEventAggregator events, SimpleContainer container, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _container = container;
            _user = user;
            _apiHelper = apiHelper;

            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(_container.GetInstance<LoginViewModel>(), new CancellationToken());
        }

        public bool IsUserLoggedIn
        {
            get
            {
                bool isLoggedIn = false;
                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    isLoggedIn = true;
                }

                return isLoggedIn;
            }
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_container.GetInstance<LoginViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsUserLoggedIn);
        }

        public async Task LogOut()
        {
            _user.ResetUserData();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(_container.GetInstance<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsUserLoggedIn);
        }

        public async Task UserManagement()
        {
            await ActivateItemAsync(_container.GetInstance<LoginViewModel>(), new CancellationToken());
        }

        public void ExitApplication()
        {
            _user.ResetUserData();
            _apiHelper.LogOffUser();
            TryCloseAsync();
        }

    }
}
