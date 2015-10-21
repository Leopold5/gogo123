using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[Serializable]
public class TerrainTypes 
{
	public Sprite icon;
	public string type;
	public GameObject[] allTerrainTilesOfCurrentType;
}

public class TerrainListWindow : MonoBehaviour 
{
	public TerrainTypes[] allTerrainTilesTypes;

	public Transform parentPanel;// <--- Assign in editor
	public GameObject buttonPrefabToBeAListElement;// <--- Assign in editor
		
	void Start () 
	{
		PopulateList ();
	}
		
	void PopulateList () 
	{
		foreach (TerrainTypes terrain in allTerrainTilesTypes) 
		{
			// we instanciate a button for each terrain type
			GameObject newTileButton = Instantiate (buttonPrefabToBeAListElement) as GameObject;
			newTileButton.transform.SetParent (parentPanel);
			// set this button to represent exact terrain
			ListButtonPropertiesScript buttonProperties_Script = newTileButton.GetComponent <ListButtonPropertiesScript> ();
			// TerrainPropertiesScript terrainProperties_Script = terrain.GetComponent <TerrainPropertiesScript> ();
			GameObject localTilePrefabHolder = terrain.allTerrainTilesOfCurrentType[0];

			buttonProperties_Script.nameLabel.text = terrain.type;
			buttonProperties_Script.icon.sprite = terrain.icon;
			buttonProperties_Script.button.onClick.AddListener 
				(delegate {WhatButtonShouldDo (localTilePrefabHolder);});
			print (localTilePrefabHolder.name);
		}	
	}
		
	public void WhatButtonShouldDo (GameObject currentTileType)
	{
		Destroy (EditorManager.instance.mouseOccupant);
		EditorManager.instance.mouseOccupant = null;
		EditorManager.instance.mouseOccupied = false;
		if (currentTileType.name == "Grass")
		{
			EditorManager.instance.activeTile = allTerrainTilesTypes[0].allTerrainTilesOfCurrentType [0];
			print (EditorManager.instance.activeTile.name);
		}
		if (currentTileType.name == "Bush")
			EditorManager.instance.activeTile = allTerrainTilesTypes[1].allTerrainTilesOfCurrentType [0];
		if (currentTileType.name == "Road") 
		{
			EditorManager.instance.activeTile = allTerrainTilesTypes[2].allTerrainTilesOfCurrentType [0];
			print (EditorManager.instance.activeTile.name);
		}
	}
}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
