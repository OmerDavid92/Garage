using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricCar
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

        public ElectricCar(CarColor i_CarColor, float i_CargoVolume)
        {
            m_CarColor = i_CarColor;
            m_CargoVolume = i_CargoVolume;
        }
    }
}
