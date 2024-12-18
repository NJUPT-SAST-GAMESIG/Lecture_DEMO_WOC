using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Class1.SunGeneration
{
    public class SunPrefab : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;
        [SerializeField] private float fallSpeed = 3f;
        [SerializeField] private float maxExistTime = 10f;
        private float _curExistTime;
        private float _finalY;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _finalY = Random.Range(-3.0f, -1.0f);
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
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
            if (curPosition.y <= _finalY)
            {
                _rb.velocity = Vector2.zero;
            }

            _curExistTime += Time.deltaTime;
            if (_curExistTime / maxExistTime >= 0.8f)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g,
                    _spriteRenderer.color.b, 0.8f);
            }

            if (collider2Ds.Contains(_collider2D))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(gameObject);
                    // TODO: add sun
                }
            }
        }
    }
}