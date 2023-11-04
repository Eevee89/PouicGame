namespace PouicGame;

public partial class PokeGames : ContentPage
	// Page de choix du jeu Pokémon (quiz ou pendu)
{

	GlobalDataList globalDL;

	public PokeGames(GlobalDataList globalDL)
	{
		InitializeComponent();
		this.globalDL = globalDL;
	}

    public async void LaunchPokeHang(object sender, EventArgs e) { await Navigation.PushModalAsync(new PokeHangMainPage(globalDL)); }

    public async void LaunchPokeShadow(object sender, EventArgs e) { await Navigation.PushModalAsync(new PokeShadowPage(globalDL, true, true, true, 0)); }

    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }
}