using Lab4.Services;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab4
{
    public class AzureService
    {
        static readonly string AppUrl = "https://maratonaxamarinhugo.azurewebsites.net";
        public MobileServiceClient Client { get; set; } = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);
        }

        public async Task<MobileServiceUser> LoginAsync()
        {

            Initialize();

            var auth = DependencyService.Get<IAuthenticate>();

            var user = await auth.Authenticate(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("OPS!", "PARECE QUE VC TA VACILANDO!", "OK...");
                });

                return null;
            }

            return user;

        }
    }
}
