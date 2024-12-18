using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    private GameObject _plantCardPanel;
    
    private List<PlantCardConfig> _cardsConfigs;//存储卡牌信息列表，存好了后再读卡牌信息加载到游戏内
    private PlantTracer _plantTracer;
    private SunManager _sunManager;
    private GridManager _gridManager;
    
    private readonly int _cardCount = 8;
    private void Start()
    {
        _plantCardPanel = GameObject.Find("Canvas/PlantsChooser/CardPanel");
        _cardsConfigs = new List<PlantCardConfig>();
        
        _plantTracer = transform.GetComponent<PlantTracer>();
        _sunManager = transform.GetComponent<SunManager>();
        _gridManager = GameObject.Find("Canvas/Grids").GetComponent<GridManager>();
        
        _gridManager.SetCardManager(this);//双向获取引用
        _gridManager.SetSunManager(_sunManager);
        _gridManager.GetSpriteRendererOnPlantTracer(_plantTracer.GetSpriteRenderer());
    }

    public void AddCardTest()
    {
        AddCard(0, "peashooter", 100, 1);
        AddCard(1, "sunflower", 50, 1);
        InstantiateAllCards();
    }

    public void AddCard(int id, string name, int sunShineReduce, int cdTime)
    {
        if (_cardCount <= _cardsConfigs.Count)//这个最大卡牌数量是只读的，看看要不要改
        {
            print("卡牌数量已满");
            return;
        }

        var config = new PlantCardConfig(id, name, sunShineReduce, cdTime);
        _cardsConfigs.Add(config);
    }
    
    //卡牌添加的重载方法
    public void AddCard(PlantCardConfig plantCardConfig)
    {
        if (_cardCount <= _cardsConfigs.Count)
        {
            print("卡牌数量已满");
            return;
        }
        
        _cardsConfigs.Add(plantCardConfig);
    }
    //初始化所有卡牌以及卡牌内的控制器
    public void InstantiateAllCards()
    {
        foreach (PlantCardConfig card in _cardsConfigs)
        {
            //进行卡牌实例化和卡牌生成
            GameObject plantCardPre = Resources.Load<GameObject>("PlantsCardPrefab/PlantCard");
            GameObject plantCardObject = Instantiate(plantCardPre, _plantCardPanel.transform);
            
            //获取卡牌脚本
            PlantCard cardScript = plantCardObject.GetComponent<PlantCard>();
            cardScript.SetCard(card);
            
            InstantiateCardImage(plantCardObject, card);
            InstantiateCardManagers(cardScript);
        }
    }

    private void InstantiateCardManagers(PlantCard cardScript)
    {
        cardScript.SetSunManager(_sunManager);
        cardScript.SetPlantTracer(_plantTracer);
        cardScript.SetGridManager(_gridManager);
    }

    //初始化卡牌图像
    private void InstantiateCardImage(GameObject plantCardObject, PlantCardConfig card)
    {
        Image plantCardImage = plantCardObject.GetComponent<Image>();
        string path = "Images/Card/card_"+card.Name;
        plantCardImage.sprite = Resources.Load<Sprite>(path);
    }
    
}