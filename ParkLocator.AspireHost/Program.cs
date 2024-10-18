using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
                                            .WithDataVolume();

builder.AddProject<ParkLocator>(nameof(ParkLocator))
    .WithReference(seq);


builder.Build().Run();
