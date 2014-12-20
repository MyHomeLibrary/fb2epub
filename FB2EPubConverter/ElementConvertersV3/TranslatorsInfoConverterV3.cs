﻿using EPubLibrary;
using Fb2epubSettings;
using FB2Library.HeaderItems;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal static class TranslatorsInfoConverterV3
    {
        public static void Convert(ItemTitleInfo titleInfo, EPubFileV3 epubFile, EPubCommonSettings settings)
        {
            foreach (var translator in titleInfo.Translators)
            {
                var person = new PersoneWithRole
                {
                    PersonName = epubFile.Transliterator.Translate(DescriptionConverters.GenerateAuthorString(translator, settings),
                        epubFile.TranslitMode),
                    FileAs = DescriptionConverters.GenerateFileAsString(translator, settings),
                    Role = RolesEnum.Translator,
                    Language = titleInfo.Language
                };
                epubFile.Title.Contributors.Add(person);
            }
        }

    }
}
