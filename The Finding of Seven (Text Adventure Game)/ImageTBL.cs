//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageTBL
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string ImageName { get; set; }
        public int PageTBLId { get; set; }
    
        public virtual PageTBL PageTBL { get; set; }
    }
}
