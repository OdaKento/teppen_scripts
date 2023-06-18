using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField, Header("offsetPos")] private float y;
    // �G�t�F�N�g�𐶐�����
    public void InstantiateEffect(ParticleSystem _particle,Transform transform)
    {
        // _particle�ɉ����Ȃ��Ƃ��́A�������Ȃ�
        if (_particle == null)
        {
            return;
        }
        //Debug.Log("�G�t�F�N�g�����I�I");
        ParticleSystem _newParticle = Instantiate(_particle);       // �G�t�F�N�g�𐶐�����
        _newParticle.transform.position = new Vector3(transform.position.x, transform.position.y + y,transform.position.z);       // �ʒu���X�V����
    }
}
