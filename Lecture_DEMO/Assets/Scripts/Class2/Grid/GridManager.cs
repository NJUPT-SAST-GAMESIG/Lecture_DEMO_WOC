using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private List<GridScript> _grids;
    private List<GameObject> _gridObjects;
    private bool _isPointerEnter;
    private CardManager _cardManager;
    private SpriteRenderer _spriteRendererOnPlantTracer;
    private SunManager _sunManager;
    public PlantCard card;
    private void OnEnable()
    {
        _gridObjects = new List<GameObject>();
        _grids = new List<GridScript>();
        //疑似高性能消耗，可能会卡顿
        for (int i = 0; i < transform.childCount; i++)
        {
            _gridObjects.Add(transform.GetChild(i).gameObject);
            _gridObjects[i].AddComponent<GridScript>();
            _grids.Add(_gridObjects[i].GetComponent<GridScript>());
            _grids[i].SetGridController(this);
        }
    }
    public void SetCardManager(CardManager cardManager)
    {
        _cardManager = cardManager;
    }
    public void SetSunManager(SunManager sunManager)
    {
        _sunManager = sunManager;
    }
    public void SetIsPointerEnter(bool value)
    {
        _isPointerEnter = value;
    }
    public void GetSpriteRendererOnPlantTracer(SpriteRenderer spriteRenderer)
    {
        _spriteRendererOnPlantTracer = spriteRenderer;
    }
    public Sprite GetSpriteOnPlantTracer()
    {
        return _spriteRendererOnPlantTracer.sprite;
    }

    public void ReduceSunNum()
    {
        _sunManager.SunReduce(card.GetCardCost());
    }
}
