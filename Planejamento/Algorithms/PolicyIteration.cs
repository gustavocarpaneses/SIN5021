using System;
using System.Collections.Generic;
using System.Linq;

namespace Planejamento.Algoritmos
{
    static class PolicyIteration
    {
        public static long Run(double[] matrizRecompensa, List<double[][]> matrizesTransicao, double gama, double epsilon)
        {
            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var pi = new int[qtdeEstados];

            var vPi = new double[qtdeEstados];
            double[] vAux;
            var vAtual = new double[qtdeEstados];

            long iterations = 0;
            double probabilidade;
            bool melhorou;
            var valoresAcoes = new double[qtdeAcoes];
            int s, a, slinha;

            for (s = 0; s < qtdeEstados; s++)
                pi[s] = new Random().Next(0, qtdeAcoes - 1);

            while (true)
            {
                vAux = (double[])vPi.Clone();
                
                //avaliar
                for (s=0; s<qtdeEstados; s++)
                {
                    vPi[s] = 0;
                    for (slinha = 0; slinha < qtdeEstados; slinha++)
                    {
                        probabilidade = matrizesTransicao[pi[s]][s][slinha];
                        vPi[s] += probabilidade * (matrizRecompensa[s] + (gama * vAux[slinha]));
                    }
                }

                melhorou = false;

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
                        }
                    }

                    vAtual[s] = valoresAcoes.Max();
                    pi[s] = Array.IndexOf(valoresAcoes, vAtual[s]);

                    if (vAtual[s] < vPi[s])
                        melhorou = true;
                }

                iterations++;

                if (!melhorou)
                    break;
            }

            Console.WriteLine($"Convergiu em {iterations} iterações");

            for (s = 0; s < qtdeEstados; s++)
            {
                Console.WriteLine($"Estado {s + 1} => {vAtual[s]}");
            }

            return iterations;
        }
    }
}
