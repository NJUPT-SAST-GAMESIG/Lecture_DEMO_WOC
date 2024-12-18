using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class SunlightPrefab : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    [SerializeField] private float fallSpeed = 3f;
    [SerializeField] private float maxExistTime = 10f;
    private float _curExistTime;
    private float _finalY;
    private Camera _camera;
    private SunManager _sunManager;
    //依赖注入版本
    // private ISunManager _sunManager; // 使用接口类型引用 SunManager
    // public void SetSunManager(ISunManager sunManager)
    // {
    //     _sunManager = sunManager;
    // }
    [SerializeField] private AudioClip sunClickSound; // 用于存储音效
    private AudioSource _audioSource;//如果有多个地方需要播放音效，可以创建一个独立的 AudioManager 脚本统一管理音效播放。
    private void Awake()
    {
        _camera = Camera.main;
        _finalY = Random.Range(-3.0f, -1.0f);
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _sunManager = GameObject.Find("PlantsChooser").GetComponent<SunManager>();
        // 获取 AudioSource 组件，如果没有，动态添加
        // _audioSource = GetComponent<AudioSource>();
        // if (_audioSource == null)
        // {
        //     Debug.LogError("No audio source found");
        _audioSource = gameObject.AddComponent<AudioSource>();
        // }
        // 初始化 AudioSource 设置
        _audioSource.playOnAwake = false; // 不自动播放
        _audioSource.spatialBlend = 0.0f; // 确保是 2D 声音
        _audioSource.volume = 1.0f; // 设置音量为 100%
        _audioSource.loop = false; // 不循环播放
        _audioSource.minDistance = 1.0f;
        _audioSource.maxDistance = 50.0f;
    }

    private void Start()
    {
        _rb.velocity += Vector2.down * fallSpeed;
        Destroy(gameObject, maxExistTime);
    }

    private void Update()
    {
        var collider2Ds = Physics2D.OverlapPointAll(_camera.ScreenToWorldPoint(Input.mousePosition));

        var curPosition = transform.position;
        if (curPosition.y <= _finalY) _rb.velocity = Vector2.zero;

        _curExistTime += Time.deltaTime;
        if (_curExistTime / maxExistTime >= 0.8f)
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g,
                _spriteRenderer.color.b, 0.8f);

        if (collider2Ds.Contains(_collider2D)&&Input.GetMouseButtonDown(0))
        {
            PlayClickSound(); // 播放点击音效
            Destroy(gameObject);
            // if (sunManager != null)
            // {
                _sunManager.SunIncrease();
            // }
        }
        
    }
    private void PlayClickSound()
    {
        if (sunClickSound != null && _audioSource != null)
        {
            // _audioSource.PlayOneShot(sunClickSound); // 播放点击音效
            CardSoundManager.Instance.Play(CardSoundType.SunClickSound);
        }
    }

}