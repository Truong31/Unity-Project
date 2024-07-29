using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefabs;

    public float speed = 5.0f;

    private bool laserActive;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.D)| Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (!laserActive)
        {
            Projectile projectTile = Instantiate(this.laserPrefabs, this.transform.position, Quaternion.identity);
            projectTile.destroyed += laserDestroyed;
            laserActive = true;
        }
    }

    private void laserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Missile")
            || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
