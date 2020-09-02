using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("parameters")]
    public class parameters
    {
        [DC.Key]
        public long parameters_id { get; set; }
        public string parameters_name { get; set; }
        public int parameters_valueint { get; set; }
        public string parameters_valuestring { get; set; }
        public double parameters_double { get; set; }
        public bool deletedparameters_id { get; set; }
        public bool parameters_active { get; set; }
        public bool parameters_use { get; set; }
    }

}
