namespace Coupon.Core.Externsion
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyValues(this string value, params string[] @values)
        {
            if (values == null)
                return true;

            foreach ( var v in @values )
            {
                if (String.IsNullOrEmpty(v))
                    return true;
            }

           
            return false;
        }
    }
}
