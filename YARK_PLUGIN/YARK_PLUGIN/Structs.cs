﻿using System;
using System.Runtime.InteropServices;


namespace KSP_PLUGIN
{
    public class Structs
    {
        public static readonly byte[] Header_Array = new[] { (byte)0xFF, (byte)0xC4, (byte)'Y', (byte)'A', (byte)'R', (byte)'K', (byte)0x00, (byte)0xFF };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct HeaderArray
        {
            public fixed byte header[8];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)] //used by reciever
        public struct Header
        {
            public HeaderArray header;
            public UInt16 checksum;
            public byte type;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct StatusPacket
        {
            public int ID;

            public byte status;
            public fixed byte vessalName[16]; //16 bytes
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NavHeading
        {
            public float Pitch, Heading;
            public NavHeading(float Pitch, float Heading)
            {
                this.Pitch = Pitch;
                this.Heading = Heading;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct VesselPacket
        {
            public int ID;

            public float deltaTime;

            //##### CRAFT ######
            public float Pitch; //pitch and heading close together so c++ can use this as a NavHeading ptr
            public float Heading;
            public float Roll;

            //#### NAVBALL VECTOR #######
            public NavHeading Prograde;
            public NavHeading Target;
            public NavHeading Maneuver;

            public byte MainControls;                   //SAS RCS Lights Gear Brakes Abort Stage
            public UInt16 ActionGroups;                   //action groups 1-10 in 2 bytes
            public float VVI;
            public float G;
            public float RAlt;
            public float Alt;
            public float Vsurf;
            public byte MaxOverHeat;    //  Max part overheat (% percent)
            public float IAS;           //  Indicated Air Speed


            //###### ORBITAL ######
            public float VOrbit;
            public float AP;
            public float PE;
            public int TAp;
            public int TPe;
            public float SemiMajorAxis;
            public float SemiMinorAxis;
            public float e;
            public float inc;
            public int period;
            public float TrueAnomaly;
            public float Lat;
            public float Lon;

            //###### FUEL #######
            public byte CurrentStage;   //  Current stage number
            public byte TotalStage;     //  TotalNumber of stages
            public float LiquidFuelTot;
            public float LiquidFuel;
            public float OxidizerTot;
            public float Oxidizer;
            public float EChargeTot;
            public float ECharge;
            public float MonoPropTot;
            public float MonoProp;
            public float IntakeAirTot;
            public float IntakeAir;
            public float SolidFuelTot;
            public float SolidFuel;
            public float XenonGasTot;
            public float XenonGas;
            public float LiquidFuelTotS;
            public float LiquidFuelS;
            public float OxidizerTotS;
            public float OxidizerS;

            //### MISC ###
            public UInt32 MissionTime;
            public UInt32 MNTime;
            public float MNDeltaV;
            public byte HasTarget;
            public float TargetDist;    //  Distance to targeted vessel (m)
            public float TargetV;       //  Target vessel relative velocity (m/s)
            public byte SOINumber;      //  SOI Number (decimal format: sun-planet-moon e.g. 130 = kerbin, 131 = mun)

            public byte SASMode; //hold, prograde, retro, etc...
            public byte SpeedMode; //Surface, orbit target

            public byte timeWarpRateIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ControlPacket
        {
            public int ID;
            public byte MainControls;                   //SAS RCS Lights Gear Brakes Abort Stage
            public UInt16 ActionGroups;                //action groups 1-10 in 2 bytes
            public short Pitch;                        //-1000 -> 1000
            public short Roll;                         //-1000 -> 1000
            public short Yaw;                          //-1000 -> 1000
            public short TX;                           //-1000 -> 1000
            public short TY;                           //-1000 -> 1000
            public short TZ;                           //-1000 -> 1000
            public short WheelSteer;                   //-1000 -> 1000
            public short Throttle;                     // 0 -> 1000
            public short WheelThrottle;                // 0 -> 1000
            public byte SASMode; //hold, prograde, retro, etc...
            public byte SpeedMode; //Surface, orbit target
            public float targetHeading, targetPitch, targetRoll;
            public byte timeWarpRateIndex;
        };

        public struct VesselControls
        {
            public Boolean SAS;
            public Boolean RCS;
            public Boolean Lights;
            public Boolean Gear;
            public Boolean Brakes;
            //public Boolean Precision;
            public Boolean Abort;
            public Boolean Stage;
            public int Mode;
            public int SASMode;
            public int SpeedMode;
            public Boolean[] ActionGroups;
            public float Pitch;
            public float Roll;
            public float Yaw;
            public float TX;
            public float TY;
            public float TZ;
            public float WheelSteer;
            public float Throttle;
            public float WheelThrottle;
            public float targetHeading, targetPitch, targetRoll;
            //public Boolean holdTargetVector;
            public byte timeWarpRateIndex;
            public VesselControls(bool ignore)
            {
                /*holdTargetVector =*/
                SAS = RCS = Lights = Gear = Brakes /*= Precision*/ = Abort = Stage = false;
                Mode = SASMode = SpeedMode = 0;
                timeWarpRateIndex = 1;
                ActionGroups = new Boolean[10];
                targetHeading = targetPitch = targetRoll = Pitch = Roll = Yaw = TX = TY = TZ = WheelSteer = Throttle = WheelThrottle = 0;
            }
        };
    }
}