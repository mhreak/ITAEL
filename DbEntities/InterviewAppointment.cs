using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("InterviewAppointment")]
    public class InterviewAppointment
    {
        [Key]
        public int InterviewAppointmentId { get; set; }

        [Required]
        public int JobAnnouncementId { get; set; }

        [ForeignKey("JobAnnouncementId")]
        public JobAnnouncement JobAnnouncement { get; set; }

        [Required]
        public int InterviewSlotId { get; set; }

        //1 -> Pending
        //2 -> Confirmed
        //3 -> Canceled
        //4 -> Completed
        [StringLength(50)]
        [Required]
        public short Status { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;

        public int? ApplicantExamAttemptId { get; set; }

        [ForeignKey("ApplicantExamAttemptId")]
        public ApplicantExamAttempt ApplicantExamAttempt { get; set; }
    }
}
