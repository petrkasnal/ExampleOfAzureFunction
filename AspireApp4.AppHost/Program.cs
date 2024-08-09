using AspireApp4.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
var blobPort = 58603;
var tablePort = 58602;

var storage = builder.AddAzureStorage("AppStorage").RunAsEmulator(x => x.WithTablePort(tablePort).WithBlobPort(blobPort));

var blobs = storage.AddBlobs("AppBlobStorage");

var test = builder.AddConnectionString("Test");
var test2 = builder.AddConnectionString("Test2");
var test3 = builder.AddConnectionString("Test3");
var test4 = builder.AddConnectionString("Test4");
var test5 = builder.AddConnectionString("Test5");

builder.AddAzureFunction<Projects.FunctionApp7>("Wtf", 7128, 7129)
    .WithReference(test)
    .WithReference(test2)
    .WithReference(test3)
    .WithReference(test4)
    .WithReference(test5)
    .WithReference(blobs)
        .WithEnvironment(
"AzureWebJobsStorage",
        () =>
        {
            return blobs.Resource.ConnectionStringExpression.ValueExpression ?? throw new InvalidOperationException("Connection string is null");
        }
        );

builder.Build().Run();
