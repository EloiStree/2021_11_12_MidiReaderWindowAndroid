using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Eloi;
using System;

public class MidiEventTypeSplitterMono : MonoBehaviour
{


    [Header("Listen to all")]
    public UnityMidiRawIntEvent m_allRawEventReceived;
    public UnityMidiEventArgsEvent m_allEventArgsReceived;

    [Header("Listen to understood by this script")]
    public UnityMidiNoteEvent m_onNoteEvent;
    public UnityMidiPatchEvent m_onPatchEvent;
    public UnityMidiControlEvent m_onControllEvent;
    public UnityMidiPitchEvent m_onPitchEvent;
    public UnitySimpleMidiEvent m_onStartPauseStopEvent;

    [Header("Listen to not managed")]
    public UnityMidiNotHandledEvent m_notHandleEvents;

    [Header("Debug")]
    public Eloi.StringClampHistory m_historyDebug;
    public Eloi.StringClampHistory m_historyLogDebug;
    public Eloi.StringClampHistory m_historyNotHandleDebug;

    public void PushReceivedMidiEvent( MidiInMessageEventArgs e)
    {
        m_historyDebug.PushIn("" + e.RawMessage);
        m_allRawEventReceived.Invoke(e.RawMessage);
        m_allEventArgsReceived.Invoke(e);

        MidiEvent me = e.MidiEvent;
        if (me == null)
            return;

        // NAudio.Midi.RawMetaEvent ,
        // NAudio.Midi.ChannelAfterTouchEvent ,
        // NAudio.Midi.ControlChangeEvent ,
        // NAudio.Midi.KeySignatureEvent ,
        // NAudio.Midi.MetaEvent ,
        // NAudio.Midi.NoteEvent ,
        // NAudio.Midi.NoteOnEvent ,
        // NAudio.Midi.PatchChangeEvent ,
        // NAudio.Midi.PitchWheelChangeEvent ,
        // NAudio.Midi.SequencerSpecificEvent ,
        // NAudio.Midi.SmpteOffsetEvent ,
        // NAudio.Midi.SysexEvent ,
        // NAudio.Midi.TempoEvent ,
        // NAudio.Midi.TextEvent ,
        // NAudio.Midi.TimeSignatureEvent ,
        // NAudio.Midi.TrackSequenceNumberEvent
        if (me is NoteEvent)
        {
            NoteEvent noteEvt = me as NoteEvent;
            MidiOneLinerDebug.GetFrom(in noteEvt, out string description);
            m_historyLogDebug.PushIn(description);
            try
            {
                m_onNoteEvent.Invoke(noteEvt);
            }
            catch (Exception exc) { Debug.LogWarning(exc.StackTrace); }
            return;
        }
        else if (me is ControlChangeEvent)
        {
            ControlChangeEvent noteEvt = me as ControlChangeEvent;
            MidiOneLinerDebug.GetFrom(in noteEvt, out string description);
            m_historyLogDebug.PushIn(description);
            try
            {
                m_onControllEvent.Invoke(noteEvt);
            }
            catch (Exception exc) { Debug.LogWarning(exc.StackTrace); }
            return;
        }
        else if (me is PatchChangeEvent)
        {

            PatchChangeEvent noteEvt = me as PatchChangeEvent;
            MidiOneLinerDebug.GetFrom(in noteEvt, out string description);
            m_historyLogDebug.PushIn(description);
            try
            {
                m_onPatchEvent.Invoke(noteEvt);
            }
            catch (Exception exc) { Debug.LogWarning(exc.StackTrace); }
            return;
        }
        else if (me is PitchWheelChangeEvent)
        {

            PitchWheelChangeEvent noteEvt = me as PitchWheelChangeEvent;
            MidiOneLinerDebug.GetFrom(in noteEvt, out string description);
            m_historyLogDebug.PushIn(description);
            try
            {
                m_onPitchEvent.Invoke(noteEvt);
            }
            catch (Exception exc) { Debug.LogWarning(exc.StackTrace); }
            return;
        }
        else if (me is MidiEvent)
        {

            if (me.CommandCode == MidiCommandCode.StartSequence || me.CommandCode == MidiCommandCode.StopSequence || me.CommandCode == MidiCommandCode.ContinueSequence)
            { 
                MidiEvent noteEvt = me as MidiEvent;
                MidiOneLinerDebug.GetFrom(in noteEvt, out string description);
                m_historyLogDebug.PushIn(description);
                try
                {
                    m_onStartPauseStopEvent.Invoke(noteEvt);
                }
                catch (Exception exc) { Debug.LogWarning(exc.StackTrace); }
                return;
            }
            //else if (me.CommandCode == MidiCommandCode.AutoSensing)
            //{
            //}
            //else if (me.CommandCode == MidiCommandCode.ChannelAfterTouch)
            //{
            //}
            //else if (me.CommandCode == MidiCommandCode.KeyAfterTouch)
            //{
            //}
            //else if (me.CommandCode == MidiCommandCode.MetaEvent)
            //{
            //}
            //else if (me.CommandCode == MidiCommandCode.Sysex)
            //{
            //}
            //else if (me.CommandCode == MidiCommandCode.TimingClock)
            //{
            //}
            else
            {
                MidiOneLinerDebug.GetFromUnkown(e.MidiEvent, out string description);
                m_historyNotHandleDebug.PushIn(description);
                m_historyLogDebug.PushIn(description);
            }
        }
        else
        {
             MidiOneLinerDebug.GetFromUnkown(e.MidiEvent, out string description);
            m_historyNotHandleDebug.PushIn(description);
            m_historyLogDebug.PushIn(description);
        }

    }
}


public class MidiOneLinerDebug {

    public static void GetFromUnkown(in MidiInMessageEventArgs value, out string description)
    {
        description= String.Format("Unmanaged: Time {0} Message 0x{1:X8} Event {2} Command:{3}",
                value.Timestamp, value.RawMessage, value.MidiEvent, value.MidiEvent.CommandCode);
    }
    public static void GetFromUnkown(in MidiEvent value, out string description)
    {
        description = String.Format("Unmanaged: Time {0} Short: {1} Channel: {2} Command:{3} Delta:{4} Type:{5}",
                value.AbsoluteTime, value.GetAsShortMessage(), value.Channel, value.CommandCode, value.DeltaTime, value.GetType());
    }
    public static void GetFrom(in MidiEvent value, out string description)
    {
        description = String.Format("Midi Event, Type{5}: Time {0} Short: {1} Channel: {2} Command:{3} Delta:{4} ",
                value.AbsoluteTime, value.GetAsShortMessage(), value.Channel, value.CommandCode, value.DeltaTime, value.GetType());
    }

    public static void GetFrom(in PitchWheelChangeEvent value, out string description)
    {
        description = String.Format("Pitch: dt {0} , chan {1} , time{2}, Pitch {4} | {3}",

           value.DeltaTime,
           value.Channel,
           value.AbsoluteTime,
           value.GetAsShortMessage(),
           value.Pitch);
    }
    public static void GetFrom(in NoteEvent value, out string description)
    {
        description = String.Format("Note: {0}, Num {1}, V {2}, DT {3}, Chan {4}, T {5} | {6}",

           value.NoteName,
           value.NoteNumber,
           value.Velocity,
           value.DeltaTime,
           value.Channel,
           value.AbsoluteTime,
           value.GetAsShortMessage());
    }
    public static void GetFrom(in PatchChangeEvent value, out string description)
    {
        description = String.Format("Patch: dt {0} , chan {1} , time{2} | {3}",

           value.DeltaTime,
           value.Channel,
           value.AbsoluteTime,
           value.GetAsShortMessage());
    }
   
    public static void GetFrom(in ControlChangeEvent value, out string description)
    {
        description = String.Format("Control: {0}, {1}, {2}, {3}, {4}, {5} | {6}",
          value.CommandCode,
          value.Controller,
          value.ControllerValue,
          value.DeltaTime,
          value.Channel,
          value.AbsoluteTime,
          value.GetAsShortMessage());
    }
   
}


[System.Serializable]
public class UnityMidiNoteEvent : UnityEvent<NoteEvent> { }
[System.Serializable]
public class UnityMidiPatchEvent : UnityEvent<PatchChangeEvent> { }
[System.Serializable]
public class UnityMidiControlEvent : UnityEvent<ControlChangeEvent> { }
[System.Serializable]
public class UnityMidiPitchEvent : UnityEvent<PitchWheelChangeEvent> { }
[System.Serializable]
public class UnityMidiNotHandledEvent : UnityEvent<MidiEvent> { }
[System.Serializable]
public class UnitySimpleMidiEvent : UnityEvent<MidiEvent> { }

[System.Serializable]
public class UnityMidiRawIntEvent : UnityEvent<int> { }
[System.Serializable]
public class UnityMidiEventArgsEvent : UnityEvent<MidiInMessageEventArgs> { }