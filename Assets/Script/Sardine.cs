using UnityEngine;

public class SardineMovement : MonoBehaviour
{
    // D�finir la zone avec xDepart, xFin, yDebut, yFin
    public float xDepart = -30f; // Limite gauche de la zone
    public float xFin = 30f;     // Limite droite de la zone
    public float yDebut = 20f;  // Limite basse de la zone
    public float yFin = 60f;     // Limite haute de la zone

    public float speed = 6f; // Vitesse de d�placement
    public float changeDirectionInterval = 2f; // Temps entre chaque changement de direction

    private Vector2 randomDirection; // Direction al�atoire actuelle (2D)
    private float timer; // Chronom�tre pour changer de direction

    void Start()
    {
        xDepart = -30f; // Limite gauche de la zone
        xFin = 30f;     // Limite droite de la zone
        yDebut = 20f;  // Limite basse de la zone
        yFin = 60f;

        // Initialisation de la premi�re direction al�atoire
        randomDirection = GetRandomDirection();
    }

    void Update()
    {
        // D�place la sardine dans la direction al�atoire
        Vector3 movement = new Vector3(randomDirection.x, randomDirection.y, 0f) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Compte le temps �coul�
        timer += Time.deltaTime;

        // Change de direction apr�s l'intervalle d�fini
        if (timer >= changeDirectionInterval)
        {
            randomDirection = GetRandomDirection();
            timer = 0f;
        }

        // Garde la sardine dans la zone d�finie
        KeepInBounds();
    }

    private Vector2 GetRandomDirection()
    {
        // G�n�re une direction al�atoire (normale) sur X et Y
        Vector2 randomDir = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
        return randomDir.normalized; // Retourne une direction avec une magnitude de 1
    }

    private void KeepInBounds()
    {
        Vector3 pos = transform.position;


        // V�rifie si la sardine est hors de la zone
        bool isOutOfBounds = false;

        // Si la sardine est en dehors de la zone sur l'axe X
        if (pos.x < xDepart || pos.x > xFin)
        {
            isOutOfBounds = true;
            // Change de direction pour ramener la sardine dans la zone (oppos� de la direction actuelle)
            randomDirection.x = (pos.x < xDepart) ? 1f : -1f;
            timer = 0f;
        }

        // Si la sardine est en dehors de la zone sur l'axe Y
        if (pos.y < yDebut || pos.y > yFin)
        {
            isOutOfBounds = true;
            // Change de direction pour ramener la sardine dans la zone (oppos� de la direction actuelle)
            randomDirection.y = (pos.y < yDebut) ? 1f : -1f;
            timer = 0f;
        }

        

        // Limite la position de la sardine pour rester dans la zone
        //pos.x = Mathf.Clamp(pos.x, xDepart, xFin);
        //pos.y = Mathf.Clamp(pos.y, yDebut, yFin);

        //transform.position = pos;
    }
}
