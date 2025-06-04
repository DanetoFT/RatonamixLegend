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

    public int nivelActual;

    Ratoncillo ratoncillo;

    public GameObject queso;
    public CambioEscena cambioEscena;

    private void Start()
    {
        ratoncillo = GetComponent<Ratoncillo>();
        nivelActual = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enTransicion && other.gameObject.layer == LayerMask.NameToLayer("nextLevel"))
        {
            ratoncillo.canMove = false;

            if (nivelActual < puntosDeNivel.Length)
            {
                AudioController.Instance.PlaySFX("Victoria");
                StartCoroutine(TransicionCoroutine(nivelActual));
                nivelActual++;
            }
            else
            {
                Debug.LogWarning("No hay más niveles definidos.");
                cambioEscena.Cambio("Final");
                AudioController.Instance.StopMusic();
                AudioController.Instance.PlaySFX("Queso");
            }
        }
    }

    public IEnumerator TransicionCoroutine(int indice)
    {
        enTransicion = true;

        yield return StartCoroutine(FadeToBlack());

        raton.position = puntosDeNivel[indice].position;
        queso.transform.position = Vector2.zero;

        if (indice < nivelesADesactivar.Length && nivelesADesactivar[indice] != null)
        {
            nivelesADesactivar[indice].SetActive(false);
            nivelesADesactivar[indice + 1].SetActive(true);
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