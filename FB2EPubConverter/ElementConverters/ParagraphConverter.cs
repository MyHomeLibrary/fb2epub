﻿using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    public enum ParagraphConvTargetEnum
    {
        Paragraph,
        H1,
        H2,
        H3,
        H4,
        H5,
        H6
    }

    internal class ParagraphConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="resultType">type of the resulting block container in EPUB</param>
        /// <returns></returns>
        public HTMLItem Convert(ParagraphItem paragraphItem,ParagraphConvTargetEnum resultType)
        {
            return Convert(paragraphItem,resultType, false);
        }

        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="resultType">type of the resulting block container in EPUB</param>
        /// <param name="startSection"> if this is a first paragraph in section</param>
        /// <returns></returns>
        public  HTMLItem Convert(ParagraphItem paragraphItem,ParagraphConvTargetEnum resultType, bool startSection)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var paragraph = CreateBlock(resultType);
            bool needToInsertDrop = Settings.CapitalDrop && startSection;

            foreach (var item in paragraphItem.ParagraphData)
            {
                if (item is SimpleText)
                {
                    var textConverter = new SimpleTextElementConverter {Settings = Settings};
                    foreach (var s in textConverter.Convert(item,needToInsertDrop))
                    {
                        if (needToInsertDrop)
                        {
                            needToInsertDrop = false;
                            paragraph.GlobalAttributes.Class.Value = "drop";
                        }
                        paragraph.Add(s);
                    }
                }
                else if (item is InlineImageItem)
                {
                    // if no image data - do not insert link
                    if (Settings.Images.HasRealImages())
                    {
                        var inlineItem = item as InlineImageItem;
                        if (Settings.Images.IsImageIdReal(inlineItem.HRef))
                        {
                            var inlineImageConverter = new InlineImageConverter {Settings = Settings};
                            paragraph.Add(inlineImageConverter.Convert(inlineItem));
                        }
                        Settings.Images.ImageIdUsed(inlineItem.HRef);
                        if (needToInsertDrop) // in case this is "drop" image - no need to create a drop
                        {
                            needToInsertDrop = false;
                        }
                    }
                }
                else if (item is InternalLinkItem)
                {
                    var internalLinkConverter = new InternalLinkConverter {Settings = Settings};
                    foreach (var s in internalLinkConverter.Convert(item as InternalLinkItem, needToInsertDrop))
                    {
                        if (needToInsertDrop)
                        {
                            needToInsertDrop = false;
                            paragraph.GlobalAttributes.Class.Value = "drop";
                        }
                        paragraph.Add(s);
                    }
                }
            }

            //SetClassType(paragraph);
            paragraph.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, paragraph);

            return paragraph;
        }

        /// <summary>
        /// Creates block element based on paragraph type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static HTMLItem CreateBlock(ParagraphConvTargetEnum type)
        {
            HTMLItem paragraph;
            switch (type)
            {
                case ParagraphConvTargetEnum.H1:
                    paragraph = new H1();
                    break;
                case ParagraphConvTargetEnum.H2:
                    paragraph = new H2();
                    break;
                case ParagraphConvTargetEnum.H3:
                    paragraph = new H3();
                    break;
                case ParagraphConvTargetEnum.H4:
                    paragraph = new H4();
                    break;
                case ParagraphConvTargetEnum.H5:
                    paragraph = new H5();
                    break;
                case ParagraphConvTargetEnum.H6:
                    paragraph = new H6();
                    break;
                default: // Paragraph or anything else
                    paragraph = new Paragraph();
                    break;

            }
            return paragraph;
        }

        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
