﻿using System;
using ConverterContracts.Settings;
using EPubLibraryContracts;
using FB2Library.HeaderItems;
using TranslitRu;


namespace FB2EPubConverter.ElementConvertersV2
{
    internal class TitleInfoConverterV2
    {
        private readonly IEPubConversionSettings _conversionSettings;

        public TitleInfoConverterV2(IEPubConversionSettings conversionSettings)
        {
            _conversionSettings = conversionSettings;
        }

        public void Convert(ItemTitleInfo itemTitleInfo, IBookInformationData titleInformation)
        {
            titleInformation.Languages.Clear();
            titleInformation.Creators.Clear();
            titleInformation.Contributors.Clear();
            titleInformation.Subjects.Clear();
            titleInformation.Identifiers.Clear();

            // in case main body title is not defined (empty) 
            if ((itemTitleInfo != null) && (itemTitleInfo.BookTitle != null))
            {
                titleInformation.BookMainTitle = itemTitleInfo.BookTitle.Text;
            }

            titleInformation.AllSequences.Clear();

            if (itemTitleInfo != null)
            {
                SequencesInfoConverterV2.Convert(itemTitleInfo, titleInformation);

                // Getting information from FB2 Title section
                ConvertMainTitle(itemTitleInfo, titleInformation);

                // add authors
                AuthorsInfoConverterV2.Convert(itemTitleInfo, _conversionSettings, titleInformation);

                // add translators
                TranslatorsInfoConverterV2.Convert(itemTitleInfo, titleInformation,_conversionSettings);

                // genres
                GenresInfoConverterV2.Convert(itemTitleInfo, titleInformation,_conversionSettings);

            }
            titleInformation.DateFileCreation = DateTime.Now;
        }


        private void ConvertMainTitle(ItemTitleInfo titleInfo, IBookInformationData titleInformation)
        {
            var bookTitle = new Title
            {
                TitleName = Rus2Lat.Instance.Translate(DescriptionConverters.FormatBookTitle(titleInfo, _conversionSettings),
                    _conversionSettings.TransliterationSettings),
                Language = string.IsNullOrEmpty(titleInfo.BookTitle.Language)
                    ? titleInfo.Language
                    : titleInfo.BookTitle.Language,
                TitleType = TitleType.Main
            };
            titleInformation.BookTitles.Add(bookTitle);
            // add main title language
            titleInformation.Languages.Add(titleInfo.Language);
        }

    }
}
