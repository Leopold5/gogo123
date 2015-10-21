using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;   

public class DrawMap : MonoBehaviour {

	public class Limits
	{
		public int minimum;             //Minimum value for our Count class.
		public int maximum;             //Maximum value for our Count class.
		
		
		//Assignment constructor.
		public Limits (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
//-----------------------------------------------------------------------------------------
	public int mapWidth;
	public int mapHeihgt;
	private int [,] terrainArray;

	private Transform terrainHolder;
	public GameObject emptyTile;
	public GameObject [] grassTile;
	public Canvas textCanvas;



//	public DrawMap (int width, int height)
//	{
//		mapWidth = width;
//		mapHeihgt = height;
//	}


//-----------------------------------------------------------------------------------------	




//	public float IsoX(int xCoord, int yCoord) {
//		float x = xCoord;
//		float y = yCoord;
//		x = (y * 0.5f) + (x * 0.5f);
//		return x;
//	}
//
//	public float IsoY(int xCoord, int yCoord) {
//		float x = xCoord;
//		float y = yCoord;
//		y = (x * 0.25f) - (y * 0.25f);
//		return y;
//	}



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

//-----------------------------------------------------------------------------------------



	void Start () {
//		EmptyTerrainArray ();
//		DrawAll ();
	}


	void Update () {


	}




//	void OnDrawGizmos() {
//		for (int x = 0; x < mapWidth; x ++) {
//			Gizmos.color = Color.red;
//		//	Gizmos.DrawLine(new Vector3 (mapCoords[x,0][0],mapCoords[x,0][1],0), new Vector3 (mapCoords[x,mapHeihgt-1][0],mapCoords[x,mapHeihgt-1][1], 0));
//			Gizmos.DrawLine(new Vector3 (mapCoords[x,0][0],Coords.y (x, 0),0), new Vector3 (mapCoords[x,mapHeihgt-1][0],mapCoords[x,mapHeihgt-1][1], 0));
//		}
//		for (int y = 0; y < mapHeihgt; y ++) {
//			Gizmos.color = Color.green;
//	//		Gizmos.DrawLine(new Vector3 (mapCoords[0,y][0],mapCoords[0,y][1],0), new Vector3 (mapCoords[mapWidth-1,y][0],mapCoords[mapWidth-1,y][1], 0));
//		}
//	}

	void DrawAll (){
		DrawTerrain ();
//		DrawMountains ();
//		DrawRoads ();
//		DrawCastles ();
	}

//	void InitialiseList ()
//	{
//		//Clear our list gridPositions.
//		gridPositions.Clear ();
//		
//		//Loop through x axis (columns).
//		for(int x = 1; x < columns-1; x++)
//		{
//			//Within each column, loop through y axis (rows).
//			for(int y = 1; y < rows-1; y++)
//			{
//				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
//				gridPositions.Add (new Vector3(x, y, 0f));
//			}
//		}
//	}


//	int CoordsTerrain (int x, int y) {
////	if terrai
//	}

	void EmptyTerrainArray (){
		terrainArray = new int[mapWidth, mapHeihgt];
		for (int x = 0; x < mapWidth; x ++) {
			for (int y = 0; y < mapHeihgt; y ++) {
				terrainArray [x, y] = 0;
			}
		}
	}




	void DrawTerrain (){
		EmptyTerrainArray ();
		terrainHolder = new GameObject ("Terrain").transform;

		for(int x = 0; x <= mapWidth-1; x++){
			for(int y = 0; y <= mapHeihgt-1; y++){
				if (terrainArray [x,y]==0)
				{
					GameObject toInstantiate = emptyTile;
					Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX(x,y), IsoYtoTwoDY (x,y), -1f), Quaternion.identity);
//					Canvas coords = Instantiate (textCanvas, new Vector3 (TwoDXtoIsoX(x,y), TwoDYtoIsoY (x,y), -1f), Quaternion.identity) as Canvas;
//					TextCanvasPropertiesScript propScript = coords.GetComponent<TextCanvasPropertiesScript>();
//					propScript.coordsText.text = x.ToString()+","+y.ToString();
				}

				else if (terrainArray [x,y]==1)
				{
					GameObject toInstantiate = grassTile[Random.Range(0,grassTile.Length)];
					Instantiate (toInstantiate, new Vector3 (IsoXtoTwoDX(x,y), IsoYtoTwoDY (x,y), -1f), Quaternion.identity);
				}
			}
		}
	}

//	void DrawMountains (){}
//	void DrawRoads (){}
//	void DrawCastles (){}

}


















