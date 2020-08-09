using System;

namespace custom_class
{
    public class Pair
    {
	    public Pair(float value1,float value2)
        {
            Set(value1, value2);
        }

        public void Set(float value1, float value2)
        {
            firstValue = value1;
            secondValue = value2;
        }

        public float first()
        {
            return firstValue;
        }

        public float second()
        {
            return secondValue;
        }

        private float firstValue;
        private float secondValue;
    };
}
