using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricVehicle
    {
        private float m_BatteryHoursLeft;
        private float m_BatteryMaxHours;

        public ElectricVehicle(float i_BatteryMaxHours, float i_BatteryHoursLeft)
        {
            m_BatteryHoursLeft = i_BatteryHoursLeft;
            m_BatteryMaxHours = i_BatteryMaxHours;
        }

        public virtual void Charge(float i_HoursToCharge)
        {
            if(m_BatteryHoursLeft + i_HoursToCharge > m_BatteryMaxHours)
            {
                //Throw exception
            }

            m_BatteryHoursLeft += i_HoursToCharge;
        }
    }
}
