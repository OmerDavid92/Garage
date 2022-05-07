using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        public string m_Model { get; }
        public string m_LisenceNumber { get; }
        public float m_RemainingEnergySourcePrecentage { get; private set; }
        public List<Wheel> m_Wheels { get; }

        public static List<string> m_DataMembers = new List<string> { "Model", "Lisence Number", "Remaining Energy Source Precentage" };
        public Vehicle(
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure,
            int i_NumOfWheels)
        {
            m_Model = i_Model;
            m_LisenceNumber = i_LisenceNumber;
            m_RemainingEnergySourcePrecentage = i_RemainingEnergySourcePrecentage;
            Wheel wheel = new Wheel(i_WheelManufacturer, i_WheelMaxAirPressure, i_CurrentMaxAirPressure);
            m_Wheels = new List<Wheel>();

            for(int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels.Add(wheel);
            }
        }

        public static List<string> GetDataMembers()
        {
            return m_DataMembers;
        }

        public void inflateMaxWheels()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateMax();
            }
        }

        public virtual List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();
            
            vehicleProperties.Add($"Model: {m_Model}");
            vehicleProperties.Add($"Lisence Number: {m_LisenceNumber}");
            vehicleProperties.Add($"Remaining Energy Source Precentage: {m_RemainingEnergySourcePrecentage}");
            vehicleProperties.AddRange(m_Wheels[0].GetWheelProperties());
            vehicleProperties.Add($"Number Of Wheels: {m_Wheels.Count}");

            return vehicleProperties;
        }

        public static bool TryParse(List<string> i_DataMembers, out Vehicle o_Vehicle)
        {
            o_Vehicle = null;
            Wheel o_Wheel = null;
            string model = i_DataMembers[0];
            string lisenceNumber = i_DataMembers[1];
            float remainingEnergySourcePrecentage = 0;
            bool successfulParse = true;

            successfulParse = float.TryParse(i_DataMembers[2], out remainingEnergySourcePrecentage);
            if (successfulParse)
            {
                successfulParse = Wheel.TryParse(i_DataMembers.GetRange(3, 3), out o_Wheel);
                if (successfulParse)
                {
                    o_Vehicle = new Vehicle(model,
                        lisenceNumber,
                        remainingEnergySourcePrecentage,
                        o_Wheel.m_Manufacturer,
                        o_Wheel.m_MaxAirPressure,
                        o_Wheel.m_CurrentAirPressure,
                        1);
                }
            }

            return successfulParse;
        }
        
    }
}
