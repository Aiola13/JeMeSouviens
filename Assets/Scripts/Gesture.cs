/* Original script found here: http://wiki.unity3d.com/index.php/Gesture_Recognizer
 * adapted script to run with tablet android
 * added a random symbol generator to draw and recognize
 */

using UnityEngine;
using System.Collections;

public class Gesture : MonoBehaviour
{
    static GameObject gestureDrawing;
    //public static GameObject GuiText;
    GestureTemplates m_Templates;

    ArrayList pointArr;
    static int mouseDown;
	
	public static int symbol = -1;
	public static bool canDraw = false;
	public static bool drawSymbol = false;
	public Texture[] textures; 
	private int texSize = Screen.height / 2;

    // runs when game starts - main function
    void Awake () {
        m_Templates = new GestureTemplates();
	    pointArr = new ArrayList();
    	
	    gestureDrawing = GameObject.Find("Gesture");
	    //GuiText = GameObject.Find("GUI Text");
	    //GuiText.guiText.text = "Templates loaded: " + GestureTemplates.Templates.Count;
		//Debug.Log("Templates loaded: " + GestureTemplates.Templates.Count);
    }	


    IEnumerator worldToScreenCoordinates () {
	    // fix world coordinate to the viewport coordinate
	    Vector3 screenSpace = Camera.main.WorldToScreenPoint(gestureDrawing.transform.position);
    	
	    while (Input.GetMouseButton(0)) {
		    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    gestureDrawing.transform.position = curPosition;
		    yield return 0;
	    }
    }

	public static void NewSymbol () {
		symbol = Random.Range(0, GestureTemplates.TemplateNames.Count - 1);
		//Debug.Log("Dessine un " + GestureTemplates.TemplateNames[symbol % 4]); // we use modulo 4, because we have two sets of 4 different symbols
		//GuiText.guiText.text = "Dessine un " + GestureTemplates.TemplateNames[symbol % 4];
		drawSymbol = true;
	}


    void Update() {
		if (canDraw) {
		    if (Input.GetMouseButtonDown(0)) {
			    mouseDown = 1;
		    }
	    	
		    if (mouseDown == 1) {
			    Vector2 p = new Vector2(Input.mousePosition.x , Input.mousePosition.y);
			    pointArr.Add(p);
			    StartCoroutine(worldToScreenCoordinates());
		    }


		    if (Input.GetMouseButtonUp(0)) {
				/*
			    if (Input.GetKey (KeyCode.LeftControl)) {
				    // if CTRL is held down, the script will record a gesture. 
				    // careful conflict when left click is assign to draw gesture
				    mouseDown = 0;
				    GestureRecognizer.recordTemplate(pointArr);
			    }
	            else {
	            	mouseDown = 0;

				    // start recognizing! 
				    GestureRecognizer.startRecognizer(pointArr);

				    pointArr.Clear();
			    }
				*/


			    mouseDown = 0;
			    // start recognizing! 
			    GestureRecognizer.startRecognizer(pointArr);
					
				// the player has drawn the correct symbol
				if (GestureRecognizer.isDrawCorrect) {
					//GuiText.guiText.text = GestureTemplates.TemplateNames[symbol % 4] + " reconnu.";
					Debug.Log(GestureTemplates.TemplateNames[symbol % 4] + " reconnu.");
					drawSymbol = false;
					canDraw = false;
					GameManagerPeche.goodBip.Play ();
					ChangeState(GameManagerPeche.GameState.degivrerTrou, GameManagerPeche.GameState.pecher);
				}
				// the player has NOT drawn the correct symbol
				else {
					//GuiText.guiText.text = "Recommencez svp!";
					Debug.Log("Recommencez svp!");
					GameManagerPeche.errorBip.Play ();
				}

			    pointArr.Clear();
		    }
		}
    } 

    void OnGUI () {
	    if (GestureRecognizer.recordDone == 1) { 
		    GUI.Window (0, new Rect (350, 220, 300, 100), DoMyWindow, "Save the template?");
	    }

		if (drawSymbol) {
			GUI.DrawTexture(new Rect((Screen.width/2) - (texSize/2), (Screen.height/2) - (texSize/2), texSize, texSize), textures[symbol % 4], ScaleMode.ScaleToFit);
		}
	}

    void DoMyWindow (int windowID) {
        GestureRecognizer.stringToEdit = GUILayout.TextField(GestureRecognizer.stringToEdit);

        if (GUI.Button (new Rect (100,50,50,20), "Save")) {
            ArrayList temp = new ArrayList();
            //ArrayList a = (ArrayList)GestureTemplates.Templates[GestureTemplates.Templates.Count - 1];

			/* for writing in file
			string txt = "ArrayList " + GestureRecognizer.stringToEdit + " = new ArrayList(new Vector2[] { ";
			*/

            for (int i = 0; i < GestureRecognizer.newTemplateArr.Count; i++) {
                temp.Add(GestureRecognizer.newTemplateArr[i]);

				/* for writing in file
				Vector2 v = (Vector2)GestureRecognizer.newTemplateArr[i];
				txt += "new Vector2(" + v.x + "f, " + v.y + "f)";
				if (i < GestureRecognizer.newTemplateArr.Count - 1)
					txt += ", ";
				*/
			}

			/* for writing in file
			txt += "});\n";
			System.IO.File.AppendAllText(@"C:\Users\Usager\Desktop\JeMeSouviens\recSym.txt", txt);
			*/

            GestureTemplates.Templates.Add(temp);
            GestureTemplates.TemplateNames.Add(GestureRecognizer.stringToEdit);
            GestureRecognizer.recordDone = 0;
            GestureRecognizer.newTemplateArr.Clear();

            //GuiText.guiText.text = "TEMPLATE: " + GestureRecognizer.stringToEdit + "\n STATUS: SAVED";
			//Debug.Log("TEMPLATE: " + GestureRecognizer.stringToEdit + " SAVED");
	    }

	    if (GUI.Button (new Rect (160,50,50,20), "Cancel")) {
            GestureRecognizer.recordDone = 0;
			//GuiText.guiText.text = "";
	    }
    }


	// Parameters: prev State, curr State
	void ChangeState(GameManagerPeche.GameState prev, GameManagerPeche.GameState current) {
		GameManagerPeche.curGameState = current;
		GameManagerPeche.prevGameState = prev;
	}
}
