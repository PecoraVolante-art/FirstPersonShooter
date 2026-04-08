using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Creazione dei bottoni per caricare le scene
    public void Playbutton()// Carica scena livello 1 
    {
        SceneManager.LoadScene("FPS");
    }

    public void Goback_Menubutton()// Carica scena Menu
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
