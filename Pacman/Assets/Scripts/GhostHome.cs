using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    /*Enable: khi đoạn script có phương thức này, nó sẽ được tự động thức hiện
    khi đoạn Script (hoặc gameObject chứa Script đó) được kích hoạt 

    Khi đoạn Script này thực hiện, tất cả các Coroutine sẽ bị tắt*/
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    //Tạo chuyển động lên xuống cho ghost trong Home
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    /*Coroutines: kiểu trả về là IEnumerator 
     Đây là cách thực hiện 1 phương thức song song nhưng không ảnh hưởng đến 
    luồng chính của chương trình (tạm dừng và thực hiện sau 1 khoảng thời gian hoặc ở khung hình tiếp theo)
     */

    /*Phương thức mô tả di chuyển của Ghost
     * Sau khi bị Pacman ăn sẽ di chuyển về Home.
     * Sau 1 khoảng thời gian sẽ ra ngoài và truy đuổi Pacman
    */
    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;   //Không bị ảnh hưởng bởi các lực vật lý
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;      //Tổng thời gian di chuyển
        float elapsed = 0.0f;       //Thời gian đã trôi qua(kể từ khi di chuyển)


        //Di chuyển Ghost từ vị trí hiện tại vào Home
        while(elapsed < duration)
        {
            /*Tính toán giá trị nội suy tuyến tính
             * (tính toán 1 giá trị trung gian ở giữa 2 giá trị trên 1 đường thẳng
             * dựa trên 1 tỉ lệ nào đó 
             * 
             * Ứng dụng: giúp chuyển động trở lên mượt mà hơn
             * 
             */
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);     //elapsed / duration: tỉ lệ di chuyển

            newPosition.z = position.z;

            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;

            yield return null;      //Tạm dừng đến khung hình tiếp theo
            
        }

        elapsed = 0.0f;

        //Di chuyển Ghost từ vị trí hiện tại ra ngoài(ra khỏi Home)
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.outside.position, elapsed / duration);

            newPosition.z = position.z;

            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;

        }
        /*Thiết lập hướng di chuyển cho Ghost
         *  Nếu < 0.5 ==> trả về -1.0 (di chuyển sang trái)
         *  Nếu >= 0.5 ==> trả về 1.0 (di chuyển sang phải)
         */
        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);

        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
