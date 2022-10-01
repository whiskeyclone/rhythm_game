using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    // Fade out sprite then destroy this Game Object
    IEnumerator Fade(float fadeTime)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
            spriteRenderer.material.color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }

    public void StartFade(float fadeTime)
    {
        StartCoroutine(Fade(fadeTime));
    }
}
