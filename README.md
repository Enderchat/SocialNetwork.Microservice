README: Микросервис социальной сети
Описание проекта
Микросервис социальной сети — это веб-приложение, реализованное на платформе .NET по архитектурному принципу Clean Architecture , с разделением на слои:

Domain — содержит бизнес-логику, доменные модели, валидации и маппинги.
Core — абстракции (интерфейсы) для репозиториев и сервисов.
Infrastructure — реализация репозиториев, подключение к базе данных (EF Core), реализация сервисов.
Api — точка входа приложения (контроллеры, middleware, файл Program.cs).
Функциональные возможности
Регистрация пользователей
Добавление друзей
Отправка сообщений
Поддержка миграций базы данных
Валидация входящих данных
Swagger UI для тестирования API

Структура проекта
SocialNetwork.Microservice/
│
├── SocialNetwork.Api/                 # Web API (точка входа)
│   ├── Program.cs                     # Настройка DI, Swagger, EF Core, обработка ошибок
│   ├── Controllers/                   # Контроллеры для работы с пользователями, дружбой, сообщениями
│   │   ├── UserController.cs
│   │   ├── FriendshipController.cs
│   │   └── MessageController.cs
│   ├── appsettings.json               # Конфигурация приложения
│   └── Middleware/                    # Глобальная обработка ошибок
│       └── ExceptionMiddleware.cs
│
├── SocialNetwork.Contracts/           # DTO: запросы и ответы
│   ├── Requests/                      # Объекты запросов
│   │   ├── CreateUserRequest.cs
│   │   ├── AddFriendRequest.cs
│   │   └── SendMessageRequest.cs
│   └── Responses/                     # Объекты ответов
│       ├── UserResponse.cs
│       ├── FriendshipResponse.cs
│       └── MessageResponse.cs
│
├── SocialNetwork.Core/                # Абстракции (интерфейсы)
│   ├── Repositories/                  # Интерфейсы репозиториев
│   │   ├── IUserRepository.cs
│   │   ├── IFriendshipRepository.cs
│   │   └── IMessageRepository.cs
│   └── Services/                      # Интерфейсы сервисов
│       ├── IUserService.cs
│       ├── IFriendshipService.cs
│       └── IMessageService.cs
│
├── SocialNetwork.Infrastructure/      # Реализация
│   ├── Data/                          # DbContext и настройки EF Core
│   │   └── AppDbContext.cs
│   ├── Entities/                      # Сущности БД
│   │   ├── UserDb.cs
│   │   └── MessageDb.cs
│   ├── Repositories/                  # Реализация репозиториев
│   │   ├── UserRepository.cs
│   │   ├── FriendshipRepository.cs
│   │   └── MessageRepository.cs
│   └── Services/                      # Реализация сервисов
│       ├── UserService.cs
│       ├── FriendshipService.cs
│       └── MessageService.cs
│
├── SocialNetwork.Domain/              # Доменная логика
│   ├── Entities/                      # Доменные модели
│   │   ├── User.cs
│   │   ├── Friendship.cs
│   │   └── Message.cs
│   ├── Mappings/                      # Настройки AutoMapper
│   │   └── MappingProfile.cs
│   └── Validations/                   # Валидаторы FluentValidation
│       ├── UserValidator.cs
│       └── MessageValidator.cs
│
└── SocialNetwork.Domain.Exceptions/   # Исключения
    ├── BaseException.cs
    ├── UserNotFoundException.cs
    ├── FriendNotFoundException.cs
    ├── ValidationException.cs
    ├── MessageNotFoundException.cs
    └── DatabaseOperationException.cs

    Тестирование
Для тестирования API вы можете использовать:
Swagger UI (встроенный)
Postman
curl
Лицензия
Проект распространяется под лицензией MIT.

Автор
Работу выполнил: Воробьев Алексей
Студент группы 210
