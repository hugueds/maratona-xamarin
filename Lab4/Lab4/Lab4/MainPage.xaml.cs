using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab4
{
    public partial class MainPage : ContentPage
    {
        readonly AzureService azureService = new AzureService();
        public MainPage()
        {
            InitializeComponent();

            loginButton.Clicked += async (sender, args) =>
            {
                var user = await azureService.LoginAsync();
                infoLabel.Text = (user != null) ? $"Bem vindo {user.UserId}!" : "LOGIN FAILURE";
            };
        }
    }
}
