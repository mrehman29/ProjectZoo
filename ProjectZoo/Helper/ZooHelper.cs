using ProjectZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectZoo.Helper
{
    public static class ZooHelper
    {
        /// <summary>
        /// Initilizinf or reinitializing simulator
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ZooAnimal>> ReInit() {
            using (var db = new DB()) {
                //Clear All Zoo If Needed
                db.ZooAnimals.RemoveRange(db.ZooAnimals);
                await db.SaveChangesAsync();
                int count = 0;
                foreach (var t in config.Types) {
                    for(var a =0; a< config.TypeCount[count]; a++)
                    {
                        db.ZooAnimals.Add(new ZooAnimal()
                        {
                            Type = t,
                            HealthLevel = config.MaxHealth[count],
                            MaxHealth = config.MaxHealth[count],
                            LastFeed = DateTime.Now,
                            HealthUpdateInteral = config.HealthUpdate[count],
                            HealthMaxUpdate = config.HealthMax[count],
                            HealthMinUpdate = config.HealthMin[count],
                            FeedMaxUpdate = config.FeedMax[count],
                            FeedMinUpdate = config.FeedMin[count],
                            CriticalHealthLevel = config.CriticalHealth[count],
                            CriticalServivalTime = config.CriticalServival[count],
                            IsAlive = true,
                            IsCritical = false,
                            HealthUpdatedOn = DateTime.Now
                            
                        });
                        
                    }
                    count += 1;
                }
                try
                {
                   await db.SaveChangesAsync();
                }
                catch (Exception ee) {
                    System.Diagnostics.Debug.WriteLine("Exception " + ee);

                }
                return db.ZooAnimals.ToList();
                
            }

        }

        /// <summary>
        /// Process called to update live animals health
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ZooAnimal>> UpdateHealth() {
            Random r = new Random();
            using (var db = new DB()) {
                var current = db.ZooAnimals.Where(x => x.IsAlive).ToList();
                foreach (var c in current) {
                    if (c.TimeFromLastUpdate >= c.HealthUpdateInteral * 60) {
                        //Need to Update health 
                        
                        c.HealthLevel -= Convert.ToDouble(r.Next(c.HealthMinUpdate, c.HealthMaxUpdate - 1) + "." + r.Next(0, 99));
                        c.HealthUpdatedOn = DateTime.Now;

                    }

                    //If IsCritical from previous Update and is critical now is dead now
                    if (c.IsCritical && c.HealthLevel <= c.CriticalHealthLevel) {
                        c.IsAlive = false;
                        c.HealthLevel = 0;
                    }

                    else if (c.HealthLevel <= c.CriticalHealthLevel)
                    {
                        c.IsCritical = true;
                        if (c.CriticalServivalTime <= DateTime.Now.Subtract(c.HealthUpdatedOn).TotalMinutes) {
                            c.IsAlive = false;
                            c.HealthLevel = 0;
                        }
                    }
                    else {
                        c.IsCritical = false;
                    }

                }
                await db.SaveChangesAsync();
                return db.ZooAnimals.ToList();
            }
            
        }

        /// <summary>
        /// process called to feed live animals
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ZooAnimal>> FeedUpdate() {
            Random r = new Random();
            using (var db = new DB())
            {
                List<double> feedhalth = new List<double>();
                int count = 0;
                foreach (var t in config.Types) {
                    var FeedMin = config.FeedMin[count];
                    var FeedMax = config.FeedMax[count];
                    feedhalth.Add(Convert.ToDouble(r.Next(FeedMin, FeedMax - 1) + "." + r.Next(0, 99)));
                    count += 1;
                }
                //Getting all live animales to update their health
                var CurrnetValues = db.ZooAnimals.Where(x => x.IsAlive);
                foreach (var c in CurrnetValues) {
                    var updatedhealth = c.HealthLevel + feedhalth[config.Types.IndexOf(c.Type)];
                    c.HealthLevel = updatedhealth <= c.MaxHealth ? updatedhealth : c.MaxHealth;
                    c.LastFeed = DateTime.Now;
                    c.HealthUpdatedOn = DateTime.Now;
                    if (c.HealthLevel > c.CriticalHealthLevel)
                    {
                        c.IsCritical = false;
                    }
                }

                

                await db.SaveChangesAsync();
                return db.ZooAnimals.ToList();

            }
        }
    }
}