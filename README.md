Вот обновленная инструкция по запуску проекта для API системы управления облачным хранилищем Stemy.Cloud с учетом использования локального дампа базы данных и тестирования через Postman.

### Инструкция по запуску проекта

#### Предварительные требования

Перед началом работы убедитесь, что на вашем компьютере установлены следующие инструменты:

1. **.NET SDK** (версии 6.0 или выше) - [Скачать .NET SDK](https://dotnet.microsoft.com/download/dotnet).
2. **PostgreSQL** - [Скачать PostgreSQL](https://www.postgresql.org/download/).
3. **pgAdmin** - [Скачать pgAdmin](https://www.pgadmin.org/download/).
4. **Visual Studio** или **Visual Studio Code** - [Скачать Visual Studio](https://visualstudio.microsoft.com/) или [Скачать Visual Studio Code](https://code.visualstudio.microsoft.com/).
5. **Postman** - [Скачать Postman](https://www.postman.com/downloads/).

#### Шаг 1: Клонирование репозитория

Сначала клонируйте репозиторий с исходным кодом проекта. В терминале или командной строке выполните команду:

```bash
git clone <URL_репозитория>
```

Замените `<URL_репозитория>` на URL вашего репозитория на GitHub или альтернативной платформе.

#### Шаг 2: Восстановление базы данных из дампа

1. Откройте pgAdmin и создайте новую базу данных (например, `StemyCloud`).
2. Импортируйте дамп базы данных, который находится в папке `DATA` вашего репозитория:
   - Щелкните правой кнопкой мыши на созданной базе данных и выберите **Restore**.
   - Выберите файл дампа базы данных из папки `DATA`.
   - Нажмите **Restore** для восстановления данных.

#### Шаг 3: Настройка приложения

1. Откройте файл `appsettings.json` вашего проекта и обновите строку подключения:

```json
"ConnectionStrings": {
    "StemyCloudConnection": "Host=localhost;Port=5432;Username=postgres;Password=ваш_пароль;Database=StemyCloud"
}
```

Замените `ваш_пароль` на фактический пароль пользователя `postgres`.

#### Шаг 4: Запуск приложения

Для запуска API выполните следующую команду в терминале:

```bash
dotnet run
```

После успешного запуска API вы должны увидеть сообщение о том, что приложение работает на указанном порту (обычно `http://localhost:5000` или `https://localhost:5001`).

#### Шаг 5: Тестирование API через Postman

1. Откройте Postman.
2. Используйте следующие запросы для тестирования API:

- **Загрузка файла** (POST):
  ```
  POST http://localhost:5000/api/files
  ```

  Body (form-data):
  - `file`: [выберите файл]
  - `author`: "Имя автора"

- **Получение списка файлов** (GET):
  ```
  GET http://localhost:5000/api/files
  ```

- **Скачивание файла** (GET):
  ```
  GET http://localhost:5000/api/files/{id}/download
  ```

- **Обновление названия файла** (PUT):
  ```
  PUT http://localhost:5000/api/files/{id}
  ```

  Body (JSON):
  ```json
  {
    "newName": "Новое название файла"
  }
  ```

- **Удаление файла** (DELETE):
  ```
  DELETE http://localhost:5000/api/files/{id}
  ```

- **Создание пользователя** (POST):
  ```
  POST http://localhost:5000/api/users
  ```

  Body (JSON):
  ```json
  {
    "firstName": "Имя",
    "lastName": "Фамилия",
    "biography": "Биография"
  }
  ```

#### Заключение

Теперь вы готовы использовать API для управления облачным хранилищем Stemy.Cloud. Убедитесь, что все зависимости установлены, и следуйте инструкциям по запуску, чтобы успешно запустить проект и протестировать его через Postman.
