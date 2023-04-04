using isRock.LineBot;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartFMS.Extensions.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFMS.UnitTest.Line
{
    [TestClass()]
    public class LineBotsTests
    {

        [TestMethod()]
        public void MessageTest()
        {
            new LineBots().Message("image");
        }

    }
}
