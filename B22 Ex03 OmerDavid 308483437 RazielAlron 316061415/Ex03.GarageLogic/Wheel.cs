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
            return new List<string>(m_DataMembers);
        }

        public void InflateMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public List<string> GetWheelProperties()
        {
            List<string> wheelProperties = new List<string>();

            wheelProperties.Add($"Manufacturer: {m_Manufacturer}");
            wheelProperties.Add($"Current Air Pressure: {m_CurrentAirPressure}");
            wheelProperties.Add($"Max Air Pressure: {m_MaxAirPressure}");

            return wheelProperties;
        }

        public static Wheel Parse(List<string> i_DataMembers)
        {
            string manufacturer = i_DataMembers[0];
            float maxAirPressure = 0;
            float currentAirPressure = 0;
            bool successfulParse = true;

            successfulParse = float.TryParse(i_DataMembers[1], out maxAirPressure);
            if (!successfulParse)
            {
                throw new FormatException("Invalid max air pressure");
            }

            successfulParse = float.TryParse(i_DataMembers[2], out currentAirPressure);
            if (!successfulParse)
            {
                throw new FormatException("Invalid current air pressure");
            }

            return new Wheel(manufacturer, maxAirPressure, currentAirPressure);
        }
    }
}
