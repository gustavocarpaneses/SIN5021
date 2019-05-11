using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class ValueIteration
    {
        public static dynamic Run(double[] matrizRecompensa, IList<double[][]> matrizesTransicao, double gama, double epsilon)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var vAtual = new double[qtdeEstados];
            var vProximo = new double[qtdeEstados];

            long totalIterations = 0;
            double maxDif, probabilidade, dif;
            var valoresAcoes = new double[qtdeAcoes];
            var pi = new int[qtdeEstados][];

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
                            totalIterations++;
                        }
                    }

                    vProximo[s] = valoresAcoes.Max();
                    pi[s] = valoresAcoes.Select((v, i) => new { v, i }).Where(v => v.v == vProximo[s]).Select(v => v.i).ToArray();

                    dif = Math.Abs(vProximo[s] - vAtual[s]);
                    if (dif > maxDif)
                        maxDif = dif;
                }

                vAtual = (double[])vProximo.Clone();

                if (maxDif < epsilon)
                    break;
            }

            sw.Stop();

            return new
            {
                totalIterations,
                pi,
                vPi = vAtual,
                tempo = sw.Elapsed.ToString()
            };
        }
    }
}
