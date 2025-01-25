using UnityEngine;

public class SardineShoalCreation : MonoBehaviour
{
    // Variables pour d�finir l'objet � instancier et la zone de spawn
    public GameObject sardineToSpawn; // L'objet � instancier
    public int numberOfObjects = 10; // Nombre d'objets � instancier

    // Zone de spawn
    public SardineZone zoneSardine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que votre joueur a le tag "Player"
        {
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // G�n�rer une position al�atoire dans la zone d�finie
            float randomX = Random.Range(zoneSardine.XMin, zoneSardine.XMax);
            float randomY = Random.Range(zoneSardine.YMin, zoneSardine.YMax);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            // Instancier l'objet � la position al�atoire
            Instantiate(sardineToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
