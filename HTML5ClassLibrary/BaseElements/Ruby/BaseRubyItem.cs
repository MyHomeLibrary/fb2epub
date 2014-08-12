﻿using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.BaseElements.Ruby
{
    public interface IRubyItem 
    {

    }

    public abstract class BaseRubyItem : HTML5Item , IRubyItem
    {
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

    }
}
