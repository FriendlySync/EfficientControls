using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace EfficientControls
{
    public sealed partial class Switch : Grid
    {
        public delegate void SwitchingEventHandler(object sender, SwitchingEventArgs e);
        public delegate void SwitchedEventHandler(object sender, EventArgs e);

        public event SwitchingEventHandler SwitchingEvent;
        public event SwitchedEventHandler SwitchedEvent;

        public static bool? defaultSwitchValue = null;

        public Switch()
        {
            this.InitializeComponent();
            ColumnDefinitionCollection columnsMain = grdMain.ColumnDefinitions;
            columnsMain[1].Width = SwitchBackWidth;
            imSwitch.Source = ImageSource.FromResource("EfficientControls.src.circle.png", Assembly.GetExecutingAssembly());
            lbSwitch.Text = Text;
            lbSwitch.TextColor = TextColor;
            frSwitch.BackgroundColor = Switched ? BackgroundSwitchOn : BackgroundSwitchOff;
            ColumnDefinitionCollection columns = grSwitch.ColumnDefinitions;
            columns[0].Width = Switched ? (SwitchBackWidth - SwitchFrontWidth - Math.Round(frSwitch.Padding.Left) - Math.Round(frSwitch.CornerRadius / 2)) : 0;
            frSwitch.WidthRequest = SwitchBackWidth;
            imSwitch.WidthRequest = SwitchFrontWidth;
            frSwitch.BorderColor = BorderColor;
            lbSwitchState.Text = Switched ? SwitchOnText : SwitchOffText;
            lbSwitchState.TextColor = TextColor;
        }

        public Switch(bool switchValue)
        {
            defaultSwitchValue = switchValue;
            this.InitializeComponent();
            ColumnDefinitionCollection columnsMain = grdMain.ColumnDefinitions;
            columnsMain[1].Width = SwitchBackWidth;
            imSwitch.Source = ImageSource.FromResource("EfficientControls.src.circle.png", Assembly.GetExecutingAssembly());
            lbSwitch.Text = Text;
            lbSwitch.TextColor = TextColor;
            frSwitch.BackgroundColor = switchValue ? BackgroundSwitchOn : BackgroundSwitchOff;
            ColumnDefinitionCollection columns = grSwitch.ColumnDefinitions;
            columns[0].Width = switchValue ? (SwitchBackWidth - SwitchFrontWidth - Math.Round(frSwitch.Padding.Left) - Math.Round(frSwitch.CornerRadius / 2)) : 0;
            frSwitch.WidthRequest = SwitchBackWidth;
            imSwitch.WidthRequest = SwitchFrontWidth;
            frSwitch.BorderColor = BorderColor;
            lbSwitchState.Text = switchValue ? SwitchOnText : SwitchOffText;
            lbSwitchState.TextColor = TextColor;
            Switched = switchValue;
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Switch), "", Xamarin.Forms.BindingMode.OneWay);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Switch), Color.Black, Xamarin.Forms.BindingMode.OneWay);
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public static readonly BindableProperty SwitchImageSourceProperty = BindableProperty.Create(nameof(SwitchImageSource), typeof(ImageSource), typeof(Switch), ImageSource.FromResource("EfficientControls.src.circle.png", Assembly.GetExecutingAssembly()), Xamarin.Forms.BindingMode.OneWay);
        public ImageSource SwitchImageSource
        {
            get
            {
                return (ImageSource)GetValue(SwitchImageSourceProperty);
            }

            set
            {
                SetValue(SwitchImageSourceProperty, value);
            }
        }

        public static readonly BindableProperty SwitchedProperty = BindableProperty.Create(nameof(Switched), typeof(bool), typeof(Switch), false, Xamarin.Forms.BindingMode.OneWay);
        public bool Switched
        {
            get
            {
                return (bool)GetValue(SwitchedProperty);
            }

            set
            {
                SetValue(SwitchedProperty, value);
            }
        }

        public static readonly BindableProperty BackgroundSwitchOnProperty = BindableProperty.Create(nameof(BackgroundSwitchOn), typeof(Color), typeof(Switch), Color.AliceBlue, Xamarin.Forms.BindingMode.OneWay);
        public Color BackgroundSwitchOn
        {
            get
            {
                return (Color)GetValue(BackgroundSwitchOnProperty);
            }

            set
            {
                SetValue(BackgroundSwitchOnProperty, value);
            }
        }

        public static readonly BindableProperty BackgroundSwitchOffProperty = BindableProperty.Create(nameof(BackgroundSwitchOff), typeof(Color), typeof(Switch), Color.AliceBlue, Xamarin.Forms.BindingMode.OneWay);
        public Color BackgroundSwitchOff
        {
            get
            {
                return (Color)GetValue(BackgroundSwitchOffProperty);
            }

            set
            {
                SetValue(BackgroundSwitchOffProperty, value);
            }
        }

        public static readonly BindableProperty SwitchBackWidthProperty = BindableProperty.Create(nameof(SwitchBackWidth), typeof(int), typeof(Switch), 50, Xamarin.Forms.BindingMode.OneWay);
        public int SwitchBackWidth
        {
            get
            {
                return (int)GetValue(SwitchBackWidthProperty);
            }

            set
            {
                SetValue(SwitchBackWidthProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Switch), Color.Black, Xamarin.Forms.BindingMode.OneWay);
        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }

            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty SwitchFrontWidthProperty = BindableProperty.Create(nameof(SwitchFrontWidth), typeof(int), typeof(Switch), 15, Xamarin.Forms.BindingMode.OneWay);
        public int SwitchFrontWidth
        {
            get
            {
                return (int)GetValue(SwitchFrontWidthProperty);
            }

            set
            {
                SetValue(SwitchFrontWidthProperty, value);
            }
        }

        public static readonly BindableProperty SwitchOnTextProperty = BindableProperty.Create(nameof(SwitchOnText), typeof(string), typeof(Switch), "On", Xamarin.Forms.BindingMode.OneWay);
        public string SwitchOnText
        {
            get
            {
                return (string)GetValue(SwitchOnTextProperty);
            }

            set
            {
                SetValue(SwitchOnTextProperty, value);
            }
        }

        public static readonly BindableProperty SwitchOffTextProperty = BindableProperty.Create(nameof(SwitchOffText), typeof(string), typeof(Switch), "Off", Xamarin.Forms.BindingMode.OneWay);
        public string SwitchOffText
        {
            get
            {
                return (string)GetValue(SwitchOffTextProperty);
            }

            set
            {
                SetValue(SwitchOffTextProperty, value);
            }
        }

        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(Switch), 12, Xamarin.Forms.BindingMode.OneWay);
        public int ImageHeight
        {
            get
            {
                return (int)GetValue(ImageHeightProperty);
            }

            set
            {
                SetValue(ImageHeightProperty, value);
            }
        }

        public void ChangeSwitch(object sender, EventArgs e)
        {
            bool switched = !Switched;
            defaultSwitchValue = null;

            SwitchingEventArgs args = new SwitchingEventArgs();
            args.Cancel = false;
            args.NewValue = switched;

            SwitchingEvent?.Invoke(this, args);
            if (args.Cancel)
            {
                return;
            }

            Switched = switched;
            SwitchedEvent?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                lbSwitch.Text = Text;
            }
            if (propertyName == TextColorProperty.PropertyName)
            {
                lbSwitch.TextColor = TextColor;
                lbSwitchState.TextColor = TextColor;
            }
            if (propertyName == SwitchedProperty.PropertyName)
            {
                if (Switched == defaultSwitchValue)
                {
                    return;
                }
                ColumnDefinitionCollection columns = grSwitch.ColumnDefinitions;
                double curWidth = columns[0].Width.Value;
                double newWidth = Switched ? (SwitchBackWidth - SwitchFrontWidth - Math.Round(frSwitch.Padding.Left) - Math.Round(frSwitch.CornerRadius / 2)) : 0;
                Animation animation = new Animation(SetColumn0Width, curWidth, newWidth);
                animation.Commit(this, "move", 12, 120, Easing.Linear);

                lbSwitchState.Text = Switched ? SwitchOnText : SwitchOffText;
                frSwitch.BackgroundColor = Switched ? BackgroundSwitchOn : BackgroundSwitchOff;
            }
            if (propertyName == BackgroundSwitchOnProperty.PropertyName)
            {
                frSwitch.BackgroundColor = Switched ? BackgroundSwitchOn : BackgroundSwitchOff;
            }
            if (propertyName == BackgroundSwitchOffProperty.PropertyName)
            {
                frSwitch.BackgroundColor = Switched ? BackgroundSwitchOn : BackgroundSwitchOff;
            }
            if (propertyName == SwitchBackWidthProperty.PropertyName)
            {
                ColumnDefinitionCollection columnsMain = grdMain.ColumnDefinitions;
                columnsMain[1].Width = SwitchBackWidth;
                frSwitch.WidthRequest = SwitchBackWidth;
            }
            if (propertyName == SwitchFrontWidthProperty.PropertyName)
            {
                imSwitch.WidthRequest = SwitchFrontWidth;
            }
            if (propertyName == BorderColorProperty.PropertyName)
            {
                frSwitch.BorderColor = BorderColor;
            }
            if (propertyName == SwitchOnTextProperty.PropertyName)
            {
                lbSwitchState.Text = Switched ? SwitchOnText : SwitchOffText;
            }
            if (propertyName == SwitchOffTextProperty.PropertyName)
            {
                lbSwitchState.Text = Switched ? SwitchOnText : SwitchOffText;
            }
            if (propertyName == SwitchImageSourceProperty.PropertyName)
            {
                imSwitch.Source = SwitchImageSource;
            }
            if (propertyName == ImageHeightProperty.PropertyName)
            {
                imSwitch.HeightRequest = ImageHeight;
            }
        }

        public static readonly BindableProperty UserLongProperty = BindableProperty.Create(nameof(UserLong), typeof(long), typeof(Switch), (long)0, Xamarin.Forms.BindingMode.OneWay);
        public long UserLong
        {
            get
            {
                return (long)GetValue(UserLongProperty);
            }

            set
            {
                SetValue(UserLongProperty, value);
            }
        }

        public static readonly BindableProperty UserStateProperty = BindableProperty.Create(nameof(UserState), typeof(object), typeof(Switch), null, Xamarin.Forms.BindingMode.OneWay);
        public object UserState
        {
            get
            {
                return (object)GetValue(UserStateProperty);
            }

            set
            {
                SetValue(UserStateProperty, value);
            }
        }

        private void SetColumn0Width(double width)
        {
            ColumnDefinitionCollection columns = grSwitch.ColumnDefinitions;
            columns[0].Width = width;
        }
    }

    public class SwitchingEventArgs
    {
        public bool NewValue { get; set; }
        public bool Cancel { get; set; }
    }
}
