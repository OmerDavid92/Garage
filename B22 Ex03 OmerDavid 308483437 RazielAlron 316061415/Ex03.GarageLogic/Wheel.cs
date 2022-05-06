using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public string m_Manufacturer { get; }

        public float m_MaxAirPressure { get; }

        public float m_CurrentAirPressure { get; private set; }

        public static List<string> m_DataMembers = new List<string> { "Manufacturer", "Max Air Pressure", "Current Air Pressure" };


        public Wheel(string i_Manufacturer, float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public static List<string> GetDataMembers()
        {
            return m_DataMembers;
        }

        public void InflateMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public static bool TryParse(List<string> i_DataMembers, out Wheel o_Wheel)
        {
            o_Wheel = null;
            string manufacturer = i_DataMembers[0];
            float maxAirPressure = 0;
            float currentAirPressure = 0;
            bool SuccessfulParse = true;

            SuccessfulParse = float.TryParse(i_DataMembers[1], out maxAirPressure);
            if (SuccessfulParse)
            {
                SuccessfulParse = float.TryParse(i_DataMembers[2], out currentAirPressure);
                if (SuccessfulParse)
                {
                    o_Wheel = new Wheel(manufacturer, maxAirPressure, currentAirPressure);
                }
            }

            return SuccessfulParse;
        }
    }
}
