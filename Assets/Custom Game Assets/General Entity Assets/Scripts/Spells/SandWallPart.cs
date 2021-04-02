using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWallPart : MonoBehaviour
{
    private int steps;

    private Vector3 maxHeight;
    private Vector3 minHeight;
    private Vector3 maxPosition;
    private Vector3 minPosition;
    private float counter;
    private float heightMod;

    private bool spawn;
    private bool melt;

    private void Start()
    {
        steps = 30;

        maxHeight = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z);
        minHeight = transform.localScale;

        maxPosition = transform.position;
        maxPosition.y = maxHeight.y / 2;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        minPosition = transform.position;

        counter = 0f;
        heightMod = Random.Range(0f / steps, 3f / steps);
        spawn = true;
        melt = false;
    }

    private void FixedUpdate()
    {
        if (spawn) Rise();
        else if (melt) Melt();
    }

    private void Rise()
    {
        if (counter <= 1f - heightMod)
        {
            transform.localScale = Vector3.Lerp(minHeight, maxHeight, counter);
            transform.position = Vector3.Lerp(minPosition, maxPosition, counter);
            counter += Random.Range(0.1f / steps, 1.2f / steps);
        }
        else
        {
            spawn = false;
            maxHeight = transform.localScale;
            maxPosition = transform.position;
            StartCoroutine(Bounce());
            Invoke(nameof(StartMelting), 5f);
        }
    }

    IEnumerator Bounce()
    {
        Vector3 downScale = transform.localScale + Vector3.down * Random.Range(1f, 2f);
        Vector3 upScale = transform.localScale;

        Vector3 downPos;
        Vector3 upPos;

        bool up = false;
        float count = 1f;
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        while (!melt)
        {
            downPos = new Vector3(maxPosition.x, downScale.y / 2f, maxPosition.z);
            upPos = new Vector3(maxPosition.x, upScale.y / 2f, maxPosition.z);

            if (up)
            {
                transform.localScale = Vector3.Lerp(downScale, upScale, count);
                transform.position = Vector3.Lerp(downPos, upPos, count);
                count += 0.1f * Random.Range(0.5f, 1f);
                if (count >= 1f)
                {
                    up = false;
                    count = 1f;
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(downScale, upScale, count);
                transform.position = Vector3.Lerp(downPos, upPos, count);
                count -= 0.03f * Random.Range(0.5f, 1f);
                if (count <= 0f)
                {
                    up = true;
                    count = 0f;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    private void StartMelting()
    {
        counter = 0;
        maxHeight = transform.localScale;
        maxPosition = transform.position;
        melt = true;
    }

    private void Melt()
    {
        if (counter <= 1f)
        {
            transform.localScale = Vector3.Lerp(minHeight, maxHeight, 1 - counter);
            transform.position = Vector3.Lerp(minPosition, maxPosition, 1 - counter);
            counter += Random.Range(0.1f / steps, 0.3f / steps);
        }
        else if (counter <= 2f)
        {
            counter += 0.05f;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
