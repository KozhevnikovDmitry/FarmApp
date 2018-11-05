using FarmApp.DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmApp.DAL.Tests
{
    /*
     * CR-1
     * These tests are redundant and can be removed, because they test EF and depend of DB state.
     * Business scenaries will be tested during integration tests, that should prepare their data in DB
     * What must be tested for EFUnitOfWork:
     * 1. Disposing of underlying DBContext on Dispose method
     * 2. Transactional performing of queries between calls of method SaveChanges()
     */
    [TestFixture]
    public class EFUnitOfWorkTests
    {
        [SetUp]
        public void Configure()
        {
            /*
             * CR-1 - clean data or remove DB files here, in order to guarantee tests independence and reproducibility
             */
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "../../../FarmApp/App_Data"));
        }


        [Test]
        public void CanReadFarms()
        { 
            IEnumerable<Farm> farms;
            using (var db = new EFUnitOfWork())
            {
                farms = db.Farms.Get().AsQueryable().ToList();
            }
            Assert.IsTrue(farms.Any());
        }

        [Test]
        public void CanAddFarmer()
        {
            var farmer = new Farmer()
            {
                Name = "Новый хитрый фермер"
            };
            using (var db = new EFUnitOfWork())
            {

                db.Farmers.Create(farmer);
                db.Save();
            }
            Assert.IsTrue(farmer.Id > 0);
        }

        [Test]
        public void CanDeleteCrop()
        {
            using (var db = new EFUnitOfWork())
            {
                var crop = db.Crops.Get().FirstOrDefault();
                if (crop != null)
                {
                    db.Crops.Remove(crop);
                }
                else
                {
                    throw new Exception("Нет ни одного объекта crop");
                }
                db.Save();
            }
        }
    }
}
