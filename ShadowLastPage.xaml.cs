namespace PouicGame;

public partial class ShadowLastPage : ContentPage
    // Page de fin du "Quel est ce Pokémon ?"
{
    GlobalDataList globalDL;

    public ShadowLastPage(GlobalDataList globalDL)
	{
        InitializeComponent();
        globalDL.countShadow++;
        this.globalDL = globalDL;
    }

	private async void ReturnToMain(object sender, EventArgs e)
	{
        MainPage main = new MainPage();
        if (sender as Button == Accueil)
        {
            main.SaveDatas(globalDL);
            main.globalDL = main.LoadDatas();
            await Navigation.PushModalAsync(main);
        }
        else
        {
            main.SaveDatas(globalDL);
            PokeShadowPage pokemain = new PokeShadowPage(globalDL, true, true, true, 0);
            await Navigation.PushModalAsync(pokemain);
        }
    }
}

