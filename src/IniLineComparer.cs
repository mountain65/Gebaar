using System.Collections.Generic;
using System.Linq;

namespace Gebaar
{
    public class IniLineComparer : IEqualityComparer<string[]>
    {
        #region IEqualityComparer implementation
        public bool Equals(string[] x, string[] y)
        {
            return x[1].Split(';').First().Equals(y[1].Split(';').First());
        }

        public int GetHashCode(string[] obj)
        {
            return obj[1].Split(';').First().GetHashCode();
        }
        #endregion
    }
}
