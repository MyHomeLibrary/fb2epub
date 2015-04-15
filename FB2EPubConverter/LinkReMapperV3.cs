﻿using System.Collections.Generic;
using ConverterContracts.ConversionElementsStyles;
using EPubLibrary.ReferenceUtils;
using EPubLibrary.V3ePubType;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using FB2EPubConverter.PrepearedHTMLFiles;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter
{
    internal class LinkReMapperV3
    {
        private readonly string _idString;
        private readonly HTMLItem _linkTargetItem;
        private readonly BaseXHTMLFileV3 _linkTargetDocument;
        private readonly IHTMLItem _linkParentContainer;
        private readonly KeyValuePair<string, List<Anchor>> _link;
        private readonly BookStructureManager _structureManager;

        private int _linksCount;

        public LinkReMapperV3(KeyValuePair<string, List<Anchor>> link, IDictionary<string, HTMLItem> ids, BookStructureManager structureManager)
        {
            _link = link;
            _structureManager = structureManager;
            _idString = ReferencesUtils.GetIdFromLink(link.Key); // Get ID of a link target;
            _linkTargetItem = ids[_idString]; // get object targeted by link
            _linkTargetDocument = GetIDParentDocument(structureManager, _linkTargetItem); // get parent document (file) containing targeted object
            if (_linkTargetDocument != null) // if link target container document (document containing item with ID we jump to) found 
            {
                _linkParentContainer = DetectItemParentContainer(_linkTargetItem); // get parent container of link target item
            }
        }

        public void Remap()
        {
            if (_linkTargetDocument == null)
            {
                Logger.Log.Error(string.Format("Internal consistency error - Used ID ({0}) has to be in one of the book documents objects", _linkTargetItem));
                return;
            }
            if (_linkParentContainer == null) // if no parent container found , means the link is directly to document , which can't be , so we ignore
            {
                Logger.Log.Error(string.Format("Internal consistency error - target link item ( {0} )has no parent container", _linkTargetItem));
                return;
            }
            if (_linkTargetDocument is FB2NotesPageSectionFile) // if it's FBE notes section
            {
                RemapLinkSecionReference();
            }
            else
            {
                RemapNormalReference();
            }
        }

        private void RemapNormalReference()
        {
            foreach (var anchor in _link.Value)
            {
                BaseXHTMLFileV3 anchorDocument = GetIDParentDocument(_structureManager, anchor); // get document containing anchor pointing to target ID
                if (anchorDocument == null) // if anchor not contained (not found) in any document
                {
                    Logger.Log.Error(string.Format("Internal consistency error - anchor ({0}) for id ({1}) not contained (not found) in any document", anchor, _linkTargetItem));
                    continue;
                }
                // if in same document - local reference (link) , if in different - create link to that document
                anchor.HRef.Value = (anchorDocument == _linkTargetDocument) ? GenerateLocalLinkReference(_idString) : GenerateFarLinkReference(_idString, _linkTargetDocument.FileName);
            }                   
        }

        private void RemapLinkSecionReference()
        {
            if (_structureManager.DoNotAddFootnotes)
            {
                RemepLinkSectionV2Style();
            }
            else
            {
                GenerateFootnotes();
            }
        }

        private void GenerateFootnotes()
        {
            foreach (var anchor in _link.Value)
            {
                BaseXHTMLFileV3 anchorDocument = GetIDParentDocument(_structureManager, anchor); // get document containing anchor pointing to target ID
                if (anchorDocument == null) // if anchor not contained (not found) in any document
                {
                    Logger.Log.Error(string.Format("Internal consistency error - anchor ({0}) for id ({1}) not contained (not found) in any document", anchor, _linkTargetItem));
                    continue;
                }
                anchor.HRef.Value = GenerateLocalLinkReference(_idString);// update reference link for an anchor, local one (without file name)
                EPubV3VocabularyStyles linkStyles = new EPubV3VocabularyStyles();
                linkStyles.SetType(EpubV3Vocabulary.NoteRef);
                anchor.CustomAttributes.Add(linkStyles.GetAsCustomAttribute());
                anchorDocument.AddFootNote(_linkTargetItem,_idString);
            }
            _linkTargetItem.GlobalAttributes.ID.Value = null; // remove attribute from the item itself to avoid double IDs
        }

        private void RemepLinkSectionV2Style()
        {
            foreach (var anchor in _link.Value)
            {
                BaseXHTMLFileV3 anchorDocument = GetIDParentDocument(_structureManager, anchor); // get document containing anchor pointing to target ID
                if (anchorDocument == null) // if anchor not contained (not found) in any document
                {
                    Logger.Log.Error(string.Format("Internal consistency error - anchor ({0}) for id ({1}) not contained (not found) in any document", anchor, _linkTargetItem));
                    continue;
                }
                string backlinkRef;
                if (anchorDocument == _linkTargetDocument) // if anchor (link) and target (id) located in same document
                {
                    anchor.HRef.Value = GenerateLocalLinkReference(_idString);// update reference link for an anchor, local one (without file name)
                    backlinkRef = GenerateLocalLinkReference(anchor.GlobalAttributes.ID.Value as string); // in case we going to insert backlin - create a local reference
                }
                else // if they are located in different documents
                {
                    anchor.HRef.Value = GenerateFarLinkReference(_idString, _linkTargetDocument.FileName); // update reference link for an anchor, "far" one (with, pointing to another file name)
                    backlinkRef = GenerateFarLinkReference(anchor.GlobalAttributes.ID.Value as string, anchorDocument.FileName); // in case we going to insert backlin - create a "far" reference
                }
                var backLinkAnchor = new Anchor(_linkParentContainer.HTMLStandard);
                backLinkAnchor.HRef.Value = backlinkRef;
                backLinkAnchor.GlobalAttributes.Class.Value = ElementStylesV3.NoteAnchor;
                _linkParentContainer.Add(new EmptyLine(_linkParentContainer.HTMLStandard));
                _linkParentContainer.Add(backLinkAnchor);
                _linksCount++;
                backLinkAnchor.Add(new SimpleHTML5Text(backLinkAnchor.HTMLStandard) { Text = (_link.Value.Count > 1) ? string.Format("(<< back {0})  ", _linksCount) : string.Format("(<< back)  ") });
            }
        }

        private string GenerateLocalLinkReference(string idToReference)
        {
            return string.Format("#{0}", idToReference);
        }

        private string GenerateFarLinkReference(string idToReference, string fileName)
        {
            return string.Format("{0}#{1}", fileName, idToReference);
        }



        private BaseXHTMLFileV3 GetIDParentDocument(BookStructureManager structureManager, IHTMLItem value)
        {
            return structureManager.GetIDOfParentDocument(value) as BaseXHTMLFileV3;
        }

        /// <summary>
        /// Detect parent container of the element
        /// </summary>
        /// <param name="referencedItem"></param>
        /// <returns></returns>
        private IHTMLItem DetectItemParentContainer(IHTMLItem referencedItem)
        {
            if (referencedItem is IBlockElement) // if item itself is container - return it
            {
                return referencedItem;
            }
            if (referencedItem.Parent != null)
            {
                if (referencedItem.Parent is IBlockElement) // if item is located inside container
                {
                    return referencedItem.Parent;
                }
                if (referencedItem.Parent is TextBasedElement) // if parent is text, i's ok for container
                {
                    return referencedItem.Parent;
                }
                return DetectItemParentContainer(referencedItem.Parent); // go up the inclusion chain
            }

            return null;
        }


    }
}
