using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.Runtime;
using AWS.Messaging.Lambda;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FastFoodLogs;


public class Functions
{

    public Functions()
    {

    }

    [LambdaFunction]
    public async Task Handler(SQSEvent evnt, ILambdaContext context)
    {
        var logger = await GetLoger();

        try
        {
            foreach (var message in evnt.Records)
            {
                var log = @$"MessageId:{message.MessageId}    ";

                if (message.MessageAttributes.ContainsKey("StackTrace"))
                    log += @$" StackTrace: {message.MessageAttributes["StackTrace"].StringValue} ";

                if (message.MessageAttributes.ContainsKey("ExceptionMessage"))
                    log += @$" ExceptionMessage: {message.MessageAttributes["ExceptionMessage"].StringValue} ";

                if (message.MessageAttributes.ContainsKey("Ex"))
                    log += @$" Ex: {message.MessageAttributes["Ex"].StringValue} ";

                if (message.MessageAttributes.ContainsKey("Service"))
                    log += @$" Service: {message.MessageAttributes["Service"].StringValue} ";

                if (message.MessageAttributes.ContainsKey("Time"))
                    log += @$" Time: {message.MessageAttributes["Time"].StringValue} ";

                logger.Log(
                    NLog.LogLevel.Error,
                    log);
            }
        }
        catch (Exception ex)
        {
            logger.Log(
                NLog.LogLevel.Error,
                $"Erro ao executar serviço de log. Exceção: ${ex}");
        }
    }

    private async Task<ILogger> GetLoger()
    {
        string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO");
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO");

        AWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);

        var config = new LoggingConfiguration();

        config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, new AWSTarget()
        {
            LogGroup = Environment.GetEnvironmentVariable("LOG_GROUP"),
            Region = Environment.GetEnvironmentVariable("LOG_REGION"),
            Credentials = credentials
        });

        LogManager.Configuration = config;

        return LogManager.GetCurrentClassLogger();
    }
}
