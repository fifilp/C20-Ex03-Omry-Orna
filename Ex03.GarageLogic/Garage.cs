using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Order> r_CurrentOrders;

        public Garage()
        {
            r_CurrentOrders = new Dictionary<string, Order>();
        }

        // GetOrder throws an exception if the license number doesn't exist in the garage dictionary
        public Order GetOrder(string i_LicenseNumber)
        {
            Order order;

            if (!r_CurrentOrders.TryGetValue(i_LicenseNumber, out order))
            {
                throw new ArgumentException("no order exists for this vehicle");
            }

            return order;
        }

        public Vehicle GetVehicle(string i_LicenseNumber)
        {
            // getOrder ensures there is such an order
            return GetOrder(i_LicenseNumber).Vehicle;
        }

        public void AddNewOrderToGarage(Vehicle i_Vehicle, string i_CustumerName, string i_CustomerPhoneNumber)
        {
            Order order = new Order(i_CustumerName, i_CustomerPhoneNumber, i_Vehicle);
            r_CurrentOrders.Add(i_Vehicle.LicenseNumber, order);
        }

        public bool isVehicleInGarage(string i_LicenseNumber)
        {
           return r_CurrentOrders.ContainsKey(i_LicenseNumber);
        }

        public List<string> GetListOfLicenseNumbersByStatus(eOrderStatus? i_Status)
        {
            List<string> listOfLicenseNumbers = new List<string>();

            if (i_Status != null)
            {
                foreach (Order order in r_CurrentOrders.Values)
                {
                    if (order.Status == i_Status)
                    {
                        listOfLicenseNumbers.Add(order.VehicleLicenseNumber);
                    }
                }
            }
            else
            {
                foreach (Order order in r_CurrentOrders.Values)
                {
                    listOfLicenseNumbers.Add(order.VehicleLicenseNumber);
                }
            }

            return listOfLicenseNumbers;
        }

        public void ChangeStatus(string i_LicenseNumber, eOrderStatus i_Status)
        {
            Order order = GetOrder(i_LicenseNumber);
            order.Status = i_Status;
        }

        public bool IsOnRepair(string i_LicenseNumber)
        {
            Order order = GetOrder(i_LicenseNumber);
            return order.IsOnRepair();
        }

        public void InflateVehicleWheelsToMax(string i_LicenseNumber)
        {
            Order order = GetOrder(i_LicenseNumber);

            if (!order.IsOnRepair())
            {
                throw new ArgumentException("the vehicle's status must be OnRepair");
            }

            order.Vehicle.InfalteAllWheelsToMax();
        }

        // will throw an exception (from Vehicle) if receives a vehicle not powered by gas
        public void FillUpTank(string i_LicenseNumber, eGasType i_Gas, float i_Amount)
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            vehicle.FillUpTank(i_Gas, i_Amount);
        }

        // will throw an exception (from Vehicle) if receives a vehicle not powered by electricity
        public void RechargeBattery(string i_LicenseNumber, float i_HoursToCharge)
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            vehicle.RechargeBattery(i_HoursToCharge);
        }
    }
}
