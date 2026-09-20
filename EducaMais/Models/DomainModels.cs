using SQLite;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace EducaMais.Models;

public record UserProfile(
    string Id,
    string Name,
    string Role, // superadmin, municipal, school_admin, teacher
    string MunicipalityId,
    string SchoolId
);

public record Municipality(
    string Id,
    string Name,
    string SecretaryName,
    string Phone,
    string Email
);

public class School
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Inep { get; set; } = string.Empty;
    public string Principal { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public string Status { get; set; } = "Ativa";
    public bool IsActive => Status == "Ativa";
}







    public class Director : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [System.Text.Json.Serialization.JsonPropertyName("$id")]
        public string Id { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("school_id")]
        public string SchoolId { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("tenant_id")]
        public string TenantId { get; set; } = string.Empty;

        // Propriedade auxiliar para a UI, ignorada no JSON pro Appwrite
        [System.Text.Json.Serialization.JsonIgnore]
        public string SchoolName { get; set; } = string.Empty;
    }

public class Teacher
{
    [JsonPropertyName("$id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("school_id")]
    public string SchoolId { get; set; } = string.Empty;

    [JsonPropertyName("subjects")]
    public List<string> Subjects { get; set; } = new();
    [JsonPropertyName("class_ids")]
    public List<string> ClassIds { get; set; } = new();
}

public class SchoolClass
{
    [SQLite.Column("sync_status")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string SyncStatus { get; set; } = "synced";

    [PrimaryKey]
    [JsonPropertyName("$id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("shift")]
    public string Shift { get; set; } = string.Empty;

    [JsonPropertyName("school_id")]
    public string SchoolId { get; set; } = string.Empty;
}

public class Student
{
    [SQLite.Column("sync_status")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string SyncStatus { get; set; } = "synced";

    [PrimaryKey]
    [JsonPropertyName("$id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("registration")]
    public string Registration { get; set; } = string.Empty;

    [JsonPropertyName("class_id")]
    public string ClassId { get; set; } = string.Empty;

    [JsonPropertyName("school_id")]
    public string SchoolId { get; set; } = string.Empty;
}

public class Attendance
{
    [SQLite.Column("sync_status")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string SyncStatus { get; set; } = "synced";

    [PrimaryKey]
    [JsonPropertyName("$id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("student_id")]
    public string StudentId { get; set; } = string.Empty;

    [JsonPropertyName("class_id")]
    public string ClassId { get; set; } = string.Empty;

    [JsonPropertyName("teacher_id")]
    public string TeacherId { get; set; } = string.Empty;

    [JsonPropertyName("discipline")]
    public string Discipline { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("is_present")]
    public bool IsPresent { get; set; }

    [JsonPropertyName("is_justified")]
    public bool IsJustified { get; set; }

    [JsonPropertyName("lesson_topic")]
    public string? LessonTopic { get; set; }
}

public class LessonPlan : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [SQLite.Column("sync_status")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string SyncStatus { get; set; } = "synced";

    [PrimaryKey]
    [JsonPropertyName("$id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("teacher_id")]
    public string TeacherId { get; set; } = string.Empty;

    [JsonPropertyName("class_id")]
    public string ClassId { get; set; } = string.Empty;

    [JsonPropertyName("discipline")]
    public string Discipline { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("plan_type")]
    public string PlanType { get; set; } = "Plano de Aula";

    [JsonPropertyName("attachment_url")]
    public string? AttachmentUrl { get; set; }
}

public class Grade
{
    [SQLite.Column("sync_status")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string SyncStatus { get; set; } = "synced";

    [PrimaryKey]
    [JsonPropertyName("$id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("student_id")]
    public string StudentId { get; set; } = string.Empty;

    [JsonPropertyName("class_id")]
    public string ClassId { get; set; } = string.Empty;

    [JsonPropertyName("teacher_id")]
    public string TeacherId { get; set; } = string.Empty;

    [JsonPropertyName("discipline")]
    public string Discipline { get; set; } = string.Empty;

    [JsonPropertyName("unit")]
    public int Unit { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }

    [JsonPropertyName("a1")]
    public double? A1 { get; set; }
    [JsonPropertyName("a2")]
    public double? A2 { get; set; }
    [JsonPropertyName("a3")]
    public double? A3 { get; set; }
    [JsonPropertyName("a4")]
    public double? A4 { get; set; }

    [JsonPropertyName("passing_grade")]
    public double PassingGrade { get; set; } = 5.0;

    [JsonPropertyName("grade_model")]
    public string GradeModel { get; set; } = "continuous";
}
