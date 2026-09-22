using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using EducaMais.ViewModels;
using EducaMais.Views;

namespace EducaMais;

/// <summary>
/// ViewLocator com mapeamento explícito — trim-safe para WebAssembly.
/// Não usa reflexão para que o IL Trimmer não remova os tipos das Views.
/// </summary>
public class ViewLocator : IDataTemplate
{
    private static readonly Dictionary<Type, Func<Control>> _map = new()
    {
        { typeof(LandingViewModel),           () => new LandingView() },
        { typeof(LoginViewModel),             () => new LoginView() },
        { typeof(AdminDashboardViewModel),    () => new AdminDashboardView() },
        { typeof(SchoolDashboardViewModel),   () => new SchoolDashboardView() },
        { typeof(SecretaryDashboardViewModel),() => new SecretaryDashboardView() },
        { typeof(TeacherDashboardViewModel),  () => new TeacherDashboardView() },
        { typeof(StudentDashboardViewModel),  () => new StudentDashboardView() },
    };

    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        if (_map.TryGetValue(param.GetType(), out var factory))
            return factory();

        return new TextBlock { Text = "View não encontrada para: " + param.GetType().Name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
