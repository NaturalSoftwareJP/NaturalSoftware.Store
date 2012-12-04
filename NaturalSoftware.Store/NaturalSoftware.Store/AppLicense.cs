using Windows.ApplicationModel.Store;

namespace NaturalSoftware.Store
{
    /// <summary>
    /// アプリケーションライセンスについてのラッパー
    /// もしかしたら、ライブラリよりもファイルごとプロジェクトにコピーしたほうが良いかも
    /// </summary>
    public static class AppLicense
    {
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

        /// <summary>
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

        /// <summary>
        /// 試用版
        /// </summary>
        public static bool IsTrial
        {
            get
            {
                return LicenseInformation.IsActive && LicenseInformation.IsTrial;
            }
        }

        /// <summary>
        /// 購入版
        /// </summary>
        public static bool IsFull
        {
            get
            {
                return LicenseInformation.IsActive && !LicenseInformation.IsTrial;
            }
        }
    }
}
