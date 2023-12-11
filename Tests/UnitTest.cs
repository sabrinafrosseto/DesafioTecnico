using Business;
using Domain.Entidades;
using Domain.Interfaces;
using Moq;

namespace Tests
{
    [TestClass]
    public class ProtocolBusinessTests
    {
        private IProtocolBusiness _business;
        private Mock<IProtocolData> _mockData;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockData = new Mock<IProtocolData>();
            _business = new ProtocolBusiness(_mockData.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForMissingProtocolNumber()
        {
            // Arrange
            var protocol = new Protocol
            {
                DocumentVersion = "1",
                CPF = "12345678900",
                PersonalId = "123456789",
                Name = "John Doe",
                Image = "image.jpg"
            };

            // Act
            _business.Create(protocol);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForMissingName()
        {
            // Arrange
            var protocol = new Protocol
            {
                ProtocolNumber = "564789",
                DocumentVersion = "1",
                CPF = "12345678900",
                PersonalId = "123456789",
                Image = "image.jpg"
            };

            // Act
            _business.Create(protocol);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForMissingCPF()
        {
            // Arrange
            var protocol = new Protocol
            {
                ProtocolNumber = "564789",
                DocumentVersion = "2",
                PersonalId = "123456789",
                Name = "John Doe",
                Image = "image.jpg"
            };

            // Act
            _business.Create(protocol);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForMissingPersonalId()
        {
            // Arrange
            var protocol = new Protocol
            {
                ProtocolNumber = "564789",
                DocumentVersion = "2",
                CPF = "12345678900",
                Name = "John Doe",
                Image = "image.jpg"
            };

            // Act
            _business.Create(protocol);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForMissingImage()
        {
            // Arrange
            var protocol = new Protocol
            {
                ProtocolNumber = "564789",
                DocumentVersion = "2",
                CPF = "12345678900",
                PersonalId = "123456789",
                Name = "John Doe",
            };

            // Act
            _business.Create(protocol);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForMissingDocumentVersion()
        {
            // Arrange
            var protocol = new Protocol
            {
                ProtocolNumber = "564789",
                CPF = "12345678900",
                PersonalId = "123456789",
                Name = "John Doe",
                Image = "image.jpg"
            };

            // Act
            _business.Create(protocol);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForDuplicateProtocolNumber()
        {
            // Arrange
            var protocol = new Protocol
            {
                DocumentVersion = "1",
                CPF = "12345678900",
                PersonalId = "123456789",
                Name = "John Doe",
                Image = "image.jpg",
                ProtocolNumber = "12345"
            };

            _mockData = new Mock<IProtocolData>();

            _mockData
                .Setup(d => d.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<Protocol> { new Protocol
                {
                    ProtocolNumber = "12345"
                } });

            _business = new ProtocolBusiness(_mockData.Object);

            // Act
            _business.Create(protocol);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_ThrowsForDuplicateDocumentVersion()
        {
            // Arrange
            var protocol = new Protocol
            {
                DocumentVersion = "1",
                CPF = "12345678900",
                PersonalId = "123456789",
                Name = "John Doe",
                Image = "image.jpg",
                ProtocolNumber = "12345"
            };

            _mockData = new Mock<IProtocolData>();

            _mockData
                .Setup(d => d.Find("12345", It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<Protocol>());
            
            _mockData
                .Setup(d => d.Find(It.IsAny<string>(), It.IsAny<string>(), "123456789"))
                .Returns(new List<Protocol> 
                { 
                    new Protocol
                    {
                        DocumentVersion = "1",
                        CPF = "12345678900",
                        PersonalId = "123456789",
                        Name = "John Doe",
                        Image = "image.jpg",
                        ProtocolNumber = "000001"
                    }
                });

            _business = new ProtocolBusiness(_mockData.Object);

            // Act
            _business.Create(protocol);
        }

        
    }
}