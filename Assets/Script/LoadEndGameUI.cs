using UnityEngine;
using TMPro;
using System.Collections;

public class LoadEndGameUI : MonoBehaviour
{
    [Header("R�f�rences GameData")]
    public GameData gameData; 

    [Header("R�f�rences TextMeshPro")]
    public TMP_Text oxygenLostText;
    public TMP_Text oxygenRecoveredText;
    public TMP_Text playtimeText;

    void Start()
    {
        if (gameData == null)
        {
            Debug.LogError("GameData n'est pas assign� !");
            return;
        }

        gameData.StopTimer();
        // Mise � jour initiale des valeurs
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (gameData == null) return;

        // Met � jour chaque champ de texte avec les valeurs actuelles de GameData
        if (oxygenLostText != null) {
            
            oxygenLostText.text = $"{gameData.OxygenLost:F1}";
    }

        if (oxygenRecoveredText != null)
            oxygenRecoveredText.text = $"{gameData.OxygenRecovered:F1}";

        if (playtimeText != null)
            playtimeText.text = $"{gameData.GetPlayTime():F1}s";
        
    }
}
