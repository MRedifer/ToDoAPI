//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDoAPI.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ToDoItem
    {
        public int Todoid { get; set; }
        public string Action { get; set; }
        public bool Done { get; set; }
        public Nullable<int> Categoryid { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
