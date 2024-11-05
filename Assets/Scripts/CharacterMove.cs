using System.Collections;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float moveDuration = 3f; // 이동 시간
    private Animator animator; // Animator 컴포넌트
    private bool isMoving = true; // 이동 중인지 여부
    private bool canMove = true; // 이동 가능 여부

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        StartCoroutine(MoveCharacter()); // 캐릭터 이동 시작
    }

    private IEnumerator MoveCharacter()
    {
        float startPosition = -10f; // 시작 위치
        float endPosition = -5f; // 끝 위치
        float elapsedTime = 0f; // 경과 시간

        // 초기 위치 설정
        transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        
        // Player Enter에서 Player Walk로 애니메이션 설정
        animator.SetTrigger("PlayerWalk"); 

        while (elapsedTime < moveDuration) // 3초 동안 이동
        {
            float t = elapsedTime / moveDuration; // 비율 계산
            float newX = Mathf.Lerp(startPosition, endPosition, t); // 선형 보간
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime; // 경과 시간 증가
            yield return null; // 다음 프레임까지 대기
        }

        // 이동이 끝난 후 Idle 상태로 전환
        isMoving = false; 
        animator.SetTrigger("PlayerIdle"); // Player Idle 애니메이션으로 전환
    }

    void Update()
    {
        if (canMove && !isMoving)
        {
            // W, A, S, D 입력에 따라 PlayerWalk 트리거 활성화
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("WalkTrigger"); // Player Walk 애니메이션으로 전환
            }
        }
    }
    public void SetMovement(bool value)
        {
            canMove = value;
            // Debug.Log("이동 가능 여부: " + canMove); // 이동 가능 여부 출력
        }
}
