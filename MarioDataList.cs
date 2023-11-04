using System.Collections.ObjectModel;

namespace PouicGame
{
    public class MarioDataList
        // Classe qui stocke les données du quiz Mario
    {
        public string pseudo;
        public string[] pseudos = { };
        public ObservableCollection<string> prix = new ObservableCollection<string>();
        public ObservableCollection<string> aguerris = new ObservableCollection<string>();
        public ObservableCollection<string> aspirants = new ObservableCollection<string>();
        public int price;
        public bool[] enabledbtn = { true, true, true };
        public string niveau = "";

        public MarioDataList(string ps, string lvl)
        {
            pseudo = ps;
            niveau = lvl;
            AjoutePseudo(ps);

            prix.Add("  15 | 1000000z");
            prix.Add("  14 | 300000z");
            prix.Add("  13 | 150000z");
            prix.Add("  12 | 72000z");
            prix.Add("  11 | 36000z");
            prix.Add("  10 | 24000z");
            prix.Add("   9 | 12000z");
            prix.Add("   8 | 8000z");
            prix.Add("   7 | 4000z");
            prix.Add("   6 | 2000z");
            prix.Add("   5 | 1000z");
            prix.Add("   4 | 500z");
            prix.Add("   3 | 300z");
            prix.Add("   2 | 200z");
            prix.Add("   1 | 100z");
        }

        public void AjoutePseudo(string ps)
        {
            pseudo = ps;
            List<string> n = pseudos.ToList();
            n.Add(ps);
            pseudos = n.ToArray();
        }

        public void Reset()
        {
            for(int i = 0; i<2; i++)
            { 
                enabledbtn[i] = true;
            }
            price = 0;
        }

        private int ToNumber(string s)
        {
            string num = s.Split("\n")[1].Split("z")[0];
            return int.Parse(num);
        }

        
        private bool IsInferior(string s1, string s2)
        {
            int n1 = ToNumber(s1);
            int n2 = ToNumber(s2);
            return n1 <= n2;
        }

        private void SortCollection(ObservableCollection<string> collection)
        {
            int len = collection.Count;
            string temp;
            int j;
            for (int i = 2; i < len; i++)
            {
                temp = collection[i];
                j = i;
                while (j > 1 && IsInferior(collection[j-1], temp))
                {
                    collection[j] = collection[j - 1];
                    j --;
                }
                collection[j] = temp;
            }
        }

        public void UpdateHallofFame(string score, ObservableCollection<string> collection)
            // Ajoute le pseudo du joueur sur le mur des victoires s'il fait parti des 10 meilleurs
        {
            if (collection.Count == 1)
            { 
                collection.Add(score);
            }
            else if (1 < collection.Count && collection.Count < 11)
            {
                collection.Add(score);
                SortCollection(collection);
            }
            else
            {
                int last = int.Parse(collection[10].Split(" : ")[1].Split("z")[0]);
                if (last < int.Parse(score.Split(" : ")[1].Split("z")[0]))
                {
                    collection[10] = score;
                    SortCollection(collection);
                }
            }
        }
    }
}