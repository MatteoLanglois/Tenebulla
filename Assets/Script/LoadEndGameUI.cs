using UnityEngine;
using TMPro;
using System.Collections;

public class LoadEndGameUI : MonoBehaviour
{
    [Header("Références GameData")]
    public GameData gameData; 

    [Header("Références TextMeshPro")]
    public TMP_Text oxygenLostText;
    public TMP_Text oxygenRecoveredText;
    public TMP_Text playtimeText;

    void Start()
    {
        if (gameData == null)
        {
            Debug.LogError("GameData n'est pas assigné !");
            return;
        }

        gameData.StopTimer();
        // Mise à jour initiale des valeurs
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (gameData == null) return;

        // Met à jour chaque champ de texte avec les valeurs actuelles de GameData
        if (oxygenLostText != null) {
            
            oxygenLostText.text = $"{gameData.OxygenLost:F1}";
    }

        if (oxygenRecoveredText != null)
            oxygenRecoveredText.text = $"{gameData.OxygenRecovered:F1}";

        if (playtimeText != null)
            playtimeText.text = $"{gameData.GetPlayTime():F1}s";
        
    }
}
