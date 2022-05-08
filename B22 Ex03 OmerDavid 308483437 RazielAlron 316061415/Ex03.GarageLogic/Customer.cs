namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public class Customer
    {
        public static List<string> m_DataMembers = new List<string> { "Owner Name", "Phone Number", "Car Status: InRepair/Repared/Paid" };

        public Customer(string i_OwnerName, string i_phoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_PhoneNumber = i_phoneNumber;
        }

        public enum CarStatus
        {
            InRepair,
            Repared,
            Paid
        }

        public string m_OwnerName { get; }

        public string m_PhoneNumber { get; }

        public CarStatus m_CarStatus { get; set; } = CarStatus.InRepair;

        public Vehicle m_Vehicle { get; set; } = null;
    }
}
