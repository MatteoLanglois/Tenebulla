using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleShooter : MonoBehaviour
{
    public Rigidbody projectile; // R�f�rence au prefab du projectile
    [Range(5f, 150f)] public float projectileForce; // Force de projection
    public float defaultForce = 50f; // Force par d�faut si aucune n'est d�finie
    public TMP_Text forceText; // Affichage de la force sur l'UI
    public float delay = 0.5f; // D�lai entre les tirs

    private IEnumerator coroutine; // Coroutine pour le d�lai

    void Start()
    {
        // Initialisation de la force de tir
        projectileForce = defaultForce;
        UpdateForceText();

        Shoot();
    }

    // Augmenter la force de tir
    public void MoreForce()
    {
        projectileForce = Mathf.Clamp(projectileForce + 5f, 5f, 150f);
        UpdateForceText();
    }

    // R�duire la force de tir
    public void LessForce()
    {
        projectileForce = Mathf.Clamp(projectileForce - 5f, 5f, 150f);
        UpdateForceText();
    }

    // Met � jour l'affichage de la force
    private void UpdateForceText()
    {
        if (forceText != null)
        {
            forceText.text = projectileForce.ToString("F1"); // Format avec une d�cimale
        }
    }

    // M�thode pour tirer
    public void Shoot()
    {
        if (coroutine == null) // �vite de d�marrer plusieurs fois la coroutine
        {
            Debug.Log("aled2");
            coroutine = ShooterDelay(delay);
            StartCoroutine(coroutine);
        }
    }

    // Coroutine pour le d�lai de tir
    private IEnumerator ShooterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("aled1");
        if (projectile != null)
        {
            
            // Instanciation du projectile
            Rigidbody cloneProjectile = Instantiate(projectile, transform.position, transform.rotation);

            // Application de la force
            cloneProjectile.linearVelocity = Vector3.up * projectileForce;
            Debug.Log("Direction : " + transform.forward + " | Vélocité : " + cloneProjectile.linearVelocity);

            // Destruction du projectile apr�s 5 secondes
            //Destroy(cloneProjectile.gameObject, 5f);
        }

        // Fin de la coroutine
        coroutine = null;
    }
}
