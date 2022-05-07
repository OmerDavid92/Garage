using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelCar : FuelVehicle
    {
        public enum CarColor
        {
            Red,
            White,
            Green,
            Blue
        }

        public CarColor m_CarColor { get; set; }
        public float m_CargoVolume { get; }

        private const int m_NumOfWheels = 4;

        public static List<string> m_DataMembers = new List<string> { "Car Color: 0 - Red / 1 - White / 2 - Green / 3 - Blue", "Cargo Volume" };

        public FuelCar(
            CarColor i_CarColor,
            float i_CargoVolume,
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage, 
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(i_FuelType,
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
            m_CarColor = i_CarColor;
            m_CargoVolume = i_CargoVolume;
        }

        public static List<string> GetDataMembers()
        {
            List<string> newList = new List<string>(FuelVehicle.GetDataMembers());
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Car Color: {m_CarColor}");
            vehicleProperties.Add($"Cargo Volume: {m_CargoVolume}");

            return vehicleProperties;
        }

        public static FuelCar Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            FuelVehicle fuelVehicle = null;
            int enumSelection = 0;
            int CarColorEnumMaxValue = (int)Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Max();
            float cargoVolume = 0;

            fuelVehicle = FuelVehicle.Parse(i_DataMembers.GetRange(0, 9));
            successfulParse = int.TryParse(i_DataMembers[9], out enumSelection);
            if (!successfulParse || CarColorEnumMaxValue < enumSelection || enumSelection < 0)
            {
                throw new FormatException("Invalid car color");
            }

            successfulParse = float.TryParse(i_DataMembers[10], out cargoVolume);
            if (!successfulParse)
            {
                throw new FormatException("Invalid cargo volume");
            }

            return new FuelCar(
                (CarColor)enumSelection,
                cargoVolume,
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
