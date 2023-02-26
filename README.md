# azure devops

- [.Net Core DevOps -免费用Azure四步实现自动化发布（CI/CD）](https://www.cnblogs.com/zhaozhengyan/p/azure-devops-aliyun.html)

- [.Net Core DevOps - 替换 ConnectionStrings](https://www.cnblogs.com/zhaozhengyan/p/azure-replace-appsettings.html)


# Docker Compose

```bash
docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" up -d
```


- 使用 Docker Compose 通过 HTTPS 托管 ASP.NET Core 映像 https://learn.microsoft.com/zh-cn/aspnet/core/security/docker-compose-https?view=aspnetcore-7.0
- 在 ASP.NET Core 中强制使用 HTTPS https://learn.microsoft.com/zh-cn/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio%2Clinux-ubuntu#ssl-linux

- macOS 或 Linux

```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$
dotnet dev-certs https --trust
```

dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p password