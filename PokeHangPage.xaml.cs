using System.Collections.ObjectModel;
using System.Text;

namespace PouicGame;

public partial class PokeHangPage : ContentPage
	// Page qui gère le pendu Pokémon
{
	string type = "";
	ObservableCollection<string> proposes = new ObservableCollection<string>();
	string mot = "";
	string img = "";
	string[] rep = { };
	int erreurs;
	int count;
	GlobalDataList globalDL;
	string rootpath;


    public PokeHangPage(GlobalDataList globalDL, string type)
	{
		InitializeComponent();
		this.type = type;
		GetWord();
		this.globalDL = globalDL;
        rootpath = AppContext.BaseDirectory.Split("PouicGame\\bin")[0];
    }

	public async void Propose(object sender, EventArgs e)
	{
        string userInput = ((Entry)sender).Text;
        string lettre = userInput.ToUpper().Replace('É', 'E').Replace('È', 'E').Replace("Ê", "E").Replace("Â", "A").Replace("À", "A");
		lettre = lettre.Replace("Î", "I").Replace("Ï", "I").Replace("Ô", "O").Replace("Ë", "E").Replace("Ü", "U").Replace("Ç", "C");

        if (lettre == mot)
        {
            Information.TextColor = Color.FromArgb("#0F0");
            Information.Text = "Réponse correcte !";
            count++;
            string rep2 = "";
            foreach (char i in mot)
            {
                rep2 += i;
                rep2 += " ";
            }
            Affiche.TextColor = Color.FromArgb("#0F0");
            Affiche.Text = rep2;
            try
            {
                AnswerImg.Source = ImageSource.FromFile(rootpath+"PouicGameDatas\\Pokemon\\" + img + ".jpg");
            }
            catch { }
            await Task.Delay(1000);
            bool action = await DisplayAlert("Bravo", $"Tu as trouvé le nom du pokémon avec {erreurs} erreurs", "Accueil", "Rejouer");
            if (action)
            {
                await Task.Delay(500);
                await Navigation.PushModalAsync(new HangLast(globalDL, "Pokemon"));
            }
            else
            {
                await Task.Delay(500);
                globalDL.countPokeHang++;
				MainPage main = new();
				main.SaveDatas(globalDL);
                await Navigation.PushModalAsync(new PokeHangMainPage(globalDL, false));
            }
        }

        else if (lettre is null || lettre.Length > 1)
		{
			Information.TextColor = Color.FromArgb("#000");
			Information.Text = "Veuillez ne rentrer qu'une lettre à la fois.";
		}
		else
		{
			if (!proposes.Contains(lettre))
			{
				if (mot.Contains(lettre))
				{
					string affiche = "";
					Information.TextColor = Color.FromArgb("#0F0");
					Information.Text = "Lettre correcte";
					count++;
					for (int i = 0; i < mot.Length; i++)
					{
						if (mot[i].ToString() == lettre)
						{
							rep[i] = lettre;
							affiche += rep[i];
						}
						else
						{
							if (rep[i] == "_") { affiche += "_"; }
							else { affiche += rep[i]; }
						}
						affiche += " ";
					}
					Affiche.Text = affiche.Substring(0, affiche.Length - 1);
				}
				else
				{
					Information.TextColor = Color.FromArgb("#F00");
					Information.Text = "Lettre incorrecte";
                    erreurs++;
					count++;
                    proposes.Add(lettre);
				}
			}
			else
			{
				Information.TextColor = Color.FromArgb("#000");
				Information.Text = "Lettre déjà jouée";
            }
		}
		await Task.Delay(500);
        TryAnswer.Text = string.Empty;
        Information.TextColor = Color.FromArgb("#000");
        Information.Text = "Lettres incorrectes : ";
		string r = "";
		foreach(string i in proposes) { r += i + " "; }
		Information.Text += r;
		if (CheckText())
		{
			Affiche.TextColor = Color.FromArgb("#0F0");
			try
			{
				AnswerImg.Source = ImageSource.FromFile(rootpath+"PouicGameDatas\\Pokemon\\" + img + ".jpg");
			}
			catch { }
            await Task.Delay(1000);
            bool action = await DisplayAlert("Bravo", $"Tu as trouvé le nom du pokémon avec {erreurs} erreurs", "Accueil", "Rejouer");
            if (action)
            {
                await Task.Delay(500);
                await Navigation.PushModalAsync(new HangLast(globalDL, "Pokemon"));
            }
            else
            {
                await Task.Delay(500);
                globalDL.countPokeHang++;
                MainPage main = new();
                main.SaveDatas(globalDL);
                await Navigation.PushModalAsync(new PokeHangMainPage(globalDL, false));
            }
        }
		if (count == 26 || erreurs==12)
		{
            Affiche.TextColor = Color.FromArgb("#F00");
            try
            {
                AnswerImg.Source = ImageSource.FromFile(rootpath + "PouicGameDatas\\Pokemon\\" + img + ".jpg");
            }
            catch { }
            await Task.Delay(1000);
            bool action = await DisplayAlert("Dommage", $"Le nom du pokémon était : {mot}", "Accueil", "Rejouer");
			if (action)
			{
				await Task.Delay(500);
				await Navigation.PushModalAsync(new HangLast(globalDL, "Pokemon"));
			}
			else 
			{
                await Task.Delay(500);
                globalDL.countPokeHang++;
                MainPage main = new();
                main.SaveDatas(globalDL);
                await Navigation.PushModalAsync(new PokeHangMainPage(globalDL, false));
            }
        }
    }

	private async void GetWord()
    {
        if (type == "Solo")
		{
            //string[] lines = File.ReadAllLines("C:\\Users\\joris\\Documents\\PouicGame\\PouicGameDatas\\pendu_pokemon.txt", Encoding.UTF8);
            string[] lines = File.ReadAllLines(rootpath+"PouicGameDatas\\pendu_pokemon.txt", Encoding.UTF8);
            Random random = new Random();
			img = lines[random.Next(lines.Length)].ToLower();
            mot = img.ToUpper().Replace('É', 'E').Replace('È', 'E').Replace("Ê", "E").Replace("Â", "A").Replace("À", "A");
			mot = mot.Replace("Î", "I").Replace("Ï", "I").Replace("Ô", "O").Replace("Ë", "E").Replace("Ü", "U").Replace("Ç", "C");
        }

		else
		{
			img = await DisplayPromptAsync("Quel monstre voulez-vous faire deviner ?", "Sans fautes, avec accent et traits d'union.");
			img = img.ToLower();
			mot = img.ToUpper().Replace('É', 'E').Replace('È', 'E').Replace("Ê", "E").Replace("Â", "A").Replace("À", "A");
            mot = mot.Replace("Î", "I").Replace("Ï", "I").Replace("Ô", "O").Replace("Ë", "E").Replace("Ü", "U").Replace("Ç", "C");
        }

        rep = new string[mot.Length];
        string r = "";
        for (int i = 0; i < rep.Length; i++)
        {
            if (mot[i] == ' ' || mot[i] == '-' || mot[i].ToString() == "'") { rep[i] = mot[i].ToString(); r += mot[i].ToString(); }
            else { rep[i] = "_"; r += "_"; }
            r += " ";
        }
        Affiche.Text = r;
    }

	private bool CheckText()
	{
		string m = "";
		for (int i = 0; i < rep.Length; i++) { m += rep[i]; }
		return m == mot;
	}
}