using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_MidiUtility
{
    public static void GetMidiOutIdOf(in string midiNameId, out bool found, out int id)
    {

        for (int device = 0; device < MidiOut.NumberOfDevices; device++)
        {
            string name = MidiOut.DeviceInfo(device).ProductName;
            if (name.Length == midiNameId.Length && name.ToLower().IndexOf(midiNameId.ToLower()) == 0)
            {
                found = true;
                id = device;
                return;
            }
        }
        found = false;
        id = -1;
    }

    public static void GetMidiInIdOf(in string midiNameId, out bool found, out int id)
    {

        for (int device = 0; device < MidiIn.NumberOfDevices; device++)
        {
            string name = MidiIn.DeviceInfo(device).ProductName;
            if (name.Length == midiNameId.Length && name.ToLower().IndexOf(midiNameId.ToLower()) == 0)
            {
                found = true;
                id = device;
                return;
            }
        }
        found = false;
        id = -1;
    }
}