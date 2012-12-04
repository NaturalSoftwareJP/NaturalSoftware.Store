using Windows.ApplicationModel.Store;

namespace NaturalSoftware.Store
{
    /// <summary>
    /// アプリケーションライセンスについてのラッパー
    /// もしかしたら、ライブラリよりもファイルごとプロジェクトにコピーしたほうが良いかも
    /// </summary>
    public static class AppLicense
    {
        /// <summary>C:\Users\Kaoru\Documents\GitHub\NaturalSoftware.Store\NaturalSoftware.Store\NaturalSoftware.Store\AppLicense.cs
        /// シミュレーターかどうか
        /// </summary>
        public static bool IsSimulator
        {
            get;
            private set;
        }

        /// <summary>
        /// ライセンス情報
        /// </summary>
        public static LicenseInformation LicenseInformation
        {
            get;
            private set;
        }

        static AppLicense()
        {
#if DEBUG
            LicenseInformation = CurrentAppSimulator.LicenseInformation;
            IsSimulator = true;
#else
            LicenseInformation = CurrentApp.LicenseInformation;
            IsSimulator = false;
#endif
        }
    }
}
