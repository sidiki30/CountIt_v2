using CountIt;
using CountIt.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var serviceProvider = ConfigureServices.AddServices();

var logger = serviceProvider.GetService<ILogger>();

try
{
    var documentProcessor = serviceProvider.GetService<IDocumentProcessor>();
    if (documentProcessor is null)
    {
        logger?.Information("Unable to process the document. The document processor is missing.");
        return;
    }

    var filePath = "file.txt";
    var wordCountDictionary = await documentProcessor.ProcessFileAsync(filePath);

    var printDocument = serviceProvider.GetService<IPrintDocument>();
    if (printDocument is null)
    {
        logger?.Information("Unable to print the document. The document printer is missing.");
        return;
    }

    printDocument.PrintToConsole(wordCountDictionary);
}
catch (Exception ex)
{
    logger?.Error("Unable to process the document. Please check the exception.", ex);
}