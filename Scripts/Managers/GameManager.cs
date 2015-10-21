using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


public class GameManager : MonoBehaviour {
	public static GameManager instance = null; 

///////////////////////////////////////////////////////////////////////////////////////////////////

// Tiles arrays ---------------------------------------------------------------------------------------------------
	public GameObject emptyTile;
	public GameObject[] grassTile;
	public GameObject[] waterTile;
	public GameObject[] snowTile;
	public GameObject[] roadTile;
// Map data ------------------------------------------------------------------------------------------------------------------
	public Map currentMap; 
	private MapDrawer mapDrawer;

// Interfaces ------------------------------------------------------------------------------------------------------------------
	public GameObject errorWindow;
	public Text errorWindowText;
// Saving and loading ------------------------------------------------------------------------------------------------------------------
	private Map mapFromSelectedFile;


///////////////////////////////////////////////////////////////////////////////////////////////////


	void Awake () 
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);    
		DontDestroyOnLoad(gameObject);
		currentMap = new Map ("NewMap", 0, 0, new int[1,1] ); 
		mapDrawer = GetComponent<MapDrawer>();
	}


///////////////////////////////////////////////////////////////////////////////////////////////////


	public void OpenCurrentMapInEditor () 
	{

		if (currentMap.width >= 10 && currentMap.height  >= 10) 
			Application.LoadLevel ("Editor");
		else
			Error ("Map must be at least 10x10 tiles");
	}

	public void Error (string description)
	{
		errorWindowText.text = description;
		errorWindow.SetActive (true);
	}

	public void SaveMap ()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath+"/"+currentMap.name+".map");
		
		bf.Serialize (file, currentMap);
		file.Close ();
	}
	public void DeleteMapFile (){DeleteFile (Application.persistentDataPath, currentMap.name, ".map");}

	void DeleteFile (string dir, string fileName, string fileExtention)	{File.Delete (dir+"/"+fileName+fileExtention);}

	private void SaveToFile (string fileName, string fileExtention)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath+"/"+fileName+".map");
		
		bf.Serialize (file, currentMap);
		file.Close ();
	}




////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	[Serializable]
	public class Map 
	{
		public string name;
		public int width;
		public int height;
		public int[,] terrainArray;
		public int[,] mModsArray;
		//	public GameObject[,] buildingsArray;
		//	public GameObject[,] unitsArray;
		
		public Map (string namE, int widtH, int heighT, int[,] terrainArraY)
		{
			name = namE;
			width = widtH;
			height = heighT;
			terrainArray = terrainArraY;
		}
		
	}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
















