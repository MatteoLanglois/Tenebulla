using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class PufferfishBehavior : MonoBehaviour
    {
        public Transform player; // Référence au joueur
        public float detectionRange = 30f; // Distance à laquelle le pufferfish détecte le joueur
        public GameObject inflatedFish;
        public float blinkInterval = 0.2f; // Intervalle de clignotement
        public int blinkCount = 5; // Nombre de clignotements avant de grossir
        private Vector3 _movementCenter; // Centre du mouvement aléatoire
        public float movementRadius = 5f; // Rayon du mouvement aléatoire
        public float movementSpeed = 1f; // Vitesse du mouvement aléatoire
        private GameObject _pufferfish; // Référence au pufferfish

        private Vector3 _originalScale; // Taille initiale
        private Color _originalColor; // Couleur initiale
        private Renderer _renderer; // Renderer pour changer la couleur
        private bool _isBlinking = false; // Indique si le pufferfish clignote
        private bool _isGrowing = false; // Indique si le pufferfish est en train de grossir
        private Vector3 _randomTarget; // Cible aléatoire pour le mouvement

        private void Start()
        {
            // Initialisation
            _pufferfish = gameObject;
            _originalScale = transform.localScale;
            _renderer = GetComponent<Renderer>();
            _originalColor = _renderer.material.color;
            _movementCenter = transform.position;
            inflatedFish.SetActive(false); // Ensure inflatedFish is initially inactive
            SetRandomTarget(); // Définir une première cible aléatoire
        }

        private void Update()
        {
            // Vérifier la distance entre le joueur et le pufferfish
            var distance = Vector3.Distance(player.position, transform.position);
            var playerInRange = distance <= detectionRange;

            // Si le joueur est dans la portée et que le pufferfish ne clignote pas déjà
            if (playerInRange && !_isBlinking && !_isGrowing)
            {
                Debug.Log("Player in range");
                StartCoroutine(BlinkAndGrow());
            }

            // Déplacer le pufferfish vers la cible aléatoire
            MoveTowardsRandomTarget();

            // Si le joueur s'éloigne pendant le clignotement, annuler le processus
            if (!playerInRange && _isBlinking)
            {
                StopAllCoroutines(); // Arrête le clignotement
                ResetPufferfish(); // Remet le pufferfish à l'état initial
            }
        }

        private void MoveTowardsRandomTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, _randomTarget, movementSpeed * Time.deltaTime);

            // Si le pufferfish atteint la cible, définir une nouvelle cible aléatoire
            if (Vector3.Distance(transform.position, _randomTarget) < 0.1f)
            {
                SetRandomTarget();
            }
        }

        private void SetRandomTarget()
        {
            var randomDirection = Random.insideUnitSphere * movementRadius;
            randomDirection += _movementCenter;
            randomDirection.z = transform.position.z; // Garder la même profondeur
            _randomTarget = randomDirection;
        }

        private IEnumerator BlinkAndGrow()
        {
            _isBlinking = true;

            // Faire clignoter le pufferfish
            for (var i = 0; i < blinkCount; i++)
            {
                transform.localScale = _originalScale * 1.1f; // Taille agrandie
                yield return new WaitForSeconds(blinkInterval);
                transform.localScale = _originalScale; // Taille originale
                yield return new WaitForSeconds(blinkInterval);

                // Si le joueur s'éloigne pendant le clignotement, arrêter
                if (Vector3.Distance(player.position, transform.position) <= detectionRange) continue;
                ResetPufferfish();
                yield break;
            }

            // Une fois le clignotement terminé, commencer à gonfler
            _isBlinking = false;
            _isGrowing = true;

            // Changer la couleur et la taille
            inflatedFish.SetActive(true);
            // Wait few seconds
            yield return new WaitForSeconds(4f);
            ResetPufferfish();
        }

        private void ResetPufferfish()
        {
            // Remettre la couleur et la taille originales
            inflatedFish.SetActive(false);
            transform.localScale = _originalScale;

            _isBlinking = false;
            _isGrowing = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _pufferfish.SetActive(true);
            inflatedFish.SetActive(true);
        }
    }
}