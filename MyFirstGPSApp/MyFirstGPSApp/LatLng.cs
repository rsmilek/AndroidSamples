namespace MyFirstGPSApp
{
    /// <summary>
    /// The very first thing to do is to create a class that houses the following properties
    /// </summary>
    public class LatLng
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLng(double lat, double lng)
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }
    }
}