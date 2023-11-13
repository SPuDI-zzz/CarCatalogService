# CarCatalogService

```powershell
docker pull postgres

docker run --name postgres --restart=always -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=carcatalogservice -v postgresvolume:/var/lib/postgresql/data -d postgres
```
