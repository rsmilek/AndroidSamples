using System;

namespace Location
{
    /// <summary>
    /// The very first thing to do is to create a class that houses the following properties
    /// </summary>
    public class LatLng
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLng()
        {
            this.Latitude = 0.0;
            this.Longitude = 0.0;
        }

        public LatLng(double lat, double lng)
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }

        public LatLng Clone()
        {
            return (LatLng)this.MemberwiseClone();
        }

        public static void CopyProperties(object objSource, object objDestination)
        {
            //get the list of all properties in the destination object
            var destProps = objDestination.GetType().GetProperties();

            //get the list of all properties in the source object
            foreach (var sourceProp in objSource.GetType().GetProperties())
            {
                foreach (var destProperty in destProps)
                {
                    //if we find match between source & destination properties name, set
                    //the value to the destination property
                    if (destProperty.Name == sourceProp.Name &&
                            destProperty.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destProperty.SetValue(destProps, sourceProp.GetValue(
                            sourceProp, new object[] { }), new object[] { });
                        break;
                    }
                }
            }
        }

        public void Assign(object source)
        {
            //get the list of all properties in the destination (this) object
            var destProps = this.GetType().GetProperties();

            //get the list of all properties in the source object
            foreach (var sourceProp in source.GetType().GetProperties())
            {
                foreach (var destProperty in destProps)
                {
                    //if we find match between source & destination properties name, set the value to the destination property
                    if (destProperty.Name == sourceProp.Name &&
                            destProperty.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destProperty.SetValue(destProps, sourceProp.GetValue(
                            sourceProp, new object[] { }), new object[] { });
                        break;
                    }
                }
            }
        }

    }
}