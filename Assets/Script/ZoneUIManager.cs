using UnityEngine;
using TMPro;  

public class ZoneTrigger : MonoBehaviour
{
    // Référence au texte de l'UI où le nom de la zone sera affiché
    public TMP_Text zoneText;

    // Quand un joueur entre dans la zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que votre joueur a le tag "Player"
        {
            // Affiche le nom de la zone dans l'UI
            zoneText.text = gameObject.name;
            zoneText.gameObject.SetActive(true); // Affiche le texte
        }
    }
    
    // Quand un joueur quitte la zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que votre joueur a le tag "Player"
        {
            // Masque le texte quand le joueur quitte la zone
            zoneText.gameObject.SetActive(false);
        }
    }
}
