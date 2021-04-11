# RedisBookStore
Rewrite of the Java Book Store App in to .NET.

## Setup

Do this once with in the `RedisBookStore.API``:

```bash
$ dotnet user-secrets init
$ dotnet user-secrets set CacheConnection "localhost,abortConnect=false,ssl=false,allowAdmin=false,password="
```

## Start Docker

```bash
$ git submodule update --init --recursive
$ cd redismod-docker-compose
$ docker-compose up
```

## Run the App
(in a new shell)
```bash
$ dotnet run
```