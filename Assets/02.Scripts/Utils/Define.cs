﻿using System.Collections;
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

    public enum Potal
    {
        goTownPotal,
        goMap1Potal,
        goMap2Potal,
        goMap3Potal,
    }
    public enum Sound
    {
        BGM,
        Effect,
        LoopEffect,
        MaxCount
    }
}

