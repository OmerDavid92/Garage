namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class ElectricVehicle : Vehicle
    {
        public static List<string> m_DataMembers = new List<string> { "Battery Hours Left", "Battery Max Hours" };
        
        public ElectricVehicle(
            float i_BatteryMaxHours,
            float i_BatteryHoursLeft,
            string i_Model,
            string i_LisenceNumber,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure,
            int i_NumOfWheels)
            : base(
                 i_Model,
                 i_LisenceNumber,
                 i_WheelManufacturer,
                 i_WheelMaxAirPressure,
                 i_CurrentMaxAirPressure,
                 i_NumOfWheels)
        {
            m_BatteryHoursLeft = i_BatteryHoursLeft;
            m_BatteryMaxHours = i_BatteryMaxHours;
            UpdateRemainingEnergySourcePrecentage(i_BatteryHoursLeft, i_BatteryMaxHours);
        }

        public float m_BatteryHoursLeft { get; private set; }

        public float m_BatteryMaxHours { get; }

        public static List<string> GetDataMembers()
        {
            List<string> newList = Vehicle.GetDataMembers();

            newList.AddRange(m_DataMembers);

            return newList;
        }

        public static ElectricVehicle Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            Vehicle vehicle = null;
            float batteryHoursLeft = 0;
            float batteryMaxHours = 0;

            vehicle = Vehicle.Parse(i_DataMembers.GetRange(0, 5));
            successfulParse = float.TryParse(i_DataMembers[5], out batteryMaxHours);
            if (!successfulParse)
            {
                throw new FormatException("Invalid battery max hours");
            }

            successfulParse = float.TryParse(i_DataMembers[6], out batteryHoursLeft);
            if (!successfulParse)
            {
                throw new FormatException("Invalid battery hours left");
            }

            return new ElectricVehicle(
                batteryMaxHours,
                batteryHoursLeft,
                vehicle.m_Model,
                vehicle.m_LicenseNumber,
                vehicle.m_Wheels[0].m_Manufacturer,
                vehicle.m_Wheels[0].m_MaxAirPressure,
                vehicle.m_Wheels[0].m_CurrentAirPressure,
                1);
        }

        public void Recharge(float i_TimeToCharge)
        {
            if ((i_TimeToCharge + m_BatteryHoursLeft) > m_BatteryMaxHours)
            {
                throw new ValueOutOfRangeException(m_BatteryMaxHours, 0);
            }

            m_BatteryHoursLeft += i_TimeToCharge;
            UpdateRemainingEnergySourcePrecentage(m_BatteryHoursLeft, m_BatteryMaxHours);
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Battery Hours Left: {m_BatteryHoursLeft}");
            vehicleProperties.Add($"Battery Max Hours: {m_BatteryMaxHours}");

            return vehicleProperties;
        }
    }
}
