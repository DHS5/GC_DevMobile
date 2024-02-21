using _2_Dev._1_Gameplay.Colectible;

namespace _2_Dev._1_Gameplay
{
    public interface ICollectibleListener
    {
        public void OnCollectibleCollected(CollectibleData data);
    }
}