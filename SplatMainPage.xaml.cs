using System.Collections.ObjectModel;
namespace PouicGame;

public partial class SplatMainPage : ContentPage
    // Page de choix du niveau de difficulté et du pseudo pour le quiz Splatoon
{
    public ObservableCollection<string> aspirants = new ObservableCollection<string>();
    public ObservableCollection<string> aguerris = new ObservableCollection<string>();
    public int count = 0;
    public GlobalDataList globalDL;
    public SplatMainPage(GlobalDataList globalDL)
    {
        InitializeComponent();
        aspirants.Add("Meilleurs Classiques");
        aguerris.Add("Meilleurs RangX");
        AspirantsView.ItemsSource = aspirants;
        AguerrisView.ItemsSource = aguerris;
        this.globalDL = globalDL;
        
    }

    public void UpdateScores(ObservableCollection<string> asp, ObservableCollection<string> agu)
    {
        aspirants = asp;
        aguerris = agu;
        AspirantsView.ItemsSource = aspirants;
        AguerrisView.ItemsSource = aguerris;
    }


    public async void Retour(object sender, EventArgs e) { await Navigation.PopModalAsync(); }

    private async void OnLauncherClicked(object sender, EventArgs e)
    {
        Button[] btns = { NormalBtn, HardBtn };
        Button btn = sender as Button;
        string niveau;
        if (btn == btns[0]) { niveau = "Aspirant"; }
        else { niveau = "Aguerri"; }
        string pseudo = await DisplayPromptAsync("Avant de commencer", "Quel est votre pseudo (1-10 caractères) ?");
        if (pseudo != null)
        {
            while (pseudo == null || 1 > pseudo.Length || pseudo.Length > 10)
            {

                if (pseudo == null) { break; }
                await Task.Delay(200);
                await DisplayAlert("Attention", "Votre pseudo doit contenir entre 1 et 10 caractères !", "OK");
                await Task.Delay(200);
                pseudo = await DisplayPromptAsync("Avant de commencer", "Quel est votre pseudo (1-10 caractères) ?");
            }
            if (pseudo != null)
            {
                if (count == 0)
                {
                    SplatDataList dataList = new SplatDataList(pseudo, niveau);
                    dataList.aspirants = aspirants;
                    dataList.aguerris = aguerris;
                    globalDL.splatdatas = dataList;
                    await Navigation.PushModalAsync(new SplatQuizPage(globalDL));
                }
                else
                {
                    while (pseudo == null || globalDL.splatdatas.pseudos.Contains(pseudo) || (1 > pseudo.Length || pseudo.Length > 10))
                    {

                        if (pseudo == null) { break; }
                        await Task.Delay(200);
                        if (globalDL.splatdatas.pseudos.Contains(pseudo))
                        {
                            await DisplayAlert("Pseudo invalide", $"Le pseudo {pseudo} a déjà été utilisé.", "Nouveau pseudo");
                        }
                        else if ((1 > pseudo.Length || pseudo.Length > 10) || pseudo == null)
                        {
                            await DisplayAlert("Attention", "Votre pseudo doit contenir entre 1 et 10 caractères !", "OK");
                        }
                        await Task.Delay(200);
                        pseudo = await DisplayPromptAsync("Avant de commencer", "Quel est votre pseudo (1-10 caractères) ?");
                    }
                    if (pseudo != null)
                    {
                        globalDL.splatdatas.AjoutePseudo(pseudo);
                        globalDL.splatdatas.niveau = niveau;
                        await Navigation.PushModalAsync(new SplatQuizPage(globalDL));
                    }
                }
            }
        }
    }
}

