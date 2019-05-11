using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class PrioritizedSweeping
    {
        public static dynamic Run(double[] matrizRecompensa, IList<double[][]> matrizesTransicao, double gama, double epsilon)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var h = new double[qtdeEstados];
            var v = new double[qtdeEstados];

            long totalIterations = 0;
            long optimizationIterations = 0;
            double probabilidade, delta;
            var valoresAcoes = new double[qtdeAcoes];
            var pi = new int[qtdeEstados][];

            double maxH, maxV, maxT;
            int s, melhorS, a, slinha;

            for (s = 0; s < qtdeEstados; s++)
                h[s] = epsilon + new Random().NextDouble();

            while (true)
            {
                maxH = h.Max();
                melhorS = h.Select((value, index) => new { v = value, i = index }).First(x => x.v == maxH).i;

                for (a = 0; a < qtdeAcoes; a++)
                    valoresAcoes[a] = 0;

                for (slinha = 0; slinha < qtdeEstados; slinha++)
                {
                    for (a = 0; a < qtdeAcoes; a++)
                    {
                        probabilidade = matrizesTransicao[a][melhorS][slinha];
                        valoresAcoes[a] += probabilidade * (matrizRecompensa[melhorS] + (v[slinha] * gama));
                        totalIterations++;
                    }
                }

                maxV = valoresAcoes.Max();
                pi[melhorS] = valoresAcoes.Select((value, index) => new { v = value, i = index }).Where(x => x.v == maxV).Select(x => x.i).ToArray();

                delta = Math.Abs(maxV - v[melhorS]);

                v[melhorS] = maxV;

                for (s = 0; s < qtdeEstados; s++)
                {
                    if (s == melhorS)
                        h[s] = delta * matrizesTransicao.Max(x => x[s][melhorS]);
                    else
                        h[s] = Math.Max(h[s], delta * matrizesTransicao.Max(x => x[s][melhorS]));
                }

                optimizationIterations++;

                if (h.Max() < epsilon)
                    break;
            }

            sw.Stop();

            return new
            {
                totalIterations,
                optimizationIterations,
                pi,
                vPi = v,
                tempo = sw.Elapsed.ToString()
            };
        }
    }
}
