using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 5; // 초기 체력
    private float damageInterval = 2f; // 체력이 줄어드는 간격
    private float nextDamageTime = 0f; // 다음 체력이 줄어들 시간

    void Update()
    {
        // 현재 시간이 다음 체력이 줄어들 시간보다 크거나 같으면

        if (Time.time >= nextDamageTime)
        {
            
            // 체력을 1 줄임
            health--;

            // 다음 체력이 줄어들 시간 계산
            nextDamageTime = Time.time + damageInterval;

            // 체력이 0 이하일 경우 오브젝트를 제거
            if (health <= 0)
            {
                Destroy(gameObject);
                // Debug.Log("적 사망");
            }
        }
    }
}