using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbstractMidi_DebugLine : MonoBehaviour, IAbstractMidiGetHandler
{

   public UI_AbstractMidi_DebugLine_PitchChange   m_pitchDisplay;
   public UI_AbstractMidi_DebugLine_ControlChange m_controlDisplay;
   public UI_AbstractMidi_DebugLine_NoteChange    m_noteDisplay;
   public UI_AbstractMidi_DebugLine_PatchChange   m_patchDisplay;
   public UI_AbstractMidi_DebugLine_ShortChange   m_shortDisplay;


    public void PushIn(IMidiNoteEventGet value)
    {
        DisableAll();
        m_noteDisplay.gameObject.SetActive(true);
        m_noteDisplay.PushIn(value);
    }


    public void PushIn(IMidiPitchChangeEventGet value)
    {
        DisableAll();
        m_pitchDisplay.gameObject.SetActive(true);
        m_pitchDisplay.PushIn(value);

    }
    public void PushIn(IMidiControlChangeEventGet value)
    {

        DisableAll();
        m_controlDisplay.gameObject.SetActive(true);
        m_controlDisplay.PushIn(value);
    }
    public void PushIn(IMidiPatchChangeEventGet value)
    {

        DisableAll();
        m_patchDisplay.gameObject.SetActive(true);
        m_patchDisplay.PushIn(value);
    }
    public void PushIn(IMidiShortWithSourceGet value)
    {
        DisableAll();
        m_shortDisplay.gameObject.SetActive(true);
        m_shortDisplay.PushIn(value);

    }
    private void DisableAll()
    {
        m_pitchDisplay.gameObject.SetActive(false);
        m_controlDisplay.gameObject.SetActive(false);
        m_noteDisplay.gameObject.SetActive(false);
        m_patchDisplay.gameObject.SetActive(false);
        m_shortDisplay.gameObject.SetActive(false);
    }
}



public interface IAbstractMidiGetHandler {
   void PushIn(IMidiNoteEventGet value);
   void PushIn(IMidiPitchChangeEventGet value);
   void PushIn(IMidiControlChangeEventGet value);
   void PushIn(IMidiPatchChangeEventGet value);
   void PushIn(IMidiShortWithSourceGet value);

}