using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridScript : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private GridManager _gridManager;
    private Image _image;
    private bool _isPlanted;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void SetGridController(GridManager gridManager)
    {
        _gridManager = gridManager;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_isPlanted) return;
        if (!PlantTracer.IsTracing) return;
        _gridManager.card.StartCoolingdown();
        _gridManager.ReduceSunNum();
        _isPlanted = true;
        _image.sprite = _gridManager.GetSpriteOnPlantTracer();
        _image.color = new Color(255, 255, 255, 1f);//植物成功种植，后面改成动画
        CardSoundManager.Instance.Play(CardSoundType.PlantSound);
        PlantTracer.StopTracing();

        //Debug.Log(_gridManager.card.GetName());
        string cardName = _gridManager.card.GetName();
        
        
        UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(gameObject, "Assets/Scripts/Class2/Grid/GridScript.cs (39,9)", cardName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isPlanted) return;
        if (!PlantTracer.IsTracing) return;
        _gridManager.SetIsPointerEnter(true);
        _image.sprite = _gridManager.GetSpriteOnPlantTracer();
        _image.color = new Color(255,255,255,0.7f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isPlanted) return;
        if(!PlantTracer.IsTracing) return;
        _gridManager.SetIsPointerEnter(false);
        _image.color = new Color(255,255,255,0f);
        _image.sprite = null;
    }

    public void ReleaseGrid()//用于在植物被销毁后的地块可种植
    {
        _isPlanted = false;
    }  
}
