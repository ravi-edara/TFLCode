using System;
using System.Net;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace TFLCodeChallenge.Tests.Acceptance.StepDefinitions
{
    [Binding]
    public class RoadStatusStepDefinitions
    {
        private string _roadId;

        [Given(@"a valid road id (.*) is specified")]
        [Given(@"an invalid road id (.*) is specified")]
        public void GivenAValidRoadAIsSpecified(string id)
        {
            _roadId = id;
        }

        [When(@"the client is run")]
        public void WhenTheClientIsRun()
        {
            var uri = $"https://api.tfl.gov.uk/Road/{_roadId}";
            var response = WebRequest.CreateWebRequest(uri);
            ScenarioContext.Current.Add("RoadJson", response.As<Road[]>());
        }

        //[Then(@"the road (.*) should be displayed")]
        //public void ThenTheRoadDisplayNameShouldBeDisplayed(string displayName)
        //{
        //    var data = ScenarioContext.Current["RoadJson"].As<Road[]>().FirstOrDefault();
        //    data.DisplayName.Should().BeEquivalentTo(displayName);
        //}

        [Then(@"the road (.*) should be displayed as (.*)")]
        public void ThenTheRoad(string propertyName, string dataToCheck)
        {
            var data = ScenarioContext.Current["RoadJson"].As<Road[]>().FirstOrDefault();
            switch (propertyName.ToLower())
            {
                case "displayName":
                    {
                        data.DisplayName.Should().BeEquivalentTo(dataToCheck);
                        break;
                    }
                case "statusSeverity":
                    {
                        data.DisplayName.Should().BeEquivalentTo(dataToCheck);
                        break;
                    }
            }
        }

        [Then(@"the application should return an informative error")]
        public void ThenTheApplicationShouldReturnAnInformativeError()
        {
            var data = ScenarioContext.Current["RoadJson"].As<Road[]>();
            data.Should().BeNull();
        }
    }
}