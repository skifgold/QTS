using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using Windows.UI.Popups;
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
    public sealed partial class FanZonePage : Page
    {
        public FanZonePage()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);
            NavigationHelper.LoadState += NavigationHelper_LoadState;
            NavigationHelper.SaveState += NavigationHelper_SaveState;
        }

        public NavigationContext MyNavigationContext { get; set; }
        public ObservableCollection<ThirdWindowApiModel> ZoneCollection { get; set; } = new ObservableCollection<ThirdWindowApiModel>();

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

        private async void ZoneLoader(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"{ConnectionUri.Uri}/api/ThirdWindowApi/{id}"))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            return;
                        }

                        var result = await response.Content.ReadAsStringAsync();
                        var desj = JsonConvert.DeserializeObject<List<ThirdWindowApiModel>>(result);
                        ZoneCollection.Clear();

                        foreach (var variable in desj)
                        {
                        
                                ZoneCollection.Add(variable);
                            
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                var msb = new MessageDialog(ex.Message) {Title = "alert"};
                await msb.ShowAsync();
            }
        }

        private void GoToZoneTickets(object sender, ItemClickEventArgs e)
        {
            var fanZone = ((ThirdWindowApiModel) e.ClickedItem).Fan;

            var ticketT = ((ThirdWindowApiModel) e.ClickedItem).TypeOfTicket;
            var ticketPrice = ((ThirdWindowApiModel) e.ClickedItem).Price;

            var ourMan = MyNavigationContext.Content;

            ourMan.TicketType = ticketT;
            ourMan.Fan = fanZone;

            Frame.Navigate(typeof (TiketsPick), new NavigationContext
            {
                Id = MyNavigationContext.Id,
                Name = MyNavigationContext.Name,
                FanZone = fanZone,
                TypeOfTickets = ticketT,
                Price = ticketPrice,
                Content = ourMan
            });
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
            MyNavigationContext = (NavigationContext) e.Parameter;
            try
            {
                ZoneLoader(MyNavigationContext.Id);
            }
            catch (Exception ex)
            {
                var msb = new MessageDialog(ex.Message) {Title = "Alert"};
                await msb.ShowAsync();
            }


            NavigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}