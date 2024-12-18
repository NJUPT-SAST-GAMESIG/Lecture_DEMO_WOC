namespace Class1.PlantsChooser
{
    public class PlantCard
    {
        public int Id;
        public string Name;
        public int SunShineReduce;
        public float CardCd;
    
        public PlantCard(int id, string name, int sunShineReduce, float cardCd)
        {
            Id = id;
            Name = name;
            SunShineReduce = sunShineReduce;
            CardCd = cardCd;
        }
    }
}
