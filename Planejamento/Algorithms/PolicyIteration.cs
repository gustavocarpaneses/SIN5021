using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Planejamento.Algoritmos
{
    static class PolicyIteration
    {
        public static long Run(double[] matrizRecompensa, List<double[][]> matrizesTransicao, double gama, double epsilon)
        {
            var qtdeEstados = matrizRecompensa.Length;
            var qtdeAcoes = matrizesTransicao.Count;
            var piAtual = new int[qtdeEstados];
            var piLinha = new int[qtdeEstados];

            var vAtual = new double[qtdeEstados];

            long iterations = 0;
            double probabilidade;
            var valoresAcoes = new double[qtdeAcoes];
            int s, a, slinha;

            for (s = 0; s < qtdeEstados; s++)
                piAtual[s] = new Random().Next(0, qtdeAcoes - 1);

            while (true)
            {
                //avaliar
                //calcular em vAtual

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
                            valoresAcoes[a] += (vAtual[slinha] * gama) * probabilidade;
                        }
                    }

                    piLinha[s] = Array.IndexOf(valoresAcoes, valoresAcoes.Max());
                }
            }

            return iterations;
        }
    }
}
