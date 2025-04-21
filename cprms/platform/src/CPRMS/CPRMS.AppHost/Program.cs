var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.Gateway>("gateway");

builder.AddProject<Projects.Auth_API>("auth-api");

builder.AddProject<Projects.Document_API>("document-api");

builder.AddProject<Projects.RmsWorkflow_API>("rmsworkflow-api");

builder.Build().Run();
