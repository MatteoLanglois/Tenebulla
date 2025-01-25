using System.Collections;
using TMPro;
using UnityEngine;

namespace Script
{
    public class SimpleShooter : MonoBehaviour
    {
        public Rigidbody projectile; // Reference au prefab du projectile
        public float projectileForce; // Force de projection
        public float defaultForce = 1f; // Force par defaut si aucune n'est definie
        public TMP_Text forceText; // Affichage de la force sur l'UI
        public float delay = 0.5f; // Delai entre les tirs

        public float fireRate = 2f; // Temps entre chaque tir (en secondes)

        private float _nextFireTime; // Chronomètre interne pour le tir

        private IEnumerator _coroutine; // Coroutine pour le délai

        void Start()
        {
            // Initialisation de la force de tir
            projectileForce = defaultForce;
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

            _coroutine = ShooterDelay(delay);
            StartCoroutine(_coroutine);

        }

        // Coroutine pour le delai de tir
        private IEnumerator ShooterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (projectile)
            {

                // Instanciation du projectile
                var cloneProjectile = Instantiate(projectile, transform.position, transform.rotation);

                // Application de la force
                cloneProjectile.linearVelocity = Vector3.up * projectileForce/10f;

                // Destruction du projectile apres 5 secondes
                Destroy(cloneProjectile.gameObject, 3f);
            }

            // Fin de la coroutine
            _coroutine = null;
        }

        void Update()
        {
            // Vérifie si le délai de tir est écoulé
            if (!(Time.time >= _nextFireTime)) return;

            Shoot(); // Tire un projectile
            _nextFireTime = Time.time + fireRate; // Planifie le prochain tir
        }
    }
}
