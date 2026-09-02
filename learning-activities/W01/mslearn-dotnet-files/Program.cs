using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

// create a variable called salesTotalDir, which holds the path to the salesTotalDir directory
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);
var salesTotal = CalculateSalesTotal(salesFiles);

// create an empty file called totals.txt inside the newly created salesTotalDir directory.
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

// generate a sales summary report file
GenerateSalesSummary(salesFiles, salesTotal, salesTotalDir, storesDirectory);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        var extension = Path.GetExtension(file);

        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

// generates a sales summary report file
void GenerateSalesSummary(
    IEnumerable<string> salesFiles,
    double salesTotal,
    string salesTotalDir,
    string storesDirectory)
{
    StringBuilder report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {salesTotal:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var file in salesFiles)
    {
        // to remove salestotls
        if (Path.GetFileName(file) != "sales.json")
        {
            continue;
        }

        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        if (data != null)
        {
            string relativePath = Path.GetRelativePath(storesDirectory, file);
            report.AppendLine($"   {relativePath}: {data.Total:C}");
        }
    }

    File.WriteAllText(
        Path.Combine(salesTotalDir, "salesSummary.txt"),
        report.ToString()
    );
}

record SalesData (double Total);

