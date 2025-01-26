using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoEndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que votre joueur a le tag "Player"
        {
            SceneManager.LoadScene("EndGameScene");
        }
    }
}
