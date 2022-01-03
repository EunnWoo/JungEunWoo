using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform camera;
    private Rigidbody characterrigid;
    Vector3 moveVec;
    Animator ani;
    float hAxis;
    float vAxis;
    bool isRun, isRoll = false;
    bool isBack = false;
    bool isGround;
    void Start()
    {
        ani = characterBody.GetComponent<Animator>();
        characterrigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
            Jump();
    }
    private void Move()
    {
        hAxis = Input.GetAxis("Horizontal");//a,d이동 값
        vAxis = Input.GetAxis("Vertical");//w,s이동 값
        isRun = Input.GetButton("Run");//left shift가 눌렸는지
        isRoll = Input.GetButton("Roll");
        if (Input.GetKeyDown(KeyCode.S))
        {
            isBack = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isBack = false;
        }
        Vector2 moveInput = new Vector2(hAxis, vAxis).normalized * (isRun && !isBack ? 1.3f : 1f) * (!isBack ? 1f : 0.5f);//이동을 하고 있는지 뛰면 속도가 1.3 뒤로가고있으면 원래 속도의 0.5
        bool isMove = moveInput.magnitude != 0;//움직이고 있는가?
        Vector3 lookForward = new Vector3(camera.forward.x, 0f, camera.forward.z).normalized;//카메라가 보는 방향
        Vector3 lookRight = new Vector3(camera.right.x, 0f, camera.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        if (vAxis != 0f && hAxis != 0f)
        {
            if (hAxis * vAxis >= 0f)
            {
                lookForward = Quaternion.Euler(0, 45, 0) * lookForward;
            }
            else if (hAxis * vAxis < 0f)
            {
                lookForward = Quaternion.Euler(0, -45, 0) * lookForward;
            }
        }
        if (Input.GetKey(KeyCode.A) && vAxis == 0f)
        {
            moveDir *= 0.7f;
            ani.SetBool("IsLeft", true);
            ani.SetBool("IsLeftRoll", isRoll);
        }
        else if (Input.GetKeyUp(KeyCode.A) || vAxis != 0f)
        {
            //moveDir *= 1f;
            ani.SetBool("IsLeft", false);
        }
        if (Input.GetKey(KeyCode.D) && vAxis == 0f)
        {
            moveDir *= 0.7f;
            ani.SetBool("IsRight", true);
            ani.SetBool("IsRightRoll", isRoll);
        }
        else if (Input.GetKeyUp(KeyCode.D) || vAxis != 0f)
        {
            //moveDir *= 1f;
            ani.SetBool("IsRight", false);
        }
        ani.SetFloat("vAxis", vAxis, 0.2f, Time.deltaTime);
        ani.SetBool("IsRun", isRun && !isBack);
        ani.SetBool("IsRoll", isRoll && !isBack);
        ani.SetBool("IsBackRoll", isRoll && isBack);
        characterBody.forward = lookForward;
        transform.position += moveDir * Time.deltaTime * 5f;
    }
    private void Jump()
    {
        characterrigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        ani.SetBool("IsJump", true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            ani.SetBool("IsJump", false);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}
