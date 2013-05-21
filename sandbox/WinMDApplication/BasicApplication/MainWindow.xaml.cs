using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Devices.Sensors;
using Windows.UI.Xaml;

namespace BasicApplication
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public LightSensor lightSensor;

        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            // 光センサー
            lightSensor = LightSensor.GetDefault();
            if ( lightSensor == null ) {
                TextLight.Text = @"光センサーはありません";
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds( 100 );
            timer.add_Tick( timer_Tick );
            timer.Start();
        }

        void timer_Tick( object sender, object e )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                var reading = lightSensor.GetCurrentReading();
                TextLight.Text = string.Format( @"Light Sensor : {0}", reading.IlluminanceInLux.ToString() );
            } ) );
        }
    }
}
