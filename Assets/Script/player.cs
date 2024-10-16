using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// 플레이어1의 입력 방향을 저장하는 벡터
	public Vector2 inputVec1;

	// 플레이어2의 입력 방향을 저장하는 벡터
	public Vector2 inputVec2;


	// 플레이어의 이동 속도
	public float speed;
		
	// Rigidbody2D 변수 선언
	Rigidbody2D rigid;

	// SpriterRenderer 변수 선언
	SpriteRenderer spriter;

	// Animator 변수 선언
	Animator anim;
		
	// 플레이어 ID 변수 추가 (1번 플레이어, 2번 플레이어 구분)
	public int playerID;
	void Start()
	{
		// Rigidbody2D 초기화
		rigid = GetComponent<Rigidbody2D>();
		// SpriterRenderer 초기화
		spriter = GetComponent<SpriteRenderer>();
		// Animator 초기화
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		// 사용자의 입력을 실시간으로 받아서 inputVec에 저장
 		// "Horizontal"과 "Vertical"은 Unity에서 설정된 입력 축을 의미하며,
		// 각각 키보드의 좌우(WASD, 화살표)와 상하 입력을 감지함
		

		if(playerID == 1)
		{
			inputVec1.x = Input.GetAxisRaw("Player1HorizontalKey");
			inputVec1.y = Input.GetAxisRaw("Player1VerticalKey");
		}
		else if(playerID == 2)
		{
			inputVec2.x = Input.GetAxisRaw("Player2HorizontalKey");
			inputVec2.y = Input.GetAxisRaw("Player2VerticalKey");
		}


	}
		
    	// FixedUpdate는 물리 연산이 이루어지는 고정된 주기로 호출되므로,
    	// 물리적 이동은 여기서 처리하는 것이 적합하다.
	void FixedUpdate()
	{
		Vector2 nextVec = Vector2.zero;	
		if(playerID == 1)
		{
			// 입력 벡터를 정규화하여 속도와 델타 시간에 맞춰 다음 위치를 계산	
			nextVec = inputVec1.normalized * speed * Time.fixedDeltaTime;
		}
		else if(playerID == 2)
		{
			// 입력 벡터를 정규화하여 속도와 델타 시간에 맞춰 다음 위치를 계산
			nextVec = inputVec2.normalized * speed * Time.fixedDeltaTime;
		}
		
		// Rigidbody2D의 MovePosition 메서드를 사용해 계산된 위치로 물체를 이동시킴
		rigid.MovePosition(rigid.position + nextVec);
	}

	void LateUpdate()
	{
		if(playerID == 1)
		{
			anim.SetFloat("Speed", inputVec1.magnitude);

			// 플레이어가 x축 방향으로 움직이고 있는지 확인
			if(inputVec1.x != 0)
			{
				// x축 입력 값이 음수일 경우 (왼쪽으로 이동 중) 스프라이트를 뒤집음
				// x축 입력 값이 양수일 경우 (오른쪽으로 이동 중) 스프라이트를 원래 방향으로 돌림
				spriter.flipX = inputVec1.x < 0;
			}
		}
		else if(playerID == 2)
		{
			anim.SetFloat("Speed", inputVec2.magnitude);

			// 플레이어가 x축 방향으로 움직이고 있는지 확인
			if(inputVec2.x != 0)
			{
				// x축 입력 값이 음수일 경우 (왼쪽으로 이동 중) 스프라이트를 뒤집음
				// x축 입력 값이 양수일 경우 (오른쪽으로 이동 중) 스프라이트를 원래 방향으로 돌림
				spriter.flipX = inputVec2.x < 0;
			}
		}
		

    	
	}
}