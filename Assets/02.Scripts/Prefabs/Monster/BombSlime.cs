using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSlime : MonsterController
{

    float explosionTime;
    public float ExplosionTime{
        set{
            explosionTime = value;
        }
    }
    SkinnedMeshRenderer bombColor;
    public SkinnedMeshRenderer BombColor{
        get{
            return bombColor;
        }
        set{
            bombColor = value;
        }
    }
    public Color baseColor{
        get; private set;
    }
    
    private void Awake() {   
        bombColor = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        baseColor = bombColor.material.color;
    }
    protected override void PlayerScan()
    {
        base.PlayerScan();
    }
    protected override void UpdateMoving()
    {
        base.UpdateMoving();
    }
    protected override void UpdateAttack()
    {
        explosionTime += Time.deltaTime;        
        bombColor.material.color -= new Color(0f,0.001f,0.001f,0f);
        if(explosionTime >= 3f){
            Managers.Resource.Instantiate("Explosion").transform.position = transform.position;
            gameObject.SetActive(false);
            bombColor.material.color = baseColor;
            explosionTime = 0;
        }
    }
}
