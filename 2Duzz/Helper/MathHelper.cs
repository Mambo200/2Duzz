using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Duzz.Helper
{
    public static class MathHelper
    {
        public static int Between(int _value, int _min, int _max)
        {
            if (_min > _max)
                throw new NotImplementedException($"Parameter {nameof(_min)} was larger than {nameof(_max)}.");
            else if (_max < _min)
                throw new NotImplementedException($"Parameter {nameof(_max)} was lower than {nameof(_min)}.");

            _value = Math.Max(_value, _min);
            return Math.Min(_value, _max);
        }
    }
}
