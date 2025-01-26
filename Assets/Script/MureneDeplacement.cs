using UnityEngine;

namespace Script
{
    public class MureneDeplacement : MonoBehaviour
    {
        //longueur de l'extention de la murene
        public float longueur = 3;
        //position de depart de la murene
        public float depart = 0;

        //vitesse d'extention de la murene
        public float speed = 1;

        //temps pass� par la murene dans son tuyau avant de sortir
        public float cooldown = 1;
        //temps pass� par la murene avant de se retracter
        public float attente = 2;

        public GameObject spawner;

        private bool _extention = false;
        private Vector3 _debutExtention;
        private Vector3 _avance;
        private float _timer;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _debutExtention = transform.position;
            InitialiseAvance();
            //setPos(depart);

            //fait en sorte que le sprite soit retourn� si il est trop tourn� (qu'il est a l'envers)
            if (spawner.transform.eulerAngles.z > 90 && spawner.transform.eulerAngles.z < 270)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
            }
        }
        private void OnValidate()
        {
            InitialiseAvance();
        }

        private void InitialiseAvance()
        {
            var direction = new Vector3(speed, 0, 0);
            var angleZ = spawner.transform.eulerAngles.z;
            var rotation = Quaternion.Euler(0, 0, angleZ);
            _avance = -(rotation * direction);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                if (_timer < 0) _timer = 0;
                return;
            }

            if (_extention)
            {
                //Debug.Log("extention");
                transform.position += _avance * Time.deltaTime;
            }
            else
            {
                transform.position -= _avance * Time.deltaTime;
            }

            var ecart = transform.position-_debutExtention;
            var angleZ = spawner.transform.eulerAngles.z;
            var rotation = Quaternion.Euler(0, 0, -angleZ);
            var avancement = -(rotation * ecart).x;

            if (avancement < 0)
            {
                _extention = true;
                transform.position = _debutExtention;
                _timer = cooldown;
            }

            if (!(avancement > longueur)) return;
            _extention = false;
            _timer = attente;
        }
    }
}
