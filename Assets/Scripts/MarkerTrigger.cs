using UnityEngine;

public class MarkerTrigger : MonoBehaviour
{
    private MarkerMove markerMove;
    private CharacterMove characterMove;

    void Start()
    {
        markerMove = GetComponent<MarkerMove>(); // MarkerMove 컴포넌트를 한 번만 가져오기
        characterMove = GetComponent<CharacterMove>(); // CharacterMove 컴포넌트 가져오기
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter: " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Marker가 Enemy와 충돌했습니다: " + other.gameObject.name);
            if (markerMove != null)
            {
                markerMove.SetMovement(false); // Marker 이동 비활성화
                if (characterMove != null)
                {
                    characterMove.SetMovement(false); // Character 이동 비활성화
                }
                Debug.Log("Marker 이동이 비활성화되었습니다.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Marker가 Enemy와의 충돌이 종료되었습니다: " + other.gameObject.name);
            if (markerMove != null)
            {
                markerMove.SetMovement(true); // Marker 이동 활성화
                if (characterMove != null)
                {
                    characterMove.SetMovement(true); // Character 이동 활성화
                }
                Debug.Log("Marker 이동이 활성화되었습니다.");
            }
        }
    }
}
