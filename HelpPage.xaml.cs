namespace PouicGame;

public partial class HelpPage : ContentPage
    // Page d'aide pour le quiz Monster Hunter
    // Accessible uniquement lors des 10 premières questions en mode Apprenti
{
    string[] lines = { };
    string rootpath;

    public HelpPage()
    {
        InitializeComponent();
        rootpath = AppContext.BaseDirectory.Split("PouicGame\\bin")[0];
    }

    public void InitHelp(int[] prop, int type)
    {
        string etat;
        if (type == 1)
        { 
            etat = "grands";
        }
        else
        { 
            etat = "petits";
        }
        lines = File.ReadAllLines(rootpath+$"PouicGameDatas\\descriptions_{etat}.txt");
        Label[] labels = { Help1, Help2, Help3, Help4 };
        for (int i = 0; i < 4; i++)
        {
            labels[i].Text = lines[prop[i]].Split(" : ")[0] + "\n" + lines[prop[i]].Split(" : ")[1];
        }
    }

    public async void Return2Quiz(object sender, EventArgs e) 
    {
        await Navigation.PopModalAsync();
    }   
}