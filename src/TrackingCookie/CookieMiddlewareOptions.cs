namespace TrackingCookie
{
    public class TrackingCookieMiddlewareOptions
    {
        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(30);
        public string Name { get; set; } = "trkId";
        public string SubDomain { get; set; } = "trk";
    } // end class
} // end class