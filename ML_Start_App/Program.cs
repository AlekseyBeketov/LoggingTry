using Serilog;
using Serilog.Events;
using System;
using System.Configuration;
using System.Text;
using FiguresLand;
using System.Threading.Tasks;

namespace ML_Start_App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string configFilePath = Path.Combine(Environment.CurrentDirectory, "App.config");
                Logging.CreateAppLogger(configFilePath);
                 
                GropeConfigurationFile.Name = "Алексей";
                GropeConfigurationFile.Female = "Бекетов";

                if (!File.Exists(configFilePath))
                {
                    try
                    {
                        GropeConfigurationFile.CreateConfigureFile(configFilePath);
                        Log.Information($"Создан файл конфигурации с именем {configFilePath}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Ошибка при создании файла конфигурации: {ex.Message}");
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                GropeConfigurationFile.LoadSettings(configFilePath);
                int N = GropeConfigurationFile.N, L = GropeConfigurationFile.L;

                // decimal element = await Task.Run(() => Element.Calc(N, L)); // поток будет выполняться параллельно с действиями ниже
                decimal element = await Element.CalcAsync(N, L); //сначала дожидаемся выполнения, потом идем дальше 

                Log.Debug($"Полученный элемент равен {element}");
                Log.Information($"Полученный элемент равен {element}");

                Console.WriteLine($"\nБукв в имени: {N}");
                Console.WriteLine($"Букв в фамилии: {L}");
                Console.WriteLine($"\nПолученный элемент: {element}");
                Console.WriteLine();

                // Генерируем себе проблемы)

                Log.Information("Начало генерации проблемы для королевства");
                List<ProblemsGeneration.Problems> problemsList = await ProblemsGeneration.GenerateProblemsAsync(GropeConfigurationFile.Delay);
                Log.Information("Проблемы для королевства сгенерированы:");

                Console.WriteLine("Проблемы с которыми столкнулось государство: ");
                foreach (var problem in problemsList)
                {
                    Log.Information($"{problem.ToString()}");
                    Console.WriteLine(problem.ToString());
                }

                Console.WriteLine();

                Log.Information("Создаем команду спасателей");
                // Инициализируем команду спасателей
                Rectangle rectangle = new(10, 10, Colors.Blue);
                Circle circle = new(15.2, Colors.Green);
                Square square = new(7.7,Colors.White);
                Triangle triangle = new(12.1,6.9,Colors.Red);

                List<Task> tasks = new List<Task>();

                if (problemsList.Count > 0)
                foreach (var problems in problemsList)
                {
                    switch (problems)
                    {
                        case (ProblemsGeneration.Problems.Stones):
                            tasks.Add(Task.Run(() => circle.DestroyStones(element)));
                            break;
                        case (ProblemsGeneration.Problems.Road_Gap):
                            tasks.Add(Task.Run(() => rectangle.BuildBridge(element)));
                            break;
                        case (ProblemsGeneration.Problems.Unstable_Construction):
                            tasks.Add(Task.Run(() => triangle.FixStable()));
                            break;
                        case (ProblemsGeneration.Problems.Pits):
                            tasks.Add(Task.Run(() => square.DestroyPit(element)));
                            break;
                        }
                }
                Log.Information("Начало решения проблем королевства");
                await Task.WhenAll(tasks);
                Log.Information("Конец решения проблем королевства");

            }
            catch (Exception ex)
            {
                if (ex is OutOfMemoryException || ex is StackOverflowException || ex is AccessViolationException)
                {
                    Log.Fatal("Фатальная ошибка. Программа завершена." + ex);
                    Console.WriteLine("Произошла фатальная ошибка. Программа будет завершена.");
                    Environment.Exit(1);
                }
                else
                {
                    Log.Error("Произошла ошибка" + ex);
                    Console.WriteLine("Произошла ошибка: " + ex);
                }
            }
            finally
            {
                Log.CloseAndFlush(); // Завершаем работу с логгером
            }
        }}
    }
