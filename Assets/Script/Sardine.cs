using UnityEngine;

namespace Script
{
    public class SardineMovement : MonoBehaviour
    {
        // Definir la zone avec xDepart, xFin, yDebut, yFin
        public float xDepart = -30f; // Limite gauche de la zone
        public float xFin = 30f;     // Limite droite de la zone
        public float yDebut = 20f;  // Limite basse de la zone
        public float yFin = 60f;     // Limite haute de la zone

        public float speed = 6f; // Vitesse de deplacement
        public float changeDirectionInterval = 2f; // Temps entre chaque changement de direction

        private Vector2 _randomDirection; // Direction aleatoire actuelle (2D)
        private float _timer; // Chronometre pour changer de direction

        private void Start()
        {
            xDepart = -30f; // Limite gauche de la zone
            xFin = 30f;     // Limite droite de la zone
            yDebut = 20f;  // Limite basse de la zone
            yFin = 60f;

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
            if (pos.x < xDepart || pos.x > xFin)
            {
                // Change de direction pour ramener la sardine dans la zone (oppos� de la direction actuelle)
                _randomDirection.x = (pos.x < xDepart) ? 1f : -1f;
                _timer = 0f;
            }

            // Si la sardine est en dehors de la zone sur l'axe Y
            if (!(pos.y < yDebut) && !(pos.y > yFin)) return;
            // Change de direction pour ramener la sardine dans la zone (oppos� de la direction actuelle)
            _randomDirection.y = (pos.y < yDebut) ? 1f : -1f;
            _timer = 0f;



            // Limite la position de la sardine pour rester dans la zone
            //pos.x = Mathf.Clamp(pos.x, xDepart, xFin);
            //pos.y = Mathf.Clamp(pos.y, yDebut, yFin);

            //transform.position = pos;
        }
    }
}
