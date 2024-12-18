using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Class1.PlantsChooser
{
    public class CardControl : MonoBehaviour, IPointerClickHandler
    {
        private PlantCard _card;

        private bool _isCd;

        private float _cdStartTime;

        private Image _cardImage;
        private Slider _cardSlider;

        private void OnEnable()
        {
            _cardImage = GetComponent<Image>();
            _cardSlider = GetComponentInChildren<Slider>();
        }

        // Start is called before the first frame update
        private void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isCd)
                return;
            _cardSlider.value = 1 - (Time.time - _cdStartTime) / _card.CardCd;
            if (Time.time > _cdStartTime + _card.CardCd)
            {
                string path = Path.Combine("Images/Card", _card.Name);
                Sprite sprite = Resources.Load<Sprite>(path);
                _cardImage.sprite = sprite;
                _isCd = false;
            }
        }

        public void SetCard(PlantCard card)
        {
            this._card = card;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isCd)
                return;
            string path = Path.Combine("Images/Card", _card.Name + "2");
            Sprite sprite = Resources.Load<Sprite>(path);
            _cardImage.sprite = sprite;
            _cdStartTime = Time.time;
            _isCd = true;
        }
    }
}