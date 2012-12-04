// アプリの試用版の作成方法 (Windows)
// http://msdn.microsoft.com/ja-jp/library/windows/apps/hh694065.aspx?cs-save-lang=1&cs-lang=csharp
// 
// アプリ内購入をサポートする方法 (Windows)
// http://msdn.microsoft.com/ja-jp/library/windows/apps/hh694067.aspx
using System;
using NaturalSoftware.Store;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace WindowsStoreAppLicense
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

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // アプリの実行中にライセンスが変更されたときに通知を受け取るイベント ハンドラーを追加
            AppLicense.LicenseInformation.LicenseChanged += LicenseInformation_LicenseChanged;
            ReloadLicense();
        }

        void LicenseInformation_LicenseChanged()
        {
            ReloadLicense();
        }

        // CurrentAppSimulator は "WindowsStoreProxy.xml" という XML ファイルからテスト専用のライセンス情報を取得します
        // 
        // WindowsStoreProxy.xml のありか
        //  C:\Users\<ユーザー名>\AppData\Local\Packages\<パッケージファミリ名>\LocalState\Microsoft\Windows Store\ApiData
        // アプリケーションのパッケージファミリ名は Package.appxmanifest の 「パッケージ化｜パッケージファミリ名」にあります
        private void ReloadLicense()
        {
            // ライセンスがアクティブである
            if ( AppLicense.LicenseInformation.IsActive ) {
                // 試用版
                if ( AppLicense.LicenseInformation.IsTrial ) {
                    textLicense.Text = "試用版";

                    var longDateFormat = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter( "longdate" );

                    // Display the expiration date using the DateTimeFormatter. 
                    // For example, longDateFormat.Format(licenseInformation.ExpirationDate)
                    // 残りの試用期間
                    var daysRemaining = (AppLicense.LicenseInformation.ExpirationDate - DateTime.Now).Days;

                    // Let the user know the number of days remaining before the feature expires
                    textRemainDays.Text = daysRemaining.ToString();
                }
                // 通常版
                else {
                    textLicense.Text = "購入版";
                }
            }
            // ライセンスがアクティブではない(何かしら不正な状況)
            else {
            }
        }
    }
}
