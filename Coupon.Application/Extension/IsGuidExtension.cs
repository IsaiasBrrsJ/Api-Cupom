namespace Coupon.Application.Extension
{
    public static class IsGuidExtension
    {
        public static bool IsGuid(this Guid guid)
        {

            if(guid == Guid.Empty)
                return false;


            return true;
        }
    }
}
