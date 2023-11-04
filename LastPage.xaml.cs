namespace PouicGame;

public partial class LastPage : ContentPage
	// Page de fin du quiz Monster Hunter
{
	string score;
	GlobalDataList globalDL;

	public LastPage(GlobalDataList globalDL)
	{
        InitializeComponent();
		score = globalDL.mhdatas.pseudo;
		score += "\n";
		if (globalDL.mhdatas.price == 0)
		{ 
			score += " 0z";
		}
		else
		{
			score += globalDL.mhdatas.prix[15 - globalDL.mhdatas.price].Split(" | ")[1];
		}
		if (globalDL.mhdatas.niveau=="Aspirant")
		{ 
			globalDL.mhdatas.UpdateHallofFame(score, globalDL.mhdatas.aspirants);
		}
        else
		{ 
			globalDL.mhdatas.UpdateHallofFame(score, globalDL.mhdatas.aguerris);
		}
		this.globalDL = globalDL;
		this.globalDL.countMH++;
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
            MHMainPage mhmain = new MHMainPage(globalDL);
            globalDL.mhdatas.Reset();
            mhmain.UpdateScores(globalDL.mhdatas.aspirants, globalDL.mhdatas.aguerris);
            mhmain.count++;
            await Navigation.PushModalAsync(mhmain);
        }
    }
}

