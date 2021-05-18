using System;
using System.Security.Cryptography;
namespace Password_Cracking
{
    class Program
    {
        public static string passcode = @"VeryHardPassword1234567";
        public static char[] chars = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789!@#$%&*()_+-=[]|,./?'\" ".ToCharArray();
        public static int populationSize = 10;
        public static int numParents = 5;
        public static int eliteSize = 2;    
        static void Main(string[] args)
        {
            
            Population pop = new Population(populationSize);
            
            foreach (Chromosome i in pop.population) i.SetChromosome(RandomString(passcode.Length));
            Console.WriteLine("Population size: " + populationSize + " Parents: " + numParents + " Elites: " + eliteSize+"\n");
            Chromosome best =new Chromosome();
            int it = 0;
            while(true)
            {
                pop.CalculateTotalFitness(passcode);
                if (best.fitness == passcode.Length) break;
                Chromosome[] parents = SelectParents(pop);
                
                it++;
                pop = CreateChildren(parents);
                Mutate(pop);
                pop.CalculateTotalFitness(passcode);
                 best = pop.SortChromosomes(1)[0];
                if (it % 500 == 1) Console.WriteLine("Generation: " + it + " " + best.chromosome+" Fitness: "+best.fitness);
            }
            Console.WriteLine("Generation: " + it + " " + best.chromosome);
        }
        static string RandomString(int len)
        {
            string str = "";
            for(int i =0; i < len; i++)
            {
                str += RandomChar();
            }
            return str;
        }
        static char RandomChar()
        {
            Random rand = new Random();
            return chars[rand.Next(0, chars.Length)];
        }
        
        static Chromosome[] SelectParents(Population population)
        {
            Chromosome[] parents = population.SortChromosomes(numParents);
            return parents;
        }

        static Chromosome Breed(Chromosome parent1, Chromosome parent2)
        {
            string childgenes = "";
            Random rand = new Random();
            int geneA = rand.Next(0, passcode.Length);
            int geneB = rand.Next(0, passcode.Length);
            while(geneA==geneB) geneB= rand.Next(0, passcode.Length);
            int start = Math.Min(geneA, geneB);
            int end= Math.Max(geneA, geneB);
            //Console.WriteLine(start + " " + end);
            for(int i =0; i< passcode.Length; i++)
            {
                if (i < start || i > end)
                {
                    childgenes += parent1.chromosome[i];
                }
                else childgenes += parent2.chromosome[i];
            }
            Chromosome child = new Chromosome(childgenes);

            return child;
        }

        static Population CreateChildren(Chromosome[] parents)
        {
            Chromosome[] children = new Chromosome[populationSize];
            for (int i = 0; i < eliteSize;i++) children[i] = parents[i];
            Random rand = new Random();
            int totalfitness = 0;
            for (int i = 0; i < parents.Length; i++) totalfitness += parents[i].fitness;
            for(int i = eliteSize; i < populationSize; i++)
            {
                /*double value =rand.NextDouble();
                Chromosome parent1 = parents[0];
                int pickval = 0;
                for(int j = 0; i < parents.Length; j++)
                {
                    pickval += parents[j].fitness+1;
                    double chance = (double)pickval / (totalfitness + parents.Length);
                    if (chance > value)
                    {
                        parent1 = parents[j];
                        break;
                    }
                    
                }
                value = rand.NextDouble();
                Chromosome parent2 = parents[0];
                pickval = 0;
                for (int j = 0; i < parents.Length; j++)
                {

                    pickval += parents[j].fitness + 1;
                    double chance = (double)pickval / (totalfitness + parents.Length);
                    if (chance > value)
                    {
                        parent2 = parents[j];
                        break;
                    }

                }*/
                int index = rand.Next(0, parents.Length);
                 Chromosome parent1 = parents[index];
                 int newindex = rand.Next(0, parents.Length);
                 Chromosome parent2 = parents[newindex];
                children[i]= Breed(parent1,parent2);
            }
            Population pop = new Population(populationSize);
            pop.population = children;
            
            return pop;
        }
        static void Mutate(Population pop)
        {
            Random rand = new Random();
            for(int i =0;i< pop.population.Length; i++)
            {
                if (rand.NextDouble() > 0.2) continue;
                else
                {
                    int pos = rand.Next(0, passcode.Length);
                    String str = pop.population[i].chromosome;
                    char c = RandomChar();
                        str = str.Substring(0, pos) + c + str.Substring(pos+1 , str.Length - pos-1);
                    pop.population[i].chromosome = str;
                    pop.population[i].CalculateFitness(passcode);

                }
            }
        }
    }
}
