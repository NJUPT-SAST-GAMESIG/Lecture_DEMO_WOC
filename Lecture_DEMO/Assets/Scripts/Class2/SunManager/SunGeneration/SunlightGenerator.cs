using UnityEngine;
using Random = UnityEngine.Random;

public class SunlightGenerator : MonoBehaviour
{
    public GameObject sunPrefab;
    [SerializeField] private float sunGenerationTime = 3f;
    [SerializeField] private float timer = 3f;
    
    private void Update()
    {
        //change position
        var position = transform.position;
        position.x = Random.Range(-9.0f, -6.0f);
        transform.position = position;

        timer += Time.deltaTime;
        if (timer >= sunGenerationTime)
        {
            timer = 0;
            Instantiate(sunPrefab, position, Quaternion.identity);
            // var sunlight = Instantiate(sunPrefab, position, Quaternion.identity);
            // var sunPrefabScript = sunlight.GetComponent<SunPrefab>();
            //依赖注入版本
            // var sunPrefabScript = sunlight.GetComponent<SunPrefab>();
            // if (sunPrefabScript != null)
            // {
            //     sunPrefabScript.SetSunManager(FindObjectOfType<SunManager>()); // 注入 SunManager
            // }
        }
    }
}