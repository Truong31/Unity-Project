using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public Projectile missilePrefab;

    public AnimationCurve speed;
    public int rows = 5;
    public int columns = 11;

    private Vector3 direction = Vector2.right;

    public float missileAttackRate = 1.0f;

    public int amountKilled { get; private set; }
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    private void Awake()
    {
        for(int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);        //Tong chieu rong cua mang luoi
                                                            //(moi invader se chiem 2 o chieu rong ==> nhan 2.0f)
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width/2, -height/2);       // Xac dinh vi tri chinh giua cua luoi
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);       //Vi tri hien tai cua mang luoi
                                                                                                    //(row * 2.0f: khoang cach giua cac hang la 2 o)
            for(int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);       // Copy 1 mau invader cho 1 hang(moi hang se co 1 mau invader khac nhau)
                invader.killed += InvaderKilled;        // Moi khi 1 invader bi tieu diet(khi invader va cham voi laser cua player)
                                                        //, phuong thuc InvaderKilled se duoc goi
                Vector3 postion = rowPosition;
                postion.x += col * 2.0f;
                invader.transform.localPosition = postion;      //  ".localPosition": vi tri cuc bo(vi tri cua vat the con so voi vat the cha),
                                                                //noi cach khac là vi tri cua cac invader con(invader da duoc copy)
                                                                //so voi vi tri cua rowPosition.
                                                                // Khac voi ".position" (vi tri toan canh). Day la vi tri cua vat the
                                                                //so voi toan bo canh(Scene)
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }
    private void Update()
    {
        this.transform.position += this.direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach(Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy) // Kiem tra xem doi tuong co dang hoat dong
            {
                continue;
            }
            if(direction == Vector3.right && invader.position.x >= (rightEdge.x -1.0f))
            {
                AdvanceRow();
            }
            else if(direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()
    {
        direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    
    private void InvaderKilled()
    {
        this.amountKilled++;
        if(this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void MissileAttack()
    {
        foreach(Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
}
