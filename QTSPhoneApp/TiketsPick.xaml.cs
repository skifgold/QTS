using System;
using System.Diagnostics;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using QTSPhoneApp.Common;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace QTSPhoneApp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TiketsPick : Page
    {
        public TiketsPick()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);
            NavigationHelper.LoadState += NavigationHelper_LoadState;
            NavigationHelper.SaveState += NavigationHelper_SaveState;
        }
        public NavigationContext MyNavigationContext { get; set; }
        public int AviableTickets { get; set; }

        /// <summary>
        ///     Gets the <see cref="NavigationHelper" /> associated with this <see cref="Page" />.
        /// </summary>
        public NavigationHelper NavigationHelper { get; }

        /// <summary>
        ///     Gets the view model for this <see cref="Page" />.
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

        private async void LoadTickets(int id, string fanZone, string ticketType)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"{ConnectionUri.Uri}/api/FourthWindowApi/{id}/{fanZone}/{ticketType}"))
                    {
                        if (!response.IsSuccessStatusCode)
                            return;

                        var result = await response.Content.ReadAsStringAsync();
                        var desj = JsonConvert.DeserializeObject(result);

                        AviableTickets = int.Parse(desj.ToString());


                     
                        ticketSlider.DataContext = AviableTickets;
                        LeftTicketsBlock.DataContext = AviableTickets;
                    }
                }
            }
            catch (Exception ex)
            {
                var msb = new MessageDialog(ex.Message) {Title = "Alert"};
                await msb.ShowAsync();
            }
        }

        private void Buy_OnClick(object sender, RoutedEventArgs e)
        {
            var ourMan = MyNavigationContext.Content;
            ourMan.Status = 0;
            ourMan.Count = (int) ticketSlider.Value;

            Frame.Navigate(typeof (SendData), new NavigationContext
            {
                TicketStatus = 0,
                TicketsCount = (int) ticketSlider.Value,
                Content = ourMan
            });
        }

        private void Reserve_OnClick(object sender, RoutedEventArgs e)
        {
            var ourMan = MyNavigationContext.Content;
            ourMan.Status = 1;
            ourMan.Count = (int) ticketSlider.Value;

            Frame.Navigate(typeof (SendData), new NavigationContext
            {
                Content = ourMan,
                TicketStatus = 1,
                TicketsCount = (int) ticketSlider.Value
            });


            var money = (MyNavigationContext.Price * ticketSlider.Value)/5;
            var percent = $"{money:C}";

            var msb = new MessageDialog($"20%  ({(percent)}) of current amount will be taken from your account. \n\n Remember! \n You need to buy ticket`s before three days to the concert. Otherwise, your purchase will be denied without money back.") {Title = "Reservation"}.ShowAsync();
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
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedTo(e);

            MyNavigationContext = (NavigationContext) e.Parameter;

            MoneyBlock.DataContext = $"{MyNavigationContext.Price:C}";
           

            try
            {
                LoadTickets(MyNavigationContext.Id, MyNavigationContext.FanZone, MyNavigationContext.TypeOfTickets);
            }
            catch (Exception ex)
            {
                var msb = new MessageDialog(ex.Message) {Title = "Alert!"};
                await msb.ShowAsync();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void TicketSlider_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (MyNavigationContext == null)
            {
                var msb = new MessageDialog("Something wrong with MyNavigationContext") {Title = "Alert"};
                return;
            }

            var money = MyNavigationContext.Price * ticketSlider.Value;
            MoneyBlock.DataContext = $"{money:C}";
        }
    }
}