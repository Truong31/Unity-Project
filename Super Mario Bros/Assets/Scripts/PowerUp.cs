using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        StarPower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                break;
            case Type.StarPower:
                break;
        }
        Destroy(gameObject);
    }
}
