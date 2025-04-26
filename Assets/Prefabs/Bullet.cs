using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 判斷是否撞到標籤為 "Enemy" 的物體
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // 消除敵人
            Destroy(gameObject);       // 消除子彈
        }
    }
}
