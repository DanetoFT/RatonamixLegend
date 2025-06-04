using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFinal : MonoBehaviour
{
    public float tiempo = 5f;
    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;

        if (tiempo <= 0 && Input.anyKeyDown)
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }
}
