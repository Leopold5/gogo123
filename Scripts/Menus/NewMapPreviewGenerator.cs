using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NewMapPreviewGenerator : MonoBehaviour {

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private MapDrawer mapDrawer;

	public InputField width;
	public InputField height;
	private string terrainType = "Empty";
	private string TerrainType
	{
		get
		{
			return terrainType;
		}
		set
		{
			terrainType = value;
			DrawPreview ();
		}
	}

	public GameObject previewPositionHolder;


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	void Start (){
		mapDrawer = GameManager.instance.GetComponent<MapDrawer>();
	}

	void OnEnable (){
		width.onValueChange.AddListener (delegate {DrawPreview ();});
		height.onValueChange.AddListener (delegate {DrawPreview ();});
	} 

	void OnDisable (){
		width.onValueChange.RemoveAllListeners (); 
		height.onValueChange.RemoveAllListeners ();
		if (mapDrawer.previewHoldingGO != null) Destroy (mapDrawer.previewHoldingGO);
		width.text = "";
		height.text = "";
		terrainType = "Empty";
	} 

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



	public void ChangeTerrainType (string newType) {
		if (TerrainType != newType)
			TerrainType = newType;
	}


	public void DrawPreview (){
		if (mapDrawer.previewHoldingGO != null) Destroy (mapDrawer.previewHoldingGO);

		int resultWidth;
		int.TryParse (width.text, out resultWidth);
		int resultHeight;
		int.TryParse (height.text, out resultHeight);

		int widthToDraw = resultWidth;
		int heightToDraw = resultHeight;
		if (widthToDraw == 0) widthToDraw = 1;
		if (heightToDraw == 0) heightToDraw = 1;

		string typeToDraw = terrainType;
		int [,] terrainArrayToDraw = new int[widthToDraw,heightToDraw];
		terrainArrayToDraw = FillTerrainArrayWithCurrentTerrain (widthToDraw, heightToDraw, typeToDraw);

		GameManager.Map mapToDraw = new GameManager.Map ("New map", widthToDraw, heightToDraw, terrainArrayToDraw);
		GameManager.instance.currentMap = mapToDraw;
		mapDrawer.DrawTerrainPreview(mapToDraw, previewPositionHolder);
		
	}

	int [,] FillTerrainArrayWithCurrentTerrain (int width, int height, string type)
	{
		int [,] resultArray = new int[width, height];
		for (int x = 0; x < width; x ++) 
		{
			for (int y = 0; y <height; y ++) 
			{
				if (type == "Empty")resultArray [x, y] = 0;
				else if (type == "Grass")resultArray [x, y] = 1;
				else if (type == "Water")resultArray [x, y] = 2;
				else if (type == "Snow")resultArray [x, y] = 3;
			}
		}
		return resultArray;
	}

}























