using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        public bool m_IsDrivingRefrigeratedContents { get; }
        public float m_CargoVolume { get; }

        private const int m_NumOfWheels = 16;

        public static List<string> m_DataMembers = new List<string> { "Is Driving Refrigerated Contents: 0 - False / 1 - True", "Cargo Volume" };

        public Truck(
            bool i_IsDrivingRefrigeratedContents,
            float i_CargoVolume,
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(i_FuelType,
                  i_CurrentFuelInLiters,
                  i_MaxFuelInLiters,
                  i_Model,
                  i_LisenceNumber,
                  i_WheelManufacturer,
                  i_WheelMaxAirPressure,
                  i_CurrentMaxAirPressure,
                  m_NumOfWheels)
        {
            m_IsDrivingRefrigeratedContents = i_IsDrivingRefrigeratedContents;
            m_CargoVolume = i_CargoVolume;
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Is Driving Refrigerated Contents: {m_IsDrivingRefrigeratedContents}");
            vehicleProperties.Add($"Cargo Volume: {m_CargoVolume}");

            return vehicleProperties;
        }
        public static List<string> GetDataMembers()
        {
            List<string> newList = new List<string>(FuelVehicle.GetDataMembers());

            newList.AddRange(m_DataMembers);

            return newList;
        }
        public static Truck Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            FuelVehicle fuelVehicle = null;
            bool isDrivingRefrigeratedContents = true;
            float cargoVolume = 0;

            fuelVehicle = FuelVehicle.Parse(i_DataMembers.GetRange(0, 8));
            if(!(i_DataMembers[9].Equals("0") || i_DataMembers[8].Equals("1")))
            {
                throw new FormatException("Invalid Refrigerated Transformation");
            }

            isDrivingRefrigeratedContents = i_DataMembers[8].Equals("1");
            successfulParse = float.TryParse(i_DataMembers[9], out cargoVolume);
            if (!successfulParse)
            {
                throw new FormatException("Invalid cargo volume");
            }

            return new Truck(
                isDrivingRefrigeratedContents,
                cargoVolume,
                fuelVehicle.m_FuelType,
                fuelVehicle.m_CurrentFuelInLiters,
                fuelVehicle.m_MaxFuelInLiters,
                fuelVehicle.m_Model,
                fuelVehicle.m_LicenseNumber,
                fuelVehicle.m_Wheels[0].m_Manufacturer,
                fuelVehicle.m_Wheels[0].m_MaxAirPressure,
                fuelVehicle.m_Wheels[0].m_CurrentAirPressure);
        }

    }
}
