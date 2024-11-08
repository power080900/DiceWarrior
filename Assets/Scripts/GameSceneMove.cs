using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScenesMove : MonoBehaviour
{
    public GameObject ConfirmDialog;  // ConfirmDialog를 연결
    public Button yesButton;          // '예' 버튼
    public Button noButton;           // '아니오' 버튼

    void Start()
    {
        // 게임 시작 시 대화상자 숨기기
        ConfirmDialog.SetActive(false);

        // '예' 버튼 클릭 시 QuitYes 메서드 연결
        yesButton.onClick.AddListener(QuitYes);

        // '아니오' 버튼 클릭 시 QuitNo 메서드 연결
        noButton.onClick.AddListener(QuitNo);
    }

    // 게임 시작 버튼 클릭 시 씬을 "CharacterSelect"으로 전환
    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    // 게임 종료 버튼 클릭 시 대화상자를 띄우는 함수
    public void ShowConfirmationDialog()
    {
        ConfirmDialog.SetActive(true);  // 대화상자 활성화
    }

    // '예' 버튼 클릭 시 게임 종료
    public void QuitYes()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 게임 종료
#else
        Application.Quit();  // 빌드된 게임에서 게임 종료
#endif
    }

    // '아니오' 버튼 클릭 시 대화상자 비활성화
    public void QuitNo()
    {
        ConfirmDialog.SetActive(false);  // 대화상자 비활성화
    }
}

