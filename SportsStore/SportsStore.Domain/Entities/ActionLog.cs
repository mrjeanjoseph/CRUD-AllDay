﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities {
    public class ActionLog {

        [Key]
        public int LogId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string HttpMethod { get; set; }
        public string URL { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
