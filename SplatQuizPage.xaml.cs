namespace PouicGame;

public partial class SplatQuizPage : ContentPage
    // Page du quiz Splatoon
{
	Color vert = Color.FromArgb("#0F0");
	Color rouge = Color.FromArgb("#F00");
    Button[] buttons = new Button[4];
    int answer;
    string[] questions = { };
    string[] reponses = { };
    string[][] fakes = new string[16][];
    public GlobalDataList globalDL;
    string rootpath;

    public SplatQuizPage(GlobalDataList globalDL)
    {
        InitializeComponent();
        rootpath = AppContext.BaseDirectory.Split("PouicGame\\bin")[0];
        this.globalDL = globalDL;
        CinqBtn.IsEnabled = globalDL.splatdatas.enabledbtn[0];
        AppelBtn.IsEnabled = globalDL.splatdatas.enabledbtn[1];
        PublicBtn.IsEnabled = globalDL.splatdatas.enabledbtn[2];
        Btn();
        ManageArrows();
        string[] lines;
        if (globalDL.splatdatas.niveau == "Aspirant") { lines = File.ReadAllLines(rootpath+"PouicGameDatas\\splatoon_classique.txt"); }
        else { lines = File.ReadAllLines(rootpath+"PouicGameDatas\\splatoon_rangx.txt"); }

        for (int k=0; k<lines.Length - 2; k+=3)
        {
            List<string> q = questions.ToList();
            q.Add(lines[k]);
            questions = q.ToArray();
            List<string> r = reponses.ToList();
            r.Add(lines[k+1]);
            reponses = r.ToArray();
            fakes[questions.Length-1] = lines[k + 2].Split(",");
        }
        PriceScale.ItemsSource = globalDL.splatdatas.prix;
        if (globalDL.splatdatas.price < questions.Length)
        {
            int i = globalDL.splatdatas.price;
            QuestionLabel.Text = questions[i];
            string[] strings = fakes[i].Append(reponses[i]).ToArray();
            Random r = new Random();
            strings = strings.OrderBy(e => r.Next()).ToArray();
            for (int j = 0; j < strings.Length; j++) 
            {
                if (strings[j].Contains("#"))
                {
                    string texte = $"Réponse {j + 1}\n";
                    foreach (string s in strings[j].Split("#")) { texte += s + "\n"; }
                    buttons[j].Text = texte;
                }
                else { buttons[j].Text = $"Réponse {j + 1}\n" + strings[j]; }
                if (strings[j] == "(ne pas cliquer)") { buttons[j].IsEnabled = false; }
                if (strings[j] == reponses[i]) { answer = j; }
            }
        }
    }

    private void Btn()
    {
        buttons[0] = Answer1;
        buttons[1] = Answer2;
        buttons[2] = Answer3;
        buttons[3] = Answer4;
    }

    private void ManageArrows()
    {
        ImageButton[] arrows = { Cursor1Btn, Cursor2Btn, Cursor3Btn, Cursor4Btn, Cursor5Btn, Cursor6Btn, Cursor7Btn, Cursor8Btn,
            Cursor9Btn, Cursor10Btn, Cursor11Btn, Cursor12Btn, Cursor13Btn, Cursor14Btn, Cursor15Btn };
        arrows = arrows.Reverse().ToArray();
        if (globalDL.splatdatas.price == 1) { Cursor15Btn.IsEnabled = true; }
        else if (globalDL.splatdatas.price > 1)
        {
            arrows[globalDL.splatdatas.price - 1].IsEnabled = true;
            arrows[globalDL.splatdatas.price - 2].IsEnabled = false;
        }
    }

    private async void TryAns(object sender, EventArgs e)
    {
        Button button = sender as Button;
        for (int i = 0; i < 4; i++)
        {
            if (i == answer) { buttons[i].BackgroundColor = vert; }
            else { buttons[i].BackgroundColor = rouge; }
        }

        if (globalDL.splatdatas.price == 15)
        {
            await Task.Delay(1000);
            await Navigation.PushModalAsync(new SplatLastPage(globalDL));
        }
        else
        {
            if (buttons[answer] == button)
            {
                AnsLabel.Text = "Bonne Réponse";
                AnsLabel.TextColor = vert;
                globalDL.splatdatas.price += 1;
                await Task.Delay(1000);
                await Navigation.PushModalAsync(new SplatQuizPage(globalDL));
            }

            else
            {
                AnsLabel.Text = "Mauvaise Réponse";
                AnsLabel.TextColor = rouge;
                await Task.Delay(1000);
                await Navigation.PushModalAsync(new SplatLastPage(globalDL));
            }
        }
    }

    private void JokerCinquante(object sender, EventArgs e)
    {
        Random rand = new Random();
        int dis = rand.Next(4);
        while (dis==answer) { dis = rand.Next(4); }
        buttons[dis].IsEnabled = false;
        int dis2 = rand.Next(4);
        while (dis2 == answer || dis2 == dis) { dis2 = rand.Next(4); }
        buttons[dis2].IsEnabled = false;
        CinqBtn.IsEnabled = false;
        globalDL.splatdatas.enabledbtn[0] = false;
    }

    private async void JokerAppel(object sender, EventArgs e)
    {
        Random random = new Random();
        int prob = random.Next(101);
        int word = random.Next(3);
        if (prob <= 50)
        {
            if (word == 1) { await DisplayAlert("Résultat de l'appel", "Votre ami.e pense à la réponse " + (answer + 1), "OK"); }
            else { await DisplayAlert("Résultat de l'appel", "Votre ami.e vous conseille la réponse " + (answer + 1), "OK"); }
        }
        else
        {
            int dis = random.Next(4);
            while (dis == answer) { dis = random.Next(4); }
            if (word == 1) { await DisplayAlert("Résultat de l'appel", "Votre ami.e vous conseille à la réponse " + (dis + 1), "OK"); }
            else { await DisplayAlert("Résultat de l'appel", "Votre ami.e pense à la réponse " + (dis + 1), "OK"); }
        }
        AppelBtn.IsEnabled = false;
        globalDL.splatdatas.enabledbtn[1] = false;
    }

    private async void JokerPublic(object sender, EventArgs e)
    {
        Random random = new Random();
        int probAns = random.Next(25, 101);
        int prob1 = random.Next(101 - probAns);
        int prob2 = random.Next(101 - probAns - prob1);
        int prob3 = 100 - probAns - prob1 - prob2;
        int[] probs = new int[4];
        probs[answer] = probAns;
        int[] p = { prob1, prob2, prob3 };
        int j = 0;
        for (int i = 0; i < probs.Length; i++)
        { 
            if (i!=answer) 
            { 
                probs[i] = p[j] ;
                j++;
            } 
        }
        await DisplayAlert("Avis du public :", $"Rep 1 : {probs[0]}% ; Rep 2 : {probs[1]}% ; Rep 3 : {probs[2]}% ; Rep 4 : {probs[3]}%", "OK");
        PublicBtn.IsEnabled = false;
        globalDL.splatdatas.enabledbtn[2] = false;
    }
}