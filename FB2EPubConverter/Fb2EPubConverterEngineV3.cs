﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ConverterContracts.Settings;
using EPubLibrary;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using Fb2ePubConverter;
using FB2EPubConverter.ElementConvertersV3;
using FB2Library;
using FB2Library.HeaderItems;
using TranslitRu;
using XHTMLClassLibrary.BaseElements.BlockElements;
using EPubLibraryContracts;
using FB2EPubConverter.PrepearedHTMLFiles;


namespace FB2EPubConverter
{
    internal class Fb2EPubConverterEngineV3 : Fb2EPubConverterEngineBase
    {
        private readonly  HRefManagerV3 _referencesManager = new HRefManagerV3();

        private const string DefaultCSSFileName = "default_v3.css";

        protected override void ConvertContent(FB2File fb2File, IEpubFile epubFile)
        {
            var epubFileV3 = epubFile as EPubFileV3;
            if (epubFileV3 == null)
            {
                throw new ArrayTypeMismatchException(string.Format("Invalid ePub object type passed, expected EPubFileV3, got {0}",epubFile.GetType()));
            }
            _referencesManager.FlatStructure = Settings.CommonSettings.FlatStructure;
            _referencesManager.DoNotAddFootnotes = Settings.V3Settings.DoNotUseFootnotes;
            PassHeaderDataFromFb2ToEpub(fb2File,epubFile.BookInformation);
            var titlePage = new TitlePageFileV3(epubFileV3.BookInformation);
            StructureManager.AddTitlePage(titlePage);
            PassCoverImageFromFB2(fb2File.TitleInfo.Cover, epubFileV3);
            ConvertAnnotation(fb2File.TitleInfo, epubFileV3);
            SetupCSS(epubFileV3);
            SetupFonts(epubFileV3);
            PassTextFromFb2ToEpub(fb2File);
            PassFb2InfoToEpub(fb2File);
            PassImagesDataFromFb2ToEpub(epubFileV3, fb2File);
            AddAboutInformation(epubFileV3);
            UpdateInternalLinks(fb2File);
        }

        private void SetupCSS(EPubFileV3 epubFile)
        {
            Assembly asm = Assembly.GetAssembly(GetType());
            string pathPreffix = Path.GetDirectoryName(asm.Location);
            if (!string.IsNullOrEmpty(Settings.ResourcesPath))
            {
                pathPreffix = Settings.ResourcesPath;
            }
            epubFile.CSSFiles.Add(new CSSFile { FilePathOnDisk = string.Format(@"{0}\CSS\{1}", pathPreffix, DefaultCSSFileName), FileName = DefaultCSSFileName });
        }


        private void SetupFonts(EPubFileV3 epubFile)
        {
            if (Settings.ConversionSettings.Fonts == null)
            {
                Logger.Log.Warn("No fonts defined in configuration file.");
                return;
            }
            epubFile.SetEPubFonts(Settings.ConversionSettings.Fonts, Settings.ResourcesPath, Settings.ConversionSettings.DecorateFontNames);
        }

        private void AddAboutInformation(EPubFileV3 epubFile)
        {
            Assembly asm = Assembly.GetAssembly(GetType());
            string version = "???";
            if (asm != null)
            {
                version = asm.GetName().Version.ToString();
            }
            epubFile.CreatorSoftwareString = string.Format(@"Fb2epub v{0} [http://www.fb2epub.net]", version);

            if (!Settings.ConversionSettings.SkipAboutPage)
            {
                var aboutPage = new AboutPageFileV3()
                {
                    FlatStructure = Settings.CommonSettings.FlatStructure,
                    EmbedStyles = Settings.CommonSettings.EmbedStyles,
                    AboutLinks = new List<string>(),
                    AboutTexts = new List<string>()
                };
                aboutPage.AboutTexts.Add(
                    string.Format("This file was generated by Lord KiRon's FB2EPUB converter version {0}.",
                                  version));
                aboutPage.AboutTexts.Add("(This book might contain copyrighted material, author of the converter bears no responsibility for it's usage)");
                aboutPage.AboutTexts.Add(
                    string.Format("Этот файл создан при помощи конвертера FB2EPUB версии {0} написанного Lord KiRon.",
                        version));
                aboutPage.AboutTexts.Add("(Эта книга может содержать материал который защищен авторским правом, автор конвертера не несет ответственности за его использование)");
                aboutPage.AboutLinks.Add(@"http://www.fb2epub.net");
                aboutPage.AboutLinks.Add(@"https://code.google.com/p/fb2epub/");
                StructureManager.AddAboutPage(aboutPage);
                StructureManager.AddAboutPage(CreateLicenseFile());
            }
        }

        private IBaseXHTMLFile CreateLicenseFile()
        {
            var licensePage = new LicenseFileV3()
            {
                FlatStructure = Settings.CommonSettings.FlatStructure,
                EmbedStyles = Settings.CommonSettings.EmbedStyles,
            };
            return licensePage;
        }

        private void PassImagesDataFromFb2ToEpub(EPubFileV3 epubFile, FB2File fb2File)
        {
            Images.ConvertFb2ToEpubImages(fb2File.Images, epubFile.Images);
        }


        private void UpdateInternalLinks(FB2File fb2File)
        {
            _referencesManager.RemoveInvalidAnchors();
            _referencesManager.RemoveInvalidImages(fb2File.Images);
            _referencesManager.RemapAnchors(StructureManager);
        }


        private void PassTextFromFb2ToEpub(FB2File fb2File)
        {
            var converter = new Fb2EPubTextConverterV3(Settings.CommonSettings, Images, _referencesManager, Settings.V3Settings.HTMLFileMaxSize);
            converter.Convert(StructureManager, fb2File);
        }

        /// <summary>
        /// Passes FB2 info to the EPub file to be added at the end of the book
        /// </summary>
        /// <param name="epubFile">destination epub object</param>
        /// <param name="fb2File">source fb2 object</param>
        private void PassFb2InfoToEpub(FB2File fb2File)
        {
            if (!Settings.ConversionSettings.Fb2Info)
            {
                return;
            }
            var infoDocument = new BaseXHTMLFileV3
            {
                PageTitle = "FB2 Info",
                FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder),
                FileName = "fb2info.xhtml",
                GuideRole = GuideTypeEnum.Notes,
                NotPartOfNavigation = true
            };

            var converterSettings = new ConverterOptionsV3
            {
                CapitalDrop = false,
                Images = Images,
                MaxSize = Settings.V3Settings.HTMLFileMaxSize,
                ReferencesManager = _referencesManager,
            };
            var infoConverter = new Fb2EpubInfoConverterV3();
            infoDocument.Content = infoConverter.Convert(fb2File, converterSettings);

            StructureManager.AddBookPage(infoDocument);
        }

        private void PassHeaderDataFromFb2ToEpub(FB2File fb2File,IBookInformationData titleInformation)
        {
            Logger.Log.Debug("Passing header data from FB2 to EPUB");

            if (fb2File.MainBody == null)
            {
                throw new NullReferenceException("MainBody section of the file passed is null");
            }
            var headerDataConverter = new HeaderDataConverterV3(Settings.ConversionSettings,Settings.V3Settings);
            headerDataConverter.Convert(fb2File, titleInformation);
        }

        private void PassCoverImageFromFB2(CoverPage coverPage, EPubFileV3 epubFile)
        {
            // if we have at least one coverpage image
            if ((coverPage != null) && (coverPage.HasImages()) && (coverPage.CoverpageImages[0].HRef != null))
            {
                var coverPageFile = new CoverPageFileV3(coverPage.CoverpageImages[0], _referencesManager);
                StructureManager.AddCoverPage(coverPageFile);
                Images.ImageIdUsed(coverPage.CoverpageImages[0].HRef);
                epubFile.SetCoverImageID(coverPage.CoverpageImages[0].HRef);
            }
        }

        private void PassPublisherInfoFromFB2(FB2File fb2File, IBookInformationData titleInformation, IEPubConversionSettings settings)
        {
            if (fb2File.PublishInfo.BookTitle != null)
            {
                var bookTitle = new Title
                {
                    TitleName =
                        Rus2Lat.Instance.Translate(fb2File.PublishInfo.BookTitle.Text, settings.TransliterationSettings),
                    Language =
                        !string.IsNullOrEmpty(fb2File.PublishInfo.BookTitle.Language)
                            ? fb2File.PublishInfo.BookTitle.Language
                            : fb2File.TitleInfo.Language
                };
                if ((Settings.ConversionSettings.IgnoreTitle != IgnoreInfoSourceOptions.IgnorePublishTitle) && (Settings.ConversionSettings.IgnoreTitle != IgnoreInfoSourceOptions.IgnoreMainAndPublish) &&
                    Settings.ConversionSettings.IgnoreTitle != IgnoreInfoSourceOptions.IgnoreSourceAndPublish)
                {
                    bookTitle.TitleType = TitleType.PublishInfo;
                    titleInformation.BookTitles.Add(bookTitle);
                }
            }


            if (fb2File.PublishInfo.ISBN != null)
            {
                var bookId = new Identifier
                {
                    IdentifierName = "BookISBN",
                    ID = fb2File.PublishInfo.ISBN.Text,
                    Scheme = "ISBN"
                };
                titleInformation.Identifiers.Add(bookId);
            }


            if (fb2File.PublishInfo.Publisher != null)
            {
                titleInformation.Publisher.PublisherName = Rus2Lat.Instance.Translate(fb2File.PublishInfo.Publisher.Text, settings.TransliterationSettings);
            }


            try
            {
                if (fb2File.PublishInfo.Year.HasValue)
                {
                    var date = new DateTime(fb2File.PublishInfo.Year.Value, 1, 1);
                    titleInformation.DatePublished = date;
                }
            }
            catch (FormatException ex)
            {
                Logger.Log.DebugFormat("Date reading format exception: {0}", ex);
            }
            catch (Exception exAll)
            {
                Logger.Log.ErrorFormat("Date reading exception: {0}", exAll);
            }
        }


        private void ConvertAnnotation(ItemTitleInfo titleInfo, EPubFileV3 epubFile)
        {
            if (titleInfo.Annotation != null)
            {
                epubFile.BookInformation.Description = new Description {DescInfo = titleInfo.Annotation.ToString()};
                var converterSettings = new ConverterOptionsV3
                {
                    CapitalDrop = Settings.CommonSettings.CapitalDrop,
                    Images = Images,
                    MaxSize = Settings.V3Settings.HTMLFileMaxSize,
                    ReferencesManager = _referencesManager,
                };
                var annotationConverter = new AnnotationConverterV3();
                var annotationPage = new AnnotationPageFileV3
                {
                    BookAnnotation = (Div)annotationConverter.Convert(titleInfo.Annotation,
                        new AnnotationConverterParamsV3 { Settings = converterSettings, Level = 1 })
                };
                StructureManager.AddAnnotationPage(annotationPage);
            }
        }


        //private void PassSeriesData(FB2File fb2File, EPubFileV3 epubFile)
        //{
        //    epubFile.Collections.CollectionMembers.Clear();
        //    foreach (var seq in fb2File.TitleInfo.Sequences)
        //    {
        //        if (!string.IsNullOrEmpty(seq.Name))
        //        {
        //            var collectionMember = new CollectionMember
        //            {
        //                CollectionName = seq.Name,
        //                Type = CollectionType.Series,
        //                CollectionPosition = seq.Number
        //            };
        //            epubFile.Collections.CollectionMembers.Add(collectionMember);
        //            foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
        //            {
        //                collectionMember = new CollectionMember
        //                {
        //                    CollectionName = subseq.Name,
        //                    Type = CollectionType.Set,
        //                    CollectionPosition = subseq.Number
        //                };
        //                epubFile.Collections.CollectionMembers.Add(collectionMember);
        //            }
        //        }
        //    }
        //}

        protected override IEpubFile CreateEpub()
        {
            return new EPubFileV3(Settings.CommonSettings, Settings.V3Settings);
        }
    }
}
