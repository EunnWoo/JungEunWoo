using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : MonoBehaviour
{
    public void HitOverlep()
    {  
        Collider[] hit = Physics.OverlapSphere(transform.position - new Vector3(0f, -1f, 0f), 0.6f, 1 << (int)Layer.Monster);
      //  if (hit == null) return;
        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("¿À¹ö·¦¸ÂÀ½");
        }
    }


}
