using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapphiArtchanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float velocityZ = 16f;
    // �������̑��x�i�ǉ��j
    private float velocityX = 10f;
    // ������̑��x�i�ǉ��j
    private float velocityY = 10f;
    // ���E�̈ړ��ł���͈́i�ǉ��j
    private float movableRange = 3.4f;
    // ����������������W���i�ǉ��j
    private float coefficient = 0.99f;
    // �Q�[���I���̔���i�ǉ��j
    private bool isEnd = false;
    // �Q�[���I�����ɕ\������e�L�X�g�i�ǉ��j
    private GameObject stateText;
    //�X�R�A��\������e�L�X�g�i�ǉ��j
    private GameObject scoreText;
    //���_�i�ǉ��j
    private int score = 0;
    //���{�^�������̔���i�ǉ��j
    private bool isLButtonDown = false;
    //�E�{�^�������̔���i�ǉ��j
    private bool isRButtonDown = false;
    //�W�����v�{�^�������̔���i�ǉ��j
    private bool isJButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();
        // �V�[������stateText�I�u�W�F�N�g���擾�i�ǉ��j
        this.stateText = GameObject.Find("GameResultText");

        // �V�[������scoreText�I�u�W�F�N�g���擾�i�ǉ��j
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //������̓��͂ɂ�鑬�x�i�ǉ��j
        float inputVelocityY = 0;
        //�W�����v�A�j�����Đ��i�ǉ��j
        this.myAnimator.SetBool("Jump", true);
        //������ւ̑��x�����i�ǉ��j
        inputVelocityY = this.velocityY;
        //���݂�Y���̑��x�����i�ǉ��j
        inputVelocityY = this.myRigidbody.velocity.y;
        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����i�ǉ��j
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        this.myRigidbody.velocity = new Vector3(0, 0, this.velocityZ);
        
    }
}
