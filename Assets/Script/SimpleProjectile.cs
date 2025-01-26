using System.Collections;
using TMPro;
using UnityEngine;

namespace Script
{
    public class SimpleShooter : MonoBehaviour
    {
        public Rigidbody projectile; // Reference au prefab du projectile
        public float projectileForce; // Force de projection
        private const float DefaultForce = 5f; // Force par defaut si aucune n'est definie
        public TMP_Text forceText; // Affichage de la force sur l'UI
        public float delay = 0.5f; // Delai entre les tirs

        private float _nextFireTime; // Chronomètre interne pour le tir

        private void Start()
        {
            // Initialisation de la force de tir
            projectileForce = DefaultForce;
            UpdateForceText();

            _nextFireTime = Time.time; // Démarre immédiatement
        }

        // Augmenter la force de tir
        public void MoreForce()
        {
            projectileForce = Mathf.Clamp(projectileForce + 5f, 5f, 150f);
            UpdateForceText();
        }

        // Reduire la force de tir
        public void LessForce()
        {
            projectileForce = Mathf.Clamp(projectileForce - 5f, 5f, 150f);
            UpdateForceText();
        }

        // Met e jour l'affichage de la force
        private void UpdateForceText()
        {
            if (forceText != null)
            {
                forceText.text = projectileForce.ToString("F1"); // Format avec une decimale
            }
        }

        // Methode pour tirer
        private void Shoot()
        {
            StartCoroutine(ShooterDelay(delay));
        }

        // Coroutine pour le delai de tir
        private IEnumerator ShooterDelay(float delayParam)
        {
            yield return new WaitForSeconds(delayParam);

            if (!projectile) yield break;
            // Instanciation du projectile
            var cloneProjectile = Instantiate(projectile, transform.position, transform.rotation);

            // Calcul de la direction en fonction de la rotation de l'objet
            var shootDirection = transform.forward;

            // Application de la force
            cloneProjectile.linearVelocity = shootDirection * projectileForce;

            // Destruction du projectile apres 5 secondes
            Destroy(cloneProjectile.gameObject, 5f);
        }

        private void Update()
        {
            // Vérifie si le délai de tir est écoulé
            if (!(Time.time >= _nextFireTime)) return;

            Shoot(); // Tire un projectile
            _nextFireTime = Time.time + delay; // Planifie le prochain tir
        }
    }
}