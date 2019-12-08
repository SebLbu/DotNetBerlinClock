using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private readonly ITimeConverter berlinClock = new TimeConverter();
        private string theTime;
        
        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            this.theTime = time;
        }
        
        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(theExpectedBerlinClockOutput, this.berlinClock.ConvertTime(this.theTime));
        }

        [Then(@"the user should get an error message")]
        public void ThenTheUserShouldGetAnErrorMessage(string theExpectedBerlinClockOutput)
        {
            try
            {
                this.berlinClock.ConvertTime(this.theTime);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, theExpectedBerlinClockOutput);
            }
        }
    }
}
