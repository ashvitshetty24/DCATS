using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Start()
    {
        SceneManager.LoadScene("UserInterface");
    }
    public void UserInterface()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene("UserInterface");
        SceneManager.UnloadSceneAsync(current.name);
    }

	public void PlayGuided()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Guided Scene");
        SceneManager.UnloadSceneAsync(current.name);
    }

	public void PlayUnGuided()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Unguided Scene");
        SceneManager.UnloadSceneAsync(current.name);
    }
	public void SystemInstruction()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene("System Instruction");
        SceneManager.UnloadSceneAsync(current.name);
    }
	public void ControllerControls()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Controller Controls");
        SceneManager.UnloadSceneAsync(current.name);
    }
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

}
