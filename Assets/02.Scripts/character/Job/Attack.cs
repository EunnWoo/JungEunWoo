//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////인터페이스
//public class Attack : MonoBehaviour 
//{
//    static public Attack instance;
//    string arrowobj;
//    [SerializeField]
//    private Animator animator;
//    [SerializeField]
//    private ObjPoolManager objpool;
//    public Transform firepos;
//    Arrow arrow; // arrow 스크립트
//    public bool Fire { get; private set; }
//    void Start()
//    {
        
//        instance = this;
//        animator = GetComponentInChildren<Animator>();
        
        
//    }
    
    
//    private void Awake()
//    {
//        arrowobj = "Arrow";
//    }


//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Use();
//        }

        
//    }
//    public void Use()
//    {
       
//            if (Job.instance.jobFix == JobInfo.BOW)
//            {
                
//                //StopCoroutine(Shoot());
//                //StartCoroutine(Shoot());

//            }
     
//            if (Job.instance.jobFix == JobInfo.SWORD)
//            {
//                StopCoroutine(Swing());
//                StartCoroutine(Swing());
//            }   
//    }

//    //IEnumerator Shoot()
//    //{
//    //    var arrowObj = objpool.MakeObj(arrowobj);  // 화살 생성
//    //    if (arrowObj != null)
//    //    {
//    //        arrow = arrowObj.GetComponent<Arrow>();
//    //        arrowObj.SetActive(true);
//    //    }
//    //    animator.SetTrigger("Attack");

//    //    yield return null;
    
//    //    while (true)
//    //    {
//    //        arrowObj.transform.position = firepos.transform.position;
//    //        arrowObj.transform.rotation = firepos.transform.rotation;
//    //        if (Input.GetMouseButtonUp(0))
//    //         {
//    //            Debug.Log("fire");
//    //            animator.SetTrigger("Fire");
//    //            Fire = true;

//    //            yield return new WaitForSeconds(0.4f);
                    
                    
              
//    //            break;
//    //         }

//    //        yield return null;
//    //    }
//    //    yield return null;
//    //}
//    IEnumerator Swing()
//    {
//        animator.SetTrigger("Attack");

     
//        yield return null;
//    }

//}

