using Planejamento.Algoritmos;
using Planejamento.Problemas;
using Planejamento.Problems.Relatorio1.Ambiente1;
using Planejamento.Problems.Relatorio1.Ambiente2;
using Planejamento.Problems.Relatorio1.Ambiente3;
using System;
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

                //ProblemaAmbiente2(relatorioBasePath);

                //ProblemaAmbiente3(relatorioBasePath);

                ProblemaDaLinhaDetLinhaProb();

                //ProblemaDoRio();

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
            var epsilon = Math.Pow(10, -15);
            double gama = 1;

            double[] matrizRecompensa;
            int qtdeEstados;

            matrizRecompensa = Ambiente1.ObterMatrizRecompensa(relatorioBasePath);
            qtdeEstados = matrizRecompensa.Length;

            Console.WriteLine($"Problema do Relatório 1 - Ambiente 1 - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

            var retorno = ValueIteration.Run(
                matrizRecompensa,
                Ambiente1.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                gama,
                epsilon);

            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            Print(15, 9, 1, retorno.pi, retorno.vPi);

            Console.WriteLine();

            Console.WriteLine($"Problema do Relatório 1 - Ambiente 1 - Policy Iteration - Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                Ambiente1.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                gama);

            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            Print(15, 9, 1, retorno.pi, retorno.vPi);

            Console.WriteLine();
        }

        private static void ProblemaAmbiente2(string relatorioBasePath)
        {
            var epsilon = Math.Pow(10, -10);
            double gama = 0.9;

            double[] matrizRecompensa;
            int qtdeEstados;

            matrizRecompensa = Ambiente2.ObterMatrizRecompensa(relatorioBasePath);
            qtdeEstados = matrizRecompensa.Length;

            Console.WriteLine($"Problema do Relatório 1 - Ambiente 2 - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

            var retorno = ValueIteration.Run(
                matrizRecompensa,
                Ambiente2.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                gama,
                epsilon);

            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            Print(15, 9, 11, retorno.pi, retorno.vPi);

            Console.WriteLine();

            Console.WriteLine($"Problema do Relatório 1 - Ambiente 2 - Policy Iteration - Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                Ambiente2.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                gama);

            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            Print(15, 9, 11, retorno.pi, retorno.vPi);

            Console.WriteLine();
        }

        private static void ProblemaAmbiente3(string relatorioBasePath)
        {
            var epsilon = Math.Pow(10, -5);
            double gama = 1;

            double[] matrizRecompensa;

            int qtdeEstados;
            matrizRecompensa = Ambiente3.ObterMatrizRecompensa(relatorioBasePath);
            qtdeEstados = matrizRecompensa.Length;

            Console.WriteLine($"Problema do Relatório 1 - Ambiente 3 - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

            var retorno = ValueIteration.Run(
                matrizRecompensa,
                Ambiente3.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                gama,
                epsilon);

            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            Print(30, 18, 35, retorno.pi, retorno.vPi);

            Console.WriteLine();

            Console.WriteLine($"Problema do Relatório 1 - Ambiente 3 - Policy Iteration - Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                Ambiente3.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                gama);

            Console.WriteLine($"Convergiu em {retorno.tempo} num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            Print(30, 18, 35, retorno.pi, retorno.vPi);

            Console.WriteLine();
        }

        private static void ProblemaDaLinhaDetLinhaProb()
        {
            var epsilon = Math.Pow(10, -15);
            double gama = 1;
            int m = 1;

            var matrizRecompensa = LinhaDetLinhaProb.ObterMatrizRecompensa();
            var matrizesTransicao = LinhaDetLinhaProb.ObterMatrizesDeTransicao();
            var qtdeEstados = matrizRecompensa.Length;
            int s;

            /* Problema da Linha Determinística e Probabilística */

            Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

            var retorno = ValueIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama,
                epsilon);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Policy Iteration - Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Modified Policy Iteration - Gama = {gama} - Epsilon = {epsilon} - m = {m}");

            //retorno = ModifiedPolicyIteration.Run(
            //    matrizRecompensa,
            //    matrizesTransicao,
            //    gama,
            //    epsilon,
            //    m);

            //Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            //for (s = 0; s < qtdeEstados; s++)
            //{
            //    Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            //}

            //Console.WriteLine();

            gama = 0.9;

            Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

            retorno = ValueIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama,
                epsilon);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Policy Iteration - Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Modified Policy Iteration - Gama = {gama} - Epsilon = {epsilon} - m = {m}");

            //retorno = ModifiedPolicyIteration.Run(
            //    matrizRecompensa,
            //    matrizesTransicao,
            //    gama,
            //    epsilon,
            //    m);

            //Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            //for (s = 0; s < qtdeEstados; s++)
            //{
            //    Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            //}

            //Console.WriteLine();
        }

        private static void ProblemaDoRio()
        {
            var epsilon = Math.Pow(10, -15);
            double gama = 1;

            var matrizRecompensa = Rio.ObterMatrizRecompensa();
            var matrizesTransicao = Rio.ObterMatrizesDeTransicao();
            var qtdeEstados = matrizRecompensa.Length;
            int s;

            /* Problema do Rio */

            Console.WriteLine($"Problema do Rio - Value Iteration -Gama = {gama} - Epsilon = {epsilon}");

            var retorno = ValueIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama,
                epsilon);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            Console.WriteLine($"Problema do Rio - Policy Iteration -Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            gama = 0.9;

            Console.WriteLine($"Problema do Rio - Value Iteration -Gama = {gama} - Epsilon = {epsilon}");

            retorno = ValueIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama,
                epsilon);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();

            Console.WriteLine($"Problema do Rio - Policy Iteration -Gama = {gama}");

            retorno = PolicyIteration.Run(
                matrizRecompensa,
                matrizesTransicao,
                gama);

            Console.WriteLine($"Convergiu num total de {retorno.totalIterations} iterações, sendo {retorno.optimizationIterations} iterações de otimização");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => Ação: {GetActionName(retorno.pi[s])} => {retorno.vPi[s]}");
            }

            Console.WriteLine();
        }

        private static string GetActionName(int[] actionsIndex)
        {
            return string.Join(",",
                actionsIndex.Select(i =>
                {
                    switch (i)
                    {
                        case 0: return "Norte";
                        case 1: return "Sul";
                        case 2: return "Leste";
                        case 3: return "Oeste";
                        case 4: return "Abaixo";
                        case 5: return "Acima";
                        default: return "Inválido";
                    }
                }));
        }

        private static string GetActionSymbol(int[] pi)
        {
            if (pi.Length == 6)
                return "G";

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
                Console.WriteLine($"Piso {p+1}");
                
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
