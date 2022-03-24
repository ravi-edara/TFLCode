using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace TFLCodeChallenge.Tests.Unit
{
    [TestFixture]
    public class RoadStatusServiceTests
    {
        private ServiceProvider _serviceProvider;
        private string id;

        [SetUp]
        public void Setup()
        {
            //setup our DI
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IRoadStatusService, RoadStatusService>()
                .BuildServiceProvider();
        }

        [Test]
        public async Task GetRoadStatusById_Should_Not_Have_NullOrEmptyObject()
        {
            // Arrange
            var roadService = _serviceProvider.GetService<IRoadStatusService>();
            id = "A2";

            // Act
            var roadStatus = await roadService?.GetRoadStatusById(id)!;

            // Assert
            Assert.IsNotNull(roadStatus);
        }

        [Test]
        public async Task GetRoadStatusById_Should_Have_DisplayName_As_A2()
        {
            // Arrange
            var roadService = _serviceProvider.GetService<IRoadStatusService>();
            id = "A2";
            var displayName = "A2";

            // Act
            var roadStatus = await roadService?.GetRoadStatusById(id)!;

            // Assert
            Assert.AreEqual(displayName, roadStatus.DisplayName);
        }

        [Test]
        public async Task GetRoadStatusById_Should_Have_StatusSeverity_As_Good()
        {
            // Arrange
            var roadService = _serviceProvider.GetService<IRoadStatusService>();
            id = "A2";
            var statusSeverity = "Good";

            // Act
            var roadStatus = await roadService?.GetRoadStatusById(id)!;

            // Assert
            Assert.AreEqual(statusSeverity, roadStatus.StatusSeverity);
        }

        [Test]
        public async Task GetRoadStatusById_Should_Have_Status_SeverityDescription_As_NoExceptionalDelays()
        {
            // Arrange
            var roadService = _serviceProvider.GetService<IRoadStatusService>();
            id = "A2";
            var statusSeverityDescription = "No Exceptional Delays";

            // Act
            var roadStatus = await roadService?.GetRoadStatusById(id)!;

            // Assert
            Assert.AreEqual(statusSeverityDescription, roadStatus.StatusSeverityDescription);
        }

        [Test]
        public async Task GetRoadStatusById_Should_Have_NullOrEmptyObject()
        {
            // Arrange
            var roadService = _serviceProvider.GetService<IRoadStatusService>();
            id = "A233";

            // Act
            var roadStatus = await roadService?.GetRoadStatusById(id)!;

            // Assert
            Assert.IsNull(roadStatus);
        }

        [TearDown]
        public void Teardown()
        {
            //setup our DI
            _serviceProvider = null;
        }
    }
}