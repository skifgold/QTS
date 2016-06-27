using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using QTSPhoneApp.Common;
using QTSPhoneApp.WebApiModels;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace QTSPhoneApp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SendData : Page
    {
        public SendData()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);
            NavigationHelper.LoadState += NavigationHelper_LoadState;
            NavigationHelper.SaveState += NavigationHelper_SaveState;
        }

        public NavigationContext MyNavigationContext { get; set; }

        /// <summary>
        ///     Gets the <see cref="NavigationHelper" /> associated with this <see cref="Windows.UI.Xaml.Controls.Page" />.
        /// </summary>
        public NavigationHelper NavigationHelper { get; }

        /// <summary>
        ///     Gets the view model for this <see cref="Windows.UI.Xaml.Controls.Page" />.
        ///     This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel { get; } = new ObservableDictionary();

        /// <summary>
        ///     Populates the page with content passed during navigation.  Any saved state is also
        ///     provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        ///     The source of the event; typically <see cref="NavigationHelper" />
        /// </param>
        /// <param name="e">
        ///     Event data that provides both the navigation parameter passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested and
        ///     a dictionary of state preserved by this page during an earlier
        ///     session.  The state will be null the first time a page is visited.
        /// </param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        ///     Preserves state associated with this page in case the application is suspended or the
        ///     page is discarded from the navigation cache.  Values must conform to the serialization
        ///     requirements of <see cref="SuspensionManager.SessionState" />.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper" /></param>
        /// <param name="e">
        ///     Event data that provides an empty dictionary to be populated with
        ///     serializable state.
        /// </param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }


        public string LoadData(Customer customer)
        {

            var json = JsonConvert.SerializeObject(customer);
            using (var client = new HttpClient())
            {
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var response =
                        client.PostAsync($"{ConnectionUri.Uri}/api/CustomerApi", stringContent)
                            .GetAwaiter()
                            .GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return result;
                }
            }
        }



        private async void LoadD_ToServer(object sender, RoutedEventArgs e)
        {
            try
            {


                var ourMan = MyNavigationContext.Content;
                ourMan.Name = FirstNameTextBox.Text;
                ourMan.Surname = SecondNameTextBox.Text;
                ourMan.Phone = TelephoneTextBox.Text;
                ourMan.Email = EmailTextBox.Text;

                var strs = new[] {ourMan.Name, ourMan.Surname, ourMan.Phone, ourMan.Email};

                if (strs.Any(x => string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x)))
                {
                    var message =
                        new MessageDialog("Not all inputs are filled!") {Title = "Something wrong!"}.ShowAsync();
                    return;

                }

                var tid = LoadData(ourMan);
                if (string.IsNullOrEmpty(tid))
                {
                    var md = new MessageDialog($"Problems when buying tickets.") { Title = "Problem!" }.ShowAsync();
                }
                else
                {
                    var md = new MessageDialog($"Purchase is complete! Your ticket number is {tid}.") { Title = "Congratulations!" }.ShowAsync();
                    Frame.Navigate(typeof(MainPage), new NavigationContext());
                }

                
            }
            catch (Exception ex)
            {
                var msg = new MessageDialog(ex.Message) { Title = "Allert!" };
                await msg.ShowAsync();
            }

        }

        #region NavigationHelper registration

        /// <summary>
        ///     The methods provided in this section are simply used to allow
        ///     NavigationHelper to respond to the page's navigation methods.
        ///     <para>
        ///         Page specific logic should be placed in event handlers for the
        ///         <see cref="NavigationHelper.LoadState" />
        ///         and <see cref="NavigationHelper.SaveState" />.
        ///         The navigation parameter is available in the LoadState method
        ///         in addition to page state preserved during an earlier session.
        ///     </para>
        /// </summary>
        /// <param name="e">
        ///     Provides data for navigation methods and event
        ///     handlers that cannot cancel the navigation request.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MyNavigationContext = (NavigationContext) e.Parameter;

            NavigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}