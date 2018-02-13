using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundControl : MonoBehaviour
{



	//public GameObject audioOnIcon;
	public GameObject audioOffIcon;
	public AudioSource source;

	public void Start()
	{

		SetSoundState ();
	}
	public void ToggleSound()
	{
		if (PlayerPrefs.GetInt ("Muted", 0) == 0) 
		{
			PlayerPrefs.SetInt ("Muted", 1);
		} 
		else 
		{
			PlayerPrefs.SetInt ("Muted", 0);
		}
		SetSoundState ();
	}

	private void SetSoundState()
	{
		if (PlayerPrefs.GetInt ("Muted", 0) == 0) 
		{
			source.volume = 1;
			//audioOnIcon.SetActive (true);
			audioOffIcon.SetActive (false);
		} 
		else 
		{
			source.volume = 0;
			//audioOnIcon.SetActive (true);
			audioOffIcon.SetActive (true);
		}
	}
}
