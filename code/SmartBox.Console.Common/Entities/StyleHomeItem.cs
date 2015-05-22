using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Table("StyleHomeItem")]
    public class StyleHomeItem
    {
        [Column("StyleID")]
        public int StyleID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("App4AIID")]
        public int App4AIID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Image")]
        public string Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("DispalyName")]
        public string DispalyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Seq")]
        public int Seq { get; set; }
    }
}
