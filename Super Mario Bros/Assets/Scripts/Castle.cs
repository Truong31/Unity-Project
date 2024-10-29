using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public Transform entrance;
    public Transform pipePosition;
    public float Speed = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MarioMovement>().enabled = false;
            StartCoroutine(MoveTo(collision.transform, pipePosition.position));
        }
    }
    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while(Vector3.Distance(subject.position, destination) > 14f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, Speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        Camera.main.GetComponent<SideScrolling>().SetUnderGround(true);
        subject.position = entrance.position;
        subject.GetComponent<MarioMovement>().enabled = true;
    }
}
