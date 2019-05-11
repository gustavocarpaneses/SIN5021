using Planejamento.Algoritmos;
using Planejamento.Problemas;
using Planejamento.Problems.Relatorio1.Ambiente1;
using Planejamento.Problems.Relatorio1.Ambiente2;
using Planejamento.Problems.Relatorio1.Ambiente3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Planejamento
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                var relatorioBasePath = "../../../Problems/Relatorio1";


                //ProblemaAmbiente1(relatorioBasePath);

                ProblemaAmbiente2(relatorioBasePath);

                //ProblemaAmbiente3(relatorioBasePath);

                //ProblemaLinhaDetLinhaProb();

                //ProblemaRio();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
        }

        private static void ProblemaAmbiente1(string relatorioBasePath)
        {
            var matrizRecompensa = Ambiente1.ObterMatrizRecompensa(relatorioBasePath);
            var qtdeEstados = matrizRecompensa.Length;
            var matrizTransicao = Ambiente1.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados);

            ExecutarAlgoritmos("Ambiente 1", matrizRecompensa, matrizTransicao, 15, 9, 1);
        }

        private static void ProblemaAmbiente2(string relatorioBasePath)
        {
            var matrizRecompensa = Ambiente2.ObterMatrizRecompensa(relatorioBasePath);
            var qtdeEstados = matrizRecompensa.Length;
            var matrizTransicao = Ambiente2.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados);

            ExecutarAlgoritmos("Ambiente 2", matrizRecompensa, matrizTransicao, 15, 9, 11);
        }

        private static void ProblemaAmbiente3(string relatorioBasePath)
        {
            var matrizRecompensa = Ambiente3.ObterMatrizRecompensa(relatorioBasePath);
            var qtdeEstados = matrizRecompensa.Length;
            var matrizTransicao = Ambiente3.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados);

            ExecutarAlgoritmos("Ambiente 3", matrizRecompensa, matrizTransicao, 30, 18, 35);
        }

        private static void ProblemaLinhaDetLinhaProb()
        {
            var matrizRecompensa = LinhaDetLinhaProb.ObterMatrizRecompensa();
            var matrizTransicao = LinhaDetLinhaProb.ObterMatrizesDeTransicao();

            ExecutarAlgoritmos("Ambiente linha det-prob", matrizRecompensa, matrizTransicao, 5, 2, 1);
        }

        private static void ProblemaRio()
        {
            var matrizRecompensa = Rio.ObterMatrizRecompensa();
            var matrizTransicao = Rio.ObterMatrizesDeTransicao();

            ExecutarAlgoritmos("Ambiente rio", matrizRecompensa, matrizTransicao, 3, 2, 1);
        }

        private static void ExecutarAlgoritmos(string ambiente, double[] matrizRecompensa, IList<double[][]> matrizTransicao, int largura, int altura, int qtdePisos)
        {
            //var epsilons = new double[] { Math.Pow(10, -20), Math.Pow(10, -15), Math.Pow(10, -10), Math.Pow(10, -5) };
            //var gamas = new double[] { 1.0, 0.99, 0.9, 0.8, 0.5 };
            //var ms = new int[] { 1, 2, 3, 4, 5 };

            var epsilons = new double[] { Math.Pow(10, -5) };
            var gamas = new double[] { 0.9 };
            var ms = new int[] { 3 };

            using (var sw = new StreamWriter($"resultado_{ambiente}_{DateTime.Now.Ticks.ToString()}.csv", false))
            {
                sw.WriteLine("algoritmo;gama;epsilon;m;totalIterations;tempo");

                foreach (var gama in gamas)
                {
                    Console.WriteLine($"{ambiente} - Policy Iteration - Gama = {gama}");

                    var retorno = PolicyIteration.Run(
                        matrizRecompensa,
                        matrizTransicao,
                        gama);

                    Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações");
                    
                    sw.WriteLine($"pi;{gama};-;-;{retorno.totalIterations};{retorno.tempo}");

                    Print(largura, altura, qtdePisos, retorno.pi, retorno.vPi);

                    Console.WriteLine();

                    foreach (var epsilon in epsilons)
                    {
                        Console.WriteLine($"{ambiente} - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

                        retorno = ValueIteration.Run(
                            matrizRecompensa,
                            matrizTransicao,
                            gama,
                            epsilon);

                        Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações");

                        sw.WriteLine($"vi;{gama};{epsilon};-;{retorno.totalIterations};{retorno.tempo}");

                        Print(largura, altura, qtdePisos, retorno.pi, retorno.vPi);

                        Console.WriteLine();

                        Console.WriteLine($"{ambiente} - Prioritized Sweeping - Gama = {gama} - Epsilon = {epsilon}");

                        retorno = PrioritizedSweeping.Run(
                            matrizRecompensa,
                            matrizTransicao,
                            gama,
                            epsilon);

                        Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações");

                        sw.WriteLine($"ps;{gama};{epsilon};-;{retorno.totalIterations};{retorno.tempo}");

                        Print(largura, altura, qtdePisos, retorno.pi, retorno.vPi);

                        Console.WriteLine();

                        foreach (var m in ms)
                        {
                            Console.WriteLine($"{ambiente} - Modified Policy Iteration - Gama = {gama} - Epsilon = {epsilon} - m = {m}");

                            retorno = ModifiedPolicyIteration.Run(
                                matrizRecompensa,
                                matrizTransicao,
                                gama,
                                epsilon,
                                m);

                            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações");

                            sw.WriteLine($"mpi;{gama};{epsilon};{m};{retorno.totalIterations};{retorno.tempo}");

                            Print(largura, altura, qtdePisos, retorno.pi, retorno.vPi);

                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        private static string GetActionSymbol(int[] pi)
        {
            var actionIndex = pi.First();

            switch (actionIndex)
            {
                case 0: return "↑"; //norte
                case 1: return "↓"; //sul
                case 2: return "→"; //leste
                case 3: return "←"; //oeste
                case 4: return "D"; //acima
                case 5: return "S"; //abaixo
                default: return "ϕ";
            };
        }

        private static void Print(int largura, int altura, int qtdePisos, int[][] pi, double[] vPi)
        {
            var max = vPi.Min();
            var min = vPi.Max();

            var tamanhoPiso = largura * altura;

            for (int p = 0; p < qtdePisos; p++)
            {
                Console.ResetColor();
                Console.WriteLine($"Piso {p + 1}");

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new string(' ', largura * 2 + 1));

                for (int i = 0; i < altura; i++)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(" ");
                    for (int j = 0; j < largura; j++)
                    {
                        var s = (p * tamanhoPiso) + (largura * i) + j;
                        var normalizedValue = ((vPi[s] - min) / (max - min)) * 5;

                        SetColor(Math.Round(normalizedValue, 0));
                        if (vPi[s] == 0)
                            Console.Write("G");
                        else
                            Console.Write(GetActionSymbol(pi[s]));

                        if (j == (largura - 1))
                        {
                            Console.ResetColor();
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("|");
                            Console.ResetColor();
                        }
                    }

                    Console.Write(Environment.NewLine);
                }

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new string(' ', largura * 2 + 1));
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        private static void SetColor(double value)
        {
            ConsoleColor background;
            ConsoleColor foreground;

            switch (value)
            {
                case 0:
                    background = ConsoleColor.Black;
                    foreground = ConsoleColor.White;
                    break;

                case 1:
                    background = ConsoleColor.DarkBlue;
                    foreground = ConsoleColor.White;
                    break;

                case 2:
                    background = ConsoleColor.Blue;
                    foreground = ConsoleColor.White;
                    break;

                case 3:
                    background = ConsoleColor.DarkCyan;
                    foreground = ConsoleColor.White;
                    break;

                case 4:
                    background = ConsoleColor.Cyan;
                    foreground = ConsoleColor.Black;
                    break;

                case 5:
                    background = ConsoleColor.White;
                    foreground = ConsoleColor.Black;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }

            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }
    }
}
