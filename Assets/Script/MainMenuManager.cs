using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void PlayGame()
    {
        // Charge la sc�ne de jeu 
        SceneManager.LoadScene("GameScene");
    }

    public void OpenOptions()
    {
        // Affiche les options (tu peux utiliser un panneau d'options s�par�)
        Debug.Log("Options Button Pressed!");
    }

    public void QuitGame()
    {
        // Quitte l'application
        Debug.Log("Quitter le jeu !");
        Application.Quit();
    }
}
