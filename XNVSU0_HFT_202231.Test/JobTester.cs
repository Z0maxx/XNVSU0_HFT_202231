using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using XNVSU0_HFT_202231.Models.TableModels;
using static XNVSU0_HFT_202231.Test.Helper;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    public class JobTester
    {
        [Test]
        public void CreateInvalidIdJob()
        {
            var job = new Job()
            {
                Id = 1,
                Name = "Segítő"
            };
            Assert.Throws<ArgumentException>(() => JobLogic.Create(job));
        }
        [Test]
        public void CreateEmptyJob()
        {
            var job = new Job();
            Assert.Throws<ValidationException>(() => JobLogic.Create(job));
        }
        [Test]
        public void CreateInvalidNameJob()
        {
            var job = new Job()
            {
                Name = "A"
            };
            Assert.Throws<ValidationException>(() => JobLogic.Create(job));
        }
        [Test]
        public void CreateValidJob()
        {
            var job = new Job()
            {
                Name = "Segítő"
            };
            JobLogic.Create(job);
            JobRepository.Verify(r => r.Create(job), Times.Once);
        }
        [Test]
        public void GetInvalidIdJob()
        {
            Assert.Throws<ArgumentException>(() => JobLogic.Read(0));
        }
        [Test]
        public void GetValidJob()
        {
            var job = new Job()
            {
                Id = 1,
                Name = "Séf"
            };
            Assert.That(JobLogic.Read(1), Is.EqualTo(job));
        }
        [Test]
        public void UpdateInvalidIdJob()
        {
            var job = new Job()
            {
                Id = 0,
                Name = "Segítő"
            };
            Assert.Throws<ArgumentException>(() => JobLogic.Update(job));
        }
        [Test]
        public void UpdateValidJob()
        {
            var job = new Job()
            {
                Id = 1,
                Name = "Segítő"
            };
            JobLogic.Update(job);
            JobRepository.Verify(r => r.Update(job), Times.Once);
        }
        [Test]
        public void DeleteInvalidJob()
        {
            Assert.Throws<ArgumentException>(() => JobLogic.Delete(0));
        }
        [Test]
        public void DeleteValidJob()
        {
            JobLogic.Delete(1);
            JobRepository.Verify(r => r.Delete(1), Times.Once);
        }
    }
}
