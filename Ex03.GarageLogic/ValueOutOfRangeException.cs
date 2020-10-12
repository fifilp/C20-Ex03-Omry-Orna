using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(
            string i_Message,
            float i_MaxValue,
            float i_MinValue = 0) : base(i_Message + ", value is out of range ")
        {
            this.r_MaxValue = i_MaxValue;
            this.r_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get
            {
                return this.r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return this.r_MinValue;
            }
        }

        public override string ToString()
        {
            string msg = string.Format("{0}: {1}-{2}", Message, this.r_MaxValue, this.r_MinValue);
            return msg;
        }
    }
}
