namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public class Vehicle
    {
        public static List<string> m_DataMembers = new List<string> { "License Number", "Model" };

        public Vehicle(
            string i_Model,
            string i_LicenseNumber,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure,
            int i_NumOfWheels)
        {
            m_Model = i_Model;
            m_LicenseNumber = i_LicenseNumber;
            Wheel wheel = new Wheel(i_WheelManufacturer, i_WheelMaxAirPressure, i_CurrentMaxAirPressure);
            m_Wheels = new List<Wheel>();

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels.Add(wheel);
            }
        }

        public string m_Model { get; }

        public string m_LicenseNumber { get; }

        public float m_RemainingEnergySourcePrecentage { get; private set; }

        public List<Wheel> m_Wheels { get; }

        public static List<string> GetDataMembers()
        {
            List<string> newList = new List<string>(m_DataMembers);

            newList.AddRange(Wheel.GetDataMembers());

            return newList;
        }

        public static Vehicle Parse(List<string> i_DataMembers)
        {
            Wheel wheel = null;
            string lisenceNumber = i_DataMembers[0];
            string model = i_DataMembers[1];

            wheel = Wheel.Parse(i_DataMembers.GetRange(2, 3));

            return new Vehicle(
                model,
                lisenceNumber,
                wheel.m_Manufacturer,
                wheel.m_MaxAirPressure,
                wheel.m_CurrentAirPressure,
                1);
        }

        public void UpdateRemainingEnergySourcePrecentage(float i_CurrentEnergySource, float i_MaxEnergySource)
        {
            m_RemainingEnergySourcePrecentage = i_CurrentEnergySource / i_MaxEnergySource;
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
            
            vehicleProperties.Add($"License Number: {m_LicenseNumber}");
            vehicleProperties.Add($"Model: {m_Model}");
            vehicleProperties.Add($"Remaining Energy Source Precentage: {m_RemainingEnergySourcePrecentage * 100}%");
            vehicleProperties.AddRange(m_Wheels[0].GetWheelProperties());
            vehicleProperties.Add($"Number Of Wheels: {m_Wheels.Count}");

            return vehicleProperties;
        }      
    }
}
