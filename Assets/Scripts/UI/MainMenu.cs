using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public string URL;
	public void OnPlayButton()
	{ 
		SceneManager.LoadScene(1);
	}
	public void Shop()
	{
		SceneManager.LoadScene(2);
	}
	public void OnExitButton()
	{
		Application.Quit();
	}
	public void OnUrlButton()
	{
		Application.OpenURL(URL);	
	}
}
