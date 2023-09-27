using Moq;
using testing_demo.after;
using testing_demo.services;

namespace test_project
{
    public class Tests
    {
        private Mock<ISMPTSender> _SMTPMock;

        [SetUp]
        public void Setup()
        {
            _SMTPMock = new Mock<ISMPTSender>();
        }

        [Test]
        public void Calling_SendMail_And_Sending_Mailmessage_should_return_True()
        {
            //arrange
            //The mock object is "dumb" - we have to tell it which method we want to call and what it SHOULD return
            _SMTPMock.Setup(x => x.SendMail(It.IsAny<string>())).Returns(true);

            //act
            OrderProcessedAfter Orderprocessor = new OrderProcessedAfter(_SMTPMock.Object);
            bool processed = Orderprocessor.Finalize();


            //Assert
            Assert.IsTrue(processed);
        }

        [TearDown]
        public void TearDown()
        {
            _SMTPMock = null;
        }
    }
}