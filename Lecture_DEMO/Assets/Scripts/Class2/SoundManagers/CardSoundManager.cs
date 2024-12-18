using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CardSoundType
{
    PlantSound,
    LackOfSunSound,
    PickUpSound,
    PutBackSound,
    SunClickSound
}
public class CardSoundManager
{
    // private static AudioSource plantSound;
    // private static AudioSource lackOfSunSound;
    // private static AudioSource pickUpSound;
    // private static AudioSource putBackSound;
    // private static AudioSource SunClickSound;
    public const string PlantSoundPath = "Audio/Sound/";

    private static AudioSource _bgmSource;
    private static Dictionary<CardSoundType,AudioClip> _clips;
    
    private static CardSoundManager _instance;
    public static CardSoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CardSoundManager();
                _bgmSource = GameObject.Find("GameApp").GetComponent<AudioSource>();
                _bgmSource.playOnAwake = false;
                _clips = new();
                _clips.Add(CardSoundType.PlantSound,Resources.Load<AudioClip>(PlantSoundPath + "plant"));
                _clips.Add(CardSoundType.PickUpSound,Resources.Load<AudioClip>(PlantSoundPath + "pause"));
                _clips.Add(CardSoundType.SunClickSound,Resources.Load<AudioClip>(PlantSoundPath + "points"));
                Debug.Log("CardSoundManager单例初始化");
            }
            Debug.Log("CardSoundManager单例获取");
            return _instance;
        }
    }

    public static CardSoundManager Ins { get; } = new CardSoundManager();
    //防止外部调用构造器
    private CardSoundManager()
    {
    }
    // private void Start()
    // {
    //     plantSound = gameObject.AddComponent<AudioSource>();
    //     plantSound.playOnAwake = false;
    //     plantSound.clip = Resources.Load<AudioClip>(PlantSoundPath + "plant");
    //     
    //     lackOfSunSound = gameObject.AddComponent<AudioSource>();
    //     lackOfSunSound.playOnAwake = false;
    //     
    //     pickUpSound = gameObject.AddComponent<AudioSource>();
    //     pickUpSound.playOnAwake = false;
    //     pickUpSound.clip = Resources.Load<AudioClip>(PlantSoundPath + "pause");
    //     
    //     putBackSound = gameObject.AddComponent<AudioSource>();
    //     putBackSound.playOnAwake = false;
    //     
    //     SunClickSound = gameObject.AddComponent<AudioSource>();
    //     SunClickSound.playOnAwake = false;
    //     SunClickSound.clip = Resources.Load<AudioClip>(PlantSoundPath + "points");
    //     
    // }

    public void Play(CardSoundType cardSoundType)
    {
        // switch (cardSoundType)
        // {
        //     case CardSoundType.PlantSound:
        //         plantSound.Play();
        //         break;
        //     case CardSoundType.PickUpSound:
        //         pickUpSound.Play();
        //         break;
        //     case CardSoundType.PutBackSound:
        //         putBackSound.Play();
        //         break;
        //     case CardSoundType.LackOfSunSound:
        //         lackOfSunSound.Play();
        //         break;
        //     case CardSoundType.SunClickSound:
        //         SunClickSound.Play();
        //         break;
        //     default: print("没有这个音效类型"); break;
        // }
        if (!_clips.TryGetValue(cardSoundType, out AudioClip clip))
            return;
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }
}