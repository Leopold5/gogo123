using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;  

public class MapDrawer : MonoBehaviour 
{

	public GameObject previewHoldingGO;
	public Camera previewCameraPrefab;

	private GameObject toInstantiate;
	private GameObject [,] tilesGOarray;
	public void DrawTerrain (GameManager.Map map, GameObject terrainParentGO)
	{

		if (EditorManager.instance !=null) 
		{
			EditorManager editorManager = EditorManager.instance;
			tilesGOarray = editorManager.tilesGOarray;
		}

		for (int x = 0; x <= map.width-1; x++) 
		{
			for (int y = 0; y <= map.height-1; y++) 
			{
				if (map.terrainArray [x, y] == 0) toInstantiate = GameManager.instance.emptyTile;
				else if (map.terrainArray [x, y] == 1) toInstantiate = GameManager.instance.grassTile [Random.Range (0, GameManager.instance.grassTile.Length)];
				else if (map.terrainArray [x, y] == 2) toInstantiate = GameManager.instance.waterTile [Random.Range (0, GameManager.instance.waterTile.Length)];
				else if (map.terrainArray [x, y] == 3) toInstantiate = GameManager.instance.snowTile [Random.Range (0, GameManager.instance.snowTile.Length)];

				GameObject instance = Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX (x, y), IsoYtoTwoDY (x, y), -1f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (terrainParentGO.transform);
				tilesGOarray [x, y] = instance;
			}
		}
	}

	public void DrawMovementModificators (GameManager.Map map, GameObject roadsAndMountainsParentGO)
	{

		if (EditorManager.instance !=null) 
		{
			EditorManager editorManager = EditorManager.instance;
			tilesGOarray = editorManager.mModsGOarray;
		}
		
		for (int x = 0; x <= map.width-1; x++) 
		{
			for (int y = 0; y <= map.height-1; y++) 
			{
				int terrainType = map.terrainArray [x, y];
				if (map.mModsArray [x, y] == 0) toInstantiate = MakeARoadTile(x, y, terrainType);

				GameObject instance = Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX (x, y), IsoYtoTwoDY (x, y), -1f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (roadsAndMountainsParentGO.transform);
				tilesGOarray [x, y] = instance;
			}
		}
	}

	public GameObject MakeARoadTile (int xCoord, int yCoord, int terrainType) 
	{
		GameObject properTile = new GameObject ("RoadTile");

		return properTile;
	}

	public void DrawTerrainPreview (GameManager.Map map, GameObject placeholder)
	{
		previewHoldingGO = new GameObject ("holder");
		Transform holder = previewHoldingGO.transform;
		holder.position = new Vector3 (1000,1000,0);

		for (int x = 0; x <= map.width-1; x++) 
		{
			for (int y = 0; y <= map.height-1; y++) 
			{
				if (map.terrainArray [x, y] == 0) {
					GameObject toInstantiate = GameManager.instance.emptyTile;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX (x, y)+1000, IsoYtoTwoDY (x, y)+1000, -1f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (holder);
					instance.layer = 8;
				} else if (map.terrainArray [x, y] == 1) {
					GameObject toInstantiate = GameManager.instance.grassTile [Random.Range (0, GameManager.instance.grassTile.Length)];
					GameObject instance = Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX (x, y)+1000, IsoYtoTwoDY (x, y)+1000, -1f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (holder);
					instance.layer = 8;
				} else if (map.terrainArray [x, y] == 2) {
					GameObject toInstantiate = GameManager.instance.waterTile [Random.Range (0, GameManager.instance.waterTile.Length)];
					GameObject instance = Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX (x, y)+1000, IsoYtoTwoDY (x, y)+1000, -1f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (holder);
					instance.layer = 8;
				} else if (map.terrainArray [x, y] == 3) {
					GameObject toInstantiate = GameManager.instance.snowTile [Random.Range (0, GameManager.instance.snowTile.Length)];
					GameObject instance = Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX (x, y)+1000, IsoYtoTwoDY (x, y)+1000, -1f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (holder);
					instance.layer = 8;
				}
			}
		}

		Camera previewCam = Instantiate (previewCameraPrefab) as Camera;
		previewCam.transform.SetParent (holder);

		float xCoordTemp = IsoXtoTwoDX ((map.width/2f-0.5f), (map.height/2f-0.5f));
		float yCoordTemp = IsoYtoTwoDY ((map.width/2f-0.5f), (map.height/2f-0.5f));
		previewCam.transform.localPosition = new Vector3 (xCoordTemp, yCoordTemp, -20 );
		float floatMapWidth = map.width;
		float floatMapHeight = map.height;
		if (map.width >= map.height) previewCam.orthographicSize = floatMapWidth/4f*1.05f+0.5f;
		else previewCam.orthographicSize = floatMapHeight/4f*1.05f +0.5f;
	}


//-----------------------------------------------------------------------------------------	
	
	public float IsoXtoTwoDX(float xCoord, float yCoord) {
		float x = xCoord;
		float y = yCoord;
		x = (y * 0.5f) + (x * 0.5f);
		return x;
	}
	
	public float IsoYtoTwoDY(float xCoord, float yCoord) {
		float x = xCoord;
		float y = yCoord;
		y = (y * 0.25f) - (x * 0.25f);
		return y;
	}


}

