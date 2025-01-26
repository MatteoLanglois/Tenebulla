using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assignez-le dans l'Inspector
    public string nextSceneName;   // Nom de la sc�ne suivante

    void Start()
    {
        // Assurez-vous que le VideoPlayer est assign�
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Abonnez-vous � l'�v�nement pour d�tecter la fin de la vid�o
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Chargez la sc�ne suivante
        SceneManager.LoadScene(nextSceneName);
    }
}
