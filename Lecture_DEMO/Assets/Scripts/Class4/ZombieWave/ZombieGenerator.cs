using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ZombieGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = new Transform[5];
    [SerializeField] private List<ZombieWave> _waves = new();
    [SerializeField] private Slider waveSlider;
    private GameObject _zombiePrefab;
    private Transform _zombieParent;
    private float _lastGenerateTime = 0;
    private bool _isWave = false;
    private float _waveStartTime;
    private float _waveDuration;
    private float _waveGenerateCd = 1;
    public float NormalGenerateCd;
    private float _generateCd;

    private void Start()
    {
        _zombiePrefab = Resources.Load<GameObject>("ZombiesPrefab/Zombie");
        _zombieParent = GameObject.Find("Canvas/Zombies").transform;
        waveSlider.onValueChanged.AddListener(ZombieWaveCallback);
        _generateCd = NormalGenerateCd;
    }

    private void FixedUpdate()
    {
        waveSlider.value += Time.fixedDeltaTime / 120; //进度条跟进
        if (!(Time.time > _generateCd + _lastGenerateTime)) //每隔generateCD在随机轨道生成僵尸
            return;
        if (_isWave)
        {
            WaveGenerate();
            return;
        }

        NormalGenerate();
    }

    public void GenerateZombies(int pointIndex)
    {
        GameObject zombie = Instantiate(_zombiePrefab, _zombieParent);
        zombie.transform.position = spawnPoints[pointIndex].position;
    }

    private void NormalGenerate()
    {
        _lastGenerateTime = Time.fixedTime;
        GenerateZombies(Random.Range(0, 5));
    }

    private void WaveGenerate()
    {
        if (Time.fixedTime > _waveStartTime + _waveDuration)
        {
            _generateCd = NormalGenerateCd;
            _isWave = false;
        }

        _lastGenerateTime = Time.fixedTime;
        GenerateZombies(Random.Range(0, 5));
    }

    private void ZombieWaveCallback(float value)
    {
        foreach (var wave in _waves)
        {
            if (value < wave.TimePoint || wave.IsHappend)
                continue;
            wave.IsHappend = true;
            _isWave = true;
            _generateCd = _waveGenerateCd;
            _waveStartTime = Time.fixedTime;
            _waveDuration = _generateCd * wave.ZombieCount;
        }
    }

    // private IEnumerator GenerateZombieWave(int zombieCount)
    // {
    //     int tempNum = 0;
    //     while (tempNum < zombieCount)
    //     {
    //         yield return new WaitForSeconds(1f);
    //         GenerateZombies(Random.Range(0, 5));
    //         tempNum++;
    //     }
    // }
}