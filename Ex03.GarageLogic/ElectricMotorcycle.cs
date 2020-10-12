using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxTime = 1.6f;

        public static new List<string> ParamatersToRequest
        {
            get
            {
                List<string> parameters = Motorcycle.ParamatersToRequest;
                parameters.Add("Remaining time in battery");
                return parameters;
            }
        }

        internal ElectricMotorcycle(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            eLicenseType i_LicenseType,
            float i_EngineVolume,
            float i_RemainingTime)
            : base(
                  i_LicenseNumber,
                  i_ModelName,
                  i_WheelsManufacturer,
                  i_CurrentAirPressure,
                  new ElectricEngine(k_MaxTime, i_RemainingTime),
                  i_LicenseType,
                  i_EngineVolume)
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
            StringBuilder ElectricMotorcycleSB = new StringBuilder();
            ElectricMotorcycleSB.Append("Electric motorcycle\n");
            ElectricMotorcycleSB.Append(base.ToString());
            ElectricMotorcycleSB.Append("\nMax Time in hours: " + MaxTimeInHours.ToString());
            ElectricMotorcycleSB.Append(", Remaining Time in hours: " + RemainingTimeInHours.ToString());
            return ElectricMotorcycleSB.ToString();
        }
    }
}
