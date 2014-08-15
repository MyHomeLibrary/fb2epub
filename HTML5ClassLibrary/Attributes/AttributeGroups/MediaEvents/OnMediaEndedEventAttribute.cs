﻿using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the media has reach the end (a useful event for messages like "thanks for listening")
    /// </summary>
    public class OnMediaEndedEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onended";
        }

        #endregion
    }
}
