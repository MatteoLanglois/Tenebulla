using UnityEngine;

public class MureneDeplacement : MonoBehaviour
{
    //longueur de l'extention de la murene
    public float longueur = 3;
    //position de depart de la murene
    public float depart = 0;

    //vitesse d'extention de la murene
    public float speed = 1;

    //temps passé par la murene dans son tuyau avant de sortir
    public float cooldown = 1;
    //temps passé par la murene avant de se retracter
    public float attente = 2;

    public GameObject spawner;

    private bool extention = false;
    private Vector3 debutExtention;
    private Vector3 avance;
    private float timer;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debutExtention = transform.position;
        initialiseAvance();
        setPos(depart);

        //fait en sorte que le sprite soit retourné si il est trop tourné (qu'il est a l'envers)
        if (spawner.transform.eulerAngles.z > 90 && spawner.transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        }
    }
    private void OnValidate()
    {
        initialiseAvance();
    }

    void initialiseAvance()
    {
        Debug.Log(speed);
        Vector3 direction = new Vector3(speed, 0, 0);
        float angleZ = spawner.transform.eulerAngles.z;
        Quaternion rotation = Quaternion.Euler(0, 0, angleZ);
        avance = -(rotation * direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        { 
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;
            return;
        }

        if (extention)
        {
            transform.position += avance * Time.deltaTime;
        }
        else
        {
            transform.position -= avance * Time.deltaTime;
        }

        Vector3 ecart = transform.position-debutExtention;
        float angleZ = spawner.transform.eulerAngles.z;
        Quaternion rotation = Quaternion.Euler(0, 0, -angleZ);
        float avancement = -(rotation * ecart).x;

        if (avancement < 0)
        {
            extention = true;
            transform.position = debutExtention;
            timer = cooldown;
        }
        if (avancement > longueur) 
        {
            extention = false;
            timer = attente;

        }
    }

    private void setPos(float longueur)
    {

    }
}
