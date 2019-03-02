# run project cli 

Require dotnet sdk 2.2
- https://dotnet.microsoft.com/download


```
# restore package
dotnet restore

```

```
# run application

dotnet run
```

# create new project
```
dotnet new mvc --auth none -o KMeeting
```

Create your on Database Context (Code first)

# Miigrate database command:

```

dotnet ef migrations add -c IdentityContext IdentityContextIntial

dotnet ef database update -c IdentityContext

```

```

dotnet ef migrations add -c MeetingContext MeetingContextInitial

dotnet ef database update -c MeetingContext

```

# Identity scafolding
```
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-2.2&tabs=netcore-cli
```


#  Boostrap theme:

- https://bootswatch.com/