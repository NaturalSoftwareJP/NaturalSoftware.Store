using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace NfcStoreApp
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

        ProximityDevice proximityDevice = null;

        long subscribeId = -1;
        long publishId = -1;

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try {

                // NFCデバイスがない場合にはnullが返る
                // NFCデバイスがあり、Package.appxmanifestの「機能｜近接」にチェックがない場合は例外が発生する
                proximityDevice = ProximityDevice.GetDefault();
                if ( proximityDevice == null ) {
                    throw new Exception( "NFCデバイスが見つかりません" );
                }

                proximityDevice.DeviceArrived += proximityDevice_DeviceArrived;
                proximityDevice.DeviceDeparted += proximityDevice_DeviceDeparted;

                TextSendMessage.Text = @"Hello NFC!! From Windows Store App";
            }
            catch ( Exception ex ) {
                MessageDialog dlg = new MessageDialog( ex.Message );
                dlg.ShowAsync();

                TextMessage.Text = ex.Message;
            }
        }

        // 対応端末がなくなった
        async void proximityDevice_DeviceDeparted( ProximityDevice sender )
        {
            await Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                TextMessage.Text = @"デバイスがなくなりました";
            } );
        }

        // 対応端末を発見した
        async void proximityDevice_DeviceArrived( ProximityDevice sender )
        {
            await Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                TextMessage.Text = @"デバイスを見つけました";
            } );
        }

        private void ButtonSend_Click( object sender, RoutedEventArgs e )
        {
            if ( proximityDevice != null ) {
                StopPublishingMessage();

                publishId = proximityDevice.PublishMessage( "Windows.SampleMessageType", TextSendMessage.Text, MessageTransmittedHandler );
                TextMessage.Text = @"Message Transmitting!!";
            }
        }

        async private void MessageTransmittedHandler( ProximityDevice sender, long messageId )
        {
            await Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StopPublishingMessage();

                TextMessage.Text = @"Message Transmitted!!";
            } );
        }

        private void ButtonRecieve_Click( object sender, RoutedEventArgs e )
        {
            if ( proximityDevice != null ) {
                StopSubscribingForMessage();

                subscribeId = proximityDevice.SubscribeForMessage( "Windows.SampleMessageType", MessageReceivedHandler );
                TextMessage.Text = string.Format( @"SubscribeForMessage:{0}", subscribeId );
            }
        }

        async private void MessageReceivedHandler( ProximityDevice sender, ProximityMessage message )
        {
            await Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                StopSubscribingForMessage();

                TextMessage.Text = @"Message Received!!";
                TextRecieveMessage.Text = message.DataAsString;
            } );
        }

        private void StopPublishingMessage()
        {
            if ( publishId != -1 ) {
                proximityDevice.StopPublishingMessage( publishId );
                publishId = -1;
            }
        }

        private void StopSubscribingForMessage()
        {
            if ( subscribeId != -1 ) {
                proximityDevice.StopSubscribingForMessage( subscribeId );
                subscribeId = -1;
            }
        }
    }
}
