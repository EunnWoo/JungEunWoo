using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowChargeParticle : MonoBehaviour
{
    
    private void OnEnable()
    {
        Managers.Sound.Play("EffectSound/Attack/ArrowCharge", Define.Sound.LoopEffect);
        
    }
    
    private void OnDisable()
    {
        Managers.Sound.StopSound("EffectSound/Attack/ArrowCharge");
    }
}
