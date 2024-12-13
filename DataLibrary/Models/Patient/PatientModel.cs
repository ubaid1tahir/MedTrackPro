
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using DataLibrary.Models.DoctorNamespace;

namespace DataLibrary.Models.Patient;

public class PatientModel
{
    [Key]
    public int PatientId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public int Age {  get; set; }

    public string? ProfilePhoto {  get; set; }

    public string? EmergencyPhoneNumber {  get; set; }

    public string? EmergencyRelationship {  get; set; }

    public string? EmergencyRelationName {  get; set; }

    public string? Allergies {  get; set; }
    public string? GuardenerName {  get; set; }

    public string PhoneNumber {  get; set; }

    public bool isMarried {  get; set; }

    public decimal? Weight {  get; set; }

    public decimal? Height { get; set; }

    public string? AdditionalNote {  get; set; }

    public DateOnly? BirthDate { get; set; }
    public int Gender; // 1 for male and 2 for female

    public int Severity {  get; set; } // 1-10

    public string UserId {  get; set; }

    public IdentityUser User { get; set; }
}

public class Medication
{
    [Key]
    public int MedicationId { get; set; }
    public string MedicationName { get; set; }
    public string Dosage {  get; set; } // 50 mg

    public string Frequency {  get; set; } // Twice a day

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int PatientId {  get; set; }

    public PatientModel Patient { get; set; }
}


public class MedicalHistory
{
    [Key]
    public int HistoryId { get; set; }
    public string Status { get; set; } // Chronic | Resolved
    
    public DateOnly DiagnosisDate { get; set; }

    public int PatientId { get; set; }

    public PatientModel Patient { get; set; }
}

public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string Reason {  get; set; } // Reason for visit

    public string Status { get; set; } // Completed | Cancelled
    
    public int PatientId { get; set; }
    public PatientModel Patient { get;set; }
}

public class LabResult
{
    [Key]
    public int LabResultId { get; set; }
    public string TestName {  get; set; } // Test name (e.g., "Blood Sugar Test")

    public string Result { get; set; } // Test result (e.g., "120 mg/dL")

    public string Unit {  get; set; } // Unit of measurement

    public string NormalRange {  get; set; }

    public DateOnly Date { get; set; }

    public string Notes { get; set; } // Additional Notes

    public int PatientId { get; set; }
    public PatientModel Patient { get; set; }
}


public class Vaccination
{
    [Key]
    public int VaccinationId { get; set; }

    public string VaccineName {  get; set; }

    public DateOnly DateAdministered { get; set; }

    public string Provider {  get; set; }

    public string LotNumber {  get; set; }

    public int PatientId { get; set; }
    public PatientModel Patient { get; set; }

}

public class VitalSign
{
    [Key]
    public int VitalSignId { get; set; }

    public DateOnly Date {  get; set; }

    public string BloodPressure {  get; set; } // Blood pressure (e.g., "120/80 mmHg")

    public int HeartRate {  get; set; } // Heart rate (e.g., 72 bpm)

    public float Temperature {  get; set; } // Body temperature (e.g., 98.6°F)

    public int RespiratoryRate {  get; set; } // Respiratory rate (e.g., 16 breaths/min)

    public float OxygenSaturation {  get; set; } // Oxygen saturation (e.g., 98%)

    public int PatientId { get; set; }
    public PatientModel Patient { get; set; }
}

public class Immunization
{
    [Key]
    public int ImmunizationId { get; set; }
    public string VaccineName { get; set; }
    public DateOnly DateAdministered { get; set; }
    public string Provider { get; set; }

    public int PatientId { get; set; }

    public PatientModel Patient { get;set; }
}

public class Document
{
    [Key]
    public int DocumentId { get; set; }

    public string DocumentType {  get; set; }

    public string FileUrl {  get; set; }

    public DateOnly UploadDate {  get; set; }

    public int PatientId { get; set; }

    public PatientModel Patient { get; set; }
}

public class Note
{
    [Key]
    public int NoteId { get; set; }

    public string Content {  get; set; }

    public string AuthorId {  get; set; }

    public IdentityUser Author { get; set; }

    public DateOnly DateAdded {  get; set; }
    public int PatientId { get; set; }

    public PatientModel Patient { get; set; }

}