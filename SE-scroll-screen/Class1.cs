using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;

public sealed class Program : MyGridProgram
{
    // НАЧАЛО СКРИПТА
    static Program myScript;
    scrollScreen scrScreen;

    public Program()

    {
        myScript = this;
        scrScreen = new scrollScreen("[DEBUG]");
        Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    public void Save()

    {
    }

    public void Main(string argument, UpdateType updateSource)

    {
        scrScreen.incomText = Me.CustomData;
        scrScreen.WriteTextOnScreen();
    }

    class scrollScreen
    {
        IMyTextPanel TPanel;
        public string incomText;
        public byte UpperLine { get; private set; }
        public byte visibleLines;

        public scrollScreen(string nameLCD, byte lineNumbers = 17)
        {
            TPanel = (IMyTextPanel)myScript.GridTerminalSystem.GetBlockWithName(nameLCD);
            if (TPanel == null) myScript.Echo("text panel \"" + nameLCD + "\" not found");
            UpperLine = 0;
            visibleLines = lineNumbers;
        }

        public void WriteTextOnScreen()
        {
            if (TPanel == null) myScript.Echo("text panel not found");

            string[] incomTextArray = incomText.Split('\n');
            TPanel.WritePublicText("");

            if ((UpperLine + visibleLines) > incomTextArray.GetLength(0)) UpperLine = 0;

            for (int i = UpperLine; i < (visibleLines + UpperLine) & i < incomTextArray.GetLength(0); i++)
            {
                TPanel.WritePublicText(incomTextArray[i] + "\n", true);
            }

            UpperLine++;

        }
    }
    // КОНЕЦ СКРИПТА
}