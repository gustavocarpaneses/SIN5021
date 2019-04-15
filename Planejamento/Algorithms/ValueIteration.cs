using System;
using System.Collections.Generic;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class ValueIteration
    {
        public static long Run(double[] matrizRecompensa, List<double[,]> matrizesTransicao, double gama, double epsilon)
        {
            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var vAtual = new double[qtdeEstados];
            var vProximo = new double[qtdeEstados];

            long iterations = 0;

            while (true)
            {
                var maxDif = 0.0;

                for (int s = 0; s < qtdeEstados; s++)
                {
                    var valoresAcoes = new double[qtdeAcoes];

                    for (int slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        for (int a = 0; a < qtdeAcoes; a++)
                        {
                            var probabilidade = matrizesTransicao[a][s, slinha];
                            valoresAcoes[a] += (vAtual[slinha] * gama) * probabilidade;
                        }
                    }

                    var novoValor = valoresAcoes.Max();
                    var acaoEscolhida = Array.IndexOf(valoresAcoes, novoValor);

                    vProximo[s] = novoValor += matrizRecompensa[s];

                    var dif = Math.Abs(vProximo[s] - vAtual[s]);
                    if (dif > maxDif)
                        maxDif = dif;
                }

                vAtual = (double[])vProximo.Clone();

                iterations++;

                if (maxDif < epsilon)
                    break;
            }

            Console.WriteLine($"Convergiu em {iterations} iterações");

            for (int s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s+1} => {vAtual[s]}");
            }

            return iterations;
        }
    }
}
