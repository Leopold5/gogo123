using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EditorManager : MonoBehaviour 
{	
	public static EditorManager instance = null; 

	GameManager.Map editedMap; 
	private MapDrawer mapDrawer;

	public GameObject activeTile = null;
	public bool mouseOccupied = false;
	public GameObject mouseOccupant = null;

	private bool movingMap;
	private Vector3 mouseDownPosition;
	private Vector3 putItHereIn2D; 
	private int putItHereInISO_x;
	private int putItHereInISO_y;

	private GameObject terrainHolder;
//	private int [,] terrainSavingArray;
	public GameObject [,] tilesGOarray;
	public GameObject [,] mModsGOarray;


	public GameObject loadPanel;
	public GameObject fileButtonPrefab;
	public Transform filesContainingPanel;
	public GameObject savePanel;
	public GameObject donePanel;
	public InputField nameInput;


	void Start ()
	{
		if (instance == null) instance = this;

		editedMap = GameManager.instance.currentMap; 
		mapDrawer = GameManager.instance.GetComponent<MapDrawer>();
		terrainHolder = new GameObject ("Terrain");
		tilesGOarray = new GameObject[editedMap.width,editedMap.height];
		savePanel.SetActive (false);	



		mapDrawer.DrawTerrain (editedMap, terrainHolder);
		PlaceTheCam ();
	}

	void Update () 
	{
		if (activeTile != null) 
		{
			TileFollowTheMouse (activeTile);
		} 
		if (activeTile != null && Input.GetButtonDown ("Fire1")&& Input.mousePosition.x<Screen.width-200) 
		{
			SetATile (activeTile);
			print (activeTile.name);
		}
		if (Input.GetButtonDown ("Fire2")&& activeTile != null) 
		{
			activeTile= null;
			mouseOccupied = false;
			Destroy (mouseOccupant);
		}

		if (Input.GetButtonDown ("Fire1")&& Input.mousePosition.x<Screen.width-200 && Input.mousePosition.y>40 && movingMap == false && activeTile == null) {
			mouseDownPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			movingMap = true;
			//			print ("Мыка нажата в координатах" + mouseDownPosition);
		}
		if (Input.GetButton ("Fire1")&& movingMap == true) { 
			Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 mousePositionDelta = currentMousePosition - mouseDownPosition;
			Camera.main.transform.position = Camera.main.transform.position - mousePositionDelta;
		}
		if (Input.GetButtonUp ("Fire1")&& activeTile == null) {
			movingMap = false;
		}
	}








	public void SaveMapAsNamed_button ()
	{
		SaveMapToFile (Application.persistentDataPath+"/", editedMap.name, ".map");
		donePanel.SetActive (true);
	}

	public void SaveMapToFile (string directory, string fileName, string fileExtention)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (directory+fileName+fileExtention);
		print (editedMap.name);
		bf.Serialize (file, editedMap);
		file.Close ();
	}


	public void SaveMap_Button ()
	{
		if (editedMap.name == "NewMap") 
		{
			savePanel.SetActive (true);	
		} 
		else 
		{
			SaveMapAsNamed_button();
		}
	}
	
	public void SaveMapAs_Button ()
	{
		savePanel.SetActive (true);	
	}
	
	public void LoadmainMenu_button ()
	{
		Destroy (GameManager.instance);
		Application.LoadLevel ("Main");
	}

	public void SetNewName_InputFieldFunction ()
	{ 
		editedMap.name = nameInput.text;
	}

	void TileFollowTheMouse (GameObject whatTile)
	{
		
		if ( mouseOccupied == false|| (mouseOccupied == false && mouseOccupant != whatTile))
		{
			mouseOccupant = Instantiate 
				(whatTile, 
				 new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
				            Camera.main.ScreenToWorldPoint (Input.mousePosition).y,
				            -5), 
				 Quaternion.identity) as GameObject;
			mouseOccupied = true;
		}
		else 
		{
			float xOfPoint_TwoD = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
			float yOfPoint_TwoD = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
			
			float xOfClosestCell_ISO = Mathf.Floor(TwoDXtoIsoX (xOfPoint_TwoD,yOfPoint_TwoD));
			float yOfClosestCell_ISO = Mathf.Floor(TwoDYtoIsoY (xOfPoint_TwoD,yOfPoint_TwoD));
			
			float xOfWereToPutATile_TwoD = IsoXtoTwoDX (xOfClosestCell_ISO, yOfClosestCell_ISO);
			float yOfWereToPutATile_TwoD = IsoYtoTwoDY (xOfClosestCell_ISO, yOfClosestCell_ISO);
			
			putItHereIn2D = new Vector3 (xOfWereToPutATile_TwoD,yOfWereToPutATile_TwoD,-5);
			putItHereInISO_x = Mathf.RoundToInt(xOfClosestCell_ISO);
			putItHereInISO_y = Mathf.RoundToInt(yOfClosestCell_ISO);
			
			mouseOccupant.transform.position = putItHereIn2D;
		}
	}
	
	void SetATile (GameObject thisTile) 
	{
		int x = putItHereInISO_x;
		int y = putItHereInISO_y;
		
		if (x<=editedMap.width-1 && x>=0 && y<=editedMap.height-1 && y>=0)
		{
			if (thisTile.name == "Grass")
			{
				GameManager.instance.currentMap.terrainArray [x,y] = 1;
			}
			else if (thisTile.name == "Bush") 
			{
				GameManager.instance.currentMap.terrainArray [x,y] = 2;
			}
			else if (thisTile.name == "Road") 
			{
				GameManager.instance.currentMap.terrainArray [x,y] = 3;
			}
			
			Destroy (tilesGOarray [x,y]);
			
			GameObject tile = Instantiate (thisTile, putItHereIn2D, Quaternion.identity)as GameObject;
			tile.transform.SetParent (terrainHolder.transform);
			tilesGOarray [x, y] = tile;
		}
	}
	
	void PlaceTheCam (){
	//	float xCoordTemp = IsoXtoTwoDX (editedMap.width / 2, editedMap.height / 2);
	//	float yCoordTemp = IsoYtoTwoDY (editedMap.width / 2, editedMap.height / 2);
	//	Camera.main.transform.position = new Vector3 (xCoordTemp, yCoordTemp, Camera.main.transform.position.z );
		Camera.main.transform.position = new Vector3 (0, 0, Camera.main.transform.position.z );
	}



	// Coordinates converters  ISO --> 2D
	//--------------------------------------------------------------------------------------------
	
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
	
	public float TwoDXtoIsoX(float isoxCoord, float isoyCoord) {
		float x;
		x = (isoxCoord - (isoyCoord - 0.25f )/0.5f);
		return x;
	}
	
	public float TwoDYtoIsoY(float isoxCoord, float isoyCoord) {
		float y;
		y = (isoxCoord + (isoyCoord + 0.25f )/0.5f);
		return y;
	}
	// Coordinates converters  ISO --> 2D
	//--------------------------------------------------------------------------------------------	

}









