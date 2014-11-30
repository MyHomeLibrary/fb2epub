﻿using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    class PoemSubtitleConverterV2 : BaseElementConverterV2
    {

        public IHTMLItem Convert(SubTitleItem subtitleItem, SubtitleConverterParamsV2 subtitleConverterParams)
        {
            if (subtitleItem == null)
            {
                throw new ArgumentNullException("subtitleItem");
            }
            //Div subtitle = new Div();
            var paragraphConverter = new ParagraphConverterV2();
            var internalData = paragraphConverter.Convert(subtitleItem, 
                new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = subtitleConverterParams.Settings, StartSection = false });
            SetClassType(internalData, "poem_subtitle");
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }
    }
}
