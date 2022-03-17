using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowChargeParticle : MonoBehaviour
{
    private void OnEnable()
    {
        Managers.Sound.Play("EffectSound/ArrowCharge", Define.Sound.LoopEffect);
    }
    private void OnEnable()
    {
        Managers.Sound.StopSound(Define.Sound.LoopEffect);
    }
}
