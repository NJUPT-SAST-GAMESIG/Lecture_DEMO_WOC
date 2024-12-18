using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    public TextMeshProUGUI uiText; // 将Unity中的Text组件拖到这里
    public SunManager sunManager; // 将GameManager的实例拖到这里

    private void Start()
    {
        // 初始化Text内容
        if (uiText != null && sunManager != null)
            // 如果需要将Text组件内容转为整数
            //int.TryParse(uiText.text, out sunShineNum);
            // 如果直接将Text组件内容作为字符串
            uiText.text = sunManager.GetSunValue().ToString();
    }

    private void Update()
    {
        uiText.text = sunManager.GetSunValue().ToString();
    }
}