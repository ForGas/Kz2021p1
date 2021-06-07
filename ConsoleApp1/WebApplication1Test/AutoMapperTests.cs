using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebApplication1;
using WebApplication1.Profiles;

namespace WebApplication1Test
{
    [TestFixture]
    class AutoMapperTests
    {
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            mapperMock = new Mock<IMapper>();
        }

        [Test]
        public void BuildingProfiles_ItShouldCorrectConfigured()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<BuildingProfiles>());

            config.AssertConfigurationIsValid();
        }

        [Test]
        public void PersonalAccountProfiles_ItShouldCorrectConfigured()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PersonalAccountProfiles>());

            config.AssertConfigurationIsValid();
        }
    }
}
