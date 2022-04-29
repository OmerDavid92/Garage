using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Wheel
    {
        public string m_Manufacturer { get; }

        public float m_MaxAirPressure { get; }

        public float m_CurrentAirPressure { get; private set; }

        public Wheel(string i_Manufacturer, float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

    }
}
