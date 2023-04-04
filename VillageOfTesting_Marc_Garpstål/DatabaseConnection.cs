using System.Threading.Channels;

namespace VillageOfTesting_Marc_Garpstål
{
    public class DatabaseConnection
    {
        public List<Village> SavedProgress = new List<Village>();

        public virtual void Save(Village village)
        {
            SavedProgress.Add(village);
        }
        public virtual Village Load()
        {
            return SavedProgress[0];
        }
    }
}
