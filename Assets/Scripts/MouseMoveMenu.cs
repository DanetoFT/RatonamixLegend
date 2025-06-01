using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseMoveMenu : MonoBehaviour
{
    public RectTransform[] waypoints;
    public float moveDuration = 2f;
    public float waitTime = 1f;

    public RectTransform spriteTransform;

    private RectTransform rt;
    private int lastIndex = -1;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        StartCoroutine(MoveBetweenWaypoints());
    }

    IEnumerator MoveBetweenWaypoints()
    {
        while (true)
        {
            int index;
            do
            {
                index = Random.Range(0, waypoints.Length);
            } while (index == lastIndex);

            lastIndex = index;
            Vector2 targetPosition = waypoints[index].anchoredPosition;
            Vector2 startPosition = rt.anchoredPosition;
            Vector2 direction = (targetPosition - startPosition).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteTransform.rotation = Quaternion.Euler(0, 0, angle + 90);

            float elapsed = 0f;
            while (elapsed < moveDuration)
            {
                rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            rt.anchoredPosition = targetPosition;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
