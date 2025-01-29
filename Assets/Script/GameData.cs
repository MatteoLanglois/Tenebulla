using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom Objects/Game Data", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] private float oxygenLost;         // Oxyg�ne perdu
    [SerializeField] private float oxygenRecovered;    // Oxyg�ne r�cup�r�
    [SerializeField] private int[] coinsCollected;       // Pi�ces collect�es

    private float startTime;                           // Temps de d�but
    private float endTime;                             // Temps de fin
    private bool isTimerRunning;                      // Indique si le timer est actif

    // Getters
    public float OxygenLost => oxygenLost;
    public float OxygenRecovered => oxygenRecovered;
    public int[] CoinsCollected => coinsCollected;

    // M�thodes d'ajout
    public void AddOxygenLost(float amount)
    {
        oxygenLost += Mathf.Max(0, amount);
    }

    public void AddOxygenRecovered(float amount)
    {
        oxygenRecovered += Mathf.Max(0, amount);
    }

    public void AddCoins(int id)
    {
        coinsCollected[id] = 1;
    }

    // Gestion du timer
    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            startTime = Time.time; // Temps courant au moment du d�marrage
        }
    }

    public void StopTimer()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            endTime = Time.time; // Temps courant au moment de l'arr�t
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
            // Si le timer est arr�t�, on retourne la diff�rence entre startTime et endTime
            return endTime - startTime;
        }
    }

    // M�thode de r�initialisation
    public void ResetData()
    {
        oxygenLost = 0f;
        oxygenRecovered = 0f;
        coinsCollected = new int[3];
        startTime = 0f;
        endTime = 0f;
        isTimerRunning = false;
        StartTimer();
    }
}
