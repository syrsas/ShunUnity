using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefab������
    public GameObject carPrefab;
    //coinPrefab������
    public GameObject coinPrefab;
    //cornPrefab������
    public GameObject conePrefab;
    //SlimePrefab������
    public GameObject SlimePrefab;
    //TurtleShell������
    public GameObject TurtleShellPrefab;
    //�X�^�[�g�n�_
    private int startPos = 40;
    //�S�[���n�_
    private int goalPos = 400;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;
    // Unity�����̃I�u�W�F�N�g
    private UnityChanController unitychan;
    // Unity�����̃I�u�W�F�N�g�i���W�j
    //private GameObject unitychan;
    // Unity�����ƃJ�����̋����i���W�j
    private float difference;

    private float lastGeneratePosZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Unity�����̃I�u�W�F�N�g���擾
        this.unitychan = GetTarget();
        // Unity�����̃I�u�W�F�N�g���擾�i���W�j
        //this.unitychan = UnityChanController.Find("unitychan");
        // Unity�����ƃJ�����̈ʒu�iz���W�j�̍������߂�i���W�j
        this.difference = unitychan.transform.position.z - this.transform.position.z; 
    }

    // Update is called once per frame
    void Update()
    {
        if (unitychan.m_isDead)
        {
            var newTarget = GetTarget();

            if (newTarget)
            {
                //Unity�����̃I�u�W�F�N�g���擾
                this.unitychan = newTarget;
                //Unity�����ƃJ�����̈ʒu�iz���W�j�̍������߂�
                this.difference = unitychan.transform.position.z - this.transform.position.z;
            }
        }
        else
        {
            //Unity�����̈ʒu�ɍ��킹�ăJ�����̈ʒu���ړ�
            this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
        }
        difference = unitychan.transform.position.z - lastGeneratePosZ;
        if (difference >= 15 && unitychan.transform.position.z < goalPos - 40)
        {
            //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
            int num = Random.Range(1, 15);
            if (num <= 2)
            {
                //�R�[����x�������Ɉ꒼���ɐ���
                for (float j = -1; j <= 1; j += 0.2f)
                {
                    GameObject cone = Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, unitychan.transform.position.z + 40);
                }
            }
            else
            {

                //���[�����ƂɃA�C�e���𐶐�
                for (int j = -1; j <= 1; j++)
                {
                    //�A�C�e���̎�ނ����߂�
                    int item = Random.Range(1, 15);
                    //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                    int offsetZ = Random.Range(-5, 6);
                    //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                    if (1 <= item && item <= 9)
                    {
                        //�R�C���𐶐�
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, unitychan.transform.position.z + 40);
                    }
                    else if (10 <= item && item <= 11)
                    {
                        //�Ԃ𐶐�
                        GameObject car = Instantiate(carPrefab);
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, unitychan.transform.position.z + 40);
                    }
                    else if(12 <= item && item <= 13)
                    {
                        GameObject slime = Instantiate(SlimePrefab);
                        slime.transform.position = new Vector3(posRange * j, slime.transform.position.y, unitychan.transform.position.z + 40);
                    }
                    else if(14 <= item && item <= 15)
                    {
                        GameObject turtleshell = Instantiate(TurtleShellPrefab);
                        turtleshell.transform.position = new Vector3(posRange * j, turtleshell.transform.position.y, unitychan.transform.position.z + 40);
                    }
                }
            }
            lastGeneratePosZ = unitychan.transform.position.z;
        }
    }
    UnityChanController GetTarget()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            GameObject p = players[i];
            UnityChanController c = p.GetComponent<UnityChanController>();

            if (c != null && c.m_isDead == false)
            {
                return c;
            }
        }
        return null;
    }
}
