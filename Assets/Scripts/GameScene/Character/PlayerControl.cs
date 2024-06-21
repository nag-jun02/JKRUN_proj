using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//�v���C���[�I�u�W�F�N�g�ɃA�^�b�`
public class PlayerControl : MonoBehaviour
{
    PlayerMove _PlayerMove;
    float direction;

    Voices _Voices; // �L�����{�C�X�Ǘ��N���X�̌Ăяo��
    ScoreManager _ScoreManager;
    Coroutine _Coroutine;
    BlinkMaterial _BlinkMaterial;

    GameObject uiObject;
    UI _UI;

    //���[���̈ړ��̐��l�����ꂼ��̕ϐ��Ő錾
    private const int MinLane = -2;
    private const int MaxLane = 2;
    private const float LaneWidth = 2.0f;
    private const float StunDuration = 0.6f;

    private CharacterController controller;
    private Animator animator;

    //�L�����̍��W��0�Ő錾
    private Vector3 moveDirection = Vector3.zero;
    private int targetLane;
    private float recoverTime = 0.0f;

    [Header("Player")]
    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    //�A�j���[�V�����̃n�b�V����
    private readonly int damageParamHash = Animator.StringToHash("damage");
    private readonly int clearParamHash = Animator.StringToHash("clear");
    private readonly int cryParamHash = Animator.StringToHash("cry");
    private readonly int jumpParamHash = Animator.StringToHash("jump");
    private readonly int runParamHash = Animator.StringToHash("run");

    private readonly string obstacleTag = "Obstacle";
    private readonly string coinTag = "Coin";
    private readonly string goalTag = "Goal";
    private readonly string uiTag = "UI";

    private void Awake()
    {
        _PlayerMove = new PlayerMove();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    
        _Voices = GetComponent<Voices>(); // �{�C�X
        _BlinkMaterial = GetComponent<BlinkMaterial>();

        // UI����
        uiObject = GameObject.FindGameObjectWithTag(uiTag);
        _UI = uiObject.GetComponent<UI>();

        _ScoreManager = new ScoreManager();
        _Coroutine = new Coroutine();

        SoundManager.instance.Sound(SoundManager.BGM.Game);
    }

    // �L�����N�^�[���C���Ԃ��ǂ�����Ԃ� 
    private bool IsStan()
    {
        return recoverTime > 0.0f || ScoreManager.life <= 0;
    }

    void Update()
    {
        //����̏���
        //HandleInput();

        //�C�₵�����̏���
        if (IsStan())
        {
            //�������~�߂ĕ��A�J�E���g�B
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            //���X�ɉ�������Z�����ɏ�ɉ���������B
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            //X�����͖ڕW�̃|�W�V�����܂ł̍����̊����ő��x���v�Z�B
            float ratioX = (targetLane * LaneWidth - this.transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }


        moveDirection.y -= gravity * Time.deltaTime;

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);
        
        if (controller.isGrounded) moveDirection.y = 0; 

        animator.SetBool(runParamHash, moveDirection.z > 0.0f);
    }

    // �ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            direction = context.ReadValue<float>();
            if (direction > 0) MoveLR(true);
            else if (direction < 0) MoveLR(false);
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsStan()) return;
        if (context.started && controller.isGrounded)
        {
            moveDirection.y = speedJump;
            animator.SetTrigger(jumpParamHash);
        }
    }

    //���E�ړ�
    private void MoveLR(bool moveRight)
    {
        if (IsStan()) return;
        if (moveRight && targetLane < MaxLane) targetLane++;
        if (!moveRight && targetLane > MinLane) targetLane--;
    }

    //�Q�[���I�[�o�[
    private void GameOver()
    {
        speedZ = 0f;
        speedX = 0f;
        speedJump = 0f;
        animator.SetTrigger(cryParamHash);
        _UI.GameOverUI();

        _Voices.GameOverVoice();
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //��Q���ɓ����������̔���   
        if (IsStan()) return;
        if (hit.gameObject.CompareTag(obstacleTag))
        {
            _Voices.DamageVoices();

            // �}�e���A���_��
            StartCoroutine(_BlinkMaterial.BlinkCoroutine());

            _ScoreManager.HitObstacle();

            //���C�tUI����
            _UI.LifeUI();

            recoverTime = StunDuration;

            animator.SetTrigger(damageParamHash);
            Destroy(hit.gameObject);

            //�Q�[���I�[�o�[
            if (ScoreManager.life <= 0)
            {
                GameOver();
            }
        }
    }
    //�S�[������A�R�C��
    private void OnTriggerEnter(Collider other)
    {
        //�R�C���l���̏���
        if (other.CompareTag(coinTag))
        {
            _ScoreManager.GetCoin();
        }
        //�S�[��
        else if (other.gameObject.CompareTag(goalTag))
        {
            if (!controller.isGrounded) moveDirection.y -= gravity * Time.deltaTime;

            //����s�ɂ���
            speedZ = 0f;
            speedX = 0f;
            speedJump = 0f;


            // �N���A���̉��o
            // �J�����̕����ɑ̂���]������
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetTrigger(clearParamHash);

            _UI.ClearUI();
            _Voices.GameClearVoice();
            _ScoreManager.CalculateScore();

            //3.5�b��Ƀ��U���g�V�[���ɑJ��
            StartCoroutine(_Coroutine.DelayCoroutine(3.5f, () =>
            {
                SceneManager.LoadScene("Result");
            }));
        }
    }

    private void OnEnable()
    {
        _PlayerMove.Player.Enable();
    }

    private void OnDisable()
    {
        _PlayerMove.Player.Disable();
    }
}