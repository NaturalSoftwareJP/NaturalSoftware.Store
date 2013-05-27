using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SensorPhoneApp.Resources;
using Windows.Devices.Geolocation;
using Windows.Devices.Sensors;

namespace SensorPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // コンストラクター
        public MainPage()
        {
            InitializeComponent();

            // ApplicationBar をローカライズするためのサンプル コード
            //BuildLocalizedApplicationBar();
        }

        public Accelerometer accelerometer
        {
            get;
            set;
        }
        public Gyrometer gyrometer
        {
            get;
            set;
        }
        public Inclinometer inclinometer
        {
            get;
            set;
        }
        public OrientationSensor orientationSensor
        {
            get;
            set;
        }

        public Compass compass
        {
            get;
            set;
        }

        Geolocator geo = new Geolocator();

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            //加速度センサー
            accelerometer = Accelerometer.GetDefault();
            if ( accelerometer != null ) {
                accelerometer.ReadingChanged += MainPage_ReadingChanged;
                accelerometer.Shaken += MainPage_Shaken;
            }
            else {
                TextAccelerometer.Text = @"加速度センサーはありません";
            }

            // 傾斜センサー
            inclinometer = Inclinometer.GetDefault();
            if ( inclinometer != null ) {
                inclinometer.ReadingChanged += MainPage_ReadingChanged;
            }
            else {
                TextInclinometer.Text = @"傾斜センサーはありません";
            }

            // 方位センサー
            orientationSensor = OrientationSensor.GetDefault();
            if ( orientationSensor != null ) {
                orientationSensor.ReadingChanged += MainPage_ReadingChanged;
            }
            else {
                TextOrientation.Text = @"方位センサーはありません";
            }

            // コンパス
            try {
                compass = Compass.GetDefault();
                if ( compass != null ) {
                    compass.ReadingChanged += MainPage_ReadingChanged;
                }
            }
            catch ( Exception ) {
                TextCompass.Text = @"コンパスはありません";
            }

            // ジャイロメーター
            try {
                gyrometer = Gyrometer.GetDefault();
                if ( gyrometer != null ) {
                    gyrometer.ReadingChanged += MainPage_ReadingChanged;
                }
            }
            catch ( Exception ) {
                TextGyrometer.Text = @"ジャイロメーターはありません";
            }

            try {
                geo.DesiredAccuracy = PositionAccuracy.High;

                // http://benjaminbaldacci.com/blog/?p=249
                geo.MovementThreshold = 10;
                geo.PositionChanged += geo_PositionChanged;
            }
            catch ( Exception ex ) {
                TextGeolocation.Text = ex.Message;
            }
        }

        void geo_PositionChanged( Geolocator sender, PositionChangedEventArgs args )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                TextGeolocation.Text = string.Format( @"Geolocation:{0} {1} {2}",
                    args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude, args.Position.Coordinate.Accuracy );
            } ) );
        }

        void MainPage_ReadingChanged( Compass sender, CompassReadingChangedEventArgs args )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                var n = args.Reading.HeadingTrueNorth;
                TextCompass.Text = string.Format( @"Compass:{0}, North:{1}", args.Reading.HeadingMagneticNorth,
                    n != null ? n.ToString() : @"Flse" );
            } ));
        }

        void MainPage_ReadingChanged( OrientationSensor sender, OrientationSensorReadingChangedEventArgs args )
        {
        }

        void MainPage_ReadingChanged( Inclinometer sender, InclinometerReadingChangedEventArgs args )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                TextInclinometer.Text = string.Format( @"Inclinometer : P={0} R={1} Y={2}", args.Reading.PitchDegrees, args.Reading.RollDegrees, args.Reading.YawDegrees );
            } ) );
        }

        void MainPage_ReadingChanged( Gyrometer sender, GyrometerReadingChangedEventArgs args )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                TextGyrometer.Text = string.Format( @"Gyrometer : X={0} Y={1} Z={2}",
                    args.Reading.AngularVelocityX.ToString(),
                    args.Reading.AngularVelocityY.ToString(),
                    args.Reading.AngularVelocityZ.ToString() );
            } ) );
        }

        void MainPage_Shaken( Accelerometer sender, AccelerometerShakenEventArgs args )
        {
        }

        void MainPage_ReadingChanged( Accelerometer sender, AccelerometerReadingChangedEventArgs args )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                TextAccelerometer.Text = string.Format( @"Accelerometer : X={0} Y={1} Z={2}", args.Reading.AccelerationX.ToString(), args.Reading.AccelerationY.ToString(), args.Reading.AccelerationZ.ToString() );
            } ));
        }


        // ローカライズされた ApplicationBar を作成するためのサンプル コード
        //private void BuildLocalizedApplicationBar()
        //{
        //    // ページの ApplicationBar を ApplicationBar の新しいインスタンスに設定します。
        //    ApplicationBar = new ApplicationBar();

        //    // 新しいボタンを作成し、テキスト値を AppResources のローカライズされた文字列に設定します。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // AppResources のローカライズされた文字列で、新しいメニュー項目を作成します。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}