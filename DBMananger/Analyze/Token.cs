using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMananger.Analyze
{
    internal class Token
    {
        public string Value { get; internal set; }

        public Token()
        { }

        public Token(string _token)
        {
            Value = _token;
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj.ToString());
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    class TokenComparer : IEqualityComparer<Token>
    {
        bool IEqualityComparer<Token>.Equals(Token x, Token y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Value.Equals(y.Value);

            throw new NotImplementedException();
        }

        int IEqualityComparer<Token>.GetHashCode(Token obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashTokenValue = obj.Value == null ? 0 : obj.Value.GetHashCode();
            return hashTokenValue;
            throw new NotImplementedException();
        }
    }
}