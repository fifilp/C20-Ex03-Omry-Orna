using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        private const float k_MaxTime = 4.8f;

        public static new List<string> ParamatersToRequest
        {
            get
            {
                List<string> parameters = Car.ParamatersToRequest;
                parameters.Add("Remaining time in battery");
                return parameters;
            }
        }

        internal ElectricCar(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            eCarColor i_Color,
            eNumOfDoors i_NumOfDoorsInCar,
            float i_RemainingTime)
            : base(
                   i_LicenseNumber,
                    i_ModelName,
                    i_WheelsManufacturer,
                    i_CurrentAirPressure,
                    new ElectricEngine(k_MaxTime, i_RemainingTime),
                  i_Color,
                  i_NumOfDoorsInCar)
        {
        }

        public ElectricEngine ElectricEngine
        {
            get
            {
                return (ElectricEngine)r_Engine;
            }
        }

        public float MaxTimeInHours
        {
            get
            {
                return ElectricEngine.MaxTimeInHours;
            }
        }

        public float RemainingTimeInHours
        {
            get
            {
                return ElectricEngine.RemainingTimeInHours;
            }
        }

        public override void RechargeBattery(float i_HoursToCharge)
        {
            ElectricEngine.RechargeBattery(i_HoursToCharge);
        }

        public override string ToString()
        {
            StringBuilder ElectricCarSB = new StringBuilder();
            ElectricCarSB.Append("ElectricCar\n");
            ElectricCarSB.Append(base.ToString());
            ElectricCarSB.Append("\nMax Time in hours: " + MaxTimeInHours.ToString());
            ElectricCarSB.Append(", Remaining Time in hours: " + RemainingTimeInHours.ToString());
            return ElectricCarSB.ToString();
        }
    }
}
