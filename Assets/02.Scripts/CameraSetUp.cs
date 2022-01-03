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
            CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>(); //���� ī�޶��� ���� ����� �ڽ��� Ʈ���������� ����
            followCam.Follow = transform;
            followCam.LookAt = transform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
