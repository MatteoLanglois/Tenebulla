using UnityEngine;

namespace Script
{
    public class SardineMovement : MonoBehaviour
    {
        // Definir la zone avec xDepart, xFin, yDebut, yFin
        public SardineZone zoneSardine;

        public float speed = 6f; // Vitesse de deplacement
        public float changeDirectionInterval = 2f; // Temps entre chaque changement de direction

        private Vector2 _randomDirection; // Direction aleatoire actuelle (2D)
        private float _timer; // Chronometre pour changer de direction

        private void Start()
        {
            // Initialisation de la premiere direction aleatoire
            _randomDirection = GetRandomDirection();
        }

        private void Update()
        {
            // Deplace la sardine dans la direction aleatoire
            var movement = new Vector3(_randomDirection.x, _randomDirection.y, 0f) * (speed * Time.deltaTime);
            transform.Translate(movement, Space.World);

            // Compte le temps ecoule
            _timer += Time.deltaTime;

            // Change de direction apres l'intervalle defini
            if (_timer >= changeDirectionInterval)
            {
                _randomDirection = GetRandomDirection();
                _timer = 0f;
            }

            // Garde la sardine dans la zone definie
            KeepInBounds();
        }

        private static Vector2 GetRandomDirection()
        {
            // Genere une direction aleatoire (normale) sur X et Y
            var randomDir = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );
            return randomDir.normalized; // Retourne une direction avec une magnitude de 1
        }

        private void KeepInBounds()
        {
            var pos = transform.position;

            // Si la sardine est en dehors de la zone sur l'axe X
            if (pos.x < zoneSardine.XMin || pos.x > zoneSardine.XMax)
            {
                // Change de direction pour ramener la sardine dans la zone (oppos� de la direction actuelle)
                _randomDirection.x = (pos.x < zoneSardine.XMin) ? 1f : -1f;
                _timer = 0f;
            }

            // Si la sardine est en dehors de la zone sur l'axe Y
            if (!(pos.y < zoneSardine.YMin) && !(pos.y > zoneSardine.YMax)) return;
            // Change de direction pour ramener la sardine dans la zone (oppos� de la direction actuelle)
            _randomDirection.y = (pos.y < zoneSardine.YMin) ? 1f : -1f;
            _timer = 0f;



            // Limite la position de la sardine pour rester dans la zone
            //pos.x = Mathf.Clamp(pos.x, xDepart, xFin);
            //pos.y = Mathf.Clamp(pos.y, yDebut, yFin);

            //transform.position = pos;
        }
    }
}
