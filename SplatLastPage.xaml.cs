namespace PouicGame;

public partial class SplatLastPage : ContentPage
    // Page de fin du quiz Splatoon
{
	string score;
	GlobalDataList globalDL;
	public SplatLastPage(GlobalDataList globalDL)
	{
        InitializeComponent();
		score = globalDL.splatdatas.pseudo;
		score += "\n";
		if (globalDL.splatdatas.price == 0) { score += " 0z"; }
		else { score += globalDL.splatdatas.prix[15 - globalDL.splatdatas.price].Split(" | ")[1]; }
		if (globalDL.splatdatas.niveau=="Aspirant") { globalDL.splatdatas.UpdateHallofFame(score, globalDL.splatdatas.aspirants); }
        else { globalDL.splatdatas.UpdateHallofFame(score, globalDL.splatdatas.aguerris); }
		this.globalDL = globalDL;
		this.globalDL.countSplat++;
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
            SplatMainPage spmain = new SplatMainPage(globalDL);
            globalDL.splatdatas.Reset();
            spmain.UpdateScores(globalDL.splatdatas.aspirants, globalDL.splatdatas.aguerris);
            spmain.count++;
            await Navigation.PushModalAsync(spmain);
        }
    }
}

