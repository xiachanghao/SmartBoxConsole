using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    /// <summary>
    /// 手风琴项元素
    /// </summary>
    public class AccordionItem
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the ico SRC.
        /// </summary>
        /// <value>The ico SRC.</value>
        public string IcoSrc { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }

        public int ID { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expand.
        /// </summary>
        /// <value><c>true</c> if this instance is expand; otherwise, <c>false</c>.</value>
        public bool IsExpand { get; set; }

        private List<AccordionItem> _child;
        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
        public List<AccordionItem> Children
        {
            get
            {
                if (_child == null)
                {
                    _child = new List<AccordionItem>();
                }
                return _child;
            }
        }

    }
    public class AccordionLeafItem
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the ico SRC.
        /// </summary>
        /// <value>The ico SRC.</value>
        public string IcoSrc { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }
        
        private List<AccordionLeafItem> _child;

        public List<AccordionLeafItem> Children
        {
            get
            {
                if (_child == null)
                {
                    _child = new List<AccordionLeafItem>();
                }
                return _child;
            }
        }
    }
}
