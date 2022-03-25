using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Define
{
    public enum UIEvent
    {
        Click,
        Drag,
        OnPointer,
        OnPointerExit
    }
    public enum AttackType
    {
        NormalAttack,
        SkillAttack
    }
    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerRightDown,
        PointerUp,
        Click,
    }
    public enum Sound
    {
        BGM,
        Effect,
        LoopEffect,
        MaxCount
    }
    public enum Error
    {
        NoneWeapon,
        OtherWeapon,
        NoneJob,
        CoolTime,
        NonePotion,
        NoneGold,
        MaxInv
    }

    public enum Items
    {
        HpPotion,
        MpPotion,
        Bow1,
        Bow2,
        Sword1,
        Sword2,
        Wand1,
        Wand2,
        Coin
    }
}

