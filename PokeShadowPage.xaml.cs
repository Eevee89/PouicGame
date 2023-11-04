using System.Collections.ObjectModel;
using System.Text;

namespace PouicGame;

public partial class PokeShadowPage : ContentPage
    // Page du "Quel est ce Pokémon ?"
{
	GlobalDataList globalDL;
    ObservableCollection<string> prix = new();
    Color vert = Color.FromArgb("#0F0");
    Color rouge = Color.FromArgb("#F00");
    Button[] buttons = new Button[4];
    int answer;
    string nom = "";
    int count;
    string rootpath;

    public PokeShadowPage(GlobalDataList globalDL, bool btn1, bool btn2, bool btn3, int c)
	{
		InitializeComponent();
        rootpath = AppContext.BaseDirectory.Split("PouicGame\\bin")[0];
        this.globalDL = globalDL;
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
        PriceScale.ItemsSource = prix;
        count = c;
        if (count < 16)
        {
            CinqBtn.IsEnabled = btn1;
            AppelBtn.IsEnabled = btn2;
            PublicBtn.IsEnabled = btn3;
            Btn();
            ManageArrows();
            string[] strings = new string[4];

            string[] lines = File.ReadAllLines(rootpath+"PouicGameDatas\\pendu_pokemon.txt", Encoding.UTF8);
            string[] valides = File.ReadAllLines(rootpath+"PouicGameDatas\\pokeshadow.txt", Encoding.UTF8);
            Random random = new Random();
            nom = valides[random.Next(valides.Length)];
            PokeImg.Source = ImageSource.FromFile(rootpath+$"PouicGameDatas\\Ombres\\O{nom}.jpg");
            strings[0] = nom[0].ToString().ToUpper() + nom[1..];
            for (int i = 1; i < 4; i++)
            {
                string fake = lines[random.Next(lines.Length)];
                while (strings.Contains(fake)) { fake = lines[random.Next(lines.Length)]; }
                strings[i] = fake;
            }
            strings = strings.OrderBy(e => random.Next()).ToArray();
            for (int i = 0; i < strings.Length; i++) { buttons[i].Text = strings[i]; if (strings[i].ToLower() == nom) { answer = i; } }
        }
    }

    private void Btn()
    {
        buttons[0] = Answer1;
        buttons[1] = Answer2;
        buttons[2] = Answer3;
        buttons[3] = Answer4;
    }
    private void ManageArrows()
    {
        ImageButton[] arrows = { Cursor1Btn, Cursor2Btn, Cursor3Btn, Cursor4Btn, Cursor5Btn, Cursor6Btn, Cursor7Btn, Cursor8Btn,
            Cursor9Btn, Cursor10Btn, Cursor11Btn, Cursor12Btn, Cursor13Btn, Cursor14Btn, Cursor15Btn };
        arrows = arrows.Reverse().ToArray();
        if (count == 1) { Cursor15Btn.IsEnabled = true; }
        else if (count > 1)
        {
            arrows[count - 1].IsEnabled = true;
            arrows[count - 2].IsEnabled = false;
        }
    }

    private async void TryAns(object sender, EventArgs e)
    {
        Button button = sender as Button;
        for (int i = 0; i < 4; i++)
        {
            if (i == answer) { buttons[i].BackgroundColor = vert; }
            else { buttons[i].BackgroundColor = rouge; }
        }

        if (count == 15)
        {
            await Task.Delay(1000);
            await Navigation.PushModalAsync(new ShadowLastPage(globalDL));
        }
        else
        {
            if (buttons[answer] == button)
            {
                AnsLabel.Text = "Bonne Réponse";
                AnsLabel.TextColor = vert;
                await Task.Delay(500);
                PokeImg.Source = ImageSource.FromFile(rootpath+$"PouicGameDatas\\Pokemon\\{nom}.jpg");
                await Task.Delay(1000);
                await Navigation.PushModalAsync(new PokeShadowPage(globalDL, CinqBtn.IsEnabled, AppelBtn.IsEnabled, PublicBtn.IsEnabled, count+1));
            }

            else
            {
                AnsLabel.Text = "Mauvaise Réponse";
                AnsLabel.TextColor = rouge;
                await Task.Delay(1000);
                await Navigation.PushModalAsync(new ShadowLastPage(globalDL));
            }
        }
    }

    private void JokerCinquante(object sender, EventArgs e)
    {
        Random rand = new Random();
        int dis = rand.Next(4);
        while (dis == answer) { dis = rand.Next(4); }
        buttons[dis].IsEnabled = false;
        int dis2 = rand.Next(4);
        while (dis2 == answer || dis2 == dis) { dis2 = rand.Next(4); }
        buttons[dis2].IsEnabled = false;
        CinqBtn.IsEnabled = false;
        globalDL.splatdatas.enabledbtn[0] = false;
    }

    private async void JokerAppel(object sender, EventArgs e)
    {
        Random random = new Random();
        int prob = random.Next(101);
        int word = random.Next(3);
        if (prob <= 50)
        {
            if (word == 1) { await DisplayAlert("Résultat de l'appel", "Votre ami.e pense à la réponse " + (answer + 1), "OK"); }
            else { await DisplayAlert("Résultat de l'appel", "Votre ami.e vous conseille la réponse " + (answer + 1), "OK"); }
        }
        else
        {
            int dis = random.Next(4);
            while (dis == answer) { dis = random.Next(4); }
            if (word == 1) { await DisplayAlert("Résultat de l'appel", "Votre ami.e vous conseille à la réponse " + (dis + 1), "OK"); }
            else { await DisplayAlert("Résultat de l'appel", "Votre ami.e pense à la réponse " + (dis + 1), "OK"); }
        }
        AppelBtn.IsEnabled = false;
        globalDL.splatdatas.enabledbtn[1] = false;
    }

    private async void JokerPublic(object sender, EventArgs e)
    {
        Random random = new Random();
        int probAns = random.Next(25, 101);
        int prob1 = random.Next(101 - probAns);
        int prob2 = random.Next(101 - probAns - prob1);
        int prob3 = 100 - probAns - prob1 - prob2;
        int[] probs = new int[4];
        probs[answer] = probAns;
        int[] p = { prob1, prob2, prob3 };
        int j = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            if (i != answer)
            {
                probs[i] = p[j];
                j++;
            }
        }
        await DisplayAlert("Avis du public :", $"Rep 1 : {probs[0]}% ; Rep 2 : {probs[1]}% ; Rep 3 : {probs[2]}% ; Rep 4 : {probs[3]}%", "OK");
        PublicBtn.IsEnabled = false;
        globalDL.splatdatas.enabledbtn[2] = false;
    }


    /*

    public async void Propose(object sender, EventArgs e)
	{
		string proposition = Corrige(TryAnswer.Text);
		
		if (proposition != null)
		{
			double leven = Levenshtein(proposition, rep);

            if (leven <= 2)
			{
				Shadow.Source = ImageSource.FromFile($"C:\\Users\\joris\\Documents\\PouicGameDatas\\Pokemon\\{nom}.jpg");
				InfoLabel.TextColor = Color.FromArgb("#0F0");
				InfoLabel.Text = rep;
				await Task.Delay(500);
				await DisplayAlert("Bravo", "Vous avez trouvé le pokémon !", "Accueil");
				await Task.Delay(100);
				await Navigation.PushModalAsync(new ShadowLastPage(globalDL));
			}

			else
			{
				if (3 <= leven && leven <= 6)
				{
                    InfoLabel.TextColor = Color.FromArgb("#FC9303");
                    InfoLabel.Text = "Presque !";
                }
				count++;
				proposes.Add(proposition);
                Information.ItemsSource = proposes;
				await Task.Delay(500);
				InfoLabel.Text = "";
				if (count == 15)
				{
                    Shadow.Source = ImageSource.FromFile($"C:\\Users\\joris\\Documents\\PouicGameDatas\\Pokemon\\{nom}.jpg");
                    await Task.Delay(500);
                    await DisplayAlert("Dommage", $"Le pokémon à trouver était {nom}", "Accueil");
                    await Task.Delay(100);
                    await Navigation.PushModalAsync(new ShadowLastPage(globalDL));
                }
            }
		}
		
    }

	private async void GetSource()
    {
		//string[] lines = File.ReadAllLines($"C:\\Users\\joris\\Documents\\PouicGameDatas\\pendu_pokemon.txt");
		string[] valides = {"abo", "abra", "absol", "arkéoptéryx", "aflamanoir", "amonita", "anorith", "arbok", "archéomire", "arkéapti", "armulys", 
							"avaltout", "babimanta", "balignon", "barloche", "batracné", "bulbizarre", "cacnea", "camérupt", "capidextre", "carapuce", 
							"carmache", "castorno", "ceribou", "chamallot",	"charmillon", "chenipan", "chenipotte", "chrysacier", "chétiflor", "cochignon",
							"coconfort", "colhomard", "colossinge", "givrali", "crustabri", "hexagel", "grindur", "maraiste", "obali", "posipi", "tyranocif",
							"spinda", "nucléos", "coupenote", "dedenne", "fantominus", "flingouste", "lixy", "airmure", "akwakwak", "alakazam", "aligatueur", "altaria",
							"amagara", "amonistar", "amphinobi", "anchwatt", "apireine", "apitrini", "aquali", "arakdo", "arcanin", "arceus", "archéodon", "arcko", 
							"armaldo", "artikodin", "aspicot", "axoloto", "azumarill", "azurill", "aéromite", "baggaïd", "baggiguane", "balbuto", "banshitrouye", 
							"barbicha", "bargantuab", "bargantua", "barpau", "bastiodon", "baudrive", "bekipan", "blindalys", "blindépique", "blizzaroi", "blizzi", 
							"boguérisse", "boréas", "boskara", "bouldeneu", "boustiflor", "braisillon", "branette", "braségali", "brocélôme", "brouhabam", "brutalibré", 
							"brutapode", "bruyverne", "bétochef", "corboss", "coxy", "crabaraque", "crabicoque", "cradopaud", "dinoclier", "drascore", "floravol", 
							"fragilady", "gigalithe", "gloupti", "griknot", "hariyama", "keunotor", "kranidos", "larveyette", "libégon", "lugulabre", "luminéon", 
							"mackogneur", "magirêve", "maracachi", "marcacrin", "marill", "mucuscule", "mustéflott", "méditikka", "mégapagos", "noarfan", "noctali",
							"octillery", "ouisticram", "ouvrifier", "pitrouille", "polichombr", "porygon", "porygon2", "pyrax", "qwilfish", "ramboum",
							"rhinastoc", "scobolide", "solaroc", "symbios", "tourtipouss", "tritonde", "tutankafer", "vacilys", "zoroark", "écrapince",
							"évoli"};
        Random random = new Random();
        nom = valides[random.Next(valides.Length)];
        Shadow.Source = ImageSource.FromFile($"C:\\Users\\joris\\Documents\\PouicGameDatas\\Ombres\\O{nom}.jpg");
        rep = Corrige(nom);
    }


	private string Corrige(string text)
	{
		text = text.ToUpper();
		text = text.Replace('É', 'E').Replace('È', 'E').Replace("Ê", "E").Replace("Â", "A").Replace("À", "A").Replace("Œ", "OE");
        text = text.Replace("Î", "I").Replace("Ï", "I").Replace("Ô", "O").Replace("Ë", "E").Replace("Ü", "U").Replace("Ç", "C");
        return text.ToLower();
	}

    private double Levenshtein(string source1, string source2)
    { // Compare 2 chaînes de caractères.
        int len1 = source1.Length;
        int len2 = source2.Length;

        int[,] matrix = new int[len1 + 1, len2 + 1];

        if (len1 == 0) { return len2; }
        if (len2 == 0) { return len1; }

        for (int i = 0; i <= len1; i++) { matrix[i, 0] = i; }
        for (int j = 0; j <= len2; j++) { matrix[0, j] = j; }

        for (int i = 1; i <= len1; i++)
        {
            for (int j = 1; j <= len2; j++)
            {
                int cost;
                if (source2[j - 1] == source1[i - 1]) { cost = 0; }
                else { cost = 1; }
                matrix[i, j] = Math.Min(
                    Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost);
            }
        }

        return 1.0 * matrix[len1, len2];
    }

    private double IsNearTo(string str1, string str2)
    { // Renvoie le pourcentage de ressemblance de 2 strings.
        return Math.Round((1 - Levenshtein(str1.ToLower(), str2.ToLower()) / Math.Max(str1.Length, str2.Length)) * 100, 1);
    }
    */
}