using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Constants
{
    public enum ScannerRewards
    {
        MONEY,
    }

    public enum FlightLevel
    {
        SIGHTSEEING_FLIGHT,
        SHORT_HAUL_FLIGHT,
        BETTER_SHORT_HAUL_FLIGHT,
        MEDIUM_HAUL_FLIGHT,
        BETTER_MEDIUM_HAUL_FLIGHT,
        LONG_HAUL_FLIGHT,
        BETTER_LONG_HAUL_FLIGHT,
        FLIGHT_AROUND_THE_WORLD
    };

    public enum FlightClass
    {
        ECONOMY_CLASS,
        BUSINESS_CLASS,
        FIRST_CLASS,
        LUXURY_FIRST_CLASS
    };

    // Multiplier for every incrementation
    public const float MULTIPLIER = 1.5f;

    // Base cost of new Lanes
    public const float LANE_BASE_PRICE = 1000.0f;

    // Price of Duty-Free shops
    public const float DUTY_FREE_SHOP_PRICE = 100.0f;

    // Base costs for very queue upgrade
    public const float QUEUE_LENGTH_UPGRADE_BASE_COST = 250.0f;
    public const float QUEUE_TIME_UPGRADE_BASE_COST = 100.0f;
    public const float QUEUE_SPAWN_UPGRADE_BASE_COST = 100.0f;

    // Payments for different airplane levels
    public const float PAYMENT_SIGHTSEEING_FLIGHT = 60.0f;
    public const float PAYMENT_SHORT_HAUL_FLIGHT = 120.0f;
    public const float PAYMENT_BETTER_SHORT_HAUL_FLIGHT = 500.0f;
    public const float PAYMENT_MEDIUM_HAUL_FLIGHT = 1000.0f;
    public const float PAYMENT_BETTER_MEDIUM_HAUL_FLIGHT = 5000.0f;
    public const float PAYMENT_LONG_HAUL_FLIGHT = 10000.0f;
    public const float PAYMENT_BETTER_LONG_HAUL_FLIGHT = 15000.0f;
    public const float PAYMENT_AROUND_THE_WORLD_FLIGHT = 20000.0f;

    // Multipliers for different airplane levels
    public const float ECONOMY_CLASS_MULTIPLIER = 1.09f;
    public const float BUSINESS_CLASS_MULTIPLIER = 1.20f;
    public const float FIRST_CLASS_MULTIPLIER = 1.50f;
    public const float LUXURY_FIRST_CLASS_MULTIPLIER = 2.00f;

    // Base cost for the scanner upgrade
    public const float SCANNER_UPGRADE_BASE_COST = 1000.0f;

    // Base cost for airplane upgrades
    public const float AIRPLANE_LENGTH_UPGRADE_BASE_COST = 500.0f;
    public const float AIRPLANE_CLASS_UPGRADE_BASE_COST = 2000.0f;
}

