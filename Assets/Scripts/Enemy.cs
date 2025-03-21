using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin; // coin 을 gameObject로 받아옴

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed; // this 는 여기의 private 값
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        
        if(transform.position.y < minY) { // 특정 y 아래로 내려가면 삭제
            Destroy(gameObject);
        }
    }

    // private void OnCollisionEnter2D(Collision2D other) { // 이거는 is Trigger 가 체크 안되어있을때
        
    // }

    private void OnTriggerEnter2D(Collider2D other) { // 이거는 is Trigger 가 체크 되어있을때
        // 충돌하게되면 자동 호출
        if (other.gameObject.tag == "Weapon") // Enemy 가 weapon 과 충돌했다면!
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0)
            {
                if (gameObject.tag == "Boss")
                {
                    GameManager.instance.SetGameOver();
                }
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
                // coin을 enemy 가 죽은 자리에 회전 없이 생성!

            }
            Destroy(other.gameObject); // 미사일 제거!
        }
    }
}
