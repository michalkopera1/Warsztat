using NUnit.Framework;
using Warsztat;

namespace TestWarsztat
{
    [TestFixture]
    public class TimeTests
    {
        [Test]
        public void Constructor_ValidTime_CreatesTime()
        {
            var time = new Time(23, 59, 59);
            Assert.AreEqual(23, time.Hours);
            Assert.AreEqual(59, time.Minutes);
            Assert.AreEqual(59, time.Seconds);
        }

        [Test]
        public void ToString_ReturnsCorrectFormat()
        {
            var time = new Time(7, 5, 9);
            Assert.AreEqual("07:05:09", time.ToString());
        }

        [Test]
        public void Plus_AddsTimePeriodCorrectly()
        {
            var time = new Time(23, 59, 30);
            var period = new TimePeriod(90); 
            var newTime = time + period;
            Assert.That(newTime.ToString(), Is.EqualTo("00:01:00"));

        }

        [Test]
        public void CompareTo_CorrectOrder()
        {
            var time1 = new Time(10, 0, 0);
            var time2 = new Time(10, 0, 1);
            Assert.Less(time1.CompareTo(time2), 0);
        }

        [Test]
        public void Equals_CorrectlyCompares()
        {
            var time1 = new Time(12, 30, 45);
            var time2 = new Time(12, 30, 45);
            Assert.True(time1 == time2);
        }
    }
}