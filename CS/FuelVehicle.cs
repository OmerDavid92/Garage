using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelVehicle
    {
        public enum FuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        private FuelType m_FuelType;
        private float m_CurrentFuelInLiters;
        private float m_MaxFuelInLiters;

        public FuelVehicle(FuelType i_FuelType, float i_CurrentFuelInLiters, float i_MaxFuelInLiters)
        {
            m_FuelType= i_FuelType;
            m_CurrentFuelInLiters = i_CurrentFuelInLiters;
            m_MaxFuelInLiters = i_MaxFuelInLiters;
        }

    }
}
