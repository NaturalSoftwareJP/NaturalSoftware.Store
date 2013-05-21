using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SensorStoreApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public LightSensor lightSensor
        {
            get;
            set;
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
        public SimpleOrientationSensor simpleOrientationSensor
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

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        async protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            // 光センサー
            lightSensor = LightSensor.GetDefault();
            if ( lightSensor != null ) {
                lightSensor.ReadingChanged += MainPage_ReadingChanged;
            }
            else {
                TextLight.Text = @"光センサーはありません";
            }

            //加速度センサー
            accelerometer = Accelerometer.GetDefault();
            if ( accelerometer != null ) {
                accelerometer.ReadingChanged += MainPage_ReadingChanged;
                accelerometer.Shaken += MainPage_Shaken;
            }
            else {
                TextAccelerometer.Text = @"加速度センサーはありません";
            }

            // ジャイロメーター
            gyrometer = Gyrometer.GetDefault();
            if ( gyrometer != null ) {
                gyrometer.ReadingChanged += MainPage_ReadingChanged;
            }
            else {
                TextGyrometer.Text = @"ジャイロメーターはありません";
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

            // 簡易方位センサー
            simpleOrientationSensor = SimpleOrientationSensor.GetDefault();
            if ( simpleOrientationSensor != null ) {
                simpleOrientationSensor.OrientationChanged += MainPage_OrientationChanged;
            }
            else {
                TextSimpleOrientation.Text = @"簡易方位センサーはありません";
            }

            // コンパス
            compass = Compass.GetDefault();
            if ( compass != null ) {
                compass.ReadingChanged += MainPage_ReadingChanged;
            }
            else {
                TextCompass.Text = @"コンパスはありません";
            }

            //try {
            //    geo.DesiredAccuracy = PositionAccuracy.High;
            //    var pos = await geo.GetGeopositionAsync();
            //    TextGeolocation.Text = string.Format( @"{0} {1} {2} {3} {4} {5} {6}",
            //        pos.Coordinate.Latitude, pos.Coordinate.Longitude, pos.Coordinate.Accuracy );
            //}
            //catch ( Exception ex ) {
            //    MessageDialog dlg=new MessageDialog( ex.Message );
            //    dlg.ShowAsync();
            //}
        }

        async void MainPage_ReadingChanged( Compass sender, CompassReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
                var n = args.Reading.HeadingTrueNorth;
                TextCompass.Text = string.Format( @"Compass:{0}, North:{1}", args.Reading.HeadingMagneticNorth,
                    n != null ? n.ToString() : @"Flse" );
            } );
        }

        async void MainPage_OrientationChanged( SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
                TextSimpleOrientation.Text = "SimpleOrientation : " +  args.Orientation.ToString();
            } );
        }

        async void MainPage_ReadingChanged( OrientationSensor sender, OrientationSensorReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
            } );
        }

        async void MainPage_ReadingChanged( Inclinometer sender, InclinometerReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
                TextInclinometer.Text = string.Format( @"Inclinometer : P={0} R={1} Y={2}", args.Reading.PitchDegrees, args.Reading.RollDegrees, args.Reading.YawDegrees );
            } );
        }

        async void MainPage_ReadingChanged( Gyrometer sender, GyrometerReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
                TextGyrometer.Text = string.Format( @"Gyrometer : X={0} Y={1} Z={2}", args.Reading.AngularVelocityX.ToString(), args.Reading.AngularVelocityY.ToString(), args.Reading.AngularVelocityZ.ToString() );
            } );
        }

        void MainPage_Shaken( Accelerometer sender, AccelerometerShakenEventArgs args )
        {
        }

        async void MainPage_ReadingChanged( Accelerometer sender, AccelerometerReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
                TextAccelerometer.Text = string.Format( @"Accelerometer : X={0} Y={1} Z={2}", args.Reading.AccelerationX.ToString(), args.Reading.AccelerationY.ToString(), args.Reading.AccelerationZ.ToString() );
            } );
        }

        async void MainPage_ReadingChanged( LightSensor sender, LightSensorReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
            {
                TextLight.Text = string.Format( @"Light Sensor : {0}", args.Reading.IlluminanceInLux.ToString() );
            } );
        }
    }
}
