using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharacterMove : MonoBehaviourPun
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform camera;
    private Rigidbody characterrigid;
    private DialogManager manager; 
    Animator ani;
    public float speed = 5f;

    float hAxis;
    float vAxis;

    bool isRun, isLeft, isRight, isRoll;
    bool isForward = false,isBack = false;
    bool isGround;

    
    Vector3 lookForward;
    void Start()
    {
        ani = characterBody.GetComponent<Animator>();
        characterrigid = GetComponent<Rigidbody>();
        manager = GameObject.Find("GameManager").GetComponent<DialogManager>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        GetInput();     
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true && !ani.GetCurrentAnimatorStateInfo(0).IsTag("Roll") && !manager.isAction)
        {
            Jump();
        }
        Animation();
    }
    void GetInput()
    {
        hAxis = manager.isAction? 0 : Input.GetAxis("Horizontal");//a,d이동 값
        vAxis = manager.isAction? 0 : Input.GetAxis("Vertical");//w,s이동 값
        isRun = Input.GetButton("Run");//left shift가 눌렸는지
        isRoll = Input.GetButtonDown("Roll");

        #region input wasd
        if (Input.GetKeyDown(KeyCode.W))
        {
            isForward = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isForward = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isBack = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isBack = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isLeft = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isLeft = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isRight = false;
        }
        #endregion
    }
    public void Move()
    {
        Vector2 moveInput = new Vector2(hAxis, vAxis).normalized;

        bool isMove = moveInput.magnitude != 0;//움직이고 있는가?

        lookForward = new Vector3(camera.forward.x, 0f, camera.forward.z).normalized;//카메라가 보는 방향
        Vector3 lookRight = new Vector3(camera.right.x, 0f, camera.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        if (vAxis != 0f && hAxis != 0f)//대각선 바라보기
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
        //if(!isBack)
        characterBody.LookAt(characterBody.position + moveDir);
        //characterBody.rotation = Quaternion.Slerp(characterBody.rotation, , Time.deltaTime);
        
       // characterBody.forward = lookForward;
        transform.position += moveDir * Time.deltaTime * speed;



    }
    void Animation()
    {
        ani.SetBool("IsLeft", isLeft && vAxis == 0f);
        ani.SetBool("IsRight", isRight && vAxis == 0f);
        ani.SetFloat("vAxis", vAxis, 0.2f, Time.deltaTime);
        ani.SetBool("IsRun", isRun && !isBack && vAxis != 0f);
        if (isRoll && isGround)
        {
            ani.SetTrigger("IsRoll");
        }
    }
    private void Jump()
    {
        characterrigid.AddForce(Vector3.up * 13, ForceMode.Impulse);
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
