using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer; // Le transform du calque
        public float parallaxFactor; // Facteur de parallaxe (0 = fixe, 1 = suit totalement la caméra)
    }

    public ParallaxLayer[] layers; // Liste des calques
    private Vector3 _previousCameraPosition; // Position de la caméra au frame précédent
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        // Initialiser la position de la caméra
        if (Camera.main != null)
            _previousCameraPosition = Camera.main.transform.position;
    }

    void LateUpdate()
    {
        if (_camera)
        {
            var cameraDelta = _camera.transform.position - _previousCameraPosition;

            foreach (var layer in layers)
            {
                if (!layer.layer) continue;
                // Appliquer un décalage proportionnel au parallaxFactor
                var layerPosition = layer.layer.position;
                layerPosition.x += cameraDelta.x * layer.parallaxFactor;
                layerPosition.y += cameraDelta.y * layer.parallaxFactor;

                layer.layer.position = layerPosition;
            }
        }

        // Mettre à jour la position précédente de la caméra
        _previousCameraPosition = _camera.transform.position;
    }
}