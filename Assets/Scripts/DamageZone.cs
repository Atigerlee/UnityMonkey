using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 10; // 扣多少血

    private void OnTriggerEnter(Collider other)
    {
        // 偵測是否是玩家碰到
        Debug.Log("isTrigger");
        if (other.CompareTag("Player"))
        {
            // 取得玩家身上的 PlayerHealth 腳本並扣血
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
