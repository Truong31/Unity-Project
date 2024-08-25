using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3.0f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1.0f;
    public int explosionRadius = 1;
    private void OnEnable()
    {
        this.bombsRemaining = this.bombAmount;
    }

    private void Update()
    {
        if(this.bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(this.bombPrefab, position, Quaternion.identity);
        this.bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(this.explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(this.explosionDuration);
        Destroy(explosion.gameObject, this.explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);

        Destroy(bomb);
        this.bombsRemaining++;
    }


    //Dieu huong cho Explposion
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if(length <= 0)
        {
            return;
        }
        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2.0f, 0.0f, this.explosionLayerMask))
        {
            return;
        }

        Explosion explosion = Instantiate(this.explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(this.explosionDuration);
        Destroy(explosion.gameObject, this.explosionDuration);

        Explode(position, direction, length - 1);
    }


    //Tat tinh nang Trigger khi player ra khoi vi tri qua bom
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }
}
