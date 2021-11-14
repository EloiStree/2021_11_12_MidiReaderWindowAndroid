using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbstractMidi_DebugLineHistory : MonoBehaviour, IAbstractMidiGetHandler
{


    public UI_AbstractMidi_DebugLine[] m_lines;





    public void PushIn(IMidiNoteEventGet value)
    {

    }
    public void PushIn(IMidiPitchChangeEventGet value)
    {

    }
    public void PushIn(IMidiControlChangeEventGet value)
    {

    }
    public void PushIn(IMidiPatchChangeEventGet value)
    {

    }
    public void PushIn(IMidiShortWithSourceGet value)
    {
    }




}
