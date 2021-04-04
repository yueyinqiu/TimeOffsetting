using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeOffsetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOffsetting.Tests
{
    [TestClass()]
    public class Tests
    {
        [TestMethod()]
        public void Test()
        {
            Assert.AreEqual(TimeZoneInfo.Local.BaseUtcOffset, TimeOffsets.LocalZone);

            {
                var timeZones = TimeZoneInfo.GetSystemTimeZones();
                foreach (var timeZone in timeZones)
                {
                    Assert.AreEqual(timeZone.BaseUtcOffset, TimeOffsets.GetByTimeZone(timeZone));
                    Assert.AreEqual(timeZone.BaseUtcOffset, TimeOffsets.GetByTimeZone(timeZone.Id));
                }
                {
                    var timeZone = TimeZoneInfo.CreateCustomTimeZone(
                        "hello, timezone", new TimeSpan(2, 3, 0), null, null);
                    Assert.AreEqual(new TimeSpan(2, 3, 0), TimeOffsets.GetByTimeZone(timeZone));
                }
                Assert.AreEqual(new TimeSpan(8, 0, 0), TimeOffsets.GetByLongitude(+120));
            }
            var offset = TimeOffsets.GetByLongitude(-52.233);
            var p8 = TimeOffsets.GetByLongitude((double)+120);

            var now = DateTime.Now;
            var utcDateTime = new UtcDateTime(now, offset);
            Assert.AreEqual(now, utcDateTime.GetDateTime(offset));

            now = DateTime.UtcNow;
            utcDateTime = new UtcDateTime(now);
            Assert.AreEqual(now.AddHours(8), utcDateTime.GetDateTime(p8));
        }
    }
}