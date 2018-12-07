using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MainUI;
    public GameObject Controller_Controls;
    public GameObject System_Instruction;

    public void Start()
    {
        Debug.Log("Init " + MainUI);
        Debug.Log("Init " + Controller_Controls);
        Debug.Log("Init " + System_Instruction);
    }
    public void UserInterface()
	{
        MainUI.SetActive(true);
        Controller_Controls.SetActive(false);
        System_Instruction.SetActive(false);
    }

	public void PlayGuided()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(current.name);
        SceneManager.LoadScene("Guided Scene");
    }

	public void PlayUnGuided()
	{
        Scene current = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(current.name);
        SceneManager.LoadScene("Unguided Scene");
    }
	public void SystemInstruction()
	{
        System_Instruction.SetActive(true);
        MainUI.SetActive(false);
        Controller_Controls.SetActive(false);
    }
	public void ControllerControls()
	{
        Controller_Controls.SetActive(true);
        MainUI.SetActive(false);
        System_Instruction.SetActive(false);
    }
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

}
