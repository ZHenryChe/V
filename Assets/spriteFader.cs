using UnityEngine;
using System.Collections;

public class SpriteFader : MonoBehaviour
{
    public SpriteRenderer sprite; // assign in inspector
    public float fadeDuration = 1f;

    public void FadeIn()
    {
        StartCoroutine(FadeTo(1f, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0f, fadeDuration));
    }

    IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = sprite.color.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
            yield return null;
        }

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, targetAlpha);
    }

    IEnumerator firstFade()
    {
        yield return new WaitForSeconds(20.0f);
        FadeIn();
        FadeOut();
    }
    void Start()
    {
        StartCoroutine(firstFade());
    }
}
