namespace PouicGame;

public partial class MHHangMainPage : ContentPage
	// Page pour spécifier si on joue seul ou à 2
{
	GlobalDataList globalDL;

	public MHHangMainPage(GlobalDataList globalDL, bool dis = true)
	{
		InitializeComponent();
		this.globalDL = globalDL;
		ReturnBtn.IsEnabled = dis;
	}

    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }

    public async void Launch(object sender, EventArgs e)
	{
		if (sender as Button == Solo) { await Navigation.PushModalAsync(new MHHangPage(globalDL, "Solo")); }
		else { await Navigation.PushModalAsync(new MHHangPage(globalDL, "Multi")); }
	}
}