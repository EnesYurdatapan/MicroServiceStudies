namespace Ms.Web.Utility
{
    public static class StaticDetails
    {
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE, 
        }
    }
}
