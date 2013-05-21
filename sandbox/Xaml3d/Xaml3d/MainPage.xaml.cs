using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Xaml3d
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

        Accelerometer accelerometer;
        Inclinometer inclinometer;
        Compass compass;

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            accelerometer = Accelerometer.GetDefault();
            if ( accelerometer!= null ) {
                accelerometer.ReadingChanged += accelerometer_ReadingChanged;
            }

            inclinometer = Inclinometer.GetDefault();
            if ( inclinometer != null ) {
                inclinometer.ReadingChanged += inclinometer_ReadingChanged;
            }

            compass = Compass.GetDefault();
            if ( compass != null ) {
                compass.ReadingChanged += compass_ReadingChanged;
            }
        }

        async void inclinometer_ReadingChanged( Inclinometer sender, InclinometerReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ProjectionImage.RotationX = 90 - args.Reading.PitchDegrees;
                ProjectionImage.RotationZ = args.Reading.RollDegrees;
                ProjectionImage.RotationY = -args.Reading.YawDegrees;
            } );
        }

        async void compass_ReadingChanged( Compass sender, CompassReadingChangedEventArgs args )
        {
            await Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
            } );
        }

        void accelerometer_ReadingChanged( Accelerometer sender, AccelerometerReadingChangedEventArgs args )
        {
        }
    }
}
