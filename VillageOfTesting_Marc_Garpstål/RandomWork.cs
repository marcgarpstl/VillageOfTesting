using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_Marc_Garpstål
{
    public class RandomWork
    {
        Random random = new Random();
        public virtual int RandomWorkNumber()
        {
            int rndWork = random.Next(1, 4);
            return rndWork;
        }
    }

    
}
