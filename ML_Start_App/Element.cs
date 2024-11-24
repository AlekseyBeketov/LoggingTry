using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Start_App
{
    internal static class Element
    {
        private readonly static int[] oddNumbers = new int[] { 5, 7, 9, 11, 13, 15, 17, 19 };

        public static async Task<decimal> CalcAsync(int N, int L)
        {
            return await Task.Run(async () => //если бы мы не устанавливали Delay снизу, async здесь былл бы не нужен
            {
                Log.Information("Начало выполнение метода CalcAsync");
                double[] randomValues = new double[13];
                var randomValue = new Random();

                Log.Information("Подбираем массив rabdomValues");
                Log.Debug("Подобраны следующие значения для массива randomValues: ");

                StringBuilder randomList = new StringBuilder();
                for (int i = 0; i < randomValues.Length; i++)
                {
                    randomValues[i] = randomValue.Next(-12, 15);
                    randomList.Append(randomValues[i]);
                    randomList.Append(' ');
                }
                Log.Debug($"{randomList}");

                Decimal[,] k = new Decimal[8, 13];

                for (int i = 0; i < oddNumbers.Length; i++)
                {
                    for (int j = 0; j < randomValues.Length; j++)
                    {
                        double x = randomValues[j];
                        if (oddNumbers[i] == 9)
                        {
                            if (x == -0.5)
                            {
                                x -= 0.00001;
                                Log.Warning("Значение x=-0.5 было уменьшено на 0.00001 для избежения ошибки типа DivideByZeroException. " +
                                    $"Событие произошло при k[{i},{j}].");
                            }
                            double secondSin = Math.Pow(x / (x + 0.5), x);
                            k[i, j] = (Decimal)Math.Sin(Math.Sin(secondSin));
                        }
                        else if (oddNumbers[i] == 5 || oddNumbers[i] == 7 || oddNumbers[i] == 11 || oddNumbers[i] == 15)
                        {
                            if (x < 0)
                            {
                                k[i, j] = decimal.MinValue;
                                //обрабатываем
                                Log.Warning("Значение k было сброшено на значение по умолчанию, " +
                                    "поскольку отрицательное число нельзя возводить в 0 < степень < 1 " +
                                    $"Событие произошло при k[{i},{j}] и x = {x}");
                                continue;
                            }

                            double tanPart = Math.Tan(2 * x) + 2 / 3.0;
                            double cube = Math.Pow(Math.Pow(x, 1.0 / 3.0), 1.0 / 3.0);

                            if (tanPart < 0 && (cube % 1 == 0))
                            {
                                k[i, j] = decimal.MinValue;
                                //обрабатываем
                                Log.Warning("Значение k было сброшено на значение по умолчанию, " +
                                    "поскольку отрицательное число нельзя возводить в 0 < степень < 1 " +
                                    $"Событие произошло при k[{i},{j}] и x = {x}, cube = {cube}, tanPart = {tanPart}");
                                continue;
                            }
                            try
                            {
                                k[i, j] = (Decimal)Math.Pow(0.5 / tanPart, cube);
                            }
                            catch (System.OverflowException ex)
                            {
                                k[i, j] = decimal.MinValue;
                                Log.Error("Произошла ошибка: " + ex.Message);
                            }
                        }
                        else
                        {
                            double ePart = Math.Pow(Math.E, (1 - x) / Math.PI);
                            k[i, j] = (Decimal)Math.Tan(Math.Pow(ePart / 12.0, 3));
                        }
                    }
                }

                Log.Information("Начался вывод построенной матрицы в консоль");
                Log.Debug("Полученная матрица k:");

                Console.Write("Полученная матрица:\n");
                for (int i = 0; i < oddNumbers.Length; i++)
                {
                    for (int j = 0; j < randomValues.Length; j++)
                    {
                        Console.Write("[" + k[i, j] + "] ");
                        Log.Debug("[" + k[i, j] + "] ");
                    }
                    Console.WriteLine();
                    Log.Debug("");
                }

                int index_i = N % 8;
                int index_j = L % 13;

                Log.Debug($"Значения index_i и index_j рассчитаны. N = {N}, L = {L}");
                Log.Information("Начало рассчета минимального k[i]");

                decimal minKi;
                if (k[index_i, 0] != decimal.MinValue)
                {
                    minKi = k[index_i, 0];
                    for (int j = 1; j < randomValues.Length; j++)
                    {
                        if (k[index_i, j] != decimal.MinValue)
                        {
                            if (minKi > k[index_i, j])
                            {
                                minKi = k[index_i, j];
                            }
                        }
                        else
                        {
                            Log.Warning("Число с значением по умолчанию встретилось при подсчете минимального значения k[i]");
                        }
                    }
                    Log.Debug($"Минимальное k[i] рассчитано и равно {minKi}");
                }
                else
                {
                    minKi = 0;
                    Log.Debug($"Минимальное k[i] не было рассчитано и поэтому равно 0");
                }


                decimal avgKj = 0;
                int kValid = 0;

                // Считаем kValid, чтобы в случае, если мы получили недействительное число в матрице,
                // это непосчитанное значение не отразилось при подсчете среднего арифметического
                Log.Information("Начало рассчета среднего арифметического k[j]");
                for (int i = 0; i < oddNumbers.Length; i++)
                {
                    if (k[i, index_j] != decimal.MinValue)
                    {
                        avgKj += k[i, index_j];
                        kValid++;
                    }
                    else
                    {
                        Log.Warning("Число с значением по умолчанию встретилось при подсчете среднего арифметического k[j]");
                    }
                }

                if (kValid != 0)
                {
                    avgKj /= kValid;
                    Log.Debug($"Среднее арифметическое k[j] рассчитано и равно {avgKj}");
                }
                else
                {
                    Log.Debug("Среднее арифметическое не рассчитано и поэтому остается равным 0");
                }
                await Task.Delay(GropeConfigurationFile.Delay);
                Log.Information("Конец выполнение метода CalcAsync");
                return minKi + avgKj;
            });
        }
    }
}
