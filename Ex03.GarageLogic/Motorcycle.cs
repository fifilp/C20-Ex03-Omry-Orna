using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        private const float k_MaxVolume = 1000f;
        private const int k_WheelMaxPressure = 28;
        private const int k_NumOfWheels = 2; 
        
        private readonly eLicenseType r_LicenseType;
        private readonly float r_EngineVolume;

        protected Motorcycle(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_CurrentPressure,
            Engine i_Engine,
            eLicenseType i_LicenseType,
            float i_EngineVolume)
            : base(
                  i_LicenseNumber,
                  i_ModelName,
                  i_WheelsManufacturer,
                  k_WheelMaxPressure,
                  i_CurrentPressure,
                  i_Engine,
                  k_NumOfWheels)
        {
            r_LicenseType = i_LicenseType;
            r_EngineVolume = setEngineVolume(i_EngineVolume);
        }

        protected static List<string> ParamatersToRequest
        {
            get
            {
                return new List<string>()
                    {
                        "Model Name (1-20 char, letters and digits only)",
                        "Wheels Manufacturer (1-20 char, letters and digits only)",
                        "Wheels Current Pressure",
                        "License Type (A, A1, B1, B2)",
                        "Engine Volume"
                    };
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return r_LicenseType;
            }
        }

        public float EngineVolume
        {
            get
            {
                return r_EngineVolume;
            }
        }

        private float setEngineVolume(float i_EngineVolume)
        {
            if (i_EngineVolume < 0 || i_EngineVolume > k_MaxVolume)
            {
                throw new ValueOutOfRangeException(
                    "cargo is out of valid range",
                    k_MaxVolume);
            }

            return i_EngineVolume;
        }

        public override string ToString()
        {
            StringBuilder motorcycleSb = new StringBuilder();
            motorcycleSb.Append(base.ToString() + "Engine Volume: " + r_EngineVolume.ToString());
            motorcycleSb.Append("\nLicense type: " + r_LicenseType.ToString());
            return motorcycleSb.ToString();
        }
    }
}
