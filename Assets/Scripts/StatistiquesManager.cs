using UnityEngine;
using System.Collections;

public class StatistiquesManager : MonoBehaviour
{

    private int idPartie;
    private int tempsPartie;
    private int nbErreurs;
    private int nbAppelsAide;
    private int nbIngOptUtil;
    private int nbCrepesFaites;
    private string cheminFichierStats;
    private string nomActivite;

    //void Start()
    //{
    //    switch (UnityEditor.EditorApplication.currentScene)
    //    {
    //        case "Assets/Scenes/a_crepe.unity":
    //            Debug.Log("Crepe");
    //            nomActivite = "Crepe";
    //            break;
    //    }
        

    //    //Check si fichier de stats existe déjà
    //    cheminFichierStats = "Assets/Statistiques/stats" + nomActivite + ".txt";
    //    Debug.Log("Existance fichier stats : " + System.IO.File.Exists(cheminFichierStats) + "  Chemin : " + cheminFichierStats);

    //    //On check si le fichier de stats n'existe pas, dans ce cas on le crée
    //    if (!System.IO.File.Exists(cheminFichierStats))
    //    {
    //        CreerFichierStats();
    //        Debug.Log("Fichier crée");
    //    }
    //    //AjouterStats();
    //    ObtenirStatsDernierePartie();
    //}
    //void CreerFichierStats()
    //{
    //    System.IO.FileStream fs = System.IO.File.Open("Assets/Statistiques/stats" + nomActivite + ".txt", System.IO.FileMode.Append);
    //    {
    //        System.Byte[] stats = new System.Text.UTF8Encoding(true).GetBytes("idPartie,tempsPartie,nbErreurs,nbAppelsAide,nbIngOptUtil,nbCrepesFaites\n");
    //        fs.Write(stats, 0, stats.Length);
    //    }
    //    fs.Close();
    //}

    void AjouterStats()
    {
        System.IO.FileStream fs = System.IO.File.Open("Assets/Statistiques/stats" + nomActivite + ".txt", System.IO.FileMode.Append);
        {
            System.Byte[] stats = new System.Text.UTF8Encoding(true).GetBytes(idPartie + "," + tempsPartie + "," + nbErreurs + "," + nbAppelsAide + "," + nbIngOptUtil + "," + nbCrepesFaites + "\n");
            fs.Write(stats, 0, stats.Length);
        }
        fs.Close();
    }

    void ObtenirStatsDernierePartie()
    {
        string derniereLigne = null;
        string ligneTraitee;
        using (var reader = new System.IO.StreamReader("Assets/Statistiques/stats" + nomActivite + ".txt"))
        {
            while ((ligneTraitee = reader.ReadLine()) != null)
            {
                derniereLigne = ligneTraitee;
            }
        }

        string[] tableauStats;
        tableauStats = derniereLigne.Split(new char[] { ',' });
        for (int i = 0; i <= tableauStats.Length - 1; i++)
        {
            Debug.Log("i : " + i + " valeur : " + tableauStats[i]);
        }
    }
}
