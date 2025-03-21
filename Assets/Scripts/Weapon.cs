using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    public float damage = 1f;

    // Start is called before the first frame update
    void Start() 
    { 
        Destroy(gameObject, 1f); // gameObject 즉, weapon 이 1f 후에 없어짐
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime; // 올라가도록 (background 의 반대)
    }
}
