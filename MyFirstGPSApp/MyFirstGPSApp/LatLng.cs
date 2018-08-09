using System;
using Android.Locations;

namespace MyFirstGPSApp
{
    /// <summary>It houses the GPS coordinates properties.</summary>
    public class LatLng
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>A constructor...</summary>
        public LatLng()
        {
            this.Latitude = 0.0;
            this.Longitude = 0.0;
        }

        /// <summary>A constructor...</summary>
        public LatLng(double lat, double lng)
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }

        /// <summary>Creates a shallow copy by creating a new object.</summary>
        /// <returns>New object with copying the nonstatic fields of the current object to them.</returns>
        public LatLng Clone()
        {
            return (LatLng)this.MemberwiseClone();
        }

        /// <summary>Assigns GPS coordinate properties from given <paramref name="location"/> object.</summary>
        public void Assign(Android.Locations.Location location)
        {
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
        }

        /// <summary>Assigns GPS coordinate properties from given <paramref name="source"/> object it there is a match.</summary>
        public void Assign(object source)
        {
            //get the list of all properties in the destination (this) object
            var destinationProperties = this.GetType().GetProperties();

            //get the list of all properties in the source object
            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                foreach (var destinationProperty in destinationProperties)
                {
                    //if we find match between source & destination properties name, set the value to the destination property
                    if (destinationProperty.Name == sourceProperty.Name &&
                            destinationProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType) &&
                            destinationProperty.CanWrite)
                    {
                        destinationProperty.SetValue(this, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }
        }

    }
}