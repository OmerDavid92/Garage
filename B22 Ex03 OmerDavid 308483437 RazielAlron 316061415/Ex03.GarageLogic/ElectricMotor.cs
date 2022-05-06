using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricMotor : ElectricVehicle
    {
        public enum LicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        public LicenseType m_LicenseType { get; }
        public int m_EngineCapacity { get; }

        private const int m_NumOfWheels = 2;

        public static List<string> m_DataMembers = new List<string> { "LicenseType: A/A1/B1/BB", "Engine Capacity" };

        public ElectricMotor(
            LicenseType i_LicenseType,
            int i_EngineCapacity,
            float i_BatteryMaxHours,
            float i_BatteryHoursLeft,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(i_BatteryMaxHours,
                i_BatteryHoursLeft,
                i_Model,
                i_LisenceNumber,
                i_RemainingEnergySourcePrecentage,
                i_WheelManufacturer,
                i_WheelMaxAirPressure,
                i_CurrentMaxAirPressure,
                m_NumOfWheels)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
        }

        public static List<string> GetDataMembers()
        {
            ElectricVehicle.GetDataMembers().AddRange(m_DataMembers);
            return m_DataMembers;
        }

        public static bool TryParse(List<string> i_DataMembers, out ElectricMotor o_ElectricMotor)
        {
            bool successfulParse = true;
            ElectricVehicle o_ElectricVehicle = null;
            o_ElectricMotor = null;
            int enumSelection = 0;
            int CarColorEnumMaxValue = (int)Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().Max();
            int engineCapacity = 0;

            successfulParse = ElectricVehicle.TryParse(i_DataMembers.GetRange(0, 8), out o_ElectricVehicle);
            if (successfulParse)
            {
                successfulParse = int.TryParse(i_DataMembers[8], out enumSelection);
                if (CarColorEnumMaxValue > enumSelection && enumSelection > 0 && successfulParse)
                {
                    successfulParse = int.TryParse(i_DataMembers[9], out engineCapacity);
                    if (successfulParse)
                    {
                        o_ElectricMotor = new ElectricMotor(
                            (LicenseType)enumSelection,
                            engineCapacity,
                            o_ElectricVehicle.m_BatteryMaxHours,
                            o_ElectricVehicle.m_BatteryHoursLeft,
                            o_ElectricVehicle.m_Model,
                            o_ElectricVehicle.m_LisenceNumber,
                            o_ElectricVehicle.m_RemainingEnergySourcePrecentage,
                            o_ElectricVehicle.m_Wheels[0].m_Manufacturer,
                            o_ElectricVehicle.m_Wheels[0].m_MaxAirPressure,
                            o_ElectricVehicle.m_Wheels[0].m_CurrentAirPressure);
                    }
                }
            }

            return successfulParse;
        }
    }
}
