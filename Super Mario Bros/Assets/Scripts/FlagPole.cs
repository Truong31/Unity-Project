using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    //NOTE: xu ly khi Mario cham vao Flag

    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 1;

    public AudioSource newStageSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<MarioMovement>().enabled = false;

        GameManager.Instance.audioSource.Stop();
        GameManager.Instance.isEndStage = true;
        newStageSound.Play();

        yield return MoveTo(player, poleBottom.position);       //Di chuyen Mario tu o vi tri cham vao co xuong day Flag(Flag gom co va HardBlock)
        yield return MoveTo(player, player.position + Vector3.right);       //Di chuyen Mario sang phai(ra khoi HardBlock), luc nay Mario van o tren khong
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);        //Tiep tuc di chuyen Mario sang phai dong thoi ha dan xuong mat dat
        yield return MoveTo(player, castle.position);       //Di chuyen den vi tri cau Castle

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
        GameManager.Instance.isEndStage = false;
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while(Vector3.Distance(subject.position, destination)> 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }
}
