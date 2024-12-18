public class PlantCardConfig
{
    public int Id;
    /// <summary>
    /// 名字示例:peashooter(豌豆射手)
    /// </summary>
    public string Name;
    public int SunShineReduce;
    public float CardCd;

    public PlantCardConfig(int id, string name, int sunShineReduce, float cardCd)
    {
        Id = id;
        Name = name;
        SunShineReduce = sunShineReduce;
        CardCd = cardCd;
    }
}