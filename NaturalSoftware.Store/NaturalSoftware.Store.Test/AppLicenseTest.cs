using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace NaturalSoftware.Store.Test
{
    [TestClass]
    public class AppLicenseTest
    {
#if DEBUG
        [TestMethod]
        public void デバッグ時はシミュレーターでライセンス管理()
        {
            Assert.IsTrue( AppLicense.IsSimulator );
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
