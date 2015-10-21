using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ListOfMapFilesAsButtons : MonoBehaviour {

	public GameObject listHoldingPanel;
	public string filesExtension;
	public string filesDirectory;
	public GameObject fileButtonPrefab;
	public GameObject previewPositionHolder;
	public GameObject expandinpPanelPrefab;
	public int buttonsActionType;

	private GameObject listHolderGO;
	private GameObject preview;
	private Button activeButton;
	private GameManager.Map mapFromSelectedFile;


	void OnEnable()
	{
		filesDirectory = Application.persistentDataPath;
		PopulateFilesList (filesExtension, filesDirectory, listHolderGO);
	}

	void OnDisable ()
	{
		Destroy (listHolderGO);
		Destroy (preview);
	}



	public void PopulateFilesList (string fileExtension, string directory, GameObject filesContainer)
	{
		Destroy (listHolderGO);

		listHolderGO = Instantiate (expandinpPanelPrefab) as GameObject;
		listHolderGO.transform.SetParent (listHoldingPanel.transform, false);

		
		DirectoryInfo dir = new DirectoryInfo(directory);
		FileInfo[] info = dir.GetFiles("*"+fileExtension);

		foreach (FileInfo f in info) 
		{
			GameObject newFileButton = Instantiate (fileButtonPrefab) as GameObject;
			newFileButton.transform.SetParent (listHolderGO.transform, false);
			ListButtonPropertiesScript buttonProperties_Script = newFileButton.GetComponent <ListButtonPropertiesScript> ();
			buttonProperties_Script.nameLabel.text = f.Name;
			string fileName = f.Name;
			buttonProperties_Script.button.onClick.AddListener 
					(delegate {FileButtonListener (fileName, directory, buttonProperties_Script.button);});
			print (f.Name);
		}
	}
					public void FileButtonListener (string mapName, string directory, Button button)
					{
						//color part
						if (activeButton == null) {
							print ("Active button null");
							activeButton = button;
						}
						else 
						{
							activeButton.GetComponentInChildren<Text>().color = Color.black;
							activeButton = button;
						}
						button.GetComponentInChildren<Text>().color = Color.green;
						//color part

						BinaryFormatter bf = new BinaryFormatter();
						FileStream file = File.Open (directory+"/"+mapName, FileMode.Open);
						mapFromSelectedFile = (GameManager.Map)bf.Deserialize (file);
						file.Close ();

						if (preview != null)Destroy (preview);
							GameManager.instance.GetComponent<MapDrawer>().DrawTerrainPreview (mapFromSelectedFile, previewPositionHolder);
							preview = GameManager.instance.GetComponent<MapDrawer>().previewHoldingGO;
							GameManager.instance.currentMap = mapFromSelectedFile;
					}
}
































