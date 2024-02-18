using CsvHelper;
using LittleOwl.Parse;
using System.Globalization;

MiuzParser parser = new();

ParserSettings settings = new("https://miuz.ru/", "page=");

settings.Url = "catalog/chain/";

settings.StartPoint = 1;
settings.EndPoint = 8;

Console.WriteLine($"\nНастройки");
Console.WriteLine($"\nОсновной url: {settings.BaseUrl}");
Console.WriteLine($"Префикс: {settings.Prefix}");
Console.WriteLine($"Путь: {settings.Url}");
Console.WriteLine($"Начальная страница: {settings.StartPoint} - " +
    $"Конечная страница: {settings.EndPoint}\n");

MiuzParserWorker worker = new(parser, settings);

Console.WriteLine($"\nПроцес...");
worker.Run();

Console.WriteLine("\nСохранение...");
string path = Environment.CurrentDirectory + @"\result.csv";

using (var writer = new StreamWriter(path))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(worker.Products);
}

Console.WriteLine($"\nФайл результатов по адресу: {path}");