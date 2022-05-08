namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ElectricCar : ElectricVehicle
    {
        public static List<string> m_DataMembers = new List<string> { "Car Color: 0 - Red / 1 - White / 2 - Green / 3 - Blue", "Number Of Doors: 2 / 3 / 4 / 5" };

        private const int m_NumOfWheels = 4;

        public ElectricCar(
            CarColor i_CarColor,
            NumOfDoors i_NumOfDoors,
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
            List<string> newList = ElectricVehicle.GetDataMembers();
            
            newList.AddRange(m_DataMembers);

            return newList;
        }

        public static ElectricCar Parse(List<string> i_DataMembers)
        {
            bool successfulParse = true;
            ElectricVehicle electricVehicle = null;
            int carColorEnumSelection = 0;
            int numOfDoorsEnumSelection = 0;
            int carColorEnumMaxValue = (int)Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Max();
            int numOfDoorsEnumMaxValue = (int)Enum.GetValues(typeof(NumOfDoors)).Cast<NumOfDoors>().Max();
            int numOfDoorsEnumMinValue = (int)Enum.GetValues(typeof(NumOfDoors)).Cast<NumOfDoors>().Min();

            electricVehicle = ElectricVehicle.Parse(i_DataMembers.GetRange(0, 8));
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

            return new ElectricCar(
                (CarColor)carColorEnumSelection,
                (NumOfDoors)numOfDoorsEnumSelection,
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
            vehicleProperties.Add($"Car Color: {m_CarColor}");
            vehicleProperties.Add($"Number Of Doors: {m_NumOfDoors}");
            vehicleProperties.Add($"Battery Max Hours: {m_BatteryMaxHours}");
            vehicleProperties.Add($"Battery Hours Left: {m_BatteryHoursLeft}");

            return vehicleProperties;
        }
    }
}
