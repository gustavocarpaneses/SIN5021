using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class ModifiedPolicyIteration
    {
        public static dynamic Run(double[] matrizRecompensa, List<double[][]> matrizesTransicao, double gama, double epsilon, int m)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var pi = new int[qtdeEstados][];
            var piLinha = new int[qtdeEstados][];

            var vAtual = new double[qtdeEstados];
            var vProximo = new double[qtdeEstados];
            double[] vAux;

            long totalIterations = 0;
            long optimizationIterations = 0;
            double maxDif, probabilidade, dif;
            var valoresAcoes = new double[qtdeAcoes];
            int s, a, slinha;

            for (s = 0; s < qtdeEstados; s++)
                pi[s] = new int[] { new Random().Next(0, qtdeAcoes - 1) };

            while (true)
            {
                maxDif = 0.0;

                for (int i = 0; i < m; i++)
                {
                    vAux = (double[])vProximo.Clone();

                    for (s = 0; s < qtdeEstados; s++)
                    {
                        for (slinha = 0; slinha < qtdeEstados; slinha++)
                        {
                            probabilidade = matrizesTransicao[pi[s].First()][s][slinha];
                            vProximo[s] += probabilidade * (matrizRecompensa[s] + (gama * vAux[slinha]));
                            totalIterations++;
                        }
                    }
                }

                for (s = 0; s < qtdeEstados; s++)
                {
                    for (a = 0; a < qtdeAcoes; a++)
                        valoresAcoes[a] = 0;

                    for (slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        for (a = 0; a < qtdeAcoes; a++)
                        {
                            probabilidade = matrizesTransicao[a][s][slinha];
                            valoresAcoes[a] += probabilidade * (matrizRecompensa[s] + (gama * vAtual[slinha]));
                            totalIterations++;
                        }
                    }

                    vProximo[s] = valoresAcoes.Max();
                    pi[s] = valoresAcoes.Select((v, i) => new { v, i }).Where(v => v.v == vProximo[s]).Select(v => v.i).ToArray();

                    dif = Math.Abs(vAtual[s] - vProximo[s]);
                    if (dif > maxDif)
                        maxDif = dif;

                    //pi[s] = piLinha[s];
                }

                vAtual = (double[])vProximo.Clone();

                optimizationIterations++;

                if (maxDif < epsilon)
                    break;
            }

            sw.Stop();

            return new
            {
                totalIterations,
                optimizationIterations,
                pi,
                vPi = vAtual,
                tempo = sw.Elapsed.ToString()
            };
        }
    }
}
