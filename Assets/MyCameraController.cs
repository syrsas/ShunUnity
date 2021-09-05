using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    // Unityちゃんのオブジェクト
    private UnityChanController unitychan;
    // Unityちゃんとカメラの距離
    private float difference;


    // Start is called before the first frame update
    void Start()
    {
        // Unityちゃんのオブジェクトを取得
        this.unitychan = GetTarget();
        //this.unitychan = GameObject.Find("unitychan");
        // Unityちゃんとカメラの位置（z座標）の差を求める
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
                //Unityちゃんのオブジェクトを取得
                this.unitychan = newTarget;
                //Unityちゃんとカメラの位置（z座標）の差を求める
                this.difference = unitychan.transform.position.z - this.transform.position.z;
            }
        }
        else
        {
            //Unityちゃんの位置に合わせてカメラの位置を移動
            this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
        }
    }
    UnityChanController GetTarget()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var p in players)
        {
            var c = p.GetComponent<UnityChanController>();

            if(c && !c.m_isDead)
            {
                return c;
            }
        }
        return null;
    }
    /*UnityChanController GetTarget()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");​
        foreach (var p in players)
        {
            var c = p.GetComponent<UnityChanController>();

            if (c && !c.m_isDead)
            {
                return c;
            }
        }​
        return null;
    }*/
}
