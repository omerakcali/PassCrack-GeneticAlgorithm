using System;
using System.Collections.Generic;
using System.Text;

namespace Password_Cracking
{
    class Chromosome
    {
        public string chromosome;
        public int fitness;
        public Chromosome(string str)
        {
            chromosome = str;
            fitness = 0;
        }
        public Chromosome()
        {
            chromosome = "";
            fitness = 0;
        }

        public int CalculateFitness(string goal)
        {
            fitness = 0;
            for(int i = 0; i < goal.Length; i++)
            {
                if (goal[i] == chromosome[i]) fitness++; ;
            }
            return fitness;
        }

        public void SetChromosome(String str) { chromosome = str; }
    }
}
