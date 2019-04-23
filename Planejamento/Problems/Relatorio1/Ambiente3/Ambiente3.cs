using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Planejamento.Problems.Relatorio1.Ambiente3
{
    /* 30x18 - 35 floors */
    static class Ambiente3
    {
        public static double[] ObterMatrizRecompensa(string basePath)
        {
            var rewards = File.ReadAllLines(Path.Combine(basePath, "Ambiente3/Rewards.txt"), Encoding.UTF8);
            return rewards.Select(r => double.Parse(r)).ToArray();
        }

        public static List<double[][]> ObterMatrizesDeTransicao(string basePath, int qtdeEstados)
        {
            var action01 = ObterDoArquivo(Path.Combine(basePath, "Ambiente3/Action01.txt"), qtdeEstados);
            var action02 = ObterDoArquivo(Path.Combine(basePath, "Ambiente3/Action02.txt"), qtdeEstados);
            var action03 = ObterDoArquivo(Path.Combine(basePath, "Ambiente3/Action03.txt"), qtdeEstados);
            var action04 = ObterDoArquivo(Path.Combine(basePath, "Ambiente3/Action04.txt"), qtdeEstados);
            var action05 = ObterDoArquivo(Path.Combine(basePath, "Ambiente3/Action05.txt"), qtdeEstados);
            var action06 = ObterDoArquivo(Path.Combine(basePath, "Ambiente3/Action06.txt"), qtdeEstados);

            return new List<double[][]>
            {
                action01, action02, action03, action04, action05, action06
            };
        }

        private static double[][] ObterDoArquivo(string path, int qtdeEstados)
        {
            var actionFile = File.ReadAllLines(path, Encoding.UTF8);

            var action = new double[qtdeEstados][];

            for (int i = 0; i < qtdeEstados; i++)
            {
                action[i] = new double[qtdeEstados];
            }

            foreach (var line in actionFile)
            {
                var splitedLine = line.Split("   ", StringSplitOptions.RemoveEmptyEntries);
                var s = (int)double.Parse(splitedLine[0]) - 1;
                var slinha = (int)double.Parse(splitedLine[1]) - 1;
                var probabilidade = double.Parse(splitedLine[2]);

                action[s][slinha] = probabilidade;
            }

            return action;
        }
    }
}
