using UnityEngine;

public class SardineShoalCreation : MonoBehaviour
{
    // Variables pour définir l'objet à instancier et la zone de spawn
    public GameObject sardineToSpawn; // L'objet à instancier
    public int numberOfObjects = 10; // Nombre d'objets à instancier

    // Définir les limites de la zone sur les axes X et Y
    public float xMin = -50f;
    public float xMax = 50f;
    public float yMin = 400f;
    public float yMax = 600f;

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
            // Générer une position aléatoire dans la zone définie
            float randomX = Random.Range(xMin, xMax);
            float randomY = Random.Range(yMin, yMax);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            // Instancier l'objet à la position aléatoire
            Instantiate(sardineToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
