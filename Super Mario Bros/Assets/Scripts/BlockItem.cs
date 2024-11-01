using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    //NOTE: Script xu ly hieu ung xuat hien cho cac Item Power(tru Coin) 

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;       //Vat the se khong chiu cac anh huong cac va cham vat ly(trong luc, ma sat...)
        physicCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        /*
        Viec tat va bat lai Sprite la vi khi Mario va cham voi Brick, 
        Brick se co hieu ung nang nhe len roi ha xuong(hieu ung duoc viet trong Script BlockHit)
        nen viec tat Sprite di giup khi Brick bi nang nen khong de lo ra Sprite cua Power
         */

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = endPosition;

        rigidbody.isKinematic = false;
        physicCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
