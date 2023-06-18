using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField, Header("offsetPos")] private float y;
    // エフェクトを生成する
    public void InstantiateEffect(ParticleSystem _particle,Transform transform)
    {
        // _particleに何もないときは、処理を省く
        if (_particle == null)
        {
            return;
        }
        //Debug.Log("エフェクト生成！！");
        ParticleSystem _newParticle = Instantiate(_particle);       // エフェクトを生成する
        _newParticle.transform.position = new Vector3(transform.position.x, transform.position.y + y,transform.position.z);       // 位置を更新する
    }
}
