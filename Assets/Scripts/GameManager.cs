using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TMP
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    // 어디서나 다 gameObject를 접근할 수 있도록 싱글톤 패턴으로 설계
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;
    
    private int coin = 0;

    [HideInInspector] // Inspector 에서 숨기기
    public bool isGameOver = false;

    void Awake() { // Awake 는 Start 메소드 전에 실행
        if (instance == null)
        {
            instance = this;
            // 이렇게 하면 GameManager Instance 를 다른곳에서도 공유헤서 사용가능
        }
    }

    public void IncreaseCoin() {
        coin++;
        text.SetText(coin.ToString()); // text 이렇게 출력! (only 문자열)

        if (coin % 30 == 0) // 30, 60, 90 ...
        {
            Player player = FindObjectOfType<Player>(); // Player를 찾아와서 넣어줌
            if (player != null)
            {
                player.Upgrade(); // Player Upgrade 메소드 실행시킴!
            }

        }
    }

    public void SetGameOver() {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>() ;
        isGameOver = true;
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }
        Invoke("ShowGameOverPanel", 1f); // 1초 후에 gameOverPanel 활성화 하기!!
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
        // SimpleScene을 다시 로드 해줌
    }

}
