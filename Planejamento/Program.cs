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
                var epsilon = 0.0000000001;
                double gama = 0.9;

                var relatorioBasePath = "../../../Problems/Relatorio1";

                double[] matrizRecompensa;
                int qtdeEstados;

                matrizRecompensa = Ambiente1.ObterMatrizRecompensa(relatorioBasePath);
                qtdeEstados = matrizRecompensa.Length;

                Console.WriteLine($"Problema do Relatório 1 - Ambiente 1 - Gama = {gama} - Epsilon = {epsilon}");

                ValueIteration.Run(
                    matrizRecompensa,
                    Ambiente1.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                    gama,
                    epsilon);

                Console.WriteLine();

                matrizRecompensa = Ambiente2.ObterMatrizRecompensa(relatorioBasePath);
                qtdeEstados = matrizRecompensa.Length;

                Console.WriteLine($"Problema do Relatório 1 - Ambiente 2 - Gama = {gama} - Epsilon = {epsilon}");

                ValueIteration.Run(
                    matrizRecompensa,
                    Ambiente2.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                    gama,
                    epsilon);

                Console.WriteLine();

                //matrizRecompensa = Ambiente3.ObterMatrizRecompensa(relatorioBasePath);
                //qtdeEstados = matrizRecompensa.Length;

                //Console.WriteLine($"Problema do Relatório 1 - Ambiente 3 - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    matrizRecompensa,
                //    Ambiente3.ObterMatrizesDeTransicao(relatorioBasePath, qtdeEstados),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                ///* Problema da Linha Determinística e Probabilística */

                //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    LinhaDetLinhaProb.ObterMatrizRecompensa(),
                //    LinhaDetLinhaProb.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                //gama = 0.9;

                //Console.WriteLine($"Problema da Linha Determinística e Linha Probabilística - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    LinhaDetLinhaProb.ObterMatrizRecompensa(),
                //    LinhaDetLinhaProb.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                ///* Problema do Rio */

                //gama = 1;

                //Console.WriteLine($"Problema do Rio - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    Rio.ObterMatrizRecompensa(),
                //    Rio.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                //Console.WriteLine();

                //gama = 0.9;

                //Console.WriteLine($"Problema do Rio - Gama = {gama} - Epsilon = {epsilon}");

                //ValueIteration.Run(
                //    Rio.ObterMatrizRecompensa(),
                //    Rio.ObterMatrizesDeTransicao(),
                //    gama,
                //    epsilon);

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
