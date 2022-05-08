namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ElectricMotor : ElectricVehicle
    {
        public static List<string> m_DataMembers = new List<string> { "LicenseType: A/A1/B1/BB", "Engine Capacity" };

        private const int m_NumOfWheels = 2;

        public ElectricMotor(
            LicenseType i_LicenseType,
            int i_EngineCapacity,
            float i_BatteryMaxHours,
            float i_BatteryHoursLeft,
            string i_Model,
            string i_LisenceNumber,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(
                i_BatteryMaxHours,
                i_BatteryHoursLeft,
                i_Model,
                i_LisenceNumber,
                i_WheelManufacturer,
                i_WheelMaxAirPressure,
                i_CurrentMaxAirPressure,
                m_NumOfWheels)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
        }

        public enum LicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        public LicenseType m_LicenseType { get; }

        public int m_EngineCapacity { get; }

        public static List<string> GetDataMembers()
        {
            List<string> newList = ElectricVehicle.GetDataMembers();
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public static ElectricMotor Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            ElectricVehicle electricVehicle = null;
            int enumSelection = 0;
            int licenseTypeEnumMaxValue = (int)Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().Max();
            int engineCapacity = 0;

            electricVehicle = ElectricVehicle.Parse(i_DataMembers.GetRange(0, 7));
            successfulParse = int.TryParse(i_DataMembers[7], out enumSelection);
            if (!successfulParse || licenseTypeEnumMaxValue < enumSelection || enumSelection < 0)
            {
                throw new FormatException("Invalid license type");
            }

            successfulParse = int.TryParse(i_DataMembers[8], out engineCapacity);
            if (!successfulParse)
            {
                throw new FormatException("Invalid engine capacity");
            }

            return new ElectricMotor(
                (LicenseType)enumSelection,
                engineCapacity,
                electricVehicle.m_BatteryMaxHours,
                electricVehicle.m_BatteryHoursLeft,
                electricVehicle.m_Model,
                electricVehicle.m_LicenseNumber,
                electricVehicle.m_Wheels[0].m_Manufacturer,
                electricVehicle.m_Wheels[0].m_MaxAirPressure,
                electricVehicle.m_Wheels[0].m_CurrentAirPressure);
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"License Type: {m_LicenseType}");
            vehicleProperties.Add($"Engine Capacity: {m_EngineCapacity}");

            return vehicleProperties;
        }
    }
}
