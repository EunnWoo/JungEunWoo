using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform camera;
    Vector3 moveVec;
    float hAxis;
    float vAxis;
    bool isRun;
    bool isBack = false;
    Animator ani;
    void Start()
    {
        ani = characterBody.GetComponent<Animator>();
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        isRun = Input.GetButton("Run");
        if(Input.GetKeyDown(KeyCode.S)){
            isBack = true;
        }
        else if(Input.GetKeyUp(KeyCode.S)){
            isBack = false;
        }
        Vector2 moveInput = new Vector2(hAxis, vAxis) * (isRun && !isBack ? 1.3f : 1f) * (!isBack ? 1f : 0.5f);
        bool isMove = moveInput.magnitude != 0;
        ani.SetFloat("vAxis",vAxis,0.2f,Time.deltaTime);
        
        // ani.SetBool("IsWalk", isMove);
        ani.SetBool("IsRun", isRun && !isBack);
        // ani.SetBool("IsBack", isBack);
        Vector3 lookForward = new Vector3(camera.forward.x, 0f, camera.forward.z).normalized;
        Vector3 lookRight = new Vector3(camera.right.x, 0f, camera.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        if (vAxis != 0f && hAxis != 0f)
        {
            if(hAxis * vAxis > 0f){
                lookForward = Quaternion.Euler(0, 45, 0).normalized * lookForward;
            }
            else{
                lookForward = Quaternion.Euler(0, -45, 0).normalized * lookForward;
            }
        }
        characterBody.forward = lookForward;
        transform.position += moveDir * Time.deltaTime * 5f;
    }
}
