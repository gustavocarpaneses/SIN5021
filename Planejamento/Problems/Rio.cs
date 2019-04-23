using System.Collections.Generic;

namespace Planejamento.Problemas
{
    /*
        
            ┌--------┐
            │03|04|05|
            |--------| 
            │00|01|02|
            └--------┘
            As ações são deterministas, exceto no estado 01, onde tem 50% chance de voltar pra 00.
            Custo de 1 por ação
            02 é o absorvedor

    */
    static class Rio
    {
        public static double[] ObterMatrizRecompensa()
        {
            var recompensa = new double[6];

            recompensa[0] = -1;
            recompensa[1] = -1;
            recompensa[2] = 0;
            recompensa[3] = -1;
            recompensa[4] = -1;
            recompensa[5] = -1;

            return recompensa;
        }

        public static List<double[][]> ObterMatrizesDeTransicao()
        {
            double[][] transicaoNorte = new double[6][];

            for (int i = 0; i < 6; i++)
                transicaoNorte[i] = new double[6];

            transicaoNorte[0][3] = 1;
            transicaoNorte[1][4] = 0.5;
            transicaoNorte[1][0] = 0.5;
            transicaoNorte[2][2] = 0; //absorvedor
            transicaoNorte[3][3] = 1;
            transicaoNorte[4][4] = 1;
            transicaoNorte[5][5] = 1;

            double[][] transicaoLeste = new double[6][];

            for (int i = 0; i < 6; i++)
                transicaoLeste[i] = new double[6];

            transicaoLeste[0][1] = 1;
            transicaoLeste[1][2] = 0.5;
            transicaoLeste[1][0] = 0.5;
            transicaoLeste[2][2] = 0; //absorvedor
            transicaoLeste[3][4] = 1;
            transicaoLeste[4][5] = 1;
            transicaoLeste[5][5] = 1;

            double[][] transicaoOeste = new double[6][];

            for (int i = 0; i < 6; i++)
                transicaoOeste[i] = new double[6];

            transicaoOeste[0][0] = 1;
            transicaoOeste[1][0] = 1;
            transicaoOeste[2][2] = 0; //absorvedor
            transicaoOeste[3][3] = 1;
            transicaoOeste[4][3] = 1;
            transicaoOeste[5][4] = 1;

            double[][] transicaoSul = new double[6][];

            for (int i = 0; i < 6; i++)
                transicaoSul[i] = new double[6];

            transicaoSul[0][0] = 1;
            transicaoSul[1][1] = 0.5;
            transicaoSul[1][0] = 0.5;
            transicaoSul[2][2] = 0; //absorvedor
            transicaoSul[3][0] = 1;
            transicaoSul[4][1] = 1;
            transicaoSul[5][2] = 1;
                       
            return new List<double[][]>
            {
                transicaoNorte, transicaoSul, transicaoLeste, transicaoOeste
            };
        }
    }
}
