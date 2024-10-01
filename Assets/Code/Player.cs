using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// 플레이어의 입력 방향을 저장하는 벡터
	public Vector2 inputVec;
	// 플레이어의 이동 속도
	public float speed;
		
	// Rigidbody2D 변수 선언
	Rigidbody2D rigid;
		
	void Start()
	{
		// Rigidbody2D 초기화
		rigid = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		inputVec.x = Input.GetAxisRaw("Horizontal");
		inputVec.y = Input.GetAxisRaw("Vertical");
	}
		
	void FixedUpdate()
	{
		Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
	}
}