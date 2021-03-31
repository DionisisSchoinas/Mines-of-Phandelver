using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapper : MonoBehaviour
{
    public float swapInterval;
    public List<Sprite> sprites;

    private Coroutine coroutine;
    private Image image;
    private Sprite originalSprite;

    private void Awake()
    {
        if (sprites == null)
            sprites = new List<Sprite>();

        image = gameObject.GetComponent<Image>();
        originalSprite = image.sprite;
    }

    public void StartSwapping()
    {
        coroutine = StartCoroutine(SwapSprites());
    }

    public void StopSwapping()
    {
        if (originalSprite == null || image == null)
            return;

        if (coroutine != null)
            StopCoroutine(coroutine);

        image.sprite = originalSprite;
    }

    private IEnumerator SwapSprites()
    {
        while (true)
        {
            foreach (Sprite sprite in sprites)
            {
                image.sprite = sprite;
                yield return new WaitForSeconds(swapInterval);
            }
        }
    }
}
