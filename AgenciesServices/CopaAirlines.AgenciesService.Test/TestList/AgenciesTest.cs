using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopaAirlines.AgenciesService.DA;
using CopaAirlines.AgenciesService.DA.DBModels;
using CopaAirlines.AgenciesService.Logic;
using CopaAirlines.AgenciesService.Test.MockData;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CopaAirlines.AgenciesService.Test.TestList
{
    [TestFixture]
    public class AgenciesTest
    {
        Mock<DbContextAgenciesServices> mockContext;
        Mock<DbSet<Agencies>> mockAgency;
        Mock<DbSet<AgencieUsers>> mockUser;

        AgenciesLogic logic;

        [SetUp]
        public void PrepareTest()
        {
            mockContext = new Mock<DbContextAgenciesServices>();
            mockAgency = new Mock<DbSet<Agencies>>();
            mockUser = new Mock<DbSet<AgencieUsers>>();
        }

        [Test]
        public void GetAgencyFromUserID_NoUser1()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsFalse(logic.GetAgencyByUserId(0).Result);
        }

        [Test]
        public void GetAgencyFromUserID_NoUser2()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsFalse(logic.GetAgencyByUserId(-5).Result);
        }

        [Test]
        public void GetAgencyFromUserID_ValidUser1()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsTrue(logic.GetAgencyByUserId(1).Result);
        }

        [Test]
        public void GetAgencyFromUserID_ValidUser2()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsTrue(logic.GetAgencyByUserId(2).Result);
        }

        [Test]
        public void GetAgencyFromUserID_ValidUser3()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsTrue(logic.GetAgencyByUserId(3).Result);
        }


        [Test]
        public void GetAgencyFromUserID_ValidUser4()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsTrue(logic.GetAgencyByUserId(4).Result);
        }


        [Test]
        public void GetAgencyFromUserID_NoExistUser1()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsFalse(logic.GetAgencyByUserId(7).Result);
        }


        [Test]
        public void GetAgencyFromUserID_NoExistUser2()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsFalse(logic.GetAgencyByUserId(12).Result);
        }


        [Test]
        public void GetAgencyFromUserID_NoExistUser3()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgenciesLogic(mockContext.Object);

            Assert.IsFalse(logic.GetAgencyByUserId(19031).Result);
        }



        private void SetMockDbTableConfiguration<T>(List<T> listModel,
                                                     Mock mockEntity)
        {
            IQueryable<T> queriable = listModel.AsQueryable();
            mockEntity.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queriable.Provider);
            mockEntity.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queriable.Expression);
            mockEntity.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queriable.ElementType);
            mockEntity.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queriable.GetEnumerator());
        }

    }
}
