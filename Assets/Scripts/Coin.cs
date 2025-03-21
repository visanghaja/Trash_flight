using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7f;
    // Start is called before the first frame update
    void Start()
    {
        Jump();
    }

    void Jump() {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>(); // rigidBody 컴포넌트에서 받아오기

        float randomJumpForce = Random.Range(4f, 8f); // jump 높이가 랜덤이 되도록
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f); // 오른쪽으로 갈지 왼쪽으로 갈지
        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
        // ForceMode2D.Impulse 이거는 즉시 힘을 가한다는 뜻!
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY) { // 특정 y 아래로 내려가면 삭제
            Destroy(gameObject);
        }
    }
}
