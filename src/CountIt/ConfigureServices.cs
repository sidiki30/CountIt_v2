using CountIt.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CountIt;

public static class ConfigureServices
{
    public static ServiceProvider AddServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IFileLoader, FileLoader>()
            .AddSingleton<IFileWrapper, FileWrapper>()
            .AddSingleton<IWordCounter, WordCounter>()
            .AddSingleton<ISorter, AlphabeticalSorter>()
            .AddSingleton<IDocumentProcessor, DocumentProcessor>()
            .AddSingleton<IPrintDocument, PrintDocument>()
            .AddSingleton<ILogger>(_ =>
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();
            })
            .AddLogging(builder =>
            {
                var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateLogger();
                builder.AddSerilog(logger);
            })
            .BuildServiceProvider();

        return serviceProvider;
    }
}
