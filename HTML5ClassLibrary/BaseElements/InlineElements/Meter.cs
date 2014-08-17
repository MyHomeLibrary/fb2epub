﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "meter" tag defines a scalar measurement within a known range, or a fractional value. This is also known as a gauge.
    /// Examples: Disk usage, the relevance of a query result, etc.
    /// Note: The "meter" tag should not be used to indicate progress (as in a progress bar). For progress bars, use the "progress" tag.
    ///</summary>
    [HTMLItemAttribute(ElementName = "meter", SupportedStandards = HTMLElementType.HTML5 )]
    public class Meter : HTMLItem, IInlineItem
    {
        public Meter()
        {
            RegisterAttribute(_formIdAttribute);
            RegisterAttribute(_highAttribute);
            RegisterAttribute(_lowAttribute);
            RegisterAttribute(_maxAttribute);
            RegisterAttribute(_minAttribute);
            RegisterAttribute(_openAttribute);
            RegisterAttribute(_meterValueAttribute);

            TextContent = new SimpleHTML5Text();
        }

        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly HighAttribute _highAttribute = new HighAttribute();
        private readonly LowAttribute _lowAttribute = new LowAttribute();
        private readonly MaxAttribute _maxAttribute = new MaxAttribute();
        private readonly MinAttribute _minAttribute = new MinAttribute();
        private readonly OptimumAttribute _openAttribute = new OptimumAttribute();
        private readonly MeterValueAttribute _meterValueAttribute = new MeterValueAttribute(); 


        /// <summary>
        /// Specifies one or more forms the "meter" element belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies the range that is considered to be a high value
        /// </summary>
        public HighAttribute High { get { return _highAttribute; }}

        /// <summary>
        /// Specifies the range that is considered to be a low value
        /// </summary>
        public LowAttribute Low { get { return _lowAttribute; }}

        /// <summary>
        /// Specifies the maximum value of the range
        /// </summary>
        public MaxAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies the minimum value of the range
        /// </summary>
        public MinAttribute Min { get { return _minAttribute; }}

        /// <summary>
        /// Specifies what value is the optimal value for the gauge
        /// </summary>
        public OptimumAttribute Optimum { get { return _openAttribute; }}

        /// <summary>
        /// Required. Specifies the current value of the gauge
        /// </summary>
        public MeterValueAttribute Value { get { return _meterValueAttribute; }}


        public override bool IsValid()
        {
            return _meterValueAttribute.HasValue();
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
