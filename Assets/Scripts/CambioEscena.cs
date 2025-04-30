using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public void Cambio(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
