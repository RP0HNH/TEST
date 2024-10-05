# Инструкция по запуску проекта Stemy.Cloud

## Предварительные требования

Перед началом работы убедитесь, что на вашем компьютере установлены следующие инструменты:

- [.NET SDK (версии 6.0 или выше)](https://dotnet.microsoft.com/download/dotnet)  
- [PostgreSQL](https://www.postgresql.org/download/)  
- [pgAdmin](https://www.pgadmin.org/download/)  
- [Visual Studio](https://visualstudio.microsoft.com/vs/) или [Visual Studio Code](https://code.visualstudio.com/)  

## Шаг 1: Клонирование репозитория

Сначала клонируйте репозиторий с исходным кодом проекта. В терминале или командной строке выполните команду:

```bash
git clone <URL_репозитория>
```

Замените `<URL_репозитория>` на URL вашего репозитория на GitHub или альтернативной платформе.

## Шаг 2: Восстановление базы данных из дампа

1. Откройте pgAdmin и создайте новую базу данных (например, `StemyCloud`).
2. Импортируйте дамп базы данных `BDStemy`, который находится в папке `DATA` вашего репозитория:
   - Щелкните правой кнопкой мыши на созданной базе данных и выберите **Restore**.
   - Выберите файл дампа базы данных из папки `DATA`.
   - Нажмите **Restore** для восстановления данных.

## Шаг 3: Настройка приложения

Откройте файл `appsettings.json` вашего проекта и обновите строку подключения:

```json
"ConnectionStrings": {
    "StemyCloudConnection": "Host=localhost;Port=5432;Username=postgres;Password=ваш_пароль;Database=StemyCloud"
}
```

Замените `ваш_пароль` на фактический пароль пользователя `postgres`.

## Шаг 4: Запуск приложения

Для запуска API выполните следующую команду в терминале:

```bash
dotnet run
```

После успешного запуска API вы должны увидеть сообщение о том, что приложение работает на указанном порту.

## Шаг 5: Тестирование API через Postman

1. Откройте Postman.
2. Используйте следующие запросы для тестирования API:

### Загрузка файла (POST)

```http
{
	"info": {
		"_postman_id": "6f29aa6d-181b-4f99-916f-8bcb7a517ca9",
		"name": "FilesController",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "38729443"
	},
	"item": [
		{
			"name": "POST: Загрузка файла (UploadFile)",
			"event": [
				{
					"listen": "test",
					"script": {
						"exec": [
							""
						],
						"type": "text/javascript",
						"packages": {}
					}
				}
			],
			"protocolProfileBehavior": {
				"protocolVersion": "http1",
				"strictSSL": false,
				"followRedirects": true,
				"followOriginalHttpMethod": false
			},
			"request": {
				"method": "POST",
				"header": [],
				"body": {
					"mode": "formdata",
					"formdata": [
						{
							"key": "file",
							"type": "file",
							"src": "/C:/Users/a/Pictures/1672008428_www-funnyart-club-p-raian-gosling-mem-kartinki-52.jpg"
						},
						{
							"key": "author",
							"value": "alexey",
							"type": "text"
						}
					]
				},
				"url": {
					"raw": "http://localhost:5184/api/files",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"files"
					]
				}
			},
			"response": []
		},
		{
			"name": "GET: Получение всех файлов (GetFiles)",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/files",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"files"
					]
				}
			},
			"response": []
		},
		{
			"name": "GET: Получение файла по ID (GetFile)",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/files/3",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"files",
						"3"
					]
				}
			},
			"response": []
		},
		{
			"name": "PUT: Обновление имени файла (UpdateFile)",
			"request": {
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "\"1672008428_www-funnyart-club-p-raian-gosling-mem-kartinki-52\"",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5184/api/files/3",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"files",
						"3"
					]
				}
			},
			"response": []
		},
		{
			"name": "GET: Загрузка файла по ID (DownloadFile)",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/files/download/3",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"files",
						"download",
						"3"
					]
				}
			},
			"response": []
		},
		{
			"name": "DELETE: Удаление файла (DeleteFile)",
			"request": {
				"method": "DELETE",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/files/1",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"files",
						"1"
					]
				}
			},
			"response": []
		}
	]
}
```

### Создание пользователя (POST)

```http
{
	"info": {
		"_postman_id": "9399928e-fada-44b9-8765-ed1b871fafce",
		"name": "UsersController",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "38729443"
	},
	"item": [
		{
			"name": "Создание пользователя (CreateUser)",
			"request": {
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n  \"firstName\": \"Ивана\",\r\n  \"lastName\": \"Иванов\",\r\n  \"biography\": \"Биография пользователя\"\r\n}\r\n",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5184/api/users",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"users"
					]
				}
			},
			"response": []
		},
		{
			"name": "Получение списка пользователей (GetUsers)",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/users",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"users"
					]
				}
			},
			"response": []
		},
		{
			"name": "Получение пользователя по ID (GetUser)",
			"request": {
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/users/1",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"users",
						"1"
					]
				}
			},
			"response": []
		},
		{
			"name": "Обновление пользователя (UpdateUser)",
			"request": {
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n  \"firstName\": \"Ивааны\",\r\n  \"lastName\": \"Петров\",\r\n  \"biography\": \"Обновлённая биография\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5184/api/users/4",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"users",
						"4"
					]
				}
			},
			"response": []
		},
		{
			"name": "Удаление пользователя (DeleteUser)",
			"request": {
				"method": "DELETE",
				"header": [],
				"url": {
					"raw": "http://localhost:5184/api/users/1",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5184",
					"path": [
						"api",
						"users",
						"1"
					]
				}
			},
			"response": []
		}
	]
}
```
