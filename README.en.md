**English** | [简体中文](README.md)

# Azure DevOps

- [.NET Core DevOps: four steps to free automated deployment with Azure (CI/CD)](https://www.cnblogs.com/zhaozhengyan/p/azure-devops-aliyun.html)
- [.NET Core DevOps: replace connection strings](https://www.cnblogs.com/zhaozhengyan/p/azure-replace-appsettings.html)

# Docker Compose

```bash
docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" up -d
```

- [Host ASP.NET Core images with HTTPS using Docker Compose](https://learn.microsoft.com/zh-cn/aspnet/core/security/docker-compose-https?view=aspnetcore-7.0)
- [Enforce HTTPS in ASP.NET Core](https://learn.microsoft.com/zh-cn/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio%2Clinux-ubuntu#ssl-linux)

On macOS or Linux:

```bash
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$
dotnet dev-certs https --trust
```
