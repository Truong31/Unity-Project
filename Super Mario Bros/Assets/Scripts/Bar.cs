using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

    private void Update()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 destinationPosition = currentPosition + Vector3.up * 7.0f;

        yield return Move(currentPosition, destinationPosition);
        yield return Move(destinationPosition, currentPosition);
    }

    //NOTE: Xu ly di chuyen cho Bar
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float duration = 3.0f;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;
    }
}
