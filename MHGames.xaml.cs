namespace PouicGame;

public partial class MHGames : ContentPage
    // Page pour choisir le quiz ou le pendu Monster Hunter
{

    GlobalDataList globalDL;

    public MHGames(GlobalDataList globalDL)
    {
        InitializeComponent();
        this.globalDL = globalDL;
    }

    public async void LaunchMHQuiz(object sender, EventArgs e)
    {
        if (globalDL.countMH == 0) { await Navigation.PushModalAsync(new MHMainPage(globalDL)); }
        else
        {
            MHMainPage main = new MHMainPage(globalDL);
            globalDL.mhdatas.Reset();
            main.UpdateScores(globalDL.mhdatas.aspirants, globalDL.mhdatas.aguerris);
            main.count++;
            await Navigation.PushModalAsync(main);
        }
    }

    public async void LaunchMHHang(object sender, EventArgs e) { await Navigation.PushModalAsync(new MHHangMainPage(globalDL)); }

    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }
}