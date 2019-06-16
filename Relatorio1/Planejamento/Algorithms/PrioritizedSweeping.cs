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
            double delta;
            var valoresAcoes = new double[qtdeAcoes];
            var pi = new int[qtdeEstados][];

            double maxH, maxV;
            int s, melhorS, a, slinha;

            for (s = 0; s < qtdeEstados; s++)
                h[s] = epsilon + (0.1 * new Random().NextDouble());

            Console.WriteLine();

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
                        valoresAcoes[a] += matrizesTransicao[a][melhorS][slinha] * (matrizRecompensa[melhorS] + (v[slinha] * gama));
                    }
                }

                totalIterations += qtdeEstados * qtdeAcoes;

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

                if (h.Max() < epsilon)
                    break;
            }

            sw.Stop();

            Console.SetCursorPosition(0, Console.CursorTop);

            return new
            {
                totalIterations,
                pi,
                vPi = v,
                estadosIndecisos = pi.Count(p => p.Length >= 4),
                tempo = sw.Elapsed.ToString()
            };
        }
    }
}
