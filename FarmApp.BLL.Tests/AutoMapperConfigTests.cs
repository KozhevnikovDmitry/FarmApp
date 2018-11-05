using AutoMapper;
using FarmApp.BLL.DTO;
using FarmApp.DAL;
using NUnit.Framework;
/*
 * CR-1 - remove redundant usings below
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.BLL.Tests
{
    /*
     * CR-1
     * In comments to AutoMapperConfig classes and DTOs classes I suggested to get rid of redundant DTOs in mapping of view models and entities
     * So tests for automapper mapping must test:
     * 1. Mapping of regions, farmers and agricultures to NamedItemViewModel
     * 2. FarmCropViewModel to Farm entity with singe Crop entity in member collection
     * 3. Farm entity to FarmViewModel
     *
     * However I will add comments to tests below, to prevent the same issues in similar future tests.
     */
    [TestFixture]
    public class AutoMapperConfigTests
    {
        private IMapper mapper;

        [SetUp]
        public void Configure()
        {
            /*
             * CR-1 - move getting mapper into tests, get rid of mapper field
             */
            mapper = AutoMapperConfig.GetMapper();
        }

        [Test]
        public void Mapping_FarmCropDtoToFarm_IsValid()
        {
            /*
             * CR-1 - structure test in Arrange-Act-Assert manner
             */
            var from = new FarmCropDto() { AgricultureId = 1, Area = 2, FarmerId = 3, Gather = 4, Name = "abc", RegionId = 5 };
            var to = mapper.Map<FarmCropDto, Farm>(from);

            /*
             * CR-1
             * Separate checkings into different calls
             * Target code here:
             * Assert.AreEqual(to.Name, "abc");
             * Assert.AreEqual(to.Area, 2);
             * Assert.AreEqual(to.FarmerId, 3);
             * Assert.AreEqual(to.RegionId, 5);
             */
            Assert.IsTrue(to.Name == "abc" && to.Area == 2 && to.FarmerId == 3 && to.RegionId == 5);
        }

        [Test]
        public void Mapping_FarmCropDtoToCrop_IsValid()
        {
            /*
             * CR-1 - structure test in Arrange-Act-Assert manner
             */
            var from = new FarmCropDto() { AgricultureId = 1, Area = 2, FarmerId = 3, Gather = 4, Name = "abc", RegionId = 5 };
            var to = mapper.Map<FarmCropDto, Crop>(from);

            /*
             * CR-1
             * Separate checkings into different calls
             * Target code here:
             * Assert.AreEqual(to.AgricultureId, 1);
             * Assert.AreEqual(to.Gather, 4);
             */
            Assert.IsTrue(to.AgricultureId == 1 && to.Gather == 4);
        }

        [Test]
        public void Mapping_RegionToRegionDto_IsValid()
        {
            /*
             * CR-1 - structure test in Arrange-Act-Assert manner
             */
            var from = new RegionDto() { Id = 1, Name = "abc" };
            var to = mapper.Map<RegionDto, Region>(from);

            /*
             * CR-1
             * Separate checkings into different calls
             * Target code here:
             * Assert.AreEqual(to.Id, 1);
             * Assert.AreEqual(to.Name, "abc");
             */
            Assert.IsTrue(to.Id == 1 && to.Name == "abc");
        }
    }
}
