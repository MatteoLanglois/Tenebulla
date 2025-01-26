using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShellUi : MonoBehaviour
{
    // Global Data
    public GameData gameData;

    public Image shellImage;
    public int id;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameData.CoinsCollected[id] == 1)
        {
            shellImage.enabled = true ;
        }
    }
}