using UnityEngine;

public class MarkerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float moveDistance = 1f; // 이동할 거리
    private Animator animator; // Animator 컴포넌트
    private Vector3 targetPosition; // 목표 위치
    private bool isWalking = false; // 애니메이션 재생 중인지 체크
    private bool canMove = true; // 이동 가능 여부

    void Start()
    {
        Debug.Log("게임시작");
        Debug.Log("이동 가능 초기설정: " + canMove);
        Debug.Log("적 등장");
        animator = GetComponent<Animator>();
        targetPosition = transform.position; // 시작 위치 초기화

    }

    public void SetMovement(bool value)
{
    canMove = value;
    // Debug.Log("이동 가능 여부: " + canMove); // 이동 가능 여부 출력
}

    void Update()
    {
        // 입력 처리
        if (canMove && !isWalking)
        {
            if (Input.GetKeyDown(KeyCode.W)) // 위
            {
                targetPosition += Vector3.up * moveDistance;
                PlayWalkAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.S)) // 아래
            {
                targetPosition += Vector3.down * moveDistance;
                PlayWalkAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.A)) // 왼쪽
            {
                targetPosition += Vector3.left * moveDistance;
                PlayWalkAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.D)) // 오른쪽
            {
                targetPosition += Vector3.right * moveDistance;
                PlayWalkAnimation();
            }
        }

        // 현재 위치에서 목표 위치로 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 애니메이션 상태 체크
        if (isWalking && !IsAnimationPlaying("MarkerWalk"))
        {
            isWalking = false; // 애니메이션이 끝나면 다시 false로 설정
        }
    }

    private void PlayWalkAnimation()
    {
        if (!isWalking)
        {
            animator.SetTrigger("MarkerWalk"); // MarkerWalk 트리거 설정
            isWalking = true; // 애니메이션 재생 중임을 표시
        }
    }

    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName); // 현재 애니메이션 이름과 비교
    }
}
