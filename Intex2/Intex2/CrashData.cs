using System;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2.Models
{
    public class CrashData
    {

        public long city_Other { get; set; }

        public long pedestrian_involved { get; set; }

        public long bicyclist_involved { get; set; }

        public long motorcycle_involved { get; set; }

        public long intersection_related { get; set; }

        public long overturn_rollover_true { get; set; }

        public long teenage_driver { get; set; }

        public long older_driver_involved { get; set; }

        public long night_dark_condition { get; set; }

        public long single_vehicle { get; set;}

        public long roadway_departure { get; set; }

        


        public Tensor<long> AsTensor()
        {
            long[] data = new long[]
            {
            city_Other, pedestrian_involved, bicyclist_involved, motorcycle_involved, intersection_related, overturn_rollover_true, teenage_driver, older_driver_involved, night_dark_condition, single_vehicle, roadway_departure
            };
            int[] dimensions = new int[] { 1, 11 };
            return new DenseTensor<long>(data, dimensions);
        }

    }
}
