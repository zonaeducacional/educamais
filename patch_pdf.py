import re

with open('EducaMais/ViewModels/SchoolDashboardViewModel.cs', 'r') as f:
    content = f.read()

# Replace PrintReportCardAsync and the helper methods
pattern = r'\[RelayCommand\]\s+private async Task PrintReportCardAsync\(\).*?private void ComposeContent\(QuestPDF.*?}\n    }'

replacement = '''[RelayCommand]
    private async Task PrintReportCardAsync()
    {
        await System.Threading.Tasks.Task.Delay(100);
        System.Console.WriteLine("PDF generation is not supported on WebAssembly. It requires QuestPDF which uses incompatible desktop Skia binaries.");
    }'''

new_content = re.sub(pattern, replacement, content, flags=re.DOTALL)

with open('EducaMais/ViewModels/SchoolDashboardViewModel.cs', 'w') as f:
    f.write(new_content)
