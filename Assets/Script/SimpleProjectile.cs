using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleShooter : MonoBehaviour
{
    public Rigidbody projectile; // R�f�rence au prefab du projectile
    public float projectileForce; // Force de projection
    public float defaultForce = 1f; // Force par d�faut si aucune n'est d�finie
    public TMP_Text forceText; // Affichage de la force sur l'UI
    public float delay = 0.5f; // D�lai entre les tirs

    public float fireRate = 2f; // Temps entre chaque tir (en secondes)

    private float nextFireTime; // Chronomètre interne pour le tir

    private IEnumerator coroutine; // Coroutine pour le délai

    void Start()
    {
        // Initialisation de la force de tir
        projectileForce = defaultForce;
        UpdateForceText();

        nextFireTime = Time.time; // Démarre immédiatement
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
        
        coroutine = ShooterDelay(delay);
        StartCoroutine(coroutine);
        
    }

    // Coroutine pour le d�lai de tir
    private IEnumerator ShooterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (projectile != null)
        {
            
            // Instanciation du projectile
            Rigidbody cloneProjectile = Instantiate(projectile, transform.position, transform.rotation);

            // Application de la force
            cloneProjectile.linearVelocity = Vector3.up * projectileForce/10f;
            Debug.Log("Direction : " + transform.forward + " | Vélocité : " + cloneProjectile.linearVelocity);

            // Destruction du projectile apr�s 5 secondes
            Destroy(cloneProjectile.gameObject, 2f);
        }

        // Fin de la coroutine
        coroutine = null;
    }

    void Update()
    {
        // Vérifie si le délai de tir est écoulé
        if (Time.time >= nextFireTime)
        {
            Shoot(); // Tire un projectile
            nextFireTime = Time.time + fireRate; // Planifie le prochain tir
        }
    }
}
