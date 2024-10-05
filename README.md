Вот оформленная инструкция по запуску проекта для GitHub:

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
POST http://localhost:5184/api/files
```

Body (form-data):
- `file`: [выберите файл]
- `author`: "Имя автора"

### Получение списка файлов (GET)

```http
GET http://localhost:5184/api/files
```

### Скачивание файла (GET)

```http
GET http://localhost:5184/api/files/{id}/download
```

### Обновление названия файла (PUT)

```http
PUT http://localhost:5184/api/files/{id}
```

Body (JSON):
```json
{
  "newName": "Новое название файла"
}
```

### Удаление файла (DELETE)

```http
DELETE http://localhost:5184/api/files/{id}
```

### Создание пользователя (POST)

```http
POST http://localhost:5184/api/users
```

Body (JSON):
```json
{
  "firstName": "Имя",
  "lastName": "Фамилия",
  "biography": "Биография"
}
```

### Получение списка пользователей (GET)

```http
GET http://localhost:5184/api/users
```

### Получение пользователя по ID (GET)

```http
GET http://localhost:5184/api/users/{id}
```

### Обновление пользователя (PUT)

```http
PUT http://localhost:5184/api/users/{id}
```

Body (JSON)**

```json
{
  "firstName": "Новое Имя",
  "lastName": "Новая Фамилия",
  "biography": "Новая Биография"
}
```

### Удаление пользователя (DELETE)

```http
DELETE http://localhost:5184/api/users/{id}
```

## Заключение

Теперь вы готовы использовать API для управления облачным хранилищем Stemy.Cloud. Убедитесь, что все зависимости установлены, и следуйте инструкциям по запуску, чтобы успешно запустить проект и протестировать его через Postman.
