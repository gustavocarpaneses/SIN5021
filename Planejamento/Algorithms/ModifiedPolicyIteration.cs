using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class ModifiedPolicyIteration
    {
        public static dynamic Run(double[] matrizRecompensa, IList<double[][]> matrizesTransicao, double gama, double epsilon, int mCount)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var vAtual = new double[qtdeEstados];
            var vProximo = new double[qtdeEstados];
            double[] vAux;

            long totalIterations = 0;
            double maxDif, dif;
            var valoresAcoes = new double[qtdeAcoes];
            var pi = new int[qtdeEstados][];

            int s, a, slinha, m;

            for (s = 0; s < qtdeEstados; s++)
                pi[s] = new int[] { new Random().Next(0, qtdeAcoes - 1) };

            Console.WriteLine();

            while (true)
            {
                for (m = 0; m < mCount; m++)
                {
                    vAux = (double[])vAtual.Clone();

                    for (s = 0; s < qtdeEstados; s++)
                    {
                        vAtual[s] = 0;

                        for (slinha = 0; slinha < qtdeEstados; slinha++)
                        {
                            vAtual[s] += matrizesTransicao[pi[s].First()][s][slinha] * (matrizRecompensa[s] + (gama * vAux[slinha]));
                        }
                    }
                }

                totalIterations += qtdeEstados * qtdeEstados * mCount;

                maxDif = 0.0;

                for (s = 0; s < qtdeEstados; s++)
                {
                    for (a = 0; a < qtdeAcoes; a++)
                        valoresAcoes[a] = 0;

                    for (slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        for (a = 0; a < qtdeAcoes; a++)
                        {
                            valoresAcoes[a] += matrizesTransicao[a][s][slinha] * (matrizRecompensa[s] + (gama * vAtual[slinha]));
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

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(totalIterations);
            }

            sw.Stop();

            Console.SetCursorPosition(0, Console.CursorTop);

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
