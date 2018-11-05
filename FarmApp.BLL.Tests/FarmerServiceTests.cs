using FarmApp.BLL.DTO;
using FarmApp.BLL.Infrastructure;
using FarmApp.BLL.Services;
using FarmApp.DAL;
using FarmApp.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace FarmApp.BLL.Tests
{
    /*
     * CR-1
     * In comments to FarmService, FarmController and FarmCropViewModel I suggested to mode validation logic to FarmCropViewModel
     * and move getting regions, farmers anf agricultires to another class, for instance ValueProvider.
     * So tests for validation logic must be implemented in FarmCropViewModelTests and cover all positive and negative scenaries
     * FarmService class as well as ValueProvider are orchestration services, so they must not contain any busines logic,
     * Their logic(let's call it application logic) must be covered with integration tests.
     *
     * Also I suggested to implement validation without throwing exceptions, so most of test below must be reimplemented
     *
     * However I will add comments to tests below, to prevent the same issues in similar future tests.
     */
    [TestFixture]
    public class FarmerServiceTests
    {
        private FarmService farmService;

        [SetUp]
        public void Configure()
        {
            var cropRepo = new Mock<IRepository<Crop>>();
            cropRepo.Setup(item => item.Create(It.IsAny<Crop>())).Callback(() => { });
            var iow = new Mock<IUnitOfWork>();
            iow.Setup(item => item.Crops).Returns(cropRepo.Object);
            farmService = new FarmService(iow.Object, AutoMapperConfig.GetMapper());
        }


        [Test]
        public void AddFarmCrop_InvalidAgricultureId_ThrowValidationException()
        { 
            /*
             * CR-1 - structure test in Arrange-Act-Assert manner
             */
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = -1, FarmerId = 1, RegionId = 1, Area = 1, Gather = 1, Name = "abc" };
            try
            {
                farmService.AddFarmCrop(enemy);
            }

            /*
             * CR-1 -
             * Use Asset.Throws<> instead of try-catch block, because if excation is not be throwed, the test will pass
             * Target code here:
             * var ex = Assert.Throws<ValidationException>(() => farmService.AddFarmCrop(enemy));
             * Assert.AreEqual(ex.Property, "AgricultureId");
             *
             * Use Assert.AreEqual istead of Assert.IsTrue with lambda, because of more meaningfull output
             */
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Property == "AgricultureId");
            }
        }

        /*
        * CR-1 - check comments to the first test, here are the same issues
        */
        [Test]
        public void AddFarmCrop_InvalidFarmerId_ThrowValidationException()
        {            
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = 1, FarmerId = -1, RegionId = 1, Area = 1, Gather = 1, Name = "abc" };
            try
            {
                farmService.AddFarmCrop(enemy);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Property == "FarmerId");
            }
        }


        /*
        * CR-1 - check comments to the first test, here are the same issues
        */
        [Test]
        public void AddFarmCrop_InvalidRegionId_ThrowValidationException()
        {            
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = 1, FarmerId = 1, RegionId = -1, Area = 1, Gather = 1, Name = "abc" };
            try
            {
                farmService.AddFarmCrop(enemy);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Property == "RegionId");
            }
        }


        /*
        * CR-1 - check comments to the first test, here are the same issues
        */
        [Test]
        public void AddFarmCrop_InvalidName_ThrowValidationException()
        {
            var cropRepo = new Mock<IRepository<Crop>>();
            cropRepo.Setup(item => item.Create(It.IsAny<Crop>())).Callback(() => { });
            var iow = new Mock<IUnitOfWork>();
            iow.Setup(item => item.Crops).Returns(cropRepo.Object);

            var farmService = new FarmService(iow.Object, AutoMapperConfig.GetMapper());
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = 1, FarmerId = 1, RegionId = 1, Area = 1, Gather = 1, Name = "" };
            try
            {
                farmService.AddFarmCrop(enemy);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Property == "Name");
            }
        }


        /*
        * CR-1 - check comments to the first test, here are the same issues
        */
        [Test]
        public void AddFarmCrop_InvalidGather_ThrowValidationException()
        {
            var cropRepo = new Mock<IRepository<Crop>>();
            cropRepo.Setup(item => item.Create(It.IsAny<Crop>())).Callback(() => { });
            var iow = new Mock<IUnitOfWork>();
            iow.Setup(item => item.Crops).Returns(cropRepo.Object);

            var farmService = new FarmService(iow.Object, AutoMapperConfig.GetMapper());
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = 1, FarmerId = 1, RegionId = 1, Area = 1, Gather = -1, Name = "abc" };
            try
            {
                farmService.AddFarmCrop(enemy);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Property == "Gather");
            }
        }


        /*
        * CR-1 - check comments to the first test, here are the same issues
        */
        [Test]
        public void AddFarmCrop_InvalidArea_ThrowValidationException()
        {
            var cropRepo = new Mock<IRepository<Crop>>();
            cropRepo.Setup(item => item.Create(It.IsAny<Crop>())).Callback(() => { });
            var iow = new Mock<IUnitOfWork>();
            iow.Setup(item => item.Crops).Returns(cropRepo.Object);

            var farmService = new FarmService(iow.Object, AutoMapperConfig.GetMapper());
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = 1, FarmerId = 1, RegionId = 1, Area = 0, Gather = 1, Name = "abc" };
            try
            {
                farmService.AddFarmCrop(enemy);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Property == "Area");
            }
        }
        

        
        [Test]
        public void AddFarmCrop_ValidateModelMappingWhenSave_IsValid_()
        {
            /*
             * CR-1 - structure test in Arrange-Act-Assert manner
             */
            bool isValid = false;

            var cropRepo = new Mock<IRepository<Crop>>();
            cropRepo.Setup(item => item.Create(It.IsAny<Crop>())).Callback<Crop>(arg =>
            {
                isValid = arg.AgricultureId == 1 && arg.Gather == 5 && arg.CropFarm.Name == "abc" && arg.CropFarm.FarmerId == 2 && arg.CropFarm.RegionId == 3;
            });
            var iow = new Mock<IUnitOfWork>();
            iow.Setup(item => item.Crops).Returns(cropRepo.Object);
            farmService = new FarmService(iow.Object, AutoMapperConfig.GetMapper());
            FarmCropDto enemy = new FarmCropDto() { AgricultureId = 1, FarmerId = 2, RegionId = 3, Area = 4, Gather = 5, Name = "abc" };
            farmService.AddFarmCrop(enemy);
            Assert.IsTrue(isValid);
        }

    }
}
