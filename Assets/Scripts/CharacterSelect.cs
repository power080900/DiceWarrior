using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Canvas Canvas;  // 캔버스를 연결
    public Image backgroundImage;  // Canvas 컴포넌트 아래의 Image

    public Button KnightButton;  // 기사 캐릭터 선택 버튼
    public Button ArcherButton;  // 궁수 캐릭터 선택 버튼
    public Button MagicianButton;    // 마법사 캐릭터 선택 버튼

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundImage.enabled = true;  // Image 컴포넌트를 활성화
        KnightButton.onClick.AddListener(SelectKnight);  // 기사 캐릭터 선택 버튼 클릭 시 SelectKnight 메서드 연결
        ArcherButton.onClick.AddListener(SelectArcher);  // 궁수 캐릭터 선택 버튼 클릭 시 SelectArcher 메서드 연결
        MagicianButton.onClick.AddListener(SelectMagician);      // 마법사 캐릭터 선택 버튼 클릭 시 SelectMage 메서드 연결
    }

    // 캐릭터 선택 시 실행되는 함수
    public void SelectKnight()
    {
        Debug.Log("Knight Selected");  // 콘솔에 "Knight Selected" 출력
        SceneManager.LoadScene("PlayScene");  // "PlayScene" 씬으로 전환
    }
    public void SelectArcher()
    {
        Debug.Log("Archer Selected");  // 콘솔에 "Archer Selected" 출력
        SceneManager.LoadScene("PlayScene");  // "PlayScene" 씬으로 전환
    }
    public void SelectMagician()
    {
        Debug.Log("Magician Selected");  // 콘솔에 "Magician Selected" 출력
        SceneManager.LoadScene("PlayScene");  // "PlayScene" 씬으로 전환
    }
}
