using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MyPortfolio
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");

      builder.Services.AddBaseAddressHttpClient();

      await builder.Build().RunAsync();
    }
    /*
    public enum SkillTypes
    {
      Persistence,
      Passion,
      Dedication
    }

    public static async Employee GetMe()
    {
      Dictionary<SkillTypes, bool> mySkills = new Dictionary<SkillTypes, bool>
      {
      (SkillTypes.Dedication,   true),
      (SkillTypes.Passion,      true),
      (SkillTypes.Persistence,  true)
      };
      if(YOU.LookingForEmployee() && YOU)
    } */
  }
}
