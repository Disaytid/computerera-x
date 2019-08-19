using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using System.Collections.ObjectModel;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        private Collection<HousingSale> housingSale = new Collection<HousingSale>();

        partial void RealEstateAgencyInit()
        {
            
        }

        public Collection<House> Houses => GameEnvironment.Houses;
    }
}
