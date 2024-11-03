using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    //NOTE: tao chuyen dong cho Mario xuong duoi mat dat khi bat dau Scene 2
    public float speed = 2.0f;
    public Transform entrance;
    public Transform pipePosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(HandleMarioTransition(collision)); 
        }
    }

    private IEnumerator HandleMarioTransition(Collider2D collision)
    {
        collision.GetComponent<MarioMovement>().enabled = false;
        collision.GetComponent<CapsuleCollider2D>().enabled = false;
        collision.GetComponent<Rigidbody2D>().isKinematic = true;

        yield return Move(collision.transform, pipePosition.position * 0.9f);

        collision.GetComponent<MarioMovement>().enabled = true;
        collision.GetComponent<CapsuleCollider2D>().enabled = true;
        collision.GetComponent<Rigidbody2D>().isKinematic = false;

    }

    private IEnumerator Move(Transform player, Vector3 endPosition)
    {
        while (Vector3.Distance(player.position, endPosition) > 0.1f)
        {
            player.localPosition = Vector3.MoveTowards(player.position, endPosition, speed * Time.deltaTime);
            yield return null;
        }

        player.position = endPosition;

        yield return Enter(player, Vector3.one * 0.5f, pipePosition.position);
    }
    private IEnumerator Enter(Transform player, Vector3 endScale, Vector3 endPosition)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;

            yield return null;
        }
        player.position = endPosition;
        player.localScale = endScale;

        yield return new WaitForSeconds(0.5f);

        player.localScale = Vector3.one;
        Camera.main.GetComponent<SideScrolling>().SetUnderGround(true);
        player.position = entrance.position;

    }
}
