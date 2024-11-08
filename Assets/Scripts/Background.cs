using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 2f;

    void Start()
    {
        Debug.Log("x: " + transform.position.x);
    }
    // Update is called once per frame
    void Update()
    {
        // W, A, S, D 키 입력 처리 (오직 왼쪽으로만 이동)
        if (Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MoveBackground(Vector3.left, 1f)); // 왼쪽으로 이동
        }

        // 위치 제한
        if (transform.position.x < -17.77778f)
        {
            transform.position = new Vector3(17.77778f, transform.position.y, transform.position.z);
        }
    }

    // 배경을 주어진 방향으로 일정 시간 동안 이동
    private IEnumerator MoveBackground(Vector3 direction, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime; // 경과 시간 추가
            yield return null; // 다음 프레임까지 대기
        }
    }
}
