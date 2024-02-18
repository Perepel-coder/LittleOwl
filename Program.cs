using CsvHelper;
using LittleOwl.Parse;
using System.Globalization;

MiuzParser parser = new();

ParserSettings settings = new("https://miuz.ru/", "page=");

settings.Url = "catalog/chain/";

settings.StartPoint = 1;
settings.EndPoint = 8;

MiuzParserWorker worker = new(parser, settings);

worker.Run();

string path = Environment.CurrentDirectory + @"\result.csv";

using (var writer = new StreamWriter(path))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(worker.Products);
}

Console.WriteLine($"\nСохранение...\n");
Console.WriteLine($"Файл результатов по адресу: {path}");