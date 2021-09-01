using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapphiArtchanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;
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
    private bool isEnd = false;
    // ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;
    //スコアを表示するテキスト（追加）
    private GameObject scoreText;
    //得点（追加）
    private int score = 0;
    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
    private bool isRButtonDown = false;
    //ジャンプボタン押下の判定（追加）
    private bool isJButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();
        // シーン中のstateTextオブジェクトを取得（追加）
        this.stateText = GameObject.Find("GameResultText");

        // シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //上方向の入力による速度（追加）
        float inputVelocityY = 0;
        //ジャンプアニメを再生（追加）
        this.myAnimator.SetBool("Jump", true);
        //上方向への速度を代入（追加）
        inputVelocityY = this.velocityY;
        //現在のY軸の速度を代入（追加）
        inputVelocityY = this.myRigidbody.velocity.y;
        //Jumpステートの場合はJumpにfalseをセットする（追加）
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        this.myRigidbody.velocity = new Vector3(0, 0, this.velocityZ);
        
    }
}
