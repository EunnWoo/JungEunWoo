using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSlime : MonsterController
{
    private float moveSpeed = 8f, rotateSpeed = 3f;
    private void Awake() {   
    }
    protected override void PlayerScan()
    {
        base.PlayerScan();
    }
    protected override void UpdateMoving()
    {
        PlayerScan();
        LookTarget(transform, Managers.Game.GetPlayer().transform, rotateSpeed);
        RigidMovePos(transform, Managers.Game.GetPlayer().transform.position - transform.position, moveSpeed);
    }
    protected override void UpdateAttack()
    {
        SkinnedMeshRenderer bombColor = new SkinnedMeshRenderer();
        bombColor.material.color = new Color(255,0,0,255);
    }
}
