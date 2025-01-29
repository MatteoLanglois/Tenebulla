using UnityEngine;
using System.Collections;

namespace Script
{
    public class MureneDeplacement : MonoBehaviour
    {
        public float longueur = 3;
        public float speed = 1;
        public float cooldown = 1;
        public float attente = 2;
        public AudioSource attackSound;
        public GameObject spawner;

        private bool _isExtending;
        private Vector3 _startPosition;
        private Vector3 _movementDirection;
        private float _timer;
        private Animator _animator;
        private int _animationHash;
        private bool _animationLaunched;

        private void Start()
        {
            _startPosition = transform.position;
            InitializeMovementDirection();
            _animator = GetComponent<Animator>();
            _animationHash = Animator.StringToHash(_animator.runtimeAnimatorController.animationClips[0].name);

            if (spawner.transform.eulerAngles.z is > 90 and < 270)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
            }

            StartCoroutine(Move());
        }

        private void OnValidate()
        {
            InitializeMovementDirection();
        }

        private void InitializeMovementDirection()
        {
            var direction = new Vector3(speed, 0, 0);
            var angleZ = spawner.transform.eulerAngles.z;
            var rotation = Quaternion.Euler(0, 0, angleZ);
            _movementDirection = -(rotation * direction);
        }

        private IEnumerator Move()
        {
            while (true)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    if (_timer < 0) _timer = 0;
                    yield return null;
                    continue;
                }

                transform.position += _movementDirection * ((_isExtending ? 1 : -1) * Time.deltaTime);

                if (_animator)
                {
                    switch (_isExtending)
                    {
                        case true when !_animationLaunched:
                            _animator.SetBool(_animationHash, true);
                            _animationLaunched = true;
                            break;
                        case false when _animationLaunched:
                            _animator.SetBool(_animationHash, false);
                            _animationLaunched = false;
                            break;
                    }
                }

                var gap = transform.position - _startPosition;
                var angleZ = spawner.transform.eulerAngles.z;
                var rotation = Quaternion.Euler(0, 0, -angleZ);
                var advancement = -(rotation * gap).x;

                if (advancement < 0)
                {
                    _isExtending = true;
                    transform.position = _startPosition;
                    _timer = cooldown;
                }

                if (advancement > longueur)
                {
                    attackSound.Play();
                    _isExtending = false;
                    _timer = attente;
                }

                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            if (spawner == null) return;

            Gizmos.color = Color.red;
            var start = transform.position;
            var direction = transform.right;
            var angleZ = spawner.transform.eulerAngles.z;
            var rotation = Quaternion.Euler(0, 0, angleZ);
            var end = start + rotation * direction * longueur;

            Gizmos.DrawLine(start, end);
        }
    }
}