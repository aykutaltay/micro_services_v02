using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("params")]
    public class params
    {
        [DC.Key]
        public long params_params_id { get; set; }
        public string params_params_name { get; set; }
        public  params_params_valueint { get; set; }
        public string params_params_valuestring { get; set; }
        public  params_params_double { get; set; }
        public bool params_deletedparams_id { get; set; }
        public bool params_params_active { get; set; }
        public bool params_params_use { get; set; }
    }

}
