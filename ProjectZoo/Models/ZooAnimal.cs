namespace ProjectZoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// DB Model for Zoo Animal
    /// </summary>
    public partial class ZooAnimal
    {
        /// <summary>
        /// animal ID Auto increment in DB 
        /// just a way to differentiate between animals of same type 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type name
        /// Type Name and ID make a Unique Identifier for Animal
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        /// <summary>
        /// Current health Level
        /// </summary>
        public double HealthLevel { get; set; }

        /// <summary>
        /// Max health 
        /// This is also Initial Health level and AMax health cap
        /// </summary>
        public double MaxHealth { get; set; }

        /// <summary>
        /// Datetime when Animal was fead last
        /// System as potential to feed singal animal at a time and that will be reflected in system
        /// </summary>
        public DateTime LastFeed { get; set; }

        /// <summary>
        /// Health Update interval 
        /// Time in Mins after with Health needs to be recalculated
        /// Once Animal is feed its healt is updated straight away and this counter is reset
        /// </summary>
        public int HealthUpdateInteral { get; set; }

        /// <summary>
        /// Min value of Health decrement counter
        /// </summary>
        public int HealthMinUpdate { get; set; }

        /// <summary>
        /// Max value of Health decrement counter
        /// </summary>
        public int HealthMaxUpdate { get; set; }

        /// <summary>
        /// Min Value for Feed Health increment
        /// </summary>
        public int FeedMinUpdate { get; set; }

        /// <summary>
        /// Max Value for Feed Health Increment
        /// </summary>
        public int FeedMaxUpdate { get; set; }

        /// <summary>
        /// Critical health level for this animal
        /// </summary>
        public double CriticalHealthLevel { get; set; }


        /// <summary>
        /// Critical servival time for this animal
        /// </summary>
        public int CriticalServivalTime { get; set; }

        /// <summary>
        /// Indicator if animal is alive
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Indicator for Health Critical
        /// </summary>
        public bool IsCritical { get; set; }


        /// <summary>
        /// date time when health was last updated on
        /// This also get updated when Animal is feed
        /// </summary>
        public DateTime HealthUpdatedOn { get; set; }


        /// <summary>
        /// number of sec since last update
        /// </summary>
        public int TimeFromLastUpdate {
            get => (int)DateTime.Now.Subtract(HealthUpdatedOn).TotalSeconds;
        }
    }
}
