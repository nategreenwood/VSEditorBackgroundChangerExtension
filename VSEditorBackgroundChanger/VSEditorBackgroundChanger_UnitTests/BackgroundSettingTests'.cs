using Microsoft.VisualStudio.TestTools.UnitTesting;
using NateGreenwood.VSEditorBackgroundChanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VSEditorBackgroundChanger_UnitTests
{
    using System.IO;

    [TestClass]
    public class BackgroundSettingTests_
    {
        [TestMethod]
        public void CreateInstance_Test()
        {
            var settings = new BackgroundImageSettings(
                new Uri(Path.Combine(Directory.GetCurrentDirectory(), "config.dat")));

            Assert.IsNotNull(settings);
            Assert.IsInstanceOfType(settings, typeof (BackgroundImageSettings));
        }
    }
}
