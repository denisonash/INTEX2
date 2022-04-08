using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class Crash
    {
        [Key]
        [Required(ErrorMessage ="This incident needs a crash id")]
        public int crash_id { get; set; }
        [Required(ErrorMessage ="Please enter a date and time.")]
        public string crash_datetime { get; set; }
        [Required(ErrorMessage = "Please enter a route.")]
        public string route { get; set; }
        [Required(ErrorMessage = "Please enter a milepoint.")]
        public string milepoint { get; set; }
        [Required(ErrorMessage = "Please enter a latitude.")]
        public string lat_utm_y { get; set; }
        [Required(ErrorMessage = "Please enter a longitude.")]
        public string long_utm_x { get; set; }
        [Required(ErrorMessage = "Please enter the main road.")]
        public string main_road_name { get; set; }
        [Required(ErrorMessage = "Please enter a city.")]
        public string city { get; set; }
        [Required(ErrorMessage = "Please enter a county")]
        public string county_name { get; set; }
        [Required(ErrorMessage = "Please enter a crash severity.")]
        public int crash_severity_id { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident incident happened in a work zone.")]
        public string work_zone_related { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a pedestrian.")]
        public string pedestrian_involved { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a bicyclist.")]
        public string bicyclist_involved { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a motorcycle.")]
        public string motorcycle_involved { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved an improper restraint.")]
        public string improper_restraint { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved an unrestrained passenger.")]
        public string unrestrained { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a DUI.")]
        public string dui { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved an intersection.")]
        public string intersection_related { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a wild animal.")]
        public string wild_animal_related { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a domestic animal.")]
        public string domestic_animal_related { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved an overturn.")]
        public string overturn_rollover { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a commercial vehicle.")]
        public string commercial_motor_veh_involved { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a teenage driver.")]
        public string teenage_driver_involved { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a senior driver.")]
        public string older_driver_involved { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident happened at night.")]
        public string night_dark_condition { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved only a single vehicle.")]
        public string single_vehicle { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved distracted driving")]
        public string distracted_driving { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved drowsy driving.")]
        public string drowsy_driving { get; set; }
        [Required(ErrorMessage = "Please indicate whether the incident involved a car leaving the road.")]
        public string roadway_departure { get; set; }
    }
}
