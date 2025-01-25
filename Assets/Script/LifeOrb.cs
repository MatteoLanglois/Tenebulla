using UnityEngine;

namespace Script
{
    public class LifeOrb : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private Rigidbody _rb;

        // Life orb settings
        public const float MaxLife = 15f; // Vie maximale de l'orbe
        public float minSize = 0.5f; // Taille minimale de l'orbe
        public float maxSize = 2f;   // Taille maximale de l'orbe
        private float _size; // Taille de l'orbe

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null)
            {
                _rb = gameObject.AddComponent<Rigidbody>();
            }
            _rb.useGravity = false;

            _size = Random.Range(minSize, maxSize);
            transform.localScale = new Vector3(_size, _size, _size);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public float GetLifeAmount()
        {
            return _size * MaxLife;
        }
    }
}
