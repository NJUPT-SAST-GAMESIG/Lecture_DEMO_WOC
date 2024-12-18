using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Class1.PlantsChooser
{
    public class LoadPlantCard : MonoBehaviour
    {
        private GameObject _plantCardPanel;
        void Start()
        {
            _plantCardPanel = GameObject.Find("Canvas/PlantsChooser/CardPanel");
            LoadCard(new PlantCard(0,"card_peashooter",100,1));
            LoadCard(new PlantCard(1,"card_cherrybomb",150,5));
        }

        // Update is called once per frame
        private void LoadCard(PlantCard plantCard)
        {
            GameObject plantCardPre = Resources.Load<GameObject>($"PlantsCardPrefab/PlantCard");
            GameObject plantCardObject = Instantiate(plantCardPre, _plantCardPanel.transform);
            plantCardObject.GetComponent<CardControl>().SetCard(plantCard);
            Image plantCardImage = plantCardObject.GetComponent<Image>();
            string path = Path.Combine("Images/Card", plantCard.Name);
            plantCardImage.sprite = Resources.Load<Sprite>(path);
        }
        
    }
}
