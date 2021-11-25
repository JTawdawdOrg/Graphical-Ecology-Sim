using System;

namespace Behaviours
{
    public class Genes
    {
        private String[] DNA;
        private float mutationRante = 0.01f;

        public string[] Dna
        {
            get => DNA;
            set => DNA = value;
        }

        public String[] recombineDNA(String[] mother, String[] father)
        {
            String[] newDna = new String[mother.Length];
            for (int i = 0; i < mother.Length; i++)
            {
                String possibleilty1 = mother[i].Substring(0, 1) + father[i].Substring(0,1);
                String possibleilty2 = mother[i].Substring(1,1) + father[i].Substring(0,1);
                String possibleilty3 = mother[i].Substring(0,1) + father[i].Substring(1,1);
                String possibleilty4 = mother[i].Substring(1,1) + father[i].Substring(1,1);

                int randomInt = UnityEngine.Random.Range(0, 3);
                switch (randomInt)
                {
                    case 0:
                        newDna[i] = possibleilty1;
                        break;
                    case 1 :
                        newDna[i] = possibleilty2;
                        break;
                    case 2 :
                        newDna[i] = possibleilty3;
                        break;
                    case 3:
                        newDna[i] = possibleilty4;
                        break;
                }
            }
            return newDna;
        }
    }
}