using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Texture2D Tex_dialogue;
    public GUIStyle TextNormal = new GUIStyle();
	public GUIStyle TextHighlight = new GUIStyle();
    public Texture rejouer;
    public Texture etoile;


    public static bool nextState;
    public static bool boutonValidation;
    public static bool boutonAnnulation;

    private static string nomActivite = "";
    private static string cheminFichierStats = "";
    private static string libelleStats;
    protected static string[] tableauStats;
    protected static string[] tableauStats;
    protected static int[] tableauStatSpecifique;
    protected static bool isNumeric;

    public static System.Diagnostics.Stopwatch chrono;

    public static int idPartie;
    public static float tempsPartie;
    public static int nbErreurs;
    public static int nbAppelsAide;


    /////////////////////////////////////////////////////////////////////
 

	void Awake() {
		TextNormal.font = (Font)Resources.Load("Roboto-Regular");
		TextHighlight.font = (Font)Resources.Load("Roboto-Regular");
	}

    void Update()
    {
        Debug.Log("Je suis dane le GameManager");
    }


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



	// affiche une boite de dialogue avec comme texture pnj et comme texte txt,
	public void AfficherDialogue(Texture2D pnj, string txt, bool displayContinuer = true) {
		
		//GameManagerCrepe.chrono.Stop();
		TextNormal.fontSize = Screen.height / 36;
		TextNormal.alignment = TextAnchor.MiddleLeft;

		float paddingX = Screen.width * 1 / 100;
		float paddingY = Screen.height * 1 / 100;

		Rect texRect = new Rect(paddingX, Screen.height * 2/3 - paddingY, Screen.width - 2*paddingX, Screen.height / 3);
		Rect pnjRect = new Rect(paddingX, Screen.height * 2/3 - paddingY, Screen.height / 3, Screen.height / 3);
		Rect txtRect = new Rect(Screen.height / 3 + 2*paddingX, Screen.height * 2 / 3 - paddingY, Screen.height / 3, Screen.height / 3);

		GUI.DrawTexture(texRect, Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.DrawTexture(pnjRect, pnj, ScaleMode.StretchToFill, true, 0);
		GUI.Label(txtRect, txt, TextNormal);

		if (displayContinuer) {
			string continueText = "TOUCHER POUR CONTINUER!";
			float sizeX = TextNormal.CalcSize(new GUIContent(continueText)).x;
			float sizeY = TextNormal.CalcSize(new GUIContent(continueText)).y;

			Rect continueRect = new Rect(Screen.width - sizeX - 2*paddingX, Screen.height - sizeY - 2*paddingY, sizeX, sizeY);

			GUI.Label(continueRect, continueText, TextNormal);
		}

		DialogueCrepe.canRestartChrono = false;
	}



	// affiche une aide en bas de l'écran sur l'action courrante a faire
	public void AfficherAide(string txt) {
		TextNormal.fontSize = Screen.height / 24;
		TextNormal.alignment = TextAnchor.MiddleCenter;

		float paddingX = Screen.width * 5 / 100;
		float paddingY = Screen.height * 5 / 100;
		float sizeX = TextNormal.CalcSize(new GUIContent(txt)).x + paddingX;
		float sizeY = TextNormal.CalcSize(new GUIContent(txt)).y + paddingY;

		// positionné en bas de l'ecran
		Rect myRect = new Rect(Screen.width / 2 - (sizeX  / 2), Screen.height - sizeY, sizeX, sizeY);

		GUI.DrawTexture(myRect, Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.Label(myRect, txt, TextNormal);
	}



	// affiche une alerte au centre de l'écran
	public void AfficherAlerte(string txt) {
		TextNormal.fontSize = Screen.height / 25;
		TextNormal.alignment = TextAnchor.MiddleCenter;

		float paddingX = Screen.width * 5 / 100;
		float paddingY = Screen.height * 5 / 100;
		float sizeX = TextNormal.CalcSize(new GUIContent(txt)).x + paddingX;
		float sizeY = TextNormal.CalcSize(new GUIContent(txt)).y + paddingY;

		// positionné au centre
		Rect myRect = new Rect(Screen.width / 2 - (sizeX / 2), Screen.height / 2 - (sizeY / 2), sizeX, sizeY);

		GUI.DrawTexture(myRect, Tex_dialogue, ScaleMode.StretchToFill, true, 0);
		GUI.Label(myRect, txt, TextNormal);
	}



    public void AfficherScore(int nbEtoiles) {

        TextNormal.fontSize = Screen.height / 25;
		TextNormal.alignment = TextAnchor.MiddleCenter;
		TextNormal.font = (Font)Resources.Load("Roboto-Regular");

        GUI.BeginGroup(new Rect(Screen.width / 8, Screen.height / 8, Screen.width * 6 / 8, Screen.height * 6 / 8), Tex_dialogue, TextNormal);

		float brd = Screen.height / 100;

        switch(nbEtoiles){
            case 1:
                GUI.DrawTexture(new Rect(Screen.width * 3 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 4, Screen.width / 2, Screen.height / 15), "Tu peux faire mieux...", TextNormal);
                break;
            case 2:
                GUI.DrawTexture(new Rect(Screen.width * 5 / 16 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(Screen.width * 7 / 16 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 4, Screen.width / 2, Screen.height / 15), "C'est presque ça!", TextNormal);
                break;
            case 3:
                GUI.DrawTexture(new Rect(Screen.width * 2 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(Screen.width * 3 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(Screen.width * 4 / 8 - Screen.width / 40, Screen.height / 8 - brd, Screen.width / 20, Screen.width / 20), etoile, ScaleMode.StretchToFill);
                GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 4, Screen.width / 2, Screen.height / 15), "Excellent!", TextNormal);
                break;
        }
        tempsPartie = chrono.Elapsed.Minutes * 60 + chrono.Elapsed.Seconds;

        GUI.TextArea(new Rect(Screen.width / 8, Screen.height * 3 / 8, Screen.width / 2, Screen.height / 15), "Temps : " + tempsPartie + " secondes", TextNormal);
        GUI.TextArea(new Rect(Screen.width / 8, Screen.height * 7 / 16, Screen.width / 2, Screen.height / 15), "Nombre d'erreurs : " + nbErreurs, TextNormal);
        GUI.TextArea(new Rect(Screen.width / 8, Screen.height / 2, Screen.width / 2, Screen.height / 15), "Nombre d'aides de Skypi : " + nbAppelsAide, TextNormal);

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

    protected void GetStatsSpecifique(int numColonne)
    {
        int i = 0;
        int valeur = 0;

        string derniereLigne = null;
        string ligneTraitee;

        using (var reader = new System.IO.StreamReader(Application.persistentDataPath + "/Stats" + nomActivite + ".txt"))
        {
            while ((ligneTraitee = reader.ReadLine()) != null)
            {
                derniereLigne = ligneTraitee;
                tableauStats = derniereLigne.Split(new char[] { ',' });
                Debug.Log("Contenu tableau : " + tableauStats[numColonne]);
                isNumeric = int.TryParse(tableauStats[numColonne], out valeur);
                if (isNumeric)
                {
                    tableauStatSpecifique[i] = valeur;
                    Debug.Log("i : " + i + " ; valeur : " + valeur);
                }
                i++;
            }
        }
    }





	#region SURBRILLANCE A FAIRE MARCHER BOWDEL

	// affiche une alerte au centre de l'écran
	public void test(string txt) {
		TextNormal.fontSize = Screen.height / 25;
		TextNormal.alignment = TextAnchor.MiddleCenter;
		
		// first split the txt string in lines
		string[] lines = txt.Split('\n');
		
		Vector2 finalSize = Vector2.zero;
		
		for (int l = 0; l < lines.Length; l++) {
			string myText = lines[l];
			Vector2 size = new Vector2(TextNormal.CalcSize(new GUIContent(myText)).x, TextNormal.CalcSize(new GUIContent(myText)).y);
			
			if (size.x > finalSize.x) finalSize.x = size.x;
			finalSize.y += size.y;
		}
		
		for (int l = 0; l < lines.Length; l++) {
			string myText = lines[l];
			Vector2 size = new Vector2(TextNormal.CalcSize(new GUIContent(myText)).x, TextNormal.CalcSize(new GUIContent(myText)).y);
			Vector2 pos = new Vector2(Screen.width / 2 - (size.x / 2), Screen.height / 2 - (finalSize.y / 2) + size.y*l);
			
			// positionné au centre
			Rect myRect = new Rect(pos.x, pos.y, size.x, size.y);
			
			//GUI.DrawTexture(myRect, Tex_dialogue, ScaleMode.StretchToFill, true, 0);
			GUI.DrawTexture(new Rect(Screen.width / 2 - (finalSize.x / 2), Screen.height / 2 - (finalSize.y / 2), finalSize.x, finalSize.y), Tex_dialogue, ScaleMode.StretchToFill, true, 0);
			
			GUI.Label(myRect, myText, TextNormal);
		}

	}


	// affiche une alerte au centre de l'écran
	public void AfficherAlerteAvecSurbrillance(string txt) {
		TextHighlight.fontSize = Screen.height / 25;
		TextHighlight.alignment = TextAnchor.MiddleCenter;
		
		// first split the txt string in lines
		string[] lines = txt.Split('\n');
		

		for (int l = 0; l < lines.Length; l++) {

			// split each line in words
			string[] words = txt.Split(' ');
			
			// create an array called TextItem of words elements
			TextItem[] itemArray = new TextItem[words.Length];
			
			for (int i = 0; i < itemArray.Length; i++) {
				string[] s = words[i].Split(new string[] { "<a>" }, System.StringSplitOptions.None);
				
				// si le mot est alerte, on change sa couleur en rouge sinon la couleur est noir
				if (s.Length > 1) {
					itemArray[i] = new TextItem(s[1], Color.red);
					
					for (int j = 0; j < s.Length; j++) {
						print(s[j]);
					}
				}
				else
					itemArray[i] = new TextItem(s[0], Color.black);
			}
			
			
			float paddingX = Screen.width * 5 / 100;
			float paddingY = Screen.height * 5 / 100;
			float sizeX = TextHighlight.CalcSize(new GUIContent(txt)).x + paddingX;
			float sizeY = TextHighlight.CalcSize(new GUIContent(txt)).y + paddingY;
			
			// positionné au centre
			Rect theRect = new Rect(Screen.width / 2 - (sizeX / 2), Screen.height / 2 - (sizeY / 2), sizeX, sizeY);
			GUI.DrawTexture(theRect, Tex_dialogue, ScaleMode.StretchToFill, true, 0);
			
			foreach (TextItem thisItem in itemArray) {
				
				// create a GUIContent and calculate its size
				GUIContent theContent = new GUIContent(thisItem.text);
				Vector2 theSize = TextHighlight.CalcSize(theContent);
				
				TextHighlight.normal.textColor = thisItem.color;
				theRect.width = theSize.x;
				
				GUI.Label(theRect, theContent, TextHighlight);
				
				// set the x-value of the next box to the end of the last box
				theRect.x += theSize.x + 5;
			}
		}
}
}

#endregion

#region SURBRILLANCE A FAIRE MARCHER BOWDEL
public class TextItem {

	public string text;
	public Color color ;

	// default constructor
	public TextItem () {
		text = "";
		color = Color.white;
	}

	public TextItem (string s, Color c) {
		text = s;
		color = c;
	}
}
#endregion

