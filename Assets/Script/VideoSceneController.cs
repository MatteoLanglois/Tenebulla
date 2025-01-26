using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assignez-le dans l'Inspector
    public string nextSceneName;   // Nom de la scène suivante

    void Start()
    {
        // Assurez-vous que le VideoPlayer est assigné
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Abonnez-vous à l'événement pour détecter la fin de la vidéo
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Chargez la scène suivante
        SceneManager.LoadScene(nextSceneName);
    }
}
