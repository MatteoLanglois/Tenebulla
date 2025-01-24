using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float moveForce = 5f;       // Force de déplacement appliquée
    public float maxSpeed = 3f;       // Vitesse maximale de la bulle
    public float waterResistance = 0.95f; // Réduction progressive de la vitesse pour simuler l'eau
    public float buoyancyFactor = 1f;     // Force de flottabilité
    public float buoyancyThreshold = 1f;  // Taille minimum pour que la bulle monte

    private Rigidbody rb;
    private Transform bubbleTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bubbleTransform = transform;
    }

    /// <summary>
    /// FixedUpdate est appelé à intervalles fixes et est indépendant du frame rate.
    /// Utilisé pour les calculs de physique.
    /// </summary>
    void FixedUpdate()
    {
        // Déplacement horizontal (entrées gauche/droite)
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 force = new Vector3(horizontalInput * moveForce, 0, 0);
        rb.AddForce(force);

        // Appliquer la résistance de l'eau
        rb.linearVelocity *= waterResistance;

        // Appliquer une force de flottabilité constante vers le haut
        rb.AddForce(Vector3.up * buoyancyFactor);

        // Limiter la vitesse maximale de la bulle
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Calculer la flottabilité en fonction de la taille de la bulle
        ApplyBuoyancy();
    }

    /// <summary>
    /// Applique la force de flottabilité à la bulle en fonction de sa taille.
    /// </summary>
    void ApplyBuoyancy()
    {
        // Obtenir la taille de la bulle (échelle Y)
        float bubbleSize = bubbleTransform.localScale.y;

        if (bubbleSize >= buoyancyThreshold)
        {
            // La bulle est assez grande pour flotter
            rb.AddForce(Vector3.up * buoyancyFactor * bubbleSize);
        }
        else
        {
            // La bulle est trop petite, elle descend
            rb.AddForce(Vector3.down * buoyancyFactor * (buoyancyThreshold - bubbleSize));
        }
    }
}