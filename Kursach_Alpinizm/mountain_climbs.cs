//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kursach_Alpinizm
{
    using System;
    using System.Collections.Generic;
    
    public partial class mountain_climbs
    {
        public int ID_mountain_climbs { get; set; }
        public Nullable<int> ID_groups { get; set; }
        public Nullable<int> ID_mountain { get; set; }
        public Nullable<int> ID_category { get; set; }
        public Nullable<System.DateTime> Start_date_ { get; set; }
        public Nullable<System.DateTime> End_date_ { get; set; }
        public string Total { get; set; }
    
        public virtual category_of_difficulty category_of_difficulty { get; set; }
        public virtual groups groups { get; set; }
        public virtual mountain mountain { get; set; }
    }
}
