using System.Collections.Generic;

namespace Planejamento.Problemas
{
    /*
        
            ┌--------------┐
            │00|01|02|03|04| ==> Prob. 50% de executar a ação e 50% de ficar no mesmo lugar
            |--------------| 
            │05|06|07|08|09| ==> Prob. 100% de executar a ação
            └--------------┘
            Custo de 1 por ação
            04 é o absorvedor

    */
    static class LinhaDetLinhaProb
    {
        public static double[] ObterMatrizRecompensa()
        {
            var recompensa = new double[10];

            recompensa[0] = -1;
            recompensa[1] = -1;
            recompensa[2] = -1;
            recompensa[3] = -1;
            recompensa[4] = 0;
            recompensa[5] = -1;
            recompensa[6] = -1;
            recompensa[7] = -1;
            recompensa[8] = -1;
            recompensa[9] = -1;

            return recompensa;
        }

        public static List<double[,]> ObterMatrizesDeTransicao()
        {
            double[,] transicaoNorte = new double[10, 10];

            transicaoNorte[0, 0] = 1;
            transicaoNorte[1, 1] = 1;
            transicaoNorte[2, 2] = 1;
            transicaoNorte[3, 3] = 1;
            transicaoNorte[4, 4] = 0; //absorvedor
            transicaoNorte[5, 0] = 1;
            transicaoNorte[6, 1] = 1;
            transicaoNorte[7, 2] = 1;
            transicaoNorte[8, 3] = 1;
            transicaoNorte[9, 4] = 1;

            double[,] transicaoLeste = new double[10, 10];

            transicaoLeste[0, 0] = 0.5;
            transicaoLeste[0, 1] = 0.5;
            transicaoLeste[1, 1] = 0.5;
            transicaoLeste[1, 2] = 0.5;
            transicaoLeste[2, 2] = 0.5;
            transicaoLeste[2, 3] = 0.5;
            transicaoLeste[3, 3] = 0.5;
            transicaoLeste[3, 4] = 0.5;
            transicaoLeste[4, 4] = 0; //absorvedor
            transicaoLeste[5, 6] = 1;
            transicaoLeste[6, 7] = 1;
            transicaoLeste[7, 8] = 1;
            transicaoLeste[8, 9] = 1;
            transicaoLeste[9, 9] = 1;

            double[,] transicaoOeste = new double[10, 10];

            transicaoOeste[0, 0] = 1;
            transicaoOeste[1, 1] = 0.5;
            transicaoOeste[1, 0] = 0.5;
            transicaoOeste[2, 2] = 0.5;
            transicaoOeste[2, 1] = 0.5;
            transicaoOeste[3, 3] = 0.5;
            transicaoOeste[3, 2] = 0.5;
            transicaoOeste[4, 4] = 0; //absorvedor
            transicaoOeste[5, 5] = 1;
            transicaoOeste[6, 5] = 1;
            transicaoOeste[7, 6] = 1;
            transicaoOeste[8, 7] = 1;
            transicaoOeste[9, 8] = 1;

            double[,] transicaoSul = new double[10, 10];

            transicaoSul[0, 0] = 0.5;
            transicaoSul[0, 5] = 0.5;
            transicaoSul[1, 1] = 0.5;
            transicaoSul[1, 6] = 0.5;
            transicaoSul[2, 2] = 0.5;
            transicaoSul[2, 7] = 0.5;
            transicaoSul[3, 3] = 0.5;
            transicaoSul[3, 8] = 0.5;
            transicaoSul[4, 4] = 0; //absorvedor
            transicaoSul[5, 5] = 1;
            transicaoSul[6, 6] = 1;
            transicaoSul[7, 7] = 1;
            transicaoSul[8, 8] = 1;
            transicaoSul[9, 9] = 1;

            return new List<double[,]>
            {
                transicaoNorte, transicaoSul, transicaoLeste, transicaoOeste
            };
        }
    }
}
