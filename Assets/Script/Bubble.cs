using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Bubble : MonoBehaviour
    {
        private GameObject _bubble;
        private Rigidbody _rb;
        public float bubbleSize = 1f;
        private Transform _initialTransform;

        // Movement settings
        public float moveForce = 3f;       // Force de déplacement appliquée
        public float maxSpeed = 200f;       // Vitesse maximale de la bulle
        public float waterResistance = 0.95f; // Réduction progressive de la vitesse pour simuler l'eau
        public float buoyancyFactor = 4f;     // Force de flottabilité
        public float buoyancyThreshold = 0.25f;  // Taille minimum pour que la bulle monte

        // Dash settings
        public float dashForce = 50f; // Force du dash
        public float dashCooldown = 1f; // Temps entre deux dashs
        public float dashPrice = 30f; // Coût en vie du dash
        private bool _canDash = true;
        public AudioSource audioSource;
        public ParticleSystem dashAnimation;

        // Life settings
        private const float MaxLife = 100f;
        public float life = 50f;
        private const float BleedRate = 0.05f;
        private const float MaxSize = 2f;

        // Death screen
        public GameObject deathScreen;
        public Button retryButton;
        private Rigidbody _rigidbody;

        // Global Data
        public GameData gameData;

        private void Start()
        {
            _bubble = gameObject;
            _rb = _bubble.GetComponent<Rigidbody>();
            _bubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, bubbleSize);
            _initialTransform = transform;
            retryButton.onClick.AddListener(RestartGame);
            gameData.ResetData();
        }

        private void Update()
        {
            // Déplacement horizontal (entrées gauche/droite)
            var horizontalInput = Input.GetAxis("Horizontal");

            // Arrondir la valeur de horizontalInput à zéro si elle est très proche de zéro
            if (Mathf.Approximately(horizontalInput, 0f))
            {
                horizontalInput = 0f;
            }

            var force = new Vector3(horizontalInput * moveForce, 0, 0);
            _rb.AddForce(force);

            if (Input.GetKeyDown(KeyCode.Space) && _canDash)
            {
                PerformDash();
            }

            _bubble.transform.position = new Vector3(_bubble.transform.position.x, _bubble.transform.position.y, _initialTransform.position.z);
        }

        private void FixedUpdate()
        {
            // Faire perdre de la vie à la bulle
            life -= BleedRate;

            if (life <= 0)
            {
                // La bulle est morte
                _bubble.SetActive(false);
                if (!_rigidbody)
                {
                    _rigidbody = GetComponent<Rigidbody>();
                }
                _rigidbody.linearVelocity = Vector3.zero;
                deathScreen.SetActive(true);
            }

            // Changer la taille de la bulle en fonction de sa vie
            bubbleSize = (life / MaxLife) * MaxSize;
            _bubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, bubbleSize);

            // Appliquer la résistance de l'eau
            _rb.linearVelocity *= waterResistance;

            // Appliquer une force de flottabilité constante vers le haut
            _rb.AddForce(Vector3.up * buoyancyFactor);

            // Limiter la vitesse maximale de la bulle
            if (_rb.linearVelocity.magnitude > maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
            }

            // Calculer la flottabilité en fonction de la taille de la bulle
            ApplyBuoyancy();
        }

        private void RestartGame()
        {
            Time.timeScale = 1f;
            gameData.ResetData();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void PerformDash()
        {
            Debug.Log("Dash sound");
            if (!audioSource.enabled)
            {
                audioSource.enabled = true;
            }
            audioSource.volume = 0.5f;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
            var dashDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 1).normalized + Vector3.up;
            _rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            StartCoroutine(DashAnimation());

            if (life > dashPrice + 1f)
            {
                life -= dashPrice;
            }
            else
            {
                life = 1f;
            }

            _canDash = false;
            Invoke(nameof(ResetDash), dashCooldown);
        }

        private IEnumerator DashAnimation()
        {
            const float duration = 0.5f; // Duration of the animation
            var elapsed = 0f;

            var originalScale = _bubble.transform.localScale;
            var dashScale = new Vector3(bubbleSize, bubbleSize, bubbleSize / 2);

            dashAnimation.Play();

            // Scale down
            while (elapsed < duration)
            {
                _bubble.transform.localScale = Vector3.Lerp(originalScale, dashScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _bubble.transform.localScale = dashScale;

            // Wait for a short period
            yield return new WaitForSeconds(0.3f);

            elapsed = 0f;

            // Scale back up
            while (elapsed < duration)
            {
                _bubble.transform.localScale = Vector3.Lerp(dashScale, originalScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _bubble.transform.localScale = originalScale;
            dashAnimation.Stop();
        }
        private void ResetDash()
        {
            _canDash = true;
        }

        private void ApplyBuoyancy()
        {
            if (bubbleSize >= buoyancyThreshold)
            {
                // La bulle est assez grande pour flotter
                var speed = bubbleSize * buoyancyFactor * 2;
                _rb.AddForce(Vector3.up * speed);
            }
            else
            {
                // La bulle est trop petite, elle descend
                var speed = bubbleSize * buoyancyFactor * 3;
                _rb.AddForce(Vector3.down * speed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LifeOrb"))
            {

                var lifeOrb = other.gameObject.GetComponent<LifeOrb>();
                if (lifeOrb == null) return;

                float lifeReceved = 0f;
                if (life + lifeOrb.GetLifeAmount()<= MaxLife)
                {
                    lifeReceved = lifeOrb.GetLifeAmount();
                } else
                {
                    lifeReceved = MaxLife - life;
                }
                life = life + lifeReceved;//Mathf.Min(life + lifeOrb.GetLifeAmount(), MaxLife);

                gameData.AddOxygenRecovered(lifeReceved);

                Destroy(other.gameObject);
            } else if (other.gameObject.CompareTag("Enemy"))
            {
                float damage = 20f;
                life -= damage;
                gameData.AddOxygenLost(damage);

                other.gameObject.SetActive(false);
            } else if (other.gameObject.CompareTag("Rock"))
            {
                float damage = 10f;
                life -= 10f;
                gameData.AddOxygenLost(damage);
            }
        }
    }
}