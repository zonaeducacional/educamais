import re

with open('EducaMais/ViewModels/SchoolDashboardViewModel.cs', 'r') as f:
    content = f.read()

# Replace the stray ComposeHeader method
pattern = r'private void ComposeHeader\(QuestPDF.*?}\n    }'
content = re.sub(pattern, '', content, flags=re.DOTALL)

with open('EducaMais/ViewModels/SchoolDashboardViewModel.cs', 'w') as f:
    f.write(content)
