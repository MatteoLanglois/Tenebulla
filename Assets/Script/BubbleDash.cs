using UnityEngine;

public class BubbleDash : MonoBehaviour
{
    public float dashForce = 10f; // Force du dash
    public float dashCooldown = 1f; // Temps entre deux dashs
    public Rigidbody rb;
    private bool canDash = true;
    public AudioSource audioSource;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Debug.Log("Dash!");
            PerformDash();
        }
    }

    void PerformDash()
    {
        // Applique une force vers le haut
        Vector3 dashDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 1).normalized;
        rb.linearVelocity = Vector3.zero; // Réinitialise toute la vitesse
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
        audioSource.Play();

        // Empêche d'utiliser le dash pendant le cooldown
        canDash = false;
        Invoke(nameof(ResetDash), dashCooldown);
    }

    void ResetDash()
    {
        canDash = true;
    }
}