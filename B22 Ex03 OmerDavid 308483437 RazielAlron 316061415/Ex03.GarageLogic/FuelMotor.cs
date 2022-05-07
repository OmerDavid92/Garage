using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelMotor : FuelVehicle
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

        public static List<string> m_DataMembers = new List<string> { "LicenseType: A/A1/B1/BB", "License Type", "Engine Capacity" };

        public FuelMotor(
            LicenseType i_LicenseType,
            int i_EngineCapacity,
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(
                i_FuelType,
                i_CurrentFuelInLiters,
                i_MaxFuelInLiters,
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
            List<string> newList = FuelVehicle.GetDataMembers();
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"License Type: {m_LicenseType}");
            vehicleProperties.Add($"Engine Capacity: {m_EngineCapacity}");

            return vehicleProperties;
        }

        public static FuelMotor Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            FuelVehicle fuelVehicle = null;
            int enumSelection = 0;
            int CarColorEnumMaxValue = (int)Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().Max();
            int engineCapacity = 0;

            fuelVehicle = FuelVehicle.Parse(i_DataMembers.GetRange(0, 9));
            successfulParse = int.TryParse(i_DataMembers[9], out enumSelection);
            if (!successfulParse || CarColorEnumMaxValue < enumSelection || enumSelection < 0)
            {
                throw new FormatException("Invalid license type");
            }

            successfulParse = int.TryParse(i_DataMembers[10], out engineCapacity);
            if (!successfulParse)
            {
                throw new FormatException("Invalid engine capacity");
            }

            return new FuelMotor(
                (LicenseType)enumSelection,
                engineCapacity,
                fuelVehicle.m_FuelType,
                fuelVehicle.m_CurrentFuelInLiters,
                fuelVehicle.m_MaxFuelInLiters,
                fuelVehicle.m_Model,
                fuelVehicle.m_LicenseNumber,
                fuelVehicle.m_RemainingEnergySourcePrecentage,
                fuelVehicle.m_Wheels[0].m_Manufacturer,
                fuelVehicle.m_Wheels[0].m_MaxAirPressure,
                fuelVehicle.m_Wheels[0].m_CurrentAirPressure);
        }
    }
}
