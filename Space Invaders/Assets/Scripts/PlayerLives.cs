using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int currentLives;
    public int MaxLives = 3;
    public GameObject livesIcon;

    private void Start()
    {
        currentLives = MaxLives;
        LivesUI();
    }
    public void LostLive()
    {
        currentLives--;
        LivesUI();
    }
    private void LivesUI()
    {
        foreach(Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        
        for(int i = 0; i < currentLives; i++)
        {
            Vector3 position = new Vector3(this.transform.position.x + i * 2.0f, this.transform.position.y, this.transform.position.z);
            Instantiate(this.livesIcon, position, Quaternion.identity, this.transform);
        }
        
    }
}
