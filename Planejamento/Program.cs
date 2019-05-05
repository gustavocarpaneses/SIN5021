using Planejamento.Algoritmos;
using Planejamento.Problemas;
using Planejamento.Problems.Relatorio1.Ambiente1;
using Planejamento.Problems.Relatorio1.Ambiente2;
using Planejamento.Problems.Relatorio1.Ambiente3;
using System;

namespace Planejamento
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                var epsilon = Math.Pow(10, -15);
                double gama = 1;

                var relatorioBasePath = "../../../Problems/Relatorio1";

                double[] matrizRecompensa;
                int qtdeEstados;

                matrizRecompensa = Ambiente1.ObterMatrizRecompensa(relatorioBasePath);
                qtdeEstados = matrizRecompensa.Length;

                Console.WriteLine($"Problema do Relatório 1 - Ambiente 1 - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

                ValueIteration.Run(
                    matrizRecompensa,
                    Ambiente1.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                    gama,
                    epsilon);

                Console.WriteLine($"Problema do Relatório 1 - Ambiente 1 - Policy Iteration - Gama = {gama} - Epsilon = {epsilon}");

                PolicyIteration.Run(
                    matrizRecompensa,
                    Ambiente1.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                    gama,
                    epsilon);

                Console.WriteLine();

                //matrizRecompensa = Ambiente2.ObterMatrizRecompensa(relatorioBasePath);
                //qtdeEstados = matrizRecompensa.Length;

                //Console.WriteLine($"Problema do Relatório 1 - Ambiente 2 - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    matrizRecompensa,
                //    Ambiente2.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                //matrizRecompensa = Ambiente3.ObterMatrizRecompensa(relatorioBasePath);
                //qtdeEstados = matrizRecompensa.Length;

                //Console.WriteLine($"Problema do Relatório 1 - Ambiente 3 - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    matrizRecompensa,
                //    Ambiente3.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                ///* Problema da Linha Determinística e Probabilística */

                //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    LinhaDetLinhaProb.ObterMatrizRecompensa(),
                //    LinhaDetLinhaProb.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Policy Iteration - Gama = {gama} - Epsilon = {epsilon}");

                //PolicyIteration.Run(
                //    LinhaDetLinhaProb.ObterMatrizRecompensa(),
                //    LinhaDetLinhaProb.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                //gama = 0.9;

                //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Value Iteration - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    LinhaDetLinhaProb.ObterMatrizRecompensa(),
                //    LinhaDetLinhaProb.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Policy Iteration - Gama = {gama} - Epsilon = {epsilon}");

                //PolicyIteration.Run(
                //    LinhaDetLinhaProb.ObterMatrizRecompensa(),
                //    LinhaDetLinhaProb.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                ///* Problema do Rio */

                //gama = 1;

                //Console.WriteLine($"Problema do Rio - Value Iteration -Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    Rio.ObterMatrizRecompensa(),
                //    Rio.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine($"Problema do Rio - Policy Iteration -Gama = {gama} - Epsilon = {epsilon}");

                //PolicyIteration.Run(
                //    Rio.ObterMatrizRecompensa(),
                //    Rio.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                //gama = 0.9;

                //Console.WriteLine($"Problema do Rio - Value Iteration -Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    Rio.ObterMatrizRecompensa(),
                //    Rio.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine($"Problema do Rio - Policy Iteration -Gama = {gama} - Epsilon = {epsilon}");

                //PolicyIteration.Run(
                //    Rio.ObterMatrizRecompensa(),
                //    Rio.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
