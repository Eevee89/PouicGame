namespace PouicGame;

public partial class MarioGames : ContentPage
    // Page principale pour les mini-jeux Mario
{

	GlobalDataList globalDL;

	public MarioGames(GlobalDataList globalDL)
	{
		InitializeComponent();
		this.globalDL = globalDL;
	}

    public async void LaunchSplatQuiz(object sender, EventArgs e)
    {
        if (globalDL.countMario == 0) { await Navigation.PushModalAsync(new MarioMainPage(globalDL)); }
        else
        {
            MarioMainPage main = new MarioMainPage(globalDL);
            globalDL.mariodatas.Reset();
            main.UpdateScores(globalDL.mariodatas.aspirants, globalDL.mariodatas.aguerris);
            main.count++;
            await Navigation.PushModalAsync(main);
        }
    }

    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }
}