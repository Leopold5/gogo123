using UnityEngine;
using System.Collections;

public class StartTurningOn : MonoBehaviour {

	public GameObject background;
	public GameObject welcomeToMapgeneratorPanel;
	public GameObject newMapPanel;
	public GameObject openMapPanel;

	// Use this for initialization
	void Start () {
		background.SetActive (true);
		welcomeToMapgeneratorPanel.SetActive (true);
		newMapPanel.SetActive (false);
		openMapPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
