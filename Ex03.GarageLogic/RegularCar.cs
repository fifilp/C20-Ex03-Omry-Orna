using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class RegularCar : Car
    {
        private const float k_MaxAmountOfGas = 50f;

        public static new List<string> ParamatersToRequest
        {
            get
            {
                List<string> parameters = Car.ParamatersToRequest;
                parameters.Add("Current amount of gas");
                return parameters;
            }
        }

        internal RegularCar(
             string i_LicenseNumber,
             string i_ModelName,
             string i_WheelsManufacturer,
             float i_CurrentAirPressure,
             eCarColor i_Color,
             eNumOfDoors i_NumOfDoorsInCar,
             float i_CurrentGas)
            : base(
                  i_LicenseNumber,
                   i_ModelName,
                   i_WheelsManufacturer,
                   i_CurrentAirPressure,
                   new GasEngine(eGasType.Octan96, k_MaxAmountOfGas, i_CurrentGas),
                  i_Color,
                  i_NumOfDoorsInCar)
        {
        }

        public GasEngine GasEngine
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
            StringBuilder regularCarSb = new StringBuilder();
            regularCarSb.Append("RegularCar\n");
            regularCarSb.Append(base.ToString());
            regularCarSb.Append("\nMax amount of gas: " + MaxAmountOfGas.ToString());
            regularCarSb.Append(", Current amount of gas: " + CurrentAmountOfGas.ToString());
            regularCarSb.Append("\nGas type:" + GasType.ToString());
            return regularCarSb.ToString();
        }
    }
}
