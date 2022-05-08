namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FuelCar : FuelVehicle
    {
        public static List<string> m_DataMembers = new List<string> { "Car Color: 0 - Red / 1 - White / 2 - Green / 3 - Blue", "Number Of Doors: 2 / 3 / 4 / 5" };

        private const int m_NumOfWheels = 4;

        public FuelCar(
            CarColor i_CarColor,
            NumOfDoors i_NumOfDoors,
            FuelType i_FuelType,
            float i_CurrentFuelInLiters,
            float i_MaxFuelInLiters,
            string i_Model,
            string i_LisenceNumber,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure)
            : base(
                  i_FuelType,
                  i_CurrentFuelInLiters,
                  i_MaxFuelInLiters,
                  i_Model,
                  i_LisenceNumber,
                  i_WheelManufacturer,
                  i_WheelMaxAirPressure,
                  i_CurrentMaxAirPressure,
                  m_NumOfWheels)
        {
            m_CarColor = i_CarColor;
            m_NumOfDoors = i_NumOfDoors;
        }

        public enum CarColor
        {
            Red,
            White,
            Green,
            Blue
        }

        public enum NumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public CarColor m_CarColor { get; set; }

        public NumOfDoors m_NumOfDoors { get; }

        public static List<string> GetDataMembers()
        {
            List<string> newList = new List<string>(FuelVehicle.GetDataMembers());
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public static FuelCar Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            FuelVehicle fuelVehicle = null;
            int carColorEnumSelection = 0;
            int numOfDoorsEnumSelection = 0;
            int carColorEnumMaxValue = (int)Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Max();
            int numOfDoorsEnumMaxValue = (int)Enum.GetValues(typeof(NumOfDoors)).Cast<NumOfDoors>().Max();
            int numOfDoorsEnumMinValue = (int)Enum.GetValues(typeof(NumOfDoors)).Cast<NumOfDoors>().Min();

            fuelVehicle = FuelVehicle.Parse(i_DataMembers.GetRange(0, 8));
            successfulParse = int.TryParse(i_DataMembers[8], out carColorEnumSelection);
            if (!successfulParse || carColorEnumMaxValue < carColorEnumSelection || carColorEnumSelection < 0)
            {
                throw new FormatException("Invalid car color");
            }

            successfulParse = int.TryParse(i_DataMembers[9], out numOfDoorsEnumSelection);
            if (!successfulParse || numOfDoorsEnumMaxValue < numOfDoorsEnumSelection || numOfDoorsEnumSelection < numOfDoorsEnumMinValue)
            {
                throw new FormatException("Invalid Number Of Doors");
            }

            return new FuelCar(
                (CarColor)carColorEnumSelection,
                (NumOfDoors)numOfDoorsEnumSelection,
                fuelVehicle.m_FuelType,
                fuelVehicle.m_CurrentFuelInLiters,
                fuelVehicle.m_MaxFuelInLiters,
                fuelVehicle.m_Model,
                fuelVehicle.m_LicenseNumber,
                fuelVehicle.m_Wheels[0].m_Manufacturer,
                fuelVehicle.m_Wheels[0].m_MaxAirPressure,
                fuelVehicle.m_Wheels[0].m_CurrentAirPressure);
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Car Color: {m_CarColor}");
            vehicleProperties.Add($"Number Of Doors: {m_NumOfDoors}");

            return vehicleProperties;
        }
    }
}
