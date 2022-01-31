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
    float moveAmount;

    bool isRun, isRoll;
    bool isForward = false;
    [SerializeField]
    bool isGround;
    bool isJump;



    Vector3 moveVec;
    Vector3 RollVec;
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
        Jump();
        Roll();
        Animation();
    }
    void GetInput()
    {
        hAxis = manager.isAction? 0 : Input.GetAxis("Horizontal");//a,d이동 값
        vAxis = manager.isAction? 0 : Input.GetAxis("Vertical");//w,s이동 값
        isRun = Input.GetButton("Run");//left shift가 눌렸는지
        isRoll = Input.GetButtonDown("Roll");
        isJump = Input.GetKeyDown(KeyCode.Space);


        if (Input.GetKeyDown(KeyCode.W))
        {
            isForward = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isForward = false;
        }
       
    }
    public void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (isRoll)
            moveVec = RollVec;

       // if (!isFireReady)
         //   moveVec = Vector3.zero; // 정지시키기 못움직이게하게

        transform.position += moveVec * speed * (isRun ? 1f : 0.5f) * Time.deltaTime;


        ani.SetBool("IsRun", moveVec != Vector3.zero);
        //ani.SetBool("IsWalk", isRun);
        transform.LookAt(transform.position + moveVec);

    }
    void Roll()
    {
        if (isJump && moveVec != Vector3.zero && !isJump && !isRoll)
        {
            RollVec = moveVec;
            speed *= 2;
            isRoll = true;
           
            Invoke("RollOut", 0.4f);
        }
    }
    void RollOut()
    {
        speed *= 0.5f;
        isRoll = false;
    }
    void Animation()
    {
        float m = Mathf.Abs(hAxis) + Mathf.Abs(vAxis);
        moveAmount = Mathf.Clamp01(m);
        ani.SetFloat("vAxis", moveAmount, 0.2f, Time.deltaTime);
        ani.SetBool("IsRun", isRun && moveAmount != 0f);
        if (isRoll && isGround)
        {
            ani.SetTrigger("IsRoll");
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true && !ani.GetCurrentAnimatorStateInfo(0).IsTag("Roll") && !manager.isAction)
        {
            characterrigid.AddForce(Vector3.up * 13, ForceMode.Impulse);

            ani.SetBool("IsJump", true);
        }
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
