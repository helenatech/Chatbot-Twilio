using ClosedXML.Excel;
using System.Reflection;

namespace Chatbot.Infrastructure.Services
{
    public class ExcelReaderService
    {
        public async Task<List<T>> ReadDataFromFileAsync<T>(string filePath) where T : new()
        {
            var data = new List<T>();

            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"O arquivo {filePath} não foi encontrado.");

                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed().ToList();

                    if (rows.Count < 2)
                        throw new Exception("A planilha não contém dados suficientes.");

                    var headerRow = rows[0];
                    var propertyMap = new Dictionary<int, PropertyInfo>();

                    for (int col = 1; col <= headerRow.CellCount(); col++)
                    {
                        var header = headerRow.Cell(col).GetString().Trim();
                        var property = typeof(T).GetProperty(header, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                        Console.WriteLine($"Cabeçalho: {header}, Propriedade: {property?.Name}");

                        if (property != null && property.CanWrite)
                        {
                            propertyMap[col] = property;
                        }
                    }

                    for (int i = 1; i < rows.Count; i++) // começa da linha 2
                    {
                        var row = rows[i];

                        bool isEmptyRow = propertyMap.Keys.All(colIndex =>
                            string.IsNullOrWhiteSpace(row.Cell(colIndex).GetString()));
                        if (isEmptyRow) continue;

                        var entity = new T();

                        foreach (var kvp in propertyMap)
                        {
                            int colIndex = kvp.Key;
                            var prop = kvp.Value;
                            var cellValue = row.Cell(colIndex).GetString().Trim();

                            Console.WriteLine($"Linha {i + 1}: {prop.Name} = '{cellValue}'");

                            try
                            {
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    object? value = Convert.ChangeType(cellValue, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                                    prop.SetValue(entity, value);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao converter valor '{cellValue}' para {prop.Name}: {ex.Message}");
                            }
                        }

                        data.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo Excel: {ex.Message}");
            }

            return await Task.FromResult(data);
        }
    }
}