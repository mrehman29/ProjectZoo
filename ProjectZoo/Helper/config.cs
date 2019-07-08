using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectZoo.Helper
{
    public static class config
    {
        /// <summary>
        /// List all animal types
        /// </summary>
        public static List<String> Types = new List<string>() { "Elephant", "Monkey", "Giraffe" };

        /// <summary>
        /// configure number of animals for each type
        /// </summary>
        public static List<int> TypeCount = new List<int>() { 5, 5, 5 };

        /// <summary>
        /// Max health level for each type 
        /// This will the start Health Level
        /// </summary>
        public static List<float> MaxHealth = new List<float> { 100, 100, 100 };

        /// <summary>
        /// Min Feed Increment 
        /// When animal type is feeded rendom feed increment will be generated between Feed Min and FeedMax value
        /// </summary>
        public static List<int> FeedMin = new List<int> { 10, 10, 10 };

        /// <summary>
        /// Max Feed Increment 
        /// When animal type is feeded rendom feed increment will be generated between Feed Min and FeedMax value
        /// </summary>
        public static List<int> FeedMax = new List<int> { 25, 25, 25 };

        /// <summary>
        /// Min Health Decrement 
        /// Random health value is calculated for Animal type based on Health Min and Max
        /// </summary>
        public static List<int> HealthMin = new List<int> { 0, 0, 0 };

        /// <summary>
        /// Max Health Decrement 
        /// Random health value is calculated for Animal type based on Health Min and Max
        /// </summary>
        public static List<int> HealthMax = new List<int> { 20, 20, 20 };

        /// <summary>
        /// Time in min after which health is decreased if they are not feeded
        /// when an animal is feeded their health update timer will be reset
        /// if they are not feeded within this time then their health is decreased
        /// </summary>
        public static List<int> HealthUpdate = new List<int> { 60, 60, 60 };


        /// <summary>
        /// Critical health level for each Animal type 
        /// </summary>
        public static List<float> CriticalHealth = new List<float> { 75, 30, 50 };

        /// <summary>
        /// time how long it will survavie after crifical health is reached
        /// </summary>
        public static List<int> CriticalServival = new List<int> { 60, 0, 0 };

        /// <summary>
        /// Timer to check and update health after every 15 sec
        /// </summary>
        public static int HealthCheckInterval = 15000; 
    }
}