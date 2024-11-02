using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    //NOTE: kiem tra va cham
    public static bool CircleCast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }
        //NOTE: chinh CircleCast nam ngay duoi chan Mario, ban kinh 0.25
        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    /*NOTE: kiem tra 2 vat the co cung huong khong
     Dot dung de tinh tich vo huong. 
        Neu tra ve 1 ==> cung huong
        Neu tra ve 0 ==> vuong goc
        Neu tra ve -1 ==> nguoc huong
     */
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
