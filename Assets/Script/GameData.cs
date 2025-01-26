using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom Objects/Game Data", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] private float oxygenLost;         // Oxygène perdu
    [SerializeField] private float oxygenRecovered;    // Oxygène récupéré
    [SerializeField] private int coinsCollected;       // Pièces collectées

    private float startTime;                           // Temps de début
    private float endTime;                             // Temps de fin
    private bool isTimerRunning;                      // Indique si le timer est actif

    // Getters
    public float OxygenLost => oxygenLost;
    public float OxygenRecovered => oxygenRecovered;
    public int CoinsCollected => coinsCollected;

    // Méthodes d'ajout
    public void AddOxygenLost(float amount)
    {
        oxygenLost += Mathf.Max(0, amount);
    }

    public void AddOxygenRecovered(float amount)
    {
        oxygenRecovered += Mathf.Max(0, amount);
    }

    public void AddCoins(int amount)
    {
        coinsCollected += Mathf.Max(0, amount);
    }

    // Gestion du timer
    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            startTime = Time.time; // Temps courant au moment du démarrage
        }
    }

    public void StopTimer()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            endTime = Time.time; // Temps courant au moment de l'arrêt
        }
    }

    public float GetPlayTime()
    {
        if (isTimerRunning)
        {
            // Si le timer est actif, on utilise l'heure courante comme fin temporaire
            return Time.time - startTime;
        }
        else
        {
            // Si le timer est arrêté, on retourne la différence entre startTime et endTime
            return endTime - startTime;
        }
    }

    // Méthode de réinitialisation
    public void ResetData()
    {
        oxygenLost = 0f;
        oxygenRecovered = 0f;
        coinsCollected = 0;
        startTime = 0f;
        endTime = 0f;
        isTimerRunning = false;
        StartTimer();
    }
}
