using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public float duration = 0.5f;        
    public float targetScale = 2.5f;

    private SpriteRenderer sr;
    private bool triggered = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(GrowAndFade());
        }
    }

    IEnumerator GrowAndFade()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * targetScale;
        Color startColor = sr.color;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            transform.localScale = Vector3.Lerp(startScale, endScale, progress);

            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, progress);
            sr.color = c;

            yield return null;
        }

        Color final = startColor;
        final.a = 0f;
        sr.color = final;

        Debug.Log("done");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}