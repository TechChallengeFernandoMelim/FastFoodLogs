using System;
using System.IO;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Moq;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;
using Xunit;

namespace FastFoodLogs.Tests
{
    public class FunctionsTest
    {
        public FunctionsTest()
        {
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO", "access_key_test");
            Environment.SetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO", "secret_key_test");

        }

        [Fact]
        public async Task Handler_WithRecords_LoggedSuccessfully()
        {
            // Arrange
            var functions = new Mock<Functions>();
            var loggerMock = new Mock<ILogger>();

            functions.Setup(x => x.GetLoger()).Returns(() => loggerMock.Object);

            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage>
                {
                    new SQSEvent.SQSMessage
                    {
                        MessageId = "1",
                        Body = "{\"key\": \"value\"}",
                        MessageAttributes = new Dictionary<string, SQSEvent.MessageAttribute>()
                        {
                                { "Service",   new SQSEvent.MessageAttribute { DataType = "String", StringValue = "Service" } },
                                { "StackTrace",   new SQSEvent.MessageAttribute { DataType = "String", StringValue = "StackTrace" } },
                                { "ExceptionMessage",  new SQSEvent.MessageAttribute { DataType = "String", StringValue = "ExceptionMessage" } },
                                { "Ex", new SQSEvent.MessageAttribute { DataType = "String", StringValue = "Ex" } },
                                { "Time", new SQSEvent.MessageAttribute { DataType = "String", StringValue = "Time" } }
                        }
                    }
                }
            };

            var lambdaContext = new TestLambdaContext();

            // Act
            await functions.Object.Handler(sqsEvent, lambdaContext);

            // Assert
            loggerMock.Verify(logger => logger.Log(It.IsAny<LogLevel>(), It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Fact]
        public void GetLoger_ReturnsLogger()
        {
            // Arrange
            var functions = new Functions();
            var accessKey = "test-access-key";
            var secretKey = "test-secret-key";
            var logGroup = "test-log-group";
            var logRegion = "test-log-region";

            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO", accessKey);
            Environment.SetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO", secretKey);
            Environment.SetEnvironmentVariable("LOG_GROUP", logGroup);
            Environment.SetEnvironmentVariable("LOG_REGION", logRegion);

            // Act
            var logger = functions.GetLoger();

            // Assert
            Assert.NotNull(logger);
            Assert.IsType<Logger>(logger);
        }

    }
}
