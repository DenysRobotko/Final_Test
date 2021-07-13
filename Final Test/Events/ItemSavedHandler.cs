using Sitecore.Diagnostics;
using System;

namespace FinalTest.Events
{
    public class ItemSavedHandler
    {
        public void OnItemSaved(object sender, EventArgs args)
        {
            Log.Info("The ItemSavedEvent was catched. Item was saved", this);
        }
    }
}