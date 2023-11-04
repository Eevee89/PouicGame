namespace PouicGame;

public partial class SplatGames : ContentPage
    // Page du choix du jeu Splatoon
{

	GlobalDataList globalDL;

	public SplatGames(GlobalDataList globalDL)
	{
		InitializeComponent();
		this.globalDL = globalDL;
	}

    // public async void LaunchPokeHang(object sender, EventArgs e) { await Navigation.PushModalAsync(new PokeHangMainPage(globalDL)); }

    public async void LaunchSplatQuiz(object sender, EventArgs e)
    {
        if (globalDL.countSplat == 0) { await Navigation.PushModalAsync(new SplatMainPage(globalDL)); }
        else
        {
            SplatMainPage main = new SplatMainPage(globalDL);
            globalDL.splatdatas.Reset();
            main.UpdateScores(globalDL.splatdatas.aspirants, globalDL.splatdatas.aguerris);
            main.count++;
            await Navigation.PushModalAsync(main);
        }
    }

    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }
}