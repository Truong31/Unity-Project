using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;

    //Set hoat hoa cho vu no
    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        this.start.enabled = renderer == this.start;
        this.middle.enabled = renderer == this.middle;
        this.end.enabled = renderer == this.end;
    }

    //Tinh toan huong cua vu no
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float second)
    {
        Destroy(this.gameObject, second);
    }
}
