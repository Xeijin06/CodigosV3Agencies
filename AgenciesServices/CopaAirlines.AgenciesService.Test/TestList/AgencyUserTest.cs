using CopaAirlines.AgenciesService.DA;
using CopaAirlines.AgenciesService.DA.DBModels;
using CopaAirlines.AgenciesService.Logic;
using CopaAirlines.AgenciesService.Test.MockData;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopaAirlines.AgenciesService.Test.TestList
{
    [TestFixture]
    public class AgencyUserTest
    {
        Mock<DbContextAgenciesServices> mockContext;
        Mock<DbSet<Agencies>> mockAgency;
        Mock<DbSet<AgencieUsers>> mockUser;

        AgencyUserLogic logic;

        [SetUp]
        public void PrepareTest()
        {
            mockContext = new Mock<DbContextAgenciesServices>();
            mockAgency = new Mock<DbSet<Agencies>>();
            mockUser = new Mock<DbSet<AgencieUsers>>();
        }

        [Test]
        public void ValidateUser_ValidUser1()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsTrue(logic.ValidateUser(MockUser.ValidUsers()[0]).Result);
        }

        [Test]
        public void ValidateUser_ValidUser2()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsTrue(logic.ValidateUser(MockUser.ValidUsers()[1]).Result);
        }


        [Test]
        public void ValidateUser_ValidUser3()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsTrue(logic.ValidateUser(MockUser.ValidUsers()[2]).Result);
        }

        [Test]
        public void ValidateUser_InvalidUser1()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsFalse(logic.ValidateUser(MockUser.InvalidUsers()[0]).Result);
        }

        [Test]
        public void ValidateUser_InvalidUser2()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsFalse(logic.ValidateUser(MockUser.InvalidUsers()[1]).Result);
        }


        [Test]
        public void ValidateUser_InvalidUser3()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsFalse(logic.ValidateUser(MockUser.InvalidUsers()[2]).Result);
        }


        [Test]
        public void ValidateUser_InvalidUser4()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsFalse(logic.ValidateUser(MockUser.InvalidUsers()[3]).Result);
        }

        [Test]
        public void ValidateUser_InvalidUser5()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsFalse(logic.ValidateUser(MockUser.InvalidUsers()[4]).Result);
        }

        [Test]
        public void ValidateUser_InvalidUser6()
        {
            SetMockDbTableConfiguration<Agencies>(MockAgencies.GetAgenciesDBList(), mockAgency);
            SetMockDbTableConfiguration<AgencieUsers>(MockUser.GetListAgencyUserDBModel(), mockUser);
            mockContext.Setup(m => m.Agencies).Returns(mockAgency.Object);
            mockContext.Setup(m => m.AgencyUsers).Returns(mockUser.Object);

            logic = new AgencyUserLogic(mockContext.Object);

            Assert.IsFalse(logic.ValidateUser(MockUser.InvalidUsers()[5]).Result);
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
