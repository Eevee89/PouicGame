namespace PouicGame;

public partial class HangLast : ContentPage
    // Page de fin d'un pendu (Monster Hunter ou Pokémon)
{
    GlobalDataList globalDL;

    public HangLast(GlobalDataList globalDL, string jeu)
	{
        InitializeComponent();
        if (jeu == "Monster Hunter")
        { 
            globalDL.countMHHang++;
        }
        else
        {
            globalDL.countPokeHang++;
        }
        this.globalDL = globalDL;
    }

	private async void ReturnToMain(object sender, EventArgs e)
	{
		MainPage main = new MainPage();
        main.SaveDatas(globalDL);
        main.globalDL = main.LoadDatas();
        await Navigation.PushModalAsync(main);
    }
}

