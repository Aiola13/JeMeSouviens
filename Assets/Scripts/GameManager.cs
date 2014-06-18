using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Texture2D Tex_dialogue;
    public Texture rejouer;
    public Texture etoile;
    protected GUIStyle style = new GUIStyle();
    protected int brd = Screen.height / 100;

    public static bool nextState;
    public static bool boutonValidation;
    public static bool boutonAnnulation;

    private static string nomActivite = "";
    private static string cheminFichierStats = "";
    private static string libelleStats;
    protected static string[] tableauStats;

    public static System.Diagnostics.Stopwatch chrono;

    public static int idPartie;
    public static float tempsPartie;
    public static int nbErreurs;
    public static int nbAppelsAide;


    /////////////////////////////////////////////////////////////////////
 

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume) {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;
        return newAudio;
    }

    // active et affiche la texture t
    public static void AfficherTexture(GUITexture t) {
        t.guiTexture.enabled = true;
        t.gameObject.SetActive(true);
    }

    // désactive et enleve l'affichage de la texture t
    public static void NePasAfficherTexture(GUITexture t) {
        t.guiTexture.enabled = false;
        t.gameObject.SetActive(false);
    }

    // active et affiche la texture t
    public static void AfficherTexture(GUITexture t, GUIText text) {
        t.guiTexture.enabled = true;
        t.gameObject.SetActive(true);
        text.enabled = true;
    }

    // désactive et enleve l'affichage de la texture t
    public static void NePasAfficherTexture(GUITexture t, GUIText text) {
        t.guiTexture.enabled = false;
        t.gameObject.SetActive(false);
        text.enabled = false;
    }

    public static void ActiverDrag() {
        Camera.main.GetComponent<IngredientsDrag>().enabled = true;
    }
    public static void DesactiverDrag() {
        Camera.main.GetComponent<IngredientsDrag>().enabled = false;
    }

    // affiche une boite de dialogue avec comme texture pnj et comme texte txt,
    public void AfficherDialogue(Texture2D pnj, string txt) {

        //GameManagerCrepe.chrono.Stop();
        style.fontSize = Screen.height / 36;
        style.alignment = TextAnchor.MiddleLeft;
        style.font = (Font)Resources.Load("Roboto-Regular");

        GUI.DrawTexture(new Rect(brd, Screen.height * 2 / 3, Screen.width - brd * 2, Screen.height / 3 - brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
        GUI.DrawTexture(new Rect(brd * 2, Screen.height * 2 / 3 + brd, Screen.width / 5, Screen.height / 3 - brd * 3), pnj, ScaleMode.ScaleToFit, true, 0);
        GUI.Box(new Rect(Screen.width * 1 / 4, Screen.height * 7 / 10 + brd * 2, Screen.width - 20, Screen.height / 5 - 10), txt, style);

        //style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = Screen.height / 28;
        GUI.Box(new Rect(Screen.width * 2 / 3, Screen.height * 2 / 3 + brd * 2, Screen.width / 10, Screen.height / 3 - 10), "TOUCHER POUR CONTINUER !", style);
        DialogueCrepe.canRestartChrono = false;
    }

    protected static void CreerFichierStats()
    {
        switch (Application.loadedLevelName)
        {
            case "a_crepe":
                Debug.Log("Crepe");
                nomActivite = "Crepe";
                libelleStats += ",nbIngOptUtil,nbCrepesFaites\n";
                break;

            case "a_peche":
                Debug.Log("Pêche");
                nomActivite = "Peche";
                libelleStats += ",nbVerifInventaire,nbCapturesRatees,nbPoissonsAttrapes\n";
                break;
        }

        //Check si fichier de stats existe déjà
        cheminFichierStats = Application.persistentDataPath + "/Stats" + nomActivite + ".txt";
        Debug.Log("Existance fichier stats : " + System.IO.File.Exists(cheminFichierStats) + "  Chemin : " + cheminFichierStats);
        Debug.Log("LibelleStats :" + libelleStats);

        //On check si le fichier de stats n'existe pas, dans ce cas on le crée
        if (!System.IO.File.Exists(cheminFichierStats))
        {
            System.IO.FileStream fs = System.IO.File.Open(Application.persistentDataPath + "/Stats" + nomActivite + ".txt", System.IO.FileMode.Append);
            {
                System.Byte[] stats = new System.Text.UTF8Encoding(true).GetBytes(libelleStats);
                fs.Write(stats, 0, stats.Length);
            }
            fs.Close();
        }  
    }


    protected static void ObtenirStatsDernierePartie()
    {
        string derniereLigne = null;
        string ligneTraitee;

        using (var reader = new System.IO.StreamReader(Application.persistentDataPath + "/Stats" + nomActivite + ".txt"))
        {
            while ((ligneTraitee = reader.ReadLine()) != null)
            {
                derniereLigne = ligneTraitee;
            }
        }


        tableauStats = derniereLigne.Split(new char[] { ',' });
        for (int i = 0; i <= tableauStats.Length - 1; i++)
        {
            Debug.Log("i : " + i + " valeur : " + tableauStats[i]);
        }

        bool isNumeric = int.TryParse(tableauStats[0], out idPartie);
        if (isNumeric)
        {
            Debug.Log("idPartie : " + idPartie);
            idPartie++;
            Debug.Log("idPartie maj : " + idPartie);
        }
        else
        {
            idPartie = 1;
            Debug.Log("idPartie : " + idPartie);
        }
    }

    public void AfficherDialogue(Texture2D pnj, string txt, string txt2) {
        style.fontSize = Screen.height / 36;
        style.alignment = TextAnchor.MiddleLeft;
        style.font = (Font)Resources.Load("Roboto-Regular");

        GUI.DrawTexture(new Rect(brd, Screen.height * 2 / 3, Screen.width - brd * 2, Screen.height / 3 - brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
        GUI.DrawTexture(new Rect(brd * 2, Screen.height * 2 / 3 + brd, Screen.width / 5, Screen.height / 3 - brd * 3), pnj, ScaleMode.ScaleToFit, true, 0);
        GUI.Box(new Rect(Screen.width * 1 / 4, Screen.height * 7 / 10 + brd * 2, Screen.width - 20, Screen.height / 5 - 10), txt, style);

        //style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = Screen.height / 28;
        GUI.Box(new Rect(Screen.width * 2 / 3, Screen.height * 2 / 3 + brd * 2, Screen.width / 10, Screen.height / 3 - 10), txt2, style);
    }

	// affiche une aide en bas de l'écran sur l'action courrante a faire
	public void AfficherAide(string txt) {
		style.fontSize = Screen.height / 24;
		style.alignment = TextAnchor.MiddleLeft;
		style.font = (Font)Resources.Load("Roboto-Regular");
		
		GUI.DrawTexture(new Rect(brd * 30, Screen.height * 90 / 100, Screen.width - brd * 60, Screen.height * 10 / 100 - brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.Box(new Rect(brd * 40, Screen.height * 90 / 100, Screen.width - brd * 70, Screen.height * 10 / 100 - brd), txt, style);
	}

	// affiche une alerte au centre de l'écran
	public void AfficherAlerte(string txt) {
		style.fontSize = Screen.height / 25;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = (Font)Resources.Load("Roboto-Regular");

		string[] phrases = txt.Split('\n');
		int h = phrases.Length * 10;

		GUI.DrawTexture(new Rect(brd * 30, Screen.height * 50 / 100 - h, Screen.width - brd * 60, Screen.height * h / 100 - brd), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.Box(new Rect(brd * 30, Screen.height * 50 / 100 - h, Screen.width - brd * 60, Screen.height * h / 100 - brd), txt, style);
	}

    public void AfficherScore(int nbEtoiles) {

        style.fontSize = Screen.height / 25;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = (Font)Resources.Load("Roboto-Regular");

        GUI.BeginGroup(new Rect(Screen.width / 8, Screen.height / 8, Screen.width * 6 / 8, Screen.height * 6 / 8), Tex_dialogue, style);

        switch(nbEtoiles){
            case 1:
                GUI.DrawTexture(new Rect(Screen.width * 3 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 4, Screen.width / 2, Screen.height / 15), "Tu peux faire mieux...", style);
                break;
            case 2:
                GUI.DrawTexture(new Rect(Screen.width * 5 / 16 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(Screen.width * 7 / 16 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 4, Screen.width / 2, Screen.height / 15), "C'est presque ça!", style);
                break;
            case 3:
                GUI.DrawTexture(new Rect(Screen.width * 2 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(Screen.width * 3 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(Screen.width * 4 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 4, Screen.width / 2, Screen.height / 15), "Excellent!", style);
                break;
        }
        tempsPartie = chrono.Elapsed.Minutes * 60 + chrono.Elapsed.Seconds;

        GUI.TextArea(new Rect(Screen.width / 8, Screen.height * 3 / 8, Screen.width / 2, Screen.height / 15), "Temps : " + tempsPartie + " secondes", style);
        GUI.TextArea(new Rect(Screen.width / 8, Screen.height * 7 / 16, Screen.width / 2, Screen.height / 15), "Nombre d'erreurs : " + nbErreurs, style);
        GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 2, Screen.width / 2, Screen.height / 15), "Nombre d'aides de Skypi : " + nbAppelsAide, style);

        GUI.EndGroup();
    }

    protected void initGameManager() {
        boutonValidation = false;
        boutonAnnulation = false;
        nextState = false;
        tempsPartie = 0.0f;
        nbErreurs = 0;
        nbAppelsAide = 0;
        libelleStats = "idPartie,tempsPartie,nbErreurs,nbAppelsAide";
    }

}
