using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class RegularMotorcycle : Motorcycle
    {
        private const float k_MaxAmountOfGas = 5.5f;

        public static new List<string> ParamatersToRequest
        {
            get
            {
                List<string> parameters = Motorcycle.ParamatersToRequest;
                parameters.Add("Current amount of gas");
                return parameters;
            }
        }

        internal RegularMotorcycle(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            eLicenseType i_LicenseType,
            float i_EngineVolume,
            float i_CurrentGas)
            : base(
                  i_LicenseNumber,
                  i_ModelName,
                  i_WheelsManufacturer,
                  i_CurrentAirPressure,
                  new GasEngine(eGasType.Octan95, k_MaxAmountOfGas, i_CurrentGas),
                  i_LicenseType,
                  i_EngineVolume)
        {
        }

        private GasEngine GasEngine
        {
            get
            {
                return (GasEngine)r_Engine;
            }
        }

        public eGasType GasType
        {
            get
            {
                return GasEngine.GasType;
            }
        }

        public float CurrentAmountOfGas
        {
            get
            {
                return GasEngine.CurrentAmountOfGas;
            }
        }

        public float MaxAmountOfGas
        {
            get
            {
                return GasEngine.MaxAmountOfGas;
            }
        }

        public override void FillUpTank(eGasType i_GasType, float i_GasLitresToFill)
        {
            GasEngine.FillUpTank(i_GasType, i_GasLitresToFill);
        }

        public override string ToString()
        {
            StringBuilder regularMotorcycleSb = new StringBuilder();
            regularMotorcycleSb.Append("Regular motorcycle\n");
            regularMotorcycleSb.Append(base.ToString());
            regularMotorcycleSb.Append("\nMax amount of gas: " + MaxAmountOfGas.ToString());
            regularMotorcycleSb.Append(", Current amount of gas: " + CurrentAmountOfGas.ToString());
            regularMotorcycleSb.Append("\nGas type:" + GasType.ToString());
            return regularMotorcycleSb.ToString();
        }
    }
}
