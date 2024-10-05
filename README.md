Инструкция по запуску проекта
Предварительные требования
Перед началом работы убедитесь, что на вашем компьютере установлены следующие инструменты:

.NET SDK (версии 6.0 или выше) - Скачать .NET SDK.
PostgreSQL - Скачать PostgreSQL.
pgAdmin - Скачать pgAdmin.
Visual Studio или Visual Studio Code - Скачать Visual Studio или Скачать Visual Studio Code.

Шаг 1: Клонирование репозитория
Сначала клонируйте репозиторий с исходным кодом проекта. В терминале или командной строке выполните команду:

Шаг 2: Восстановление базы данных из дампа
Откройте pgAdmin и создайте новую базу данных (например, StemyCloud).
Импортируйте дамп базы данных BDStemy, который находится в папке DATA вашего репозитория:
Щелкните правой кнопкой мыши на созданной базе данных и выберите Restore.
Выберите файл дампа базы данных из папки DATA.
Нажмите Restore для восстановления данных.
Шаг 3: Настройка приложения
Откройте файл appsettings.json вашего проекта и обновите строку подключения:
json
Копировать код
"ConnectionStrings": {
    "StemyCloudConnection": "Host=localhost;Port=5432;Username=postgres;Password=ваш_пароль;Database=StemyCloud"
}
Замените ваш_пароль на фактический пароль пользователя postgres.

Шаг 4: Запуск приложения
Для запуска API выполните следующую команду в терминале:

bash
Копировать код
dotnet run
После успешного запуска API вы должны увидеть сообщение о том, что приложение работает на указанном порту.

Шаг 5: Тестирование API через Postman
Откройте Postman.
Используйте следующие запросы для тестирования API:
Загрузка файла (POST):

bash
Копировать код
POST http://localhost:5184/api/files
Body (form-data):

file: [выберите файл]
author: "Имя автора"
Получение списка файлов (GET):

bash
Копировать код
GET http://localhost:5184/api/files
Скачивание файла (GET):

bash
Копировать код
GET http://localhost:5184/api/files/{id}/download
Обновление названия файла (PUT):

bash
Копировать код
PUT http://localhost:5184/api/files/{id}
Body (JSON):

json
Копировать код
{
  "newName": "Новое название файла"
}
Удаление файла (DELETE):

bash
Копировать код
DELETE http://localhost:5184/api/files/{id}
Создание пользователя (POST):

bash
Копировать код
POST http://localhost:5184/api/users
Body (JSON):

json
Копировать код
{
  "firstName": "Имя",
  "lastName": "Фамилия",
  "biography": "Биография"
}
