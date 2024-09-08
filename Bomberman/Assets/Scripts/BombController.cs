using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public int explosionRadius = 1;     //Ban kinh vu no

    [Header("Destructible")]
    public Destructible destructiblePrefab;
    public Tilemap destructibleTile;
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


    //Tinh nang dat bomb
    private IEnumerator PlaceBomb()
    {
        //Lay vi tri de tha qua bomb
        Vector2 position = this.transform.position;
        position.x = Mathf.Round(position.x);   //Lam tron toa do x den so nguyen gan nhat
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(this.bombPrefab, position, Quaternion.identity);
        this.bombsRemaining--;

        //Tam dung
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


    //Dieu huong cho Explposion(vi tri vu no, huong vu no, do dai(ban kinh) vu no)
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if(length <= 0)
        {
            return;
        }
        position += direction;      //Vi tri moi cua vu no

        //Khong cho Explosion de len tuong
        //Neu gap vat the chua layermask(this.explosionLayerMask) thi ket thuc
        //Dieu kien trong if se tra ve BoxCollider2D
        if (Physics2D.OverlapBox(position, Vector2.one / 2.0f, 0.0f, this.explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(this.explosionPrefab, position, Quaternion.identity);

        //Neu ban kinh(length) lon hon 1 ==> set hoat hoa Middle.
        //Bang 1 ==> set hoat hoa End
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(this.explosionDuration);
        Destroy(explosion.gameObject, this.explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTile.WorldToCell(position);
        TileBase tile = destructibleTile.GetTile(cell);

        if(tile != null)
        {
            Instantiate(this.destructiblePrefab, position, Quaternion.identity);
            destructibleTile.SetTile(cell, null);
        }
    }

    //Tat tinh nang Trigger khi player ra khoi vi tri qua bom
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }

    public void AddBomb()
    {
        this.bombAmount++;
        this.bombsRemaining++;
    }
}
