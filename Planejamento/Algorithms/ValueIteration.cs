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
            double maxDif, dif;
            var valoresAcoes = new double[qtdeAcoes];
            var pi = new int[qtdeEstados][];

            int s, a, slinha;

            Console.WriteLine();

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
                            valoresAcoes[a] += matrizesTransicao[a][s][slinha] * (matrizRecompensa[s] + (vAtual[slinha] * gama));
                        }
                    }

                    vProximo[s] = valoresAcoes.Max();
                    pi[s] = valoresAcoes.Select((v, i) => new { v, i }).Where(v => v.v == vProximo[s]).Select(v => v.i).ToArray();

                    dif = Math.Abs(vProximo[s] - vAtual[s]);
                    if (dif > maxDif)
                        maxDif = dif;
                }

                totalIterations += qtdeEstados * qtdeEstados * qtdeAcoes;

                vAtual = (double[])vProximo.Clone();

                if (maxDif < epsilon)
                    break;
            }

            sw.Stop();

            Console.SetCursorPosition(0, Console.CursorTop);

            return new
            {
                totalIterations,
                pi,
                vPi = vAtual,
                estadosIndecisos = pi.Count(p => p.Length > 4),
                tempo = sw.Elapsed.ToString()
            };
        }
    }
}
