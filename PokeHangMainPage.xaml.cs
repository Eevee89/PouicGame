namespace PouicGame;

public partial class PokeHangMainPage : ContentPage
	// Page du choix du mode du pendu Pokémon (à 1 ou 2 joueurs)
{
	GlobalDataList globalDL;
	public PokeHangMainPage(GlobalDataList globalDL, bool dis = true)
	{
		InitializeComponent();
		this.globalDL = globalDL;
		ReturnBtn.IsEnabled = dis;
	}

    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }

    public async void Launch(object sender, EventArgs e)
	{
		if (sender as Button == Solo) { await Navigation.PushModalAsync(new PokeHangPage(globalDL, "Solo")); }
		else { await Navigation.PushModalAsync(new PokeHangPage(globalDL, "Multi")); }
	}
}