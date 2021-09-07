using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //SlimePrefabを入れる
    public GameObject SlimePrefab;
    //TurtleShellを入れる
    public GameObject TurtleShellPrefab;
    //スタート地点
    private int startPos = 40;
    //ゴール地点
    private int goalPos = 400;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    // Unityちゃんのオブジェクト
    private UnityChanController unitychan;
    // Unityちゃんのオブジェクト（発展）
    //private GameObject unitychan;
    // Unityちゃんとカメラの距離（発展）
    private float difference;

    private float lastGeneratePosZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Unityちゃんのオブジェクトを取得
        this.unitychan = GetTarget();
        // Unityちゃんのオブジェクトを取得（発展）
        //this.unitychan = UnityChanController.Find("unitychan");
        // Unityちゃんとカメラの位置（z座標）の差を求める（発展）
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
        difference = unitychan.transform.position.z - lastGeneratePosZ;
        if (difference >= 15 && unitychan.transform.position.z < goalPos - 40)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 15);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.2f)
                {
                    GameObject cone = Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, unitychan.transform.position.z + 40);
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 15);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 9)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, unitychan.transform.position.z + 40);
                    }
                    else if (10 <= item && item <= 11)
                    {
                        //車を生成
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
