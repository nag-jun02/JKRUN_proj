using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//プレイヤーオブジェクトにアタッチ
public class PlayerControl : MonoBehaviour
{
    PlayerMove _PlayerMove;
    float direction;

    Voices _Voices; // キャラボイス管理クラスの呼び出し
    ScoreManager _ScoreManager;
    Coroutine _Coroutine;
    BlinkMaterial _BlinkMaterial;

    GameObject uiObject;
    UI _UI;

    //レーンの移動の数値をそれぞれの変数で宣言
    private const int MinLane = -2;
    private const int MaxLane = 2;
    private const float LaneWidth = 2.0f;
    private const float StunDuration = 0.6f;

    private CharacterController controller;
    private Animator animator;

    //キャラの座標を0で宣言
    private Vector3 moveDirection = Vector3.zero;
    private int targetLane;
    private float recoverTime = 0.0f;

    [Header("Player")]
    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    //アニメーションのハッシュ化
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
    
        _Voices = GetComponent<Voices>(); // ボイス
        _BlinkMaterial = GetComponent<BlinkMaterial>();

        // UI制御
        uiObject = GameObject.FindGameObjectWithTag(uiTag);
        _UI = uiObject.GetComponent<UI>();

        _ScoreManager = new ScoreManager();
        _Coroutine = new Coroutine();

        SoundManager.instance.Sound(SoundManager.BGM.Game);
    }

    // キャラクターが気絶状態かどうかを返す 
    private bool IsStan()
    {
        return recoverTime > 0.0f || ScoreManager.life <= 0;
    }

    void Update()
    {
        //操作の処理
        //HandleInput();

        //気絶した時の処理
        if (IsStan())
        {
            //動きを止めて復帰カウント。
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            //徐々に加速してZ方向に常に加速させる。
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            //X方向は目標のポジションまでの差分の割合で速度を計算。
            float ratioX = (targetLane * LaneWidth - this.transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }


        moveDirection.y -= gravity * Time.deltaTime;

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);
        
        if (controller.isGrounded) moveDirection.y = 0; 

        animator.SetBool(runParamHash, moveDirection.z > 0.0f);
    }

    // 移動処理
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

    //左右移動
    private void MoveLR(bool moveRight)
    {
        if (IsStan()) return;
        if (moveRight && targetLane < MaxLane) targetLane++;
        if (!moveRight && targetLane > MinLane) targetLane--;
    }

    //ゲームオーバー
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
        //障害物に当たった時の判定   
        if (IsStan()) return;
        if (hit.gameObject.CompareTag(obstacleTag))
        {
            _Voices.DamageVoices();

            // マテリアル点滅
            StartCoroutine(_BlinkMaterial.BlinkCoroutine());

            _ScoreManager.HitObstacle();

            //ライフUI処理
            _UI.LifeUI();

            recoverTime = StunDuration;

            animator.SetTrigger(damageParamHash);
            Destroy(hit.gameObject);

            //ゲームオーバー
            if (ScoreManager.life <= 0)
            {
                GameOver();
            }
        }
    }
    //ゴール判定、コイン
    private void OnTriggerEnter(Collider other)
    {
        //コイン獲得の処理
        if (other.CompareTag(coinTag))
        {
            _ScoreManager.GetCoin();
        }
        //ゴール
        else if (other.gameObject.CompareTag(goalTag))
        {
            if (!controller.isGrounded) moveDirection.y -= gravity * Time.deltaTime;

            //操作不可にする
            speedZ = 0f;
            speedX = 0f;
            speedJump = 0f;


            // クリア時の演出
            // カメラの方向に体を回転させる
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetTrigger(clearParamHash);

            _UI.ClearUI();
            _Voices.GameClearVoice();
            _ScoreManager.CalculateScore();

            //3.5秒後にリザルトシーンに遷移
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