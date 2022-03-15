using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Damage : UI_Base
{
    bool bInit;
    enum GameObjects
    {
        DamageText
    }
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;

    TextMeshProUGUI damageText;

    [SerializeField]
    Color alpha;

    public float damage;
    public Transform target;

    public override void Init()
    {
        if (bInit) return;
        bInit = true;


        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        Bind<GameObject>(typeof(GameObjects));
        damageText = Get<GameObject>((int)GameObjects.DamageText).GetComponent<TextMeshProUGUI>();
        alpha = damageText.color;
        damageText.text = damage.ToString();
        
        Vector3 parent = new Vector3(transform.parent.position.x, transform.parent.GetComponent<Collider>().bounds.max.y, transform.parent.position.z);
        
        transform.position = parent;
        
        
        Invoke("DestroyObject", destroyTime);
    }

    private void Update()
    {
        
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);// �ؽ�Ʈ ���� ����
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); //�ؽ�Ʈ ���İ� ����
        damageText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
