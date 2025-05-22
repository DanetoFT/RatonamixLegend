using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTransition : MonoBehaviour
{
    public Image blackScreen;
    public Transform raton;

    public Transform[] puntosDeNivel;
    public GameObject[] nivelesADesactivar;

    public float fadeDuration = 1f;
    private bool enTransicion = false;

    public int nivelActual = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enTransicion && other.gameObject.layer == LayerMask.NameToLayer("nextLevel"))
        {
            if (nivelActual < puntosDeNivel.Length)
            {
                StartCoroutine(TransicionCoroutine(nivelActual));
                nivelActual++;
            }
            else
            {
                Debug.LogWarning("No hay m�s niveles definidos.");
            }
        }
    }

    public IEnumerator TransicionCoroutine(int indice)
    {
        enTransicion = true;

        yield return StartCoroutine(FadeToBlack());

        raton.position = puntosDeNivel[indice].position;

        if (indice < nivelesADesactivar.Length && nivelesADesactivar[indice] != null)
        {
            nivelesADesactivar[indice].SetActive(false);
            nivelActual++;
        }

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(FadeFromBlack());

        enTransicion = false;
    }

    public IEnumerator FadeToBlack()
    {
        float tiempo = 0f;
        while (tiempo < fadeDuration)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, tiempo / fadeDuration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
    public IEnumerator FadeFromBlack()
    {
        float tiempo = 0f;
        while (tiempo < fadeDuration)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, tiempo / fadeDuration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}