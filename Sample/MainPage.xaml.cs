using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Button btn = new Button();
            btn.BackgroundColor = Color.Aqua;
            btn.HorizontalOptions = LayoutOptions.FillAndExpand;
            btn.VerticalOptions = LayoutOptions.FillAndExpand;
            btn.Text = "First";
            btn.TextColor = Color.Black;
            clFilesAndFolders.SetView(btn);
        }

        private void SetContent_Clicked(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.BackgroundColor = Color.Cornsilk;
            btn.HorizontalOptions = LayoutOptions.FillAndExpand;
            btn.VerticalOptions = LayoutOptions.FillAndExpand;
            btn.Text = "Button";
            btn.TextColor = Color.Black;
            clFilesAndFolders.SetView(btn);
        }

        private void SlideLeft_Clicked(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.BackgroundColor = Color.GreenYellow;
            btn.HorizontalOptions = LayoutOptions.FillAndExpand;
            btn.VerticalOptions = LayoutOptions.FillAndExpand;
            btn.Text = "Left";
            btn.TextColor = Color.Black;
            clFilesAndFolders.SlideFromLeft(btn, 3000);
        }

        private void SlideRight_Clicked(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.BackgroundColor = Color.DeepPink;
            btn.HorizontalOptions = LayoutOptions.FillAndExpand;
            btn.VerticalOptions = LayoutOptions.FillAndExpand;
            btn.Text = "Right";
            btn.TextColor = Color.Black;
            clFilesAndFolders.SlideFromRight(btn, 3000);
        }

        private void ecsSample_SwitchedEvent(object sender, EventArgs e)
        {
            Console.WriteLine("ecsSample_SwitchedEvent");
        }

        private void ecsSample_SwitchingEvent(object sender, EfficientControls.SwitchingEventArgs e)
        {
            Console.WriteLine("ecsSample_SwitchingEvent");
        }
    }
}
