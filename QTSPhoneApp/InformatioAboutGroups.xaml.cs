using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class InformatioAboutGroups : Page
    {
        public InformatioAboutGroups()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);
            NavigationHelper.LoadState += NavigationHelper_LoadState;
            NavigationHelper.SaveState += NavigationHelper_SaveState;
        }

        public ObservableCollection<MusicGroupApiModel> GroupListCollection { get; set; } = new ObservableCollection<MusicGroupApiModel>();
        public NavigationContext MyNavigationContext { get; set; }

        /// <summary>
        ///     Gets the <see cref="NavigationHelper" /> associated with this <see cref="Page" />.
        /// </summary>
        public NavigationHelper NavigationHelper { get; }

        /// <summary>
        ///     Gets the view model for this <see cref="Page" />.
        ///     This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel { get; } = new ObservableDictionary();


        public async void LoadInformationAboutGroup(string name)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"{ConnectionUri.Uri}/api/MusicGroupApi/{name}"))
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var desj = JsonConvert.DeserializeObject<List<MusicGroupApiModel>>(result);


                        GroupListCollection.Clear();

                        foreach (var variable in desj)
                        {
                            GroupListCollection.Add(variable);
                        }

                        foreach (var variable in GroupListCollection)
                        {
                            TitleBox.DataContext = variable.Name;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                var msb = new MessageDialog(exception.Message) {Title = "Alert"};
                await msb.ShowAsync();
            }
        }

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
            NavigationHelper.OnNavigatedTo(e);

            var tmp = (MusicGroupApiModel) e.Parameter;
            var musicGroupName = tmp.Name;

            LoadInformationAboutGroup(musicGroupName);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}