using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FiguresLand
{
    public static class ProblemsGeneration
    {
        public readonly static int NumberOfProblems = 20;

        public enum Problems
        {
            Stones,
            Unstable_Construction,
            Pits,
            Road_Gap
        }

        public static async Task<List<Problems>> GenerateProblemsAsync(int problemDelay)
        {
            return await Task.Run(async () =>
            {
                List<Problems> problemsList = new List<Problems>();
                Random random = new Random();
                int enumCount = Enum.GetValues(typeof(Problems)).Length;
                for (int i = 0; i < NumberOfProblems; i++)
                {
                    // Генерация случайного индекса
                    int randomIndex = random.Next(0, enumCount);

                    // Получение случайного значения из enum по индексу
                    Problems randomEnumValue = (Problems)Enum.GetValues(typeof(Problems)).GetValue(randomIndex);

                    // Добавление случайного значения в список
                    problemsList.Add(randomEnumValue);
                }
                await Task.Delay(problemDelay);
                return problemsList;
            });
        }
    }
}
