using UnityEngine;
using TMPro;
using System.Collections;

public class ZoneTrigger : MonoBehaviour
{
    // R�f�rence au texte de l'UI o� le nom de la zone sera affich�
    public TMP_Text zoneText;

    // Dur�e pendant laquelle le texte reste visible
    public float displayDuration = 3f;

    // Vitesse de l'effet de fondu
    public float fadeSpeed = 2f;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que votre joueur a le tag "Player"
        {
            // D�marre le processus d'affichage du texte
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(ShowZoneText());
        }
    }

    private IEnumerator ShowZoneText()
    {
        // Met � jour le texte
        zoneText.text = gameObject.name;

        // Affiche et commence le fondu
        yield return FadeText(1f); // Fade in (apparition)

        // Attend la dur�e sp�cifi�e
        yield return new WaitForSeconds(displayDuration);

        // Masque le texte avec un fondu
        yield return FadeText(0f); // Fade out (disparition)
    }

    private IEnumerator FadeText(float targetAlpha)
    {
        // R�cup�re la couleur actuelle du texte
        Color currentColor = zoneText.color;

        // Calcule le pas de fondu
        while (!Mathf.Approximately(currentColor.a, targetAlpha))
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            zoneText.color = currentColor;
            yield return null;
        }
    }
}
