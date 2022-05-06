using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Customer
    {
        public enum CarStatus
        {
            InRepair,
            Repared,
            Paid
        }

        public string m_OwnerName { get; }
        public string m_phoneNumber { get; }
        public CarStatus m_CarStatus { get; set; } = CarStatus.InRepair;
        public Vehicle m_Vehicle { get; }

        public static List<string> m_DataMembers = new List<string> { "Owner Name", "Phone Number", "Car Status: InRepair/Repared/Paid" };

        public Customer (string i_OwnerName, string i_phoneNumber, CarStatus i_CarStatus, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_phoneNumber = i_phoneNumber;
            m_CarStatus = i_CarStatus;
            m_Vehicle = i_Vehicle;
        }

        //public static List<string> GetDataMembers()
        //{
        //    m_DataMembers.AddRange(Vehicle.GetDataMembers());
        //    return m_DataMembers;
        //}

    }
}
