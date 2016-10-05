using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LifeLink.Models
{
    public class Questionnaire
    {
        [Key]
        public int QuestionnaireId { get; set;}

        public bool GeneralHealth1 { get; set; }

        public bool DonationHistory2 { get; set; }

        public bool VaxOrShots3 { get; set; }

        public bool Pregnant4 { get; set; }

        public bool Medications5 { get; set; }

        public bool Weight6 { get; set; }

        public bool RiskySex7 { get; set; }

        public bool TatooOrPiercing8 { get; set; }

        public bool Jail9 { get; set; }

        public bool Needles10 { get; set; }

        [ForeignKey("ClientInfo")]
        public string ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        
    }
}