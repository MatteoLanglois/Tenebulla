using UnityEngine;

[CreateAssetMenu(fileName = "SardineZone", menuName = "Scriptable Objects/SardineZone", order = 1)]
public class SardineZone : ScriptableObject
{
    [SerializeField] private float xMin = -50f;
    [SerializeField] private float xMax = 50f;
    [SerializeField] private float yMin = 400f;
    [SerializeField] private float yMax = 600f;

    // Getters pour accéder aux attributs
    public float XMin => xMin;
    public float XMax => xMax;
    public float YMin => yMin;
    public float YMax => yMax;

    // Méthode utilitaire pour récupérer les limites de la zone sous forme de Bounds
    public Bounds GetBounds()
    {
        return new Bounds(
            new Vector3((xMin + xMax) / 2, (yMin + yMax) / 2, 0f),
            new Vector3(xMax - xMin, yMax - yMin, 0f)
        );
    }
}