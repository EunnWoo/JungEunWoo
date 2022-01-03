using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraSetUp : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>(); //가상 카메라의 추적 대상을 자신의 트랜스폼으로 변경
            followCam.Follow = transform;
            followCam.LookAt = transform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
