using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricVehicle : Vehicle
    {
        public float m_BatteryHoursLeft { get; private set; }

        public float m_BatteryMaxHours { get; }

        public static List<string> m_DataMembers = new List<string> { "Battery Hours Left", "Battery Max Hours" };

        public ElectricVehicle(
            float i_BatteryMaxHours,
            float i_BatteryHoursLeft,
            string i_Model,
            string i_LisenceNumber,
            float i_RemainingEnergySourcePrecentage,
            string i_WheelManufacturer,
            float i_WheelMaxAirPressure,
            float i_CurrentMaxAirPressure,
            int i_NumOfWheels)
            : base(i_Model,
                i_LisenceNumber,
                i_RemainingEnergySourcePrecentage,
                 i_WheelManufacturer,
                 i_WheelMaxAirPressure,
                 i_CurrentMaxAirPressure,
                 i_NumOfWheels)
        {
            m_BatteryHoursLeft = i_BatteryHoursLeft;
            m_BatteryMaxHours = i_BatteryMaxHours;
        }

        public static List<string> GetDataMembers()
        {
            List<string> newList = Vehicle.GetDataMembers();

            newList.AddRange(m_DataMembers);

            return newList;
        }

        public void Recharge(float i_TimeToCharge)
        {
            if ((i_TimeToCharge + m_BatteryHoursLeft) > m_BatteryMaxHours)
            {
                throw new ValueOutOfRangeException(m_BatteryMaxHours, 0);
            }

            m_BatteryHoursLeft += i_TimeToCharge;
        }

        public override List<string> GetVehicleProperties()
        {
            List<string> vehicleProperties = new List<string>();

            vehicleProperties.AddRange(base.GetVehicleProperties());
            vehicleProperties.Add($"Battery Hours Left: {m_BatteryHoursLeft}");
            vehicleProperties.Add($"Battery Max Hours: {m_BatteryMaxHours}");

            return vehicleProperties;
        }
        public static bool TryParse(List<string> i_DataMembers, out ElectricVehicle o_ElectricVehicle)
        {
            bool successfulParse = true;
            Vehicle o_Vehicle = null;
            o_ElectricVehicle = null;
            float m_BatteryHoursLeft = 0;
            float m_BatteryMaxHours = 0;

            successfulParse = Vehicle.TryParse(i_DataMembers.GetRange(0, 6), out o_Vehicle);
            if (successfulParse)
            {
                successfulParse = float.TryParse(i_DataMembers[6], out m_BatteryMaxHours);
                if (successfulParse)
                {
                    successfulParse = float.TryParse(i_DataMembers[7], out m_BatteryHoursLeft);
                    if (successfulParse)
                    {
                        o_ElectricVehicle = new ElectricVehicle(
                            m_BatteryMaxHours,
                            m_BatteryHoursLeft,
                            o_Vehicle.m_Model,
                            o_Vehicle.m_LicenseNumber,
                            o_Vehicle.m_RemainingEnergySourcePrecentage,
                            o_Vehicle.m_Wheels[0].m_Manufacturer,
                            o_Vehicle.m_Wheels[0].m_MaxAirPressure,
                            o_Vehicle.m_Wheels[0].m_CurrentAirPressure,
                            1);
                    }
                }
            }

            return successfulParse;
        }
    }
}
