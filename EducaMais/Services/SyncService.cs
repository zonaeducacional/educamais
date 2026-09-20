using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EducaMais.Models;

namespace EducaMais.Services
{
    public class SyncService
    {
        private static SyncService? _instance;
        public static SyncService Instance => _instance ??= new SyncService();

        private bool _isSyncing = false;

        private SyncService() { }

        public async Task SyncPendingDataAsync()
        {
            if (_isSyncing) return;
            _isSyncing = true;

            try
            {
                // Ping Appwrite (simple check if online)
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var ping = await client.GetAsync($"{AppwriteService.Endpoint}/health");
                if (!ping.IsSuccessStatusCode) throw new Exception("Offline");

                var db = LocalDbService.Instance.GetConnection();

                // 1. Sync Attendances
                var pendingAttendances = await db.Table<Attendance>().Where(a => a.SyncStatus == "pending").ToListAsync();
                foreach (var att in pendingAttendances)
                {
                    try
                    {
                        var data = new Dictionary<string, object>
                        {
                            { "student_id", att.StudentId },
                            { "class_id", att.ClassId },
                            { "teacher_id", att.TeacherId },
                            { "discipline", att.Discipline },
                            { "date", att.Date },
                            { "is_present", att.IsPresent },
                            { "is_justified", att.IsJustified },
                            { "lesson_topic", att.LessonTopic ?? "" }
                        };
                        // We use a predefined UUID or let Appwrite generate? We already have `att.Id` generated locally.
                        // If `att.Id` starts with "local_", we can pass "unique()" to Appwrite.
                        string docId = att.Id.StartsWith("local_") ? "unique()" : att.Id;
                        
                        if (docId == "unique()")
                        {
                            await AppwriteService.Instance.RawCreateDocumentAsync("attendances", data, new List<string> { "read(\"any\")", $"update(\"user:{att.TeacherId}\")", $"delete(\"user:{att.TeacherId}\")" });
                            // Update local DB with new ID? For simplicity, we just mark as synced and don't care about updating local ID, 
                            // OR we can just delete the local and wait for next Pull. 
                            // Actually, let's just mark it "synced" so we don't resend it.
                            att.SyncStatus = "synced";
                            await db.UpdateAsync(att);
                        }
                        else
                        {
                            await AppwriteService.Instance.RawUpdateDocumentAsync("attendances", att.Id, data);
                            att.SyncStatus = "synced";
                            await db.UpdateAsync(att);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error syncing attendance {att.Id}: {ex.Message}");
                    }
                }

                // 2. Sync Grades
                var pendingGrades = await db.Table<Grade>().Where(g => g.SyncStatus == "pending").ToListAsync();
                foreach (var g in pendingGrades)
                {
                    try
                    {
                        var data = new Dictionary<string, object>
                        {
                            { "student_id", g.StudentId },
                            { "class_id", g.ClassId },
                            { "teacher_id", g.TeacherId },
                            { "discipline", g.Discipline },
                            { "unit", g.Unit },
                            { "value", g.Value },
                            { "a1", g.A1 },
                            { "a2", g.A2 },
                            { "a3", g.A3 },
                            { "a4", g.A4 },
                            { "passing_grade", g.PassingGrade },
                            { "grade_model", g.GradeModel }
                        };
                        string docId = g.Id.StartsWith("local_") ? "unique()" : g.Id;
                        if (docId == "unique()")
                        {
                            await AppwriteService.Instance.RawCreateDocumentAsync("grades", data, new List<string> { "read(\"any\")", $"update(\"user:{g.TeacherId}\")", $"delete(\"user:{g.TeacherId}\")" });
                        }
                        else
                        {
                            await AppwriteService.Instance.RawUpdateDocumentAsync("grades", g.Id, data);
                        }
                        g.SyncStatus = "synced";
                        await db.UpdateAsync(g);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error syncing grade {g.Id}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sync Service: Currently offline or error: {ex.Message}");
            }
            finally
            {
                _isSyncing = false;
            }
        }
    }
}
