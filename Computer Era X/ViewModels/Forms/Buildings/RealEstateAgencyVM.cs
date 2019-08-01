using Computer_Era_X.DataTypes.Objects;
using System.Collections.ObjectModel;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        partial void RealEstateAgencyInit()
        {
            
        }

        public Collection<House> Houses => GameEnvironment.Houses;
    }
}
