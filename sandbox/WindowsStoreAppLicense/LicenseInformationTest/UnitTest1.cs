using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;

namespace LicenseInformationTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// シミュレーターをリロードする
        /// 
        /// コードサンプル
        ///  Windows 8 ハンズオン ラボ
        ///  http://msdn.microsoft.com/ja-JP/windows/apps/jj674832
        /// 
        /// XMLファイルの仕様
        /// http://msdn.microsoft.com/ja-jp/library/windows/apps/windows.applicationmodel.store.currentappsimulator.aspx
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static async Task ReloadSimulatorAsync( string fileName )
        {
            var file = await Package.Current.InstalledLocation.GetFileAsync( fileName );
            await CurrentAppSimulator.ReloadSimulatorAsync( file );
        }

        [TestMethod]
        public async Task 試用版ライセンス()
        {
            await ReloadSimulatorAsync( "Data\\license-trial.xml" );

            Assert.IsTrue( CurrentAppSimulator.LicenseInformation.IsActive );
            Assert.IsTrue( CurrentAppSimulator.LicenseInformation.IsTrial );
        }

        [TestMethod]
        public async Task 購入版ライセンス()
        {
            await ReloadSimulatorAsync( "Data\\license-full.xml" );

            Assert.IsTrue( CurrentAppSimulator.LicenseInformation.IsActive );
            Assert.IsFalse( CurrentAppSimulator.LicenseInformation.IsTrial );
        }
    }
}
