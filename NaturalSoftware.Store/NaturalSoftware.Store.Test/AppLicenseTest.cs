using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.ApplicationModel;

namespace NaturalSoftware.Store.Test
{
    [TestClass]
    public class AppLicenseTest
    {
#if DEBUG

        /// <summary>
        /// シミュレーターをリロードする
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static async Task ReloadSimulatorAsync( string fileName )
        {
            var file = await Package.Current.InstalledLocation.GetFileAsync( fileName );
            await Windows.ApplicationModel.Store.CurrentAppSimulator.ReloadSimulatorAsync( file );
        }

        [TestMethod]
        public void デバッグ時はシミュレーターでライセンス管理()
        {
            Assert.IsTrue( AppLicense.IsSimulator );
        }

        [TestMethod]
        public async Task 試用版ライセンス()
        {
            await ReloadSimulatorAsync( "Data\\license-trial.xml" );

            Assert.IsTrue( AppLicense.IsTrial );
            Assert.IsFalse( AppLicense.IsFull );
        }

        [TestMethod]
        public async Task 購入版ライセンス()
        {
            await ReloadSimulatorAsync( "Data\\license-full.xml" );

            Assert.IsFalse( AppLicense.IsTrial );
            Assert.IsTrue( AppLicense.IsFull );
        }

#else
        [TestMethod]
        public void リリース時は実環境でライセンス管理()
        {
            Assert.IsFalse( AppLicense.IsSimulator );
        }
#endif
    }
}
