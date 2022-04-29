using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Vehicle
    {
        public string m_Model { get; }
        public string m_LisenceNumber { get; }
        public float m_RemainingEnergySourcePrecentage { get; private set; }
        public List<Wheel> m_Wheels { get; }

        public Vehicle(string i_Model, string i_LisenceNumber, float i_RemainingEnergySourcePrecentage, List<Wheel> i_Wheels)
        {
            m_Model = i_Model;
            m_LisenceNumber = i_LisenceNumber;
            m_RemainingEnergySourcePrecentage = i_RemainingEnergySourcePrecentage;
            m_Wheels = i_Wheels;
        }
        

    }
}
