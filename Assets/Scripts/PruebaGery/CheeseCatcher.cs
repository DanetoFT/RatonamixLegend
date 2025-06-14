using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheeseCatcher : MonoBehaviour
{
    public Transform cheeseHoldPoint;
    public Sprite[] cheeseSprites;
    public float spriteChangeDelay;
    public float particuleTime;
    public GameObject particuleSystem;

    public Dictionary<GameObject, Coroutine> activeCoroutines = new Dictionary<GameObject, Coroutine>();
    private Dictionary<GameObject, int> currentSpriteIndices = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, RigidbodyType2D> initialBodyTypes = new Dictionary<GameObject, RigidbodyType2D>();

    public Dictionary<GameObject, int> CurrentSpriteIndices => currentSpriteIndices;

    private Ratoncillo raton;

    private void Start()
    {
        raton = GetComponent<Ratoncillo>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (raton != null && raton.isTrapped) return;

        var queso = other.gameObject.GetComponent<Queso>();
        if (queso != null && !activeCoroutines.ContainsKey(other.gameObject))
        {
            int startIndex = currentSpriteIndices.TryGetValue(other.gameObject, out int idx) ? idx : 0;
            Coroutine coroutine = StartCoroutine(AcopleQueso(other.gameObject, startIndex));
            activeCoroutines.Add(other.gameObject, coroutine);

            raton.canRotate = false;
            raton.mouseMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var queso = other.gameObject.GetComponent<Queso>();
        if (queso != null && activeCoroutines.ContainsKey(other.gameObject))
        {
            StopProcessingQueso(other.gameObject);
            Invoke("moverRaton", .5f);
        }
    }

    void moverRaton()
    {
        raton.canRotate = true;
        raton.mouseMove = true;
    }

    public void StopProcessingQueso(GameObject queso)
    {
        if (activeCoroutines.TryGetValue(queso, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            activeCoroutines.Remove(queso);
            queso.transform.SetParent(null);

            raton.animator.SetBool("Eating", false);

            if (initialBodyTypes.TryGetValue(queso, out RigidbodyType2D initialType))
            {
                Rigidbody2D rb = queso.GetComponent<Rigidbody2D>();
                if (rb != null) rb.bodyType = initialType;
                initialBodyTypes.Remove(queso);
            }

            foreach (Transform child in queso.transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("ParticleEffect"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    IEnumerator AcopleQueso(GameObject queso, int startIndex)
    {
        Rigidbody2D rb = queso.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            initialBodyTypes[queso] = rb.bodyType;
            raton.animator.SetBool("Eating", true);
        }

        SpriteRenderer Sr = queso.GetComponent<SpriteRenderer>();
        if (Sr != null && cheeseSprites.Length > 0)
        {
            if (raton != null && raton.isTrapped)
            {
                StopProcessingQueso(queso);
                yield break;
            }

            for (int i = startIndex; i < cheeseSprites.Length; i++)
            {
                if (raton != null && raton.isTrapped)
                {
                    StopProcessingQueso(queso);
                    yield break;
                }

                currentSpriteIndices[queso] = i;
                Sr.sprite = cheeseSprites[i];

                GameObject particuleInstance = null;
                if (particuleSystem != null)
                {
                    Quaternion RotationParticule = Quaternion.Euler(-180f, 90f, 0f);
                    particuleInstance = Instantiate(particuleSystem, queso.transform.position, RotationParticule, queso.transform);
                    particuleInstance.layer = LayerMask.NameToLayer("ParticleEffect");
                }

                yield return new WaitForSeconds(particuleTime);

                if (particuleInstance != null) Destroy(particuleInstance);

                float remainingDelay = spriteChangeDelay - particuleTime;
                if (remainingDelay > 0) yield return new WaitForSeconds(remainingDelay);
            }

            if (rb != null && initialBodyTypes.ContainsKey(queso))
            {
                rb.bodyType = initialBodyTypes[queso];
                initialBodyTypes.Remove(queso);
            }
            Destroy(queso);
        }

        currentSpriteIndices.Remove(queso);
        activeCoroutines.Remove(queso);
    }
}
