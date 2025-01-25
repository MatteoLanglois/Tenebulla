using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class PufferfishBehavior : MonoBehaviour
    {
        public Transform player; // Référence au joueur
        public float detectionRange = 30f; // Distance à laquelle le pufferfish détecte le joueur
        public float growScale = 10f; // Taille maximale lorsqu'il gonfle
        public float growSpeed = 2f; // Vitesse à laquelle il grossit
        public Color inflatedColor = Color.red; // Couleur lorsqu'il est gonflé
        public float blinkInterval = 0.2f; // Intervalle de clignotement
        public int blinkCount = 5; // Nombre de clignotements avant de grossir

        private Vector3 _originalScale; // Taille initiale
        private Color _originalColor; // Couleur initiale
        private new Renderer _renderer; // Renderer pour changer la couleur
        private bool _isBlinking = false; // Indique si le pufferfish clignote
        private bool _isGrowing = false; // Indique si le pufferfish est en train de grossir
        private bool _playerInRange = false; // Indique si le joueur est proche

        private void Start()
        {
            // Initialisation
            _originalScale = transform.localScale;
            _renderer = GetComponent<Renderer>();
            _originalColor = _renderer.material.color;
        }

        private void Update()
        {
            // Vérifier la distance entre le joueur et le pufferfish
            var distance = Vector3.Distance(player.position, transform.position);
            _playerInRange = distance <= detectionRange;

            // Si le joueur est dans la portée et que le pufferfish ne clignote pas déjà
            if (_playerInRange && !_isBlinking && !_isGrowing)
            {
                StartCoroutine(BlinkAndGrow());
            }

            // Si le joueur s'éloigne pendant le clignotement, annuler le processus
            if (_playerInRange || !_isBlinking) return;
            StopAllCoroutines(); // Arrête le clignotement
            ResetPufferfish(); // Remet le pufferfish à l'état initial
        }

        private IEnumerator BlinkAndGrow()
        {
            _isBlinking = true;

            // Faire clignoter le pufferfish
            for (var i = 0; i < blinkCount; i++)
            {
                _renderer.material.color = Color.clear; // Transparence
                yield return new WaitForSeconds(blinkInterval);
                _renderer.material.color = _originalColor; // Couleur originale
                yield return new WaitForSeconds(blinkInterval);

                // Si le joueur s'éloigne pendant le clignotement, arrêter
                if (_playerInRange) continue;
                ResetPufferfish();
                yield break;
            }

            // Une fois le clignotement terminé, commencer à gonfler
            _isBlinking = false;
            _isGrowing = true;

            // Changer la couleur et la taille
            _renderer.material.color = inflatedColor;
            var t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * growSpeed;
                transform.localScale = Vector3.Lerp(_originalScale, _originalScale * growScale, t);
                yield return null;
            }

            _isGrowing = false; // Fin du processus de gonflement
        }

        private void ResetPufferfish()
        {
            // Remettre la couleur et la taille originales
            _renderer.material.color = _originalColor;
            transform.localScale = _originalScale;

            _isBlinking = false;
            _isGrowing = false;
        }

        private void OnDrawGizmosSelected()
        {
            // Dessiner la portée de détection dans la scène
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}
