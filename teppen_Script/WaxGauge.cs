using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaxGauge : playerattack
{
    private int Waxgauge = 0;
    private int MaxWax = 100;
    private bool WaxAttack = false;
    private bool Brock = false;
    private bool EX = false;
    private Slider Waxslider;


    // Start is called before the first frame update
    void Start()
    {
        //初期量を追加
        Waxgauge = 75;
        //Sliderを規定値に
        Waxslider.value = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃が当たったか
        if (WaxAttack)
        {
            Waxgauge += 10;
            Waxslider.value = (float)Waxgauge / MaxWax;
            Debug.Log("slider.value : " + Waxslider.value);
            WaxAttack = false;
        }

        //防御した場合(仮組みでし)
        if (Brock)
        {
            Waxgauge += 10;
            Waxslider.value = (float)Waxgauge / MaxWax;
            Debug.Log("slider.value : " + Waxslider.value);
            Brock = false;
        }

        //必殺技使用時(仮組みでち)
        if (EX)
        {
            Waxgauge += 10;
            Waxslider.value = (float)Waxgauge / MaxWax;
            Debug.Log("slider.value : " + Waxslider.value);
            EX = false;
        }

    }

    //攻撃の当たり判定
    private void OnTriggerEnter(Collider other)
    {
        //髪の毛が触れているか
        if (other.gameObject.CompareTag("Hit"))
        {
            //Debug.Log("[1] other.gameObject.tag : " + other.gameObject.tag);

            //攻撃判定が出ている時Hitフラグを上げる
            if (other.gameObject.transform.root.gameObject.GetComponent<playerattack>()._get_HardAttack_CollisionF())
            {
                //Debug.Log("[2] other.gameObject.tag : " + other.gameObject.tag);
                WaxAttack = true;
            }

        }

    }
}
