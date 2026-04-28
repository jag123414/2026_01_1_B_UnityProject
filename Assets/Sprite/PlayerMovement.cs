using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float turnSpped = 10f; // 오타(Spped)가 있지만 기존 변수명 유지함

    public Rigidbody rb;

    public bool isGrounded = true; // 세미콜론(;) 추가
    public float lowJump;          // 세미콜론(;) 추가

    void Start()
    {
        // Rigidbody가 할당되지 않았다면 자동으로 가져오기
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // 수정: movement가 0이 아닐 때만 회전하도록 조건 추가
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            // 수정: Slerp 매개변수 형식 수정 (시작값, 목표값, 속도)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpped * Time.deltaTime);
        }

        // 물리 이동 (Rigidbody 사용)
        rb.linearVelocity = new Vector3(moveHorizontal * moveSpeed, rb.linearVelocity.y, moveVertical * moveSpeed);

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // 바닥에 닿았는지 체크하는 함수 추가 (점프를 다시 하기 위해 필요)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
