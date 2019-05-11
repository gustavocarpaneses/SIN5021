using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class PolicyIteration
    {
        public static dynamic Run(double[] matrizRecompensa, IList<double[][]> matrizesTransicao, double gama)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var pi = new int[qtdeEstados][];
            var piLinha = new int[qtdeEstados][];

            var vPi = new double[qtdeEstados];
            double[] vAux;

            long totalIterations = 0;
            double probabilidade;
            bool mudou;
            var valoresAcoes = new double[qtdeAcoes];
            var valorMelhorAcao = 0.0;
            int s, a, slinha;

            for (s = 0; s < qtdeEstados; s++)
                pi[s] = new int[] { new Random().Next(0, qtdeAcoes - 1) };

            while (true)
            {
                vAux = (double[])vPi.Clone();
                
                //avaliar
                for (s=0; s<qtdeEstados; s++)
                {
                    vPi[s] = 0;
                    for (slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        probabilidade = matrizesTransicao[pi[s].First()][s][slinha];
                        vPi[s] += probabilidade * (matrizRecompensa[s] + (gama * vAux[slinha]));
                        totalIterations++;
                    }
                }

                mudou = false;

                //melhorar
                for (s = 0; s < qtdeEstados; s++)
                {
                    for (a = 0; a < qtdeAcoes; a++)
                        valoresAcoes[a] = 0;

                    for (slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        for (a = 0; a < qtdeAcoes; a++)
                        {
                            probabilidade = matrizesTransicao[a][s][slinha];
                            valoresAcoes[a] += probabilidade * (matrizRecompensa[s] + (gama * vPi[slinha]));
                            totalIterations++;
                        }
                    }

                    valorMelhorAcao = valoresAcoes.Max();
                    piLinha[s] = valoresAcoes.Select((v, i) => new { v, i }).Where(v => v.v == valorMelhorAcao).Select(v => v.i).ToArray();

                    if (!piLinha[s].SequenceEqual(pi[s]))
                        mudou = true;

                    pi[s] = piLinha[s];
                }

                if (!mudou)
                    break;
            }

            sw.Stop();

            return new
            {
                totalIterations,
                pi,
                vPi,
                tempo = sw.Elapsed.ToString()
            };
        }
    }
}
