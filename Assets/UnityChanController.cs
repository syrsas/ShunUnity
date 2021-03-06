using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //追加

public class UnityChanController : MonoBehaviour
{
    // Unityちゃんのオブジェクト
    private UnityChanController unitychan;
    // Unityちゃんとカメラの距離
    private float difference;
    // アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    // Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;
    // 前方向の速度（追加）
    private float velocityZ = 16f;
    // 横方向の速度（追加）
    private float velocityX = 10f;
    // 上方向の速度（追加）
    private float velocityY = 10f;
    // 左右の移動できる範囲（追加）
    private float movableRange = 3.4f;
    // 動きを減速させる係数（追加）
    private float coefficient = 0.99f;
    // ゲーム終了の判定（追加）
    public static bool isEnd = false;
    // ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;
    //スコアを表示するテキスト（追加）
    private GameObject scoreText;
    //得点（追加）
    private static int score = 0;
    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
    private bool isRButtonDown = false;
    //ジャンプボタン押下の判定（追加）
    private bool isJButtonDown = false;
    public bool m_isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        // Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        // 走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        // Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();

        // シーン中のstateTextオブジェクトを取得（追加）
        this.stateText = GameObject.Find("GameResultText");

        // シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("ScoreText");
        // Unityちゃんとカメラの位置（z座標）の差を求める
        //this.difference = unitychan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Unityちゃんのオブジェクトを取得
        //this.unitychan = GetTarget();
        var newTarget = GetTarget();
        if (newTarget == null)
        {
            m_isDead = true;
            //stateTextにGAME OVERを表示（追加）
            this.stateText.GetComponent<Text>().text = "GAME OVER";
            //Unityちゃんの位置に合わせてカメラの位置を移動
            //this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
        }

        //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
        if (isEnd || m_isDead)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //横方向の入力による速度（追加）
        float inputVelocityX = 0;
        //上方向の入力による速度（追加）
        float inputVelocityY = 0;

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左方向への速度を代入（追加）
            inputVelocityX = -this.velocityX;
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入（追加）
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていない時にスペースが押されたらジャンプする（追加）
        if((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生（追加）
            this.myAnimator.SetBool("Jump", true);
            //上方向への速度を代入（追加）
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入（追加）
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jumpステートの場合はJumpにfalseをセットする（追加）
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unityちゃんに速度を与える（追加）
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理（追加）
    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合（追加）
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            
             isEnd = true;
             //stateTextにGAME OVERを表示（追加）
             this.stateText.GetComponent<Text>().text = "GAME OVER";
        
            
        }
        //敵にぶつかった場合
        if (other.gameObject.tag == "EmemyTag")
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
        }
        //ゴール地点に到達した場合（追加）
        if(other.gameObject.tag == "GoalTag")
        {
            isEnd = true;
            //stateTextにGAME CLEARを表示（追加）
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        //コインに衝突した場合（追加）
        if(other.gameObject.tag == "CoinTag")
        {
            //スコアを加算（追加）
            score += 10;
            //ScoreText獲得した点数を表示（追加）
            this.scoreText.GetComponent<Text>().text = "Score " + score + "pt";
            //パーティクルを再生（追加）
            GetComponent<ParticleSystem>().Play();
            /*
            if (unitychan.m_isDead)
            {
                var newTarget = GetTarget();

                if (newTarget)
                {
                    //Unityちゃんのオブジェクトを取得
                    this.unitychan = newTarget;
                    //Unityちゃんとカメラの位置（z座標）の差を求める
                    this.difference = unitychan.transform.position.z - this.transform.position.z;
                    //スコアを加算（追加）
                    score += 10;
                    //ScoreText獲得した点数を表示（追加）
                    this.scoreText.GetComponent<Text>().text = "Score " + score + "pt";
                    //パーティクルを再生（追加）
                    GetComponent<ParticleSystem>().Play();
                    //接触したコインのオブジェクトを破棄（追加）
                    Destroy(other.gameObject);
                }
            }*/
            //接触したコインのオブジェクトを破棄（追加）
            Destroy(other.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EmemyTag" || collision.gameObject.tag == "CarTag" || collision.gameObject.tag == "TrafficConeTag")
        {
            m_isDead = true;
        }
    }
    //ジャンプボタンを押した場合の処理（追加）
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }
    //ジャンプボタンを離した場合の処理（追加）
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }
    //左ボタンを押し続けた場合の処理（追加）
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理（追加）
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    //右ボタンを押し続けた場合の処理（追加）
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理（追加）
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
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
