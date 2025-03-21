using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    // prefabs 에서 드래그 앤 드롭

    [SerializeField]
    private GameObject boss;
    
    private float[] arrPosX = { -2.2f, -1.1f, 0f, 1.1f, 2.2f }; 
    // enemy 가 생성될 위치를 배열로 저장

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start() 
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine(){ // 시간 기다렸다 하는 반복은 coroutine!
        StartCoroutine("EnemyRoutine");
        // Coroutine 시작하는 method
    }

    public void StopEnemyRoutine() { // Game manager 에서 접근 할 수 있도록 public
        StopCoroutine("EnemyRoutine");
        // Coroutine 끝내는 method
    }

    IEnumerator EnemyRoutine() { // 반복문을 몇초 기다렸다 실행할 것인지 설정 가능!
        yield return new WaitForSeconds(3f); // 3초 동안 대기!

        float moveSpeed = 5f; // 시간 지날 수록 점점 빨라지도록!
        int spawnCount = 0;
        int enemyIndex = 0;

        while(true){
            foreach (float posX in arrPosX) { // arrPosX 에서 꺼내서 posX 에다가 넣음
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount += 1;

            if(spawnCount % 10 == 0) { // 10번 돌리면 enemyIndex가 1 올라가도록! (10, 20, 30 ...)
                enemyIndex += 1;
                moveSpeed += 2;
            }

            if (enemyIndex >= enemies.Length) 
            {
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }
            yield return new WaitForSeconds(spawnInterval); // spawnInterval 동안 대기
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0,5) == 0){ // 20% 의 확률로 다음 적이 나오게 됨!
            index += 1;
        }

        if(index >= enemies.Length) { // index를 넘어가지 않도록!
            index = enemies.Length - 1;
        }

        GameObject enemyObject =  Instantiate(enemies[index], spawnPos, Quaternion.identity);
        // 무엇을, 어디서, 회전 값을 받아서 새로운 게임 오브젝트를 만듬
        Enemy enemy = enemyObject.GetComponent<Enemy>(); // Enemy 컴포넌트 가져오기!
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
