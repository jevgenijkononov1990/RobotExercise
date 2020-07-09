
namespace Robot.Common.SettingsRepos
{
    public interface IBaseSettingsRepo<Tclass> where Tclass : class
    {
        Tclass GetEnviromentSettings();
    }
}
