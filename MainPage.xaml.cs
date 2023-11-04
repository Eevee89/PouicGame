using Newtonsoft.Json;

namespace PouicGame;

public partial class MainPage : ContentPage
{
    public GlobalDataList globalDL;
    string rootpath = AppContext.BaseDirectory.Split("PouicGame\\bin")[0];

    public MainPage()
    {
		InitializeComponent();
        globalDL = LoadDatas();
    }

    public GlobalDataList LoadDatas()
        // Récupérer les données sauvegardées
    {
        string fileName = rootpath + "PouicGameDatas\\pouicgamedatas.json";
        string jsonString = File.ReadAllText(fileName);
        GlobalDataList gdl = JsonConvert.DeserializeObject<GlobalDataList>(jsonString);

        return gdl;
    }

    public void SaveDatas(GlobalDataList gdl)
        // Sauvegarder les données en cas de crash du jeu
    {
        string fileName = rootpath + "PouicGameDatas\\pouicgamedatas.json";
        string jsonString = JsonConvert.SerializeObject(gdl);
        File.WriteAllText(fileName, jsonString);
    }

    public void DeleteDatas()
        // Permet d'effacer les données si souhaité
    {
        GlobalDataList globalDataList = new GlobalDataList();
        globalDataList.mhdatas = new MHDataList("Pseudo", "Niveau");
        globalDataList.splatdatas = new SplatDataList("Pseudo", "Niveau");
        globalDataList.mariodatas = new MarioDataList("Pseudo", "Niveau");
        string fileName = rootpath + "PouicGameDatas\\pouicgamedatas.json";
        string jsonString = JsonConvert.SerializeObject(globalDataList);
        File.WriteAllText(fileName, jsonString);
    }

    public async void LaunchMHGames(object sender, EventArgs e) 
    { 
        await Navigation.PushModalAsync(new MHGames(globalDL));
    }

    public async void LaunchPokeGames(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PokeGames(globalDL));
    }

    public async void LaunchSplatGames(object sender, EventArgs e)
    { 
        await Navigation.PushModalAsync(new SplatGames(globalDL));
    }

    public async void LaunchMarioGames(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MarioGames(globalDL));
    }

    public async void Statistiques(object sender, EventArgs e)
        // Affiche le nombre de fois qu'un mini-jeu a été lancé
    {
        /*DeleteDatas();
        await Task.Delay(1000);
        LoadDatas();*/
        await DisplayAlert("Statistiques", $"Quiz MH : {globalDL.countMH}\nQuel est ce Pokémon : {globalDL.countShadow}\nQui veut gagner des super-coquillages ? : {globalDL.countSplat}\nQui veut gagner des champignons ? : {globalDL.countMario}\nPendu MH : {globalDL.countMHHang}\nPendu Pokemon : {globalDL.countPokeHang}", "Ok");
    }
}

