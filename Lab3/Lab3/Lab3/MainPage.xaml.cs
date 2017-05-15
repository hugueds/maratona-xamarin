using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.btnCarregar.Clicked += async (sender, e) =>
            {
                var tags = await Api.GetTagsAsync();
                this.lvwTags.ItemsSource = tags;
            };
        }
    }
}
