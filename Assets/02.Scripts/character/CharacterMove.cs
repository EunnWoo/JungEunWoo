//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;

//public class CharacterMove : MonoBehaviourPun
//{
//    [SerializeField]
//    private Transform characterBody;
//    [SerializeField]
//    private Transform target;
//    [SerializeField]
//    private Transform camera;
//    private Rigidbody characterrigid;
//    private DialogManager manager; 
//    Animator ani;
//    public float speed = 5f;

//    float hAxis;
//    float vAxis;
//    float moveAmount;

//    bool isRun, isRoll, roll;
//    bool isForward = false;
//    [SerializeField]
//    bool isGround;
//    bool isJump;
//    [HideInInspector]
//    public bool canMove;


//    Vector3 moveVec;
//    Vector3 RollVec;
//    void Start()
//    {
//        ani = characterBody.GetComponent<Animator>();
//        characterrigid = GetComponent<Rigidbody>();
//        manager = GameObject.Find("GameManager").GetComponent<DialogManager>();
//        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//        canMove = true;
//    }
//    void Update()
//    {
       
//        GetInput();
//        Jump();
//        Move();
       
//        Roll();
       
//    }
//    void GetInput()
//    {
//        hAxis = manager.isAction? 0 : Input.GetAxis("Horizontal");//a,d이동 값
//        vAxis = manager.isAction? 0 : Input.GetAxis("Vertical");//w,s이동 값
//        isRun = Input.GetButton("Run");//left shift가 눌렸는지
//        isRoll = Input.GetButtonDown("Roll");
//        isJump = Input.GetKeyDown(KeyCode.Space);


//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            isForward = true;
//        }
//        else if (Input.GetKeyUp(KeyCode.W))
//        {
//            isForward = false;
//        }
       
//    }
//    public void Move()
//    {
//        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

//        if (roll)
//            moveVec = RollVec;

//        if (!canMove)
//        {
//            moveVec = Vector3.zero; // 정지시키기 못움직이게하게
//            Debug.Log(canMove);
//        }

//        transform.position += moveVec * speed * (isRun ? 1f : 0.5f) * Time.deltaTime;

//        float m = Mathf.Abs(hAxis) + Mathf.Abs(vAxis);
//        moveAmount = Mathf.Clamp01(m);
//        ani.SetFloat("vAxis", moveAmount, 0.2f, Time.deltaTime);
//        ani.SetBool("IsRun", isRun && moveAmount != 0f);
//      //  ani.SetBool("IsRun", moveVec != Vector3.zero);
//        //ani.SetBool("IsWalk", isRun);
//        transform.LookAt(transform.position + moveVec);

//    }


//    private void Jump()
//    {
//        if (Input.GetKeyDown(KeyCode.Space) && isGround == true && 
//            !roll && !manager.isAction)
//        {
//            characterrigid.AddForce(Vector3.up * 17, ForceMode.Impulse);

            
//        }
//    }
//    //private void OnTriggerStay(Collider other)
//    //{
//    //    if (other.gameObject.tag == "Ground")
//    //    {
//    //        isGround = true;
//    //        ani.SetBool("IsJump", false);
//    //    }
//    //}
//    //private void OnTriggerExit(Collider collision)
//    //{
//    //    if (collision.gameObject.tag == "Ground")
//    //    {
//    //        isGround = false;
//    //        ani.SetBool("IsJump", true);
//    //    }
//    //}
//}
