using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void UserInterface()
	{
		SceneManager.LoadScene(0);
	}

	public void PlayGuided()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void PlayUnGuided()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}
	public void SystemInstruction()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
	}
	public void ControllerControls()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
	}
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

}
