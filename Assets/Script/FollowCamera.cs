using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Position cible de la caméra
            Vector3 desiredPosition = target.position + offset;

            // Interpolation pour un mouvement fluide
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Appliquer la position
            transform.position = smoothedPosition;
        }
    }
}
