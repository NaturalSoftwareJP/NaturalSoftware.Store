using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NfcPhoneApp.Resources;
using Windows.Networking.Proximity;

namespace NfcPhoneApp
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

        ProximityDevice proximityDevice = null;

        long subscribeId = -1;
        long publishId = -1;

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            base.OnNavigatedTo( e );

            try {

                // NFCデバイスがない場合にはnullが返る
                // NFCデバイスがあり、WMAppManifest.xmlの「機能｜ID_CAP_PROXINITY」にチェックがない場合は例外が発生する
                proximityDevice = ProximityDevice.GetDefault();
                if ( proximityDevice == null ) {
                    throw new Exception( "NFCデバイスが見つかりません" );
                }

                // デバイスの認識、消失イベント
                proximityDevice.DeviceArrived += proximityDevice_DeviceArrived;
                proximityDevice.DeviceDeparted += proximityDevice_DeviceDeparted;

                TextSendMessage.Text = @"Hello NFC!! From Windows Phone";
            }
            catch ( Exception ex ) {
                MessageBox.Show( ex.Message );

                TextMessage.Text = ex.Message;
            }
        }

        // デバイスが離れた
        void proximityDevice_DeviceDeparted( ProximityDevice sender )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                TextMessage.Text = @"デバイスがなくなりました";
            } ));
        }

        // デバイスを認識した
        void proximityDevice_DeviceArrived( ProximityDevice sender )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                TextMessage.Text = @"デバイスを見つけました";
            } ) );
        }

        private void ButtonSend_Click( object sender, RoutedEventArgs e )
        {
            if ( proximityDevice != null ) {
                StopPublishingMessage();

                // メッセージを送る
                proximityDevice.PublishMessage( "Windows.SampleMessageType", TextSendMessage.Text, MessageTransmittedHandler );
            }
        }

        private void StopPublishingMessage()
        {
            if ( publishId != -1 ) {
                proximityDevice.StopPublishingMessage( publishId );
                publishId = -1;
            }
        }

        private void MessageTransmittedHandler( ProximityDevice sender, long messageId )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                StopPublishingMessage();

                TextMessage.Text = @"Message Transmitted!!";
            } ) );
        }

        private void ButtonRecieve_Click( object sender, RoutedEventArgs e )
        {
            if ( proximityDevice != null ) {
                StopSubscribingForMessage();

                // メッセージを受ける
                subscribeId = proximityDevice.SubscribeForMessage( "Windows.SampleMessageType", MessageReceivedHandler );

                TextMessage.Text = string.Format( @"SubscribeForMessage:{0}", subscribeId );
            }
        }

        private void StopSubscribingForMessage()
        {
            if ( subscribeId != -1 ) {
                proximityDevice.StopSubscribingForMessage( subscribeId );
                subscribeId = -1;
            }
        }

        private void MessageReceivedHandler( ProximityDevice sender, ProximityMessage message )
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                StopSubscribingForMessage();

                TextMessage.Text = @"Message Received!!";
                TextRecieveMessage.Text = message.DataAsString;
            } ) );
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