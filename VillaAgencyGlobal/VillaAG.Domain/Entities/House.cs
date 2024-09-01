using System;
using System.Collections.Generic;

namespace VillaAG.Domain.Entities {

    public class House {
        // Fields
        private string address;
        private double areaInSquareFeet;
        private int numberOfBedrooms;
        private int numberOfBathrooms;
        private int numberOfFloors;
        private bool hasGarage;
        private bool hasGarden;
        private bool hasSwimmingPool;
        private DateTime constructionYear;
        private string houseType; // e.g., Detached, Semi-Detached, Terraced, etc.
        private string roofType; // e.g., Flat, Gabled, Hipped, etc.
        private HeatingType heatingType;
        private CoolingType coolingType;
        private List<Room> rooms;
        private bool hasFireplace;
        private bool hasBasement;
        private bool hasAttic;
        private List<Utility> utilities;
        private List<Appliance> appliances;

        // Properties
        public string Address {
            get { return address; }
            set { address = value; }
        }

        public double AreaInSquareFeet {
            get { return areaInSquareFeet; }
            set { areaInSquareFeet = value; }
        }

        public int NumberOfBedrooms {
            get { return numberOfBedrooms; }
            set { numberOfBedrooms = value; }
        }

        public int NumberOfBathrooms {
            get { return numberOfBathrooms; }
            set { numberOfBathrooms = value; }
        }

        public int NumberOfFloors {
            get { return numberOfFloors; }
            set { numberOfFloors = value; }
        }

        public bool HasGarage {
            get { return hasGarage; }
            set { hasGarage = value; }
        }

        public bool HasGarden {
            get { return hasGarden; }
            set { hasGarden = value; }
        }

        public bool HasSwimmingPool {
            get { return hasSwimmingPool; }
            set { hasSwimmingPool = value; }
        }

        public DateTime ConstructionYear {
            get { return constructionYear; }
            set { constructionYear = value; }
        }

        public string HouseType {
            get { return houseType; }
            set { houseType = value; }
        }

        public string RoofType {
            get { return roofType; }
            set { roofType = value; }
        }

        public HeatingType HeatingType {
            get { return heatingType; }
            set { heatingType = value; }
        }

        public CoolingType CoolingType {
            get { return coolingType; }
            set { coolingType = value; }
        }

        public List<Room> Rooms {
            get { return rooms; }
            set { rooms = value; }
        }

        public bool HasFireplace {
            get { return hasFireplace; }
            set { hasFireplace = value; }
        }

        public bool HasBasement {
            get { return hasBasement; }
            set { hasBasement = value; }
        }

        public bool HasAttic {
            get { return hasAttic; }
            set { hasAttic = value; }
        }

        public List<Utility> Utilities {
            get { return utilities; }
            set { utilities = value; }
        }

        public List<Appliance> Appliances {
            get { return appliances; }
            set { appliances = value; }
        }
    }

    // Additional classes to represent complex properties
    public class Room {
        public string RoomType { get; set; } // e.g., Bedroom, Kitchen, Living Room, etc.
        public double AreaInSquareFeet { get; set; }
        public bool HasWindow { get; set; }
        public bool HasEnsuiteBathroom { get; set; }
    }

    public class Utility {
        public string UtilityType { get; set; } // e.g., Electricity, Water, Gas, etc.
        public bool IsConnected { get; set; }
    }

    public class Appliance {
        public string ApplianceType { get; set; } // e.g., Refrigerator, Oven, Washing Machine, etc.
        public string Brand { get; set; }
        public bool IsEnergyEfficient { get; set; }
    }

    // Enumerations for predefined options
    public enum HeatingType {
        None,
        Gas,
        Electric,
        Oil,
        Wood,
        Solar,
        Geothermal
    }

    public enum CoolingType {
        None,
        CentralAir,
        WindowUnit,
        EvaporativeCooler,
        Geothermal
    }
}
