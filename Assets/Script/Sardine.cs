using UnityEngine;

public class SardineMovement : MonoBehaviour
{
    // Définir la zone avec xDepart, xFin, yDebut, yFin
    public float xDepart = -30f; // Limite gauche de la zone
    public float xFin = 30f;     // Limite droite de la zone
    public float yDebut = 20f;  // Limite basse de la zone
    public float yFin = 60f;     // Limite haute de la zone

    public float speed = 6f; // Vitesse de déplacement
    public float changeDirectionInterval = 2f; // Temps entre chaque changement de direction

    private Vector2 randomDirection; // Direction aléatoire actuelle (2D)
    private float timer; // Chronomètre pour changer de direction

    void Start()
    {
        xDepart = -30f; // Limite gauche de la zone
        xFin = 30f;     // Limite droite de la zone
        yDebut = 20f;  // Limite basse de la zone
        yFin = 60f;

        // Initialisation de la première direction aléatoire
        randomDirection = GetRandomDirection();
    }

    void Update()
    {
        // Déplace la sardine dans la direction aléatoire
        Vector3 movement = new Vector3(randomDirection.x, randomDirection.y, 0f) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Compte le temps écoulé
        timer += Time.deltaTime;

        // Change de direction après l'intervalle défini
        if (timer >= changeDirectionInterval)
        {
            randomDirection = GetRandomDirection();
            timer = 0f;
        }

        // Garde la sardine dans la zone définie
        KeepInBounds();
    }

    private Vector2 GetRandomDirection()
    {
        // Génère une direction aléatoire (normale) sur X et Y
        Vector2 randomDir = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
        return randomDir.normalized; // Retourne une direction avec une magnitude de 1
    }

    private void KeepInBounds()
    {
        Vector3 pos = transform.position;


        // Vérifie si la sardine est hors de la zone
        bool isOutOfBounds = false;

        // Si la sardine est en dehors de la zone sur l'axe X
        if (pos.x < xDepart || pos.x > xFin)
        {
            isOutOfBounds = true;
            // Change de direction pour ramener la sardine dans la zone (opposé de la direction actuelle)
            randomDirection.x = (pos.x < xDepart) ? 1f : -1f;
            timer = 0f;
        }

        // Si la sardine est en dehors de la zone sur l'axe Y
        if (pos.y < yDebut || pos.y > yFin)
        {
            isOutOfBounds = true;
            // Change de direction pour ramener la sardine dans la zone (opposé de la direction actuelle)
            randomDirection.y = (pos.y < yDebut) ? 1f : -1f;
            timer = 0f;
        }

        

        // Limite la position de la sardine pour rester dans la zone
        //pos.x = Mathf.Clamp(pos.x, xDepart, xFin);
        //pos.y = Mathf.Clamp(pos.y, yDebut, yFin);

        //transform.position = pos;
    }
}
