namespace PouicGame;

public partial class MarioLastPage : ContentPage
    // Page de retour au menu principale pour depuis le quiz Mario
{
	string score;
	GlobalDataList globalDL;

	public MarioLastPage(GlobalDataList globalDL)
	{
        InitializeComponent();
		score = globalDL.mariodatas.pseudo;
		score += "\n";
		if (globalDL.mariodatas.price == 0)
        { 
            score += " 0z";
        }
		else
        {
            score += globalDL.mariodatas.prix[15 - globalDL.mariodatas.price].Split(" | ")[1];
        }
		if (globalDL.mariodatas.niveau=="Aspirant")
        {
            globalDL.mariodatas.UpdateHallofFame(score, globalDL.mariodatas.aspirants);
        }
        else
        {
            globalDL.mariodatas.UpdateHallofFame(score, globalDL.mariodatas.aguerris);
        }
		this.globalDL = globalDL;
		this.globalDL.countMario++;
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
            MarioMainPage spmain = new MarioMainPage(globalDL);
            globalDL.mariodatas.Reset();
            spmain.UpdateScores(globalDL.mariodatas.aspirants, globalDL.mariodatas.aguerris);
            spmain.count++;
            await Navigation.PushModalAsync(spmain);
        }
    }
}

