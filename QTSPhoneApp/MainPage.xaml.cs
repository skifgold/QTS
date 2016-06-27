using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using QTSPhoneApp.WebApiModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace QTSPhoneApp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        public ObservableCollection<ConcertApiModel> ValueList { get; set; } = new ObservableCollection<ConcertApiModel>();
        public NavigationContext MyNavigationContext { get; set; }
        public Customer OurMan { get; set; } = new Customer();

        /// <summary>
        ///     Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">
        ///     Event data that describes how this page was reached.
        ///     This parameter is typically used to configure the page.
        /// </param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            try
            {
                LoadPage();
            }
            catch (Exception ex)
            {
                var msb = new MessageDialog(ex.Message) {Title = "ALERT!"};
                await msb.ShowAsync();
            }
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void LoadPage()
        {
            try
            {
                var endUrl = string.Join("/", ConnectionUri.Uri, "api/ConcertApi");
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(endUrl))
                    {
                        if (!response.IsSuccessStatusCode)
                            return;

                        var result = await response.Content.ReadAsStringAsync();
                        var desJ = JsonConvert.DeserializeObject<List<ConcertApiModel>>(result);

                        ValueList.Clear();
                        foreach (var s in desJ)
                        {
                            ValueList.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msb = new MessageDialog(ex.Message) {Title = "Alert!"};
                await msb.ShowAsync();
            }
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var concertId = ((ConcertApiModel) e.ClickedItem).Id;
            var concertPoster = ((ConcertApiModel) e.ClickedItem).PosterUrl;
            OurMan.ConcertId = concertId;
            Frame.Navigate(typeof (MoreDetails), new NavigationContext
            {
                Id = concertId,
                Poster = concertPoster,
                Content = OurMan
            });
        }
    }
}