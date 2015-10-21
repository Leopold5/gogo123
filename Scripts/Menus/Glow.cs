using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;   


public class Glow : MonoBehaviour {

	private Renderer rend;

//	public float glowUpSpeed;
//	public float glowDownSpeed;
//	public int doubleBlowChance;
	
	
	private float timeAmountTillNextBlow = 0;
	private float nextBlowPower = 0.1f;
//	private float miniBlowPower = 0.1f;
	private float blowTime;
//	private bool doubleNextBlow = false;
	private bool blowStart = true;
	
//	public GameObject carrier;
//	public float smooth =1;
//	private Color colourAtBlowStart;
	private Color newColour;
//	private Color lerpedColor;

	
	
	private float red  = 0f ;
	private float green  = 0f ;
	private float blue  = 0f;
	private float alpha  = 1f ;
	private bool lightItUp = false;
	private float targetAmount = 0;
	private float tempGlow = 0;

	
	void Start () {
//		newColour = new Color(red, green, blue, alpha) ;
		rend = GetComponent<Renderer>();

	}
	
	//	void OnGUI(){
	//		GUI.Label(new Rect(10,10,100,200),"time.deltatime = "+ timeDotDeltatime);
	//		GUI.Label(new Rect(10,40,100,200),"time.FixedDeltatime = "+ timeDotFixedDeltatime);
	//	}
	
	void Update () 
	{
//		if (blowStart == true) 
//		{
//			StopCoroutine("LittleBlowsAndfire");
//			StartCoroutine("LittleBlowsAndfire");
//		}


		if (Time.time >= blowTime) 
		{
			StopCoroutine("Blow");
			StartCoroutine("Blow");
		}
	}
	
	//// Big blow
	IEnumerator Blow ()
	{
		while((rend.material.GetColor ("_TintColor").r < nextBlowPower) && lightItUp)
		{
			red += 0.1f;
			green += 0.1f;
			blue += 0.1f;
			newColour = new Color (red, green, blue, alpha);
			rend.material.SetColor ("_TintColor", newColour);
			yield return null;
		}

		lightItUp = false;

		while(rend.material.GetColor ("_TintColor").r > 0)
		{
			red -= 0.003f;
			green -= 0.003f;
			blue -= 0.003f;
			newColour = new Color (red, green, blue, alpha);
			rend.material.SetColor ("_TintColor", newColour);
			yield return null;
		}

		nextBlowPower = Random.Range (0.1f, 0.3f);
		timeAmountTillNextBlow = Random.Range (0, 4);
		blowTime = Time.time + timeAmountTillNextBlow;
		lightItUp = true;

	}

}

