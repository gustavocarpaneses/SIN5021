using System;
using System.Collections.Generic;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class ValueIteration
    {
        public static long Run(double[] matrizRecompensa, List<double[][]> matrizesTransicao, double gama, double epsilon)
        {
            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var vAtual = new double[qtdeEstados];
            var vProximo = new double[qtdeEstados];

            long iterations = 0;
            double maxDif, probabilidade, dif;
            var valoresAcoes = new double[qtdeAcoes];

            int s, a, slinha;

            while (true)
            {
                maxDif = 0.0;

                for (s = 0; s < qtdeEstados; s++)
                {
                    for (a = 0; a < qtdeAcoes; a++)
                        valoresAcoes[a] = 0;

                    for (slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        for (a = 0; a < qtdeAcoes; a++)
                        {
                            probabilidade = matrizesTransicao[a][s][slinha];
                            valoresAcoes[a] += probabilidade * (matrizRecompensa[s] + (vAtual[slinha] * gama));
                        }
                    }

                    vProximo[s] = valoresAcoes.Max();

                    dif = Math.Abs(vProximo[s] - vAtual[s]);
                    if (dif > maxDif)
                        maxDif = dif;
                }

                vAtual = (double[])vProximo.Clone();

                iterations++;

                if (maxDif < epsilon)
                    break;
            }

            Console.WriteLine($"Convergiu em {iterations} iterações");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s+1} => {vAtual[s]}");
            }

            return iterations;
        }
    }
}
