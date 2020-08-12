using DirectoriesCreator.Interfaces;
using DownloadDataFromTraseo.Interfaces;
using DownloadDataFromTraseo.Parser;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using ParserTests.Interfaces;
using ParserTests.Models;
using Serializer.Interfaces;

namespace ParserTests
{
    public class AditionalInformationsParserTests
    {
        private Mock<ISerializer> _serializer;
        private Mock<IFilePath> _filePath;
        private ITrailAdditionalInformationDataDownloader _trailAdditionalInformationDataDownloader;
        private IAditionalInformationsModel _aditionalInformationsModel;

        [SetUp]
        public void SetUp()
        {
            _aditionalInformationsModel = new AditionalInformationsModel();
            _serializer = new Mock<ISerializer>();
            _filePath = new Mock<IFilePath>();
            _trailAdditionalInformationDataDownloader = new TrailAdditionalInformationDataDownloader(_serializer.Object,_filePath.Object);
        }
        [Test]
        public void ReturnKindOfActivity_EmptyPage_Null()
        {
            // Arrange

            HtmlDocument model = null;

            // Act 

            var result = _trailAdditionalInformationDataDownloader.ReturnKindOfActivity(model);

            //Assert

            Assert.IsNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void ReturnKindOfActivity_CorrectPage_Equal()
        {
            // Arrange

            HtmlDocument model = _aditionalInformationsModel.ReturnKindOfActivity();

            // Act 

            var result = _trailAdditionalInformationDataDownloader.ReturnKindOfActivity(model);

            //Assert

            Assert.AreEqual(result, "rower górski");
        }
        [Test]
        public void ReturnExceeding_CorrectPage_Equal()
        {
            // Arrange

            HtmlDocument model = _aditionalInformationsModel.ReturnExceeding();

            // Act 

            var result = _trailAdditionalInformationDataDownloader.ReturnExceeding(model);

            //Assert

            Assert.AreEqual(result, "150 m");
        }

        [Test]
        public void ReturnSumUp_CorrectPage_Equal()
        {
            // Arrange

            HtmlDocument model = _aditionalInformationsModel.ReturnExceeding();

            // Act 

            var result = _trailAdditionalInformationDataDownloader.ReturnSumUp(model);

            //Assert

            Assert.AreEqual(result, "350 m");
        }
    }
}
