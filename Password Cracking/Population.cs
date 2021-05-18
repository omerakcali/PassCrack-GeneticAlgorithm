using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Password_Cracking
{
    class Population
    {
        public int size;
        public Chromosome[] population;
        public int totalFitness;
        public Population(int size)
            {
            this.size=size;
            population = new Chromosome[size];
            for (int i = 0; i < size; i++) population[i] = new Chromosome();
            }

        public int CalculateTotalFitness(string passcode)
        {
            totalFitness = 0;
            foreach (Chromosome i in population) totalFitness += i.CalculateFitness(passcode);
            return totalFitness;
        }

        public Chromosome[] SortChromosomes(int len)
        {
            Chromosome[] sortedpopulation = population.OrderByDescending(x => x.fitness).ToArray();
            
            Chromosome[] parents = new Chromosome[len];
            Array.Copy(sortedpopulation, parents, len);
            return parents;
        }


}
    }

