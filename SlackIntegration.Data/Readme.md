# Ohje https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell
1. Valitse Slackintegration.Data käynnistys projektiksi
2. Poista Kaikki tiedostot paitsi tämä ja 00_Extensions kansio (tai käyttää -Force)
3. Avaa package manager console (Tools->NuGet package manager->package manager console)
5. Aja komento
Scaffold-DbContext "Server=.;Database=alfamereporting;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -Force -DataAnnotations -UseDatabaseNames -Context Context -Project SlackIntegration.Data -StartupProject SlackIntegration.Data.ScaffoldStartup
6. Poista tyhmä OnConfiguring Context luokasta (https://github.com/aspnet/EntityFrameworkCore/issues/10432)
- Lisää -Verbose jos tulee ongelmia

P.S. Viime versioissa on tullut parannuksia tuohon työkaluun, muista tarkistaa onko jotain uutta tullut