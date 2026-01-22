var builder = DistributedApplication.CreateBuilder(args);

var bookifyDb = builder.AddContainer("bookify-db", "postgres", "latest")
    .WithEnvironment("POSTGRES_DB", "bookify")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "postgres")
    .WithBindMount("./.containers/database", "/var/lib/postgresql/data")
    .WithEndpoint(
        name: "postgres",
        port: 5432,
        targetPort: 5432
    );

var bookifyApi = builder
    .AddProject<Projects.Bookify_Api>("bookify-api")
    .WaitFor(bookifyDb)

    .WithHttpEndpoint(
        name: "bookify-api-http",
        port: 8080,
        targetPort: 8080,
        isProxied: false
    )
    .WithEndpoint(
        name: "bookify-api-https",
        scheme: "https",
        port: 5001,
        targetPort: 8081,
        isProxied: true
    );


builder.Build().Run();
