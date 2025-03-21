using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 3f;
    // Update is called once per frame
    void Update() // Update method는 게임이 실행되는 동안 계속 실행됨
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime; // Vector3.down 은 내려가도록 
        // Time.deltaTime 은 Update method 가 호출되는 빈도를 같게 하도록 하기 위해서 (사양에 따라 달라질수 있기 때문에)

        if (transform.position.y < -10) { // background 가 -10 즉, 화면 밖으로 나가게 된다면
            transform.position += new Vector3(0, 20f, 0); // 새로운 Vector3 구조체를 만들어서 y 위치 올리기
            
        }
        
    }
}
