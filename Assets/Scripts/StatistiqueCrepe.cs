using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StatistiqueCrepe : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (File.Exists(Application.persistentDataPath + "/statCrepe.dat")) {
            BinaryFormatter b = new BinaryFormatter();
            FileStream f = File.Open(Application.persistentDataPath + "/statCrepe.dat", FileMode.Open);
            //highScores = (List<ScoreEntry>)b.Deserialize(f);
            f.Close();
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SaveScores() {
        //Créer un BinaryFormatter
        BinaryFormatter b = new BinaryFormatter();
        //Créer un fichier
        var f = File.Create(Application.persistentDataPath + "/statCrepe.dat");
        //Sauvegarder les scores
        //b.Serialize(f, highScores);
        f.Close();
    }

}
