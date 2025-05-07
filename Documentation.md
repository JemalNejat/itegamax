# 🛠 Guide: Steg-för-steg för att koppla tabeller till adminpanelen


Denna guide beskriver hur du skapar en ny entitet, kopplar den till databasen och visar den i adminpanelens sidomeny. Följ stegen nedan för att implementera en ny datatabell i adminpanelen.

---

## 📌 Steg 1: Installera nödvändiga paket
Innan du skapar entiteter måste du installera följande paket:

```shell
# Grundläggande ORM för att arbeta med databaser
# Nödvändig för att använda GetConnectionString() och UseMySQL()
dotnet add package Microsoft.EntityFrameworkCore

# Stöd för MySQL som databas
# Nödvändig för att använda UseMySQL()
dotnet add package Microsoft.EntityFrameworkCore.MySql

# Verktyg för att skapa och hantera migrationer
dotnet add package Microsoft.EntityFrameworkCore.Tools 

# Krävs för designrelaterade funktioner
# nödvändigt för att använda kommandon som dotnet ef migrations add och dotnet ef database update
dotnet add package Microsoft.EntityFrameworkCore.Design

# Innehåller funktioner för relationella databaser
# Gör det möjligt att definiera och hantera primärnycklar, relationer och constraints i databasen.
dotnet add package Microsoft.EntityFrameworkCore.Relational

# Gör det möjligt att använda konfigurationsinställningar
dotnet add package Microsoft.EntityFrameworkCore.Configuration

# Låter oss använda JSON-konfigurationer
dotnet add package Microsoft.EntityFrameworkCore.Configuration.Json
```


## 📌 Steg 2: Konfigurera anslutning i appsettings.json 
Lägg till din databasanslutning i appsettings.json. Det är viktigt att inkludera anslutningar för både ditt **hemnätverk** och din **arbetsplats**:

**Exempel:**
```csharp
  "ConnectionStrings": {
    //COMMENT OR UNCOMMENT THE BELOW LINE TO GET THE: Database connection for development from: ITeGAMAX Handen:
    //"MariaDbConnectionString": "Data Source=mysql369.loopia.se;port=3306;Initial Catalog=itegamax_se;User Id=itmxdev@i371810;password=Ep!7i3Y02q!90ed2"
    ////COMMENT OR UNCOMMENT THE BELOW LINE TO GET THE: Database connection for development from: Home:
    //"MariaDbConnectionString": "Data Source=mysql369.loopia.se;port=3306;Initial Catalog=itegamax_se;User Id=aba@i371810;password=H@8dTp4?b9@!",
  }
```
### **OBS:** 
Kommentera eller avkommentera rätt anslutning beroende på din arbetsmiljö.


## 📌 Steg 3: Konfigurera databaskoppling i Program.cs 
Lägg till följande kod i `Program.cs` för att koppla samman din applikation med databasen:

**Exempel:**
```csharp
  builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("MariaDbConnectionString")!));
```
### **OBS:** 
Kommentera eller avkommentera rätt anslutning beroende på din arbetsmiljö.


## 📌 Steg 4: Skapa **Entity-klassen**
1. Navigera till `App_Data`-mappen.
2. Skapa en ny **C#-klass** med det **singulära namnet** t.ex. StSocialMedia.
3. Definiera dess **egenskaper** enligt databasens kolumner i **itegamax_se** databasen.

**Exempel:**
```csharp
namespace ADMIN.ITEGAMAX._4._0.App_Data
{
    public class StSocialMedia
    {
        public int StSocialMediaId { get; set; }
        public string StSocialMediaName { get; set; }
        public string StSocialMediaShort { get; set; }
        public int StSocialMediaStatus { get; set; }
    }
}
```

## 📌 Steg 5: Skapa och konfigurera ITeGAMAX4Context.cs
1. Skapa en ny fil `ITeGAMAX4Context`.cs.
2. Definiera din kontextklass och konfigurera den med `DbSet<>` och `Fluent API`.


**Exempel:**
```csharp
public partial class ITeGAMAX4Context : DbContext
{
		public ITeGAMAX4Context()
		{
		}

		public ITeGAMAX4Context(DbContextOptions<ITeGAMAX4Context> options)
			: base(options)
		{
		}

        public virtual DbSet<StSocialMedium> StSocialMedia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StSocialMedium>(entity =>
            {
                entity.HasKey(e => e.StSocialMediaId).HasName("PRIMARY");

                entity.ToTable("st_social_media");

                entity.Property(e => e.StSocialMediaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("st_social_media_id");
                entity.Property(e => e.StSocialMediaName)
                    .HasMaxLength(45)
                    .HasColumnName("st_social_media_name");
                entity.Property(e => e.StSocialMediaShort)
                    .HasMaxLength(45)
                    .HasColumnName("st_social_media_short");
                entity.Property(e => e.StSocialMediaStatus)
                    .HasDefaultValueSql("'3'")
                    .HasColumnType("int(11)")
                    .HasColumnName("st_social_media_status");
            });

         }
}
```

## 📌 Steg 6: Skapa och tillämpa migrationer

Efter att ha skapat entiteten och konfigurerat kontexten behöver du migrera modellen till databasen.

Kör följande kommandon:
**Exempel:**
```csharp
 dotnet ef migrations add InitialCreate
 dotnet ef database update
```

### **OBS:** 
- Kontrollera att din MariaDbConnectionString är korrekt konfigurerad innan du kör dessa kommandon
- Om databasen redan innehåller tabellen ska du **inte** köra migrationer, eftersom det kan orsaka konflikter eller duplicering av tabeller.


## 📌 Steg 7: Skapa en mapp för sidhantering
Navigera till `Areas/database/Pages/` och skapa en ny mapp med **samma namn som entiteten men i plural**. Om entiten heter `StSocialMedia`, ska mappen heta `StSocialMedias`.


## 📌 Steg 8: Lägg till en länk i sidomenyn
För att enkelt kunna navigera till sidan via sidomenyn, öppna `Pages/Shared/Components/AppMainMenu/Default.cshtml`  
och lägg till följande kod:

**Exempel:**
```csharp
<li class="slide">
    <a href="~/database/[mappnamn]" class="side-menu__item">[ange länk namn]</a> 
</li>
```
### 📝 **Notera:**

*   Byt ut **`[mappnamn]`** mot det faktiska namnet på mappen i `Areas/database/Pages/`.
*   Ändra **`[ange länk namn]`** till det namn som ska visas i sidomenyn, t.ex. namnet på tabellen eller en beskrivande titel.

## 📌 Steg 9: Skapa sidor för CRUD-operationer
Följande filer behöver skapas för att hantera CRUD-operationer:

- `New.cshtml` och `New.cshtml.cs`
- `Edit.cshtml` och `Edit.cshtml.cs`
- `View.cshtml` och `View.cshtml.cs`
- *(Valfritt)* `Delete.cshtml` och `Delete.cshtml.cs`

### 📝 Notera:
 Skapandet av en Razor `.cshtml`-fil **automatiskt** skapar en tillhörande `.cshtml.cs`-fil.


 ### ℹ️ Viktigt att tänka på:

 **Entitetsnamn**  
Se till att namnet i `List<>` överensstämmer med den entitet du har definierat i `ITeGAMAX4Context.cs`.  
Exempelvis, om du har **`public DbSet<StPageArticle> StPageArticles { get; set; }`** i `ITeGAMAX4Context.cs`,  
ska `List<>` i `Index.cshtml.cs` också använda `StPageArticle` för att säkerställa korrekt koppling.

**Anpassa fälten**  
Se till att `asp-for`-fälten matchar de faktiska tabellfälten.

**ID-hantering**  
Om din tabell använder en sträng som ID, generera ett unikt ID genom att lägga till ett dolt fält i formuläret.  
Om ID istället är ett `int` eller `Guid`, och genereras automatiskt av databasen, behövs inte detta fält.

**Fälthantering**  
Anpassa fälten i den sida du arbetar med genom att lägga till de som behövs och ta bort de som inte används.

**Sidtitel**  
Uppdatera `ViewData["Title"]` så att den visar rätt tabellnamn.

**Validering**  
Se till att fälten har rätt krav, t.ex. `required`, `min` och `max`.

**Ändra entitetsnamnet**  
Byt ut `StSocialMedia` till rätt entitet, exempelvis `public StPageArticle StPageArticles { get; set; }`.

**Tidsstämplar**  
Om tabellen har fält för CreatedDate och UpdatedDate, sätt deras värde till DateTime.Now; i entiteten innan du sparar datan.

**Manuell fältuppdatering**  
I `Edit.cshtml.cs` måste varje fält uppdateras manuellt för att **undvika** att andra fält skrivs över.

**View-sidan**  
- Ta bort `<span asp-validation-for="">`, eftersom validering **inte** behövs vid visning av data.  
- Ta bort `required`-attributet från alla fält.  
- Lägg till `disabled`-attributet på varje fält för att förhindra redigering.



