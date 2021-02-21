using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class Location
  {
    public long ElapsedRealtimeNanos { get; set; }

    public double ElapsedRealtimeUncertaintyNanos { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Altitude { get; set; }

    public float Speed { get; set; }

    public float Bearing { get; set; }

    public float HorizontalAccuracyMeters { get; set; }

    public float VerticalAccuracyMeters { get; set; }


    public float SpeedAccuracyMetersPerSecond { get; set; }

    public float BearingAccuracyDegrees { get; set; }
  }
}
