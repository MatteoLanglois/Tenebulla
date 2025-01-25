using UnityEngine;

namespace Script
{
    public class Bubble : MonoBehaviour
    {
        private GameObject _bubble;
        private Rigidbody _rb;
        private float _bubbleSize = 2f;
        private Transform _initialTransform;

        // Movement settings
        public float moveForce = 5f;       // Force de déplacement appliquée
        public float maxSpeed = 4f;       // Vitesse maximale de la bulle
        public float waterResistance = 0.95f; // Réduction progressive de la vitesse pour simuler l'eau
        public float buoyancyFactor = 1f;     // Force de flottabilité
        public float buoyancyThreshold = 0.75f;  // Taille minimum pour que la bulle monte

        // Dash settings
        public float dashForce = 50f; // Force du dash
        public float dashCooldown = 1f; // Temps entre deux dashs
        public float dashPrice = 30f; // Coût en vie du dash
        private bool _canDash = true;
        public AudioSource audioSource;

        // Life settings
        private const float MaxLife = 100f;
        public float life = 50f;
        private const float BleedRate = 0.05f;
        private const float MaxSize = 5f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _bubble = gameObject;
            _bubble.transform.localScale = new Vector3(_bubbleSize, _bubbleSize, _bubbleSize);
            _initialTransform = transform;
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
                Destroy(gameObject);
            }

            // Changer la taille de la bulle en fonction de sa vie
            _bubbleSize = (life / MaxLife) * MaxSize;
            _bubble.transform.localScale = new Vector3(_bubbleSize, _bubbleSize, _bubbleSize);

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

        private void PerformDash()
        {
            // Applique une force vers le haut
            var dashDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 1).normalized + Vector3.up;
            //_rb.linearVelocity = Vector3.zero; // Réinitialise toute la vitesse
            _rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            if (life > dashPrice + 1f)
            {
                life -= dashPrice;
            }
            else
            {
                life = 1f;
            }
            if (audioSource) {
                audioSource.Play();
            }

            // Empêche d'utiliser le dash pendant le cooldown
            _canDash = false;
            Invoke(nameof(ResetDash), dashCooldown);
        }

        private void ResetDash()
        {
            _canDash = true;
        }

        private void ApplyBuoyancy()
        {
            if (_bubbleSize >= buoyancyThreshold)
            {
                // La bulle est assez grande pour flotter
                var speed = _bubbleSize * buoyancyFactor;
                _rb.AddForce(Vector3.up * speed);
            }
            else
            {
                // La bulle est trop petite, elle descend
                var speed = _bubbleSize * buoyancyFactor * 3;
                _rb.AddForce(Vector3.down * speed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LifeOrb"))
            {

                var lifeOrb = other.gameObject.GetComponent<LifeOrb>();
                if (lifeOrb == null) return;
                life = Mathf.Min(life + lifeOrb.GetLifeAmount(), MaxLife);
                Destroy(other.gameObject);
            } else if (other.gameObject.CompareTag("Ennemy"))
            {
                life -= 20f;
                other.gameObject.SetActive(false);
            }
        }
    }
}