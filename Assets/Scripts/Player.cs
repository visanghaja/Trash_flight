using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // 이렇게 하면 유니티에서 moveSpeed 값을 변경 가능함!
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons; // 무기나 배경 모두 게임 오브젝트에 속함
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform; // 총알의 발사 위치 받기 (Transform class 로!)

    [SerializeField]
    private float shootInterval = 0.05f; // 총알 발사 간에 텀을 주기 위해서!
    private float lastShotTime = 0f;
    
    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal"); // 키보드로 좌우로 움직이는 입력값 받기
        // // float verticalInput = Input.GetAxisRaw("Vertical"); // 위아래로 입력받은 값
        // Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f); // 플레이어가 어디로 움직일지 Vector3 구조체에 저장받기
        // transform.position += moveTo * moveSpeed * Time.deltaTime; // transform.position 으로 위치변경!

        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // } else if (Input.GetKey(KeyCode.RightArrow)) {
        //     transform.position += moveTo;
        // }

        // Debug.Log(Input.mousePosition); // Debug.Log 하면 Unity 의 Console 창에 프린트 가능

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Unity 에서 보여주는 좌표값으로 변환하기
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f); 
        // 마우스로 하면 왼쪽 오른쪽 벽이 무용지물이 되기 때문에 mousePos.x 값이 -2.35f 보다 작으면 -2.35f 가 적용되고 2.35f 도 마찬가지
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);
        // mousePos 의 x 값만 받기위해서 새로운 구조체를 만듬 (y, z 값은 원래 있던 값으로 유지)
        
        if (!GameManager.instance.isGameOver)
        {
            Shoot();
        }

    }

    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) { // Time.time -> 게임이 시작된 이후로 현재까지 흐른 시간      
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity); // Quaternion.identity 는 아무 회전 없이 앞으로 쭉
            // 어떤 오브젝트를, 어디에, 회전을 어떻게 할건지
            // 게임 오브젝트를 만드는 Instantiate method
            lastShotTime = Time.time; // lastShotTime 갱신
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.SetGameOver();
            // GameManager 에서 가져올려면 instance 통해서!
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            // 이렇게 해서 GameManager 에 있는 method 에 접근 가능!
            Destroy(other.gameObject);
        }

    }

    public void Upgrade() {
        weaponIndex++;
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
