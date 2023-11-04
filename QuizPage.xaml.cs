namespace PouicGame;

public partial class QuizPage : ContentPage
    // Page du quiz Monster Hunter
{
    string[] grands = { "grand_izuchi.jpg", "grand_baggi.jpg", "kulu_ya_ku.jpg", "grand_wroggi.jpg", "arzuros.jpg", "lagombi.jpg", "volvidon.jpg", 
        "aknosom.jpg", "ludroth_royal.jpg", "barroth.jpg", "hermitaur_daimyo.jpg", "khezu.jpg", "tetranadon.jpg", "bishaten.jpg", "bishaten_sanguin.jpg",
        "pukei_pukei.jpg", "jyuratodus.jpg", "basarios.jpg", "somnacanth.jpg", "somnacanth_aurore.jpg", "rathian.jpg", "rathian_or.jpg", "barioth.jpg", 
        "tobi_kadachi.jpg", "magnamalo.jpg", "magnamalo_enrage.jpg", "anjanath.jpg", "nargacuga.jpg", "nargacuga_selenite.jpg", "mizutsune.jpg", 
        "mizutsune_mauve.jpg", "goss_harag.jpg", "garangolm.jpg", "ceanataur_shogun.jpg", "rathalos.jpg", "rathalos_argent.jpg", "almudron.jpg", 
        "almudron_magma.jpg", "zinogre.jpg", "lunagaron.jpg", "astalos.jpg", "espinas.jpg", "espinas_de_feu.jpg", "gore_magala.jpg", "gore_magala_du_chaos.jpg",
        "seregios.jpg", "tigrex.jpg", "diablos.jpg", "rakna_kadaki.jpg", "rakna_kadaki_de_feu.jpg", "kushala_daora.jpg", /*"kushala_daora_eveille.jpg",*/ 
        "chameleos.jpg", /*"chameloes_eveille.jpg",*/ "teostra.jpg", /*"teostra_eveille.jpg",*/ "malzeno.jpg", "shagaru_magala.jpg", "velkhana.jpg",
        "rajang.jpg", "rajang_orage.jpg", "bazelgeuse.jpg", "bazelgeuse_vulcan.jpg", "ibushi_du_vent.jpg", "narwa_du_tonnerre.jpg", 
        /*"narwa_la_mere_de_tous.jpg",*/ "valstrax_ecarlate.jpg", /*"valstrax_ecarlate_eveille.jpg",*/ "gaismagorm.jpg", "arzuros_superieur.jpg", 
        "rathian_superieure.jpg", "mizutsune_superieur.jpg", "rathalos_superieur.jpg", "diablos_superieur.jpg", "zinogre_superieur.jpg"};

    string[] petits = { "felyne.jpg", "melynx.jpg", "kelbi.jpg", "bombagdy.jpg", "gargwa.jpg", "popo.jpg", "anteka.jpg", "slagtoth.jpg", "kestodon.jpg", 
        "rhenoplos.jpg", "chevrecape.jpg", "bullfango.jpg", "jagras.jpg", "zamite.jpg", "delex.jpg", "ludroth.jpg", "uroktor.jpg", "rachnoid.jpg", 
        "pyrantula.jpg", "hermitaur.jpg", "ceanataur.jpg", "bnahabra.jpg", "altaroth.jpg", "gajau.jpg", "remobra.jpg", "hornetaur.jpg", "vespoid.jpg", 
        "izuchi.jpg", "wroggi.jpg", "baggi.jpg", "jaggi.jpg", "jaggia.jpg", "velociprey.jpg", "boggi.jpg"};

    string[] faune = { "spectroiseau_rouge.jpg", "spectroiseau_orange.jpg", "spectroiseau_vert.jpg", "spectroiseau_jaune.jpg", "spectroiseau_prisme.jpg",
        "poisoncrapaud.jpg", "paralycrapaud.jpg", "somnicrapaud.jpg", "explocrapaud.jpg", "scarafeu.jpg", "scaraboue.jpg", "scaratonnerre.jpg", 
        "scaraneige.jpg", "cendrelette.jpg", "escurgeot.jpg", "araignee_poupee.jpg", "araignee_pantin.jpg", "geminard.jpg", "pieginsectes.jpg",
        "antidobra.jpg", "lievure.jpg", "lanterninsecte.jpg", "crabe_roquette.jpg", "tortaube.jpg", "filoptere.jpg", "filoptere_rubis.jpg", 
        "filoptere_dore.jpg", "vitaguepe.jpg", "tissinsecte.jpg", "feupillon.jpg", "papoeillons.jpg", "coupillon.jpg", "calampar_rouge.jpg",
        "calampar_jaune.jpg", "calampar_vert.jpg", "calampar_or.jpg", "flashinsecte.jpg", "piegecrapaud.jpg", "crabe_pince.jpg", "crabachoir.jpg", 
        "giganha.jpg", "chauvecho.jpg", "scarabee_etoile.jpg", "calamaracere.jpg", "crapaud_ventouse.jpg", "lezard_de_pierre.jpg", "lezard_de_roche.jpg",
        /*"lezard_a_ecailles.jpg"*/ "fortunibou.jpg", "corbojoie.jpg", "renard_neigeux.jpg", "escargot_moine.jpg", "regitrice.jpg", "quetzalcobra.jpg",
        "bec_infernal.jpg", "noblauphin.jpg", "gargouilleau.jpg", "grand_filoptere.jpg", /*"spectrinsecte_or.jpg",*/ "spectrinsecte_dore.jpg"};

	Color vert = Color.FromArgb("#0F0");
	Color rouge = Color.FromArgb("#F00");
    Button[] buttons = new Button[4];
    int current;
    int answer;
    int[] prop = new int[4];
    HelpPage aide = new HelpPage();
    public GlobalDataList globalDL;
    string rootpath;

    public QuizPage(GlobalDataList globalDL)
    {
        InitializeComponent();
        rootpath = AppContext.BaseDirectory.Split("PouicGame\\bin")[0];
        this.globalDL = globalDL;
        CinqBtn.IsEnabled = globalDL.mhdatas.enabledbtn[0];
        AppelBtn.IsEnabled = globalDL.mhdatas.enabledbtn[1];
        PublicBtn.IsEnabled = globalDL.mhdatas.enabledbtn[2];
        Btn();
        ManageArrows();
        Random rand = new Random();
        int type = 0;
        if (globalDL.mhdatas.niveau=="Aspirant")
        {
            if (globalDL.mhdatas.faits.Length >= 10 && rand.Next(1, 3) == 2)
            {
                InitPage(petits, 2);
            }
            else { InitPage(grands, 1); }
        }
        else 
        {
            if (globalDL.mhdatas.faits.Length >= 10)
            {
                type = rand.Next(1, 4);
                if (type==1) { InitPage(grands, 1); }
                else if (type==2) { InitPage(petits, 2);}
                else {  InitPage(faune, 3); }
            }
            else
            {
                if (rand.Next(1, 3) == 2)
                {
                    InitPage(petits, 2);
                }
                else { InitPage(grands, 1); }
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
        if (globalDL.mhdatas.price==1) { Cursor15Btn.IsEnabled = true; }
        else if (globalDL.mhdatas.price>1) 
        {
            arrows[globalDL.mhdatas.price-1].IsEnabled = true;
            arrows[globalDL.mhdatas.price-2].IsEnabled = false;
        }
    }
    private string Capitalise(string s)
    {
        string cap;
        cap = s.ToUpper()[0] + s.Substring(1) + " ";
        return cap;
    }
    private string SourceToAns(string source, int i)
    {
        string monster_name = source.Split(".jpg")[0];
        string ans = $"{i + 1} : ";

        // Mettre des traits d'union s'il faut...
        if (monster_name == "kulu_ya_ku") { ans += "Kulu-Ya-Ku"; }
        else if (monster_name == "pukei_pukei") { ans += "Pukei-Pukei"; }
        else if (monster_name == "tobi_kadachi") { ans += "Tobi-Kadachi"; }
        else if (monster_name == "rakna_kadaki") { ans += "Rakna-Kadaki"; }
        else if (monster_name == "rakna_kadaki_de_feu") { ans += "Rakna-Kadaki de Feu"; }
        else if (monster_name == "araignee_poupee") { ans += "Araignée Poupée"; }
        else if (monster_name == "araignee_pantin") { ans += "Araignée Pantin"; }
        else if (monster_name == "crabe_roquette") { ans += "Crabe-Roquette"; }
        else if (monster_name == "crabe_pince") { ans += "Crabe-pince"; }
        else if (monster_name == "scarabee_etoile") { ans += "Scarabée-étoile"; }
        else if (monster_name == "crapaud_ventouse") { ans += "Crapaud-ventouse"; }
        else if (monster_name == "escargot_moine") { ans += "Escargot-Moine"; }
        else
        {
            string[] name = monster_name.Split("_");
            string[] prepositions = { "de", "du", "la", "pierre", "roche" };
            foreach (string s in name)
            {
                // ... ou rajouter des accents si nécessaire.
                if (prepositions.Contains(s)) { ans += s + " "; }
                else if (s == "a") { ans += "à "; }
                else if (s == "or") { ans += "d'or"; }
                else if (s == "superieur") { ans += "Supérieur"; }
                else if (s == "superieure") { ans += "Supérieure"; }
                else if (s == "argent") { ans += "d'argent"; }
                else if (s == "dore") { ans += "doré"; }
                else if (s == "lezard") { ans += "Lézard "; }
                else if (s == "ecailles") { ans += "écailles"; }
                else if (s == "filoptere") { ans += "Filoptère "; }
                else if (s == "pieginsectes") { ans += "Pièginsectes"; }
                else if (s == "lievure") { ans += "Lièvure"; }
                else if (s == "vitaguepe") { ans += "Vitaguêpe"; }
                else if (s == "papoeillons") { ans += "Paœillons"; }
                else if (s == "piegecrapaud") { ans += "Piègecrapaud"; }
                else if (s == "chauvecho") { ans += "Chauvécho"; }
                else if (s == "calamaracere") { ans += "Calamaracéré"; }
                else if (s == "regitrice") { ans += "Régitrice"; }
                else if (s == "chevrecape") { ans += "Chèvrecape"; }
                else if (s == "enrage") { ans += "Enragé"; }
                else if (s == "selenite") { ans += "Sélénite"; }
                else { ans += Capitalise(s); }

            }
        }
        return ans;
    }

    private void DisabledJokers()
    {
        AppelBtn.IsEnabled = false;
        CinqBtn.IsEnabled = false;
        PublicBtn.IsEnabled = false;
    }

    private void InitPage(string[] imgs, int type)
    {
        if (globalDL.mhdatas.LengthFait() < 16)
        {
            if (type == 1) { AnsLabel.Text = "Quel est ce grand monstre ?"; }
            else if (type == 2) {  AnsLabel.Text = "Quel est ce petit monstre ?"; }
            else { AnsLabel.Text = "Quel est cet animal de la faune ?"; }
            PriceScale.ItemsSource = globalDL.mhdatas.prix;
            var rand = new Random();
            current = rand.Next(imgs.Length);
            int relatif = current;
            if (type == 2) { relatif += grands.Length; }
            else if (type == 3) { relatif += (grands.Length + petits.Length); }
            while (globalDL.mhdatas.faits.Contains(relatif)) 
            {
                current = rand.Next(imgs.Length);
                relatif = current;
                if (type == 2) { relatif += grands.Length; }
                else if (type == 3) { relatif += (grands.Length + petits.Length); }
            }
            globalDL.mhdatas.UpdateFait(relatif);
            MonsterImg.Source = ImageSource.FromFile(rootpath+$"PouicGameDatas\\Monstres\\{imgs[current]}");
            answer = rand.Next(4);
            prop[answer] = current;
            buttons[answer].Text = SourceToAns(imgs[current], answer);

            int[] fakes = new int[3];
            int fake = 0;
            for (int i = 0; i < 3; i++)
            {
                fake = rand.Next(imgs.Length);
                while (fakes.Contains(fake) || fake == current) { fake = rand.Next(imgs.Length); }
                fakes[i] = fake;
            }
            int j = 0;
            for (int i = 0; i < 4; i++) 
            { 
                if (i != answer) 
                { 
                    buttons[i].Text = SourceToAns(imgs[fakes[j]], i);
                    prop[i] = fakes[j];
                    j++;
                } 
            }

            if (globalDL.mhdatas.niveau == "Aspirant" && globalDL.mhdatas.LengthFait() < 10)
            {
                DisabledJokers();
                Button button = new Button();
                button.TextColor = Color.FromArgb("#FFF");
                button.BackgroundColor = Color.FromArgb("#00008B");
                button.CornerRadius = 10;
                button.BorderWidth = 2;
                button.FontAttributes = FontAttributes.Bold;
                button.FontSize = 20;
                button.Text = "Aide";
                button.Clicked += GoToHelp;

                GameGrid.Add(button, 2, 0);
                GameGrid.SetColumnSpan(button, 3);
                aide.InitHelp(prop, type);
            }
        }
    }
 
    private async void TryAns(object sender, EventArgs e)
    {
        Button button = sender as Button;
        for (int i=0; i<4; i++)
        {
            if (i == answer) { buttons[i].BackgroundColor = vert; }
            else { buttons[i].BackgroundColor = rouge; }
        }

        if (globalDL.mhdatas.faits.Length == 16)
        {
            await Task.Delay(1000);
            await Navigation.PushModalAsync(new LastPage(globalDL));
        }
        else
        {
            if (buttons[answer] == button)
            {
                AnsLabel.Text = "Bonne Réponse";
                AnsLabel.TextColor = vert;
                globalDL.mhdatas.price += 1;
                await Task.Delay(1000);
                await Navigation.PushModalAsync(new QuizPage(globalDL));
            }

            else
            {
                AnsLabel.Text = "Mauvaise Réponse";
                AnsLabel.TextColor = rouge;
                await Task.Delay(1000);
                await Navigation.PushModalAsync(new LastPage(globalDL));
            }
        }
        
    }

    public async void GoToHelp(object sender, EventArgs e) { await Navigation.PushModalAsync(aide); }

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
        globalDL.mhdatas.enabledbtn[0] = false;
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
        globalDL.mhdatas.enabledbtn[1] = false;
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
        globalDL.mhdatas.enabledbtn[2] = false;
    }
}