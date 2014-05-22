using UnityEngine;
using System.Collections;

public class EtalerBeurre : TouchLogic {

    public Texture2D tmpTexture;
    public Texture2D targetTexture;

    public int brushHeight = 10;
    public int brushWidth = 10;

    Ray ray;
    RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnTouchBeganAnywhere() {
        ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

        // si on touche la poele
        if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject == this)) {
            Debug.Log("au calme");
            etalerBeurre(hit.textureCoord);
        }
    }

    //The main painting method
    //theObject     = the object to be painted
    //tmpTexture    = the object current texture
    //targetTexture = the new texture 
    void etalerBeurre(Vector2 pointDImpact)
    {
        //x and y are 2 floats from another class
        //they store the coordinates of the pixel 
        //that get hit by the RayCast
        int x = (int)(pointDImpact.x);
        int y = (int)(pointDImpact.y);

        //iterate through a block of pixels that goes fro
        //Y and X and go #brushHeight Pixels up
        // and #brushWeight Pixels right
        for (int tmpY = y; tmpY < y + brushHeight; tmpY++)
        {
            for (int tmpX = x; tmpX < x + brushWidth; tmpX++)
            {
                //check if the current pixel is different from the target pixel
                if (tmpTexture.GetPixel(tmpX, tmpY) != targetTexture.GetPixel(tmpX, tmpY))
                {
                    //create a temporary color from the target pixel at the given coordinates
                    Color tmpCol = targetTexture.GetPixel(tmpX, tmpY);
                    //change the alpha of that pixel based on the brush alpha
                    //myBrushAlpha is a 2 Dimensional array that contain
                    //the different Alpha values of the brush
                    //the substractions are to keep the index in range
                    //if (myBrushAlpha[tmpY - y, tmpX - x].a > 0)
                    //{
                        //tmpCol.a = myBrushAlpha[tmpY - y, tmpX - x].a;
                    //}
                    //set the new pixel to the current texture
                    tmpTexture.SetPixel(tmpX, tmpY, tmpCol);
                }
            }
        }
        //Apply 
        tmpTexture.Apply();
        //change the object main texture 
        this.renderer.material.mainTexture = tmpTexture;
    }

}
