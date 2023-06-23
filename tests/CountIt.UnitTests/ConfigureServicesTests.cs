using CountIt.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Xunit;

namespace CountIt.UnitTests;

public class ConfigureServicesTests
{
    [Fact]
    public void AddServices_ShouldRegisterDependencies()
    {
        // Arrange & Act
        var serviceProvider = ConfigureServices.AddServices();

        // Assert
        serviceProvider.Should().NotBeNull();

        serviceProvider.GetService<IFileWrapper>().Should().NotBeNull();
        serviceProvider.GetService<IFileLoader>().Should().NotBeNull();
        serviceProvider.GetService<IWordCounter>().Should().NotBeNull();
        serviceProvider.GetService<ISorter>().Should().NotBeNull();
        serviceProvider.GetService<IDocumentProcessor>().Should().NotBeNull();
        serviceProvider.GetService<IPrintDocument>().Should().NotBeNull();
        serviceProvider.GetService<ILogger>().Should().NotBeNull();
    }
}