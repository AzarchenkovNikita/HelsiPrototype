# HelsiPrototype

Вітаю, це тестове завдання, під час розробки намагався знайти баланс між якістью коду і швидкістью розробки

Актуальна версія проекту також розгорнута по цьому адресу:
https://helsiprototype.azurewebsites.net/swagger/index.html

Конекшн стрінг до mongodb актуальний і доступ э з будь якого ip,
тобто проект можна запустити локально і він має працювать по /swagger/index.html

При розробці обрав mongodb по рекомендації, структуру данних зробив реляційною,
тобто як із user tasklist зберігає не самі task а звязки до них,
мені здалось це краще ніж зберігати task всередині tasklist.

В цілому workflow розділений на 3 частини:
Controller - Service - DAL(data access layer)

TaskController -> interface <- TaskService -> interface <- MongoTaskRepository
UserController -> interface <- UserService -> interface <- MongoUserRepository
TaskListController -> interface <- TaskListService -> interface <- MongoTaskListRepository

Дечого можливо не встиг (якщо вдруг не встигнуть перевірити, постараюсь доробити)
перевести string id в Guid тип(через поспіх зробив string)
можливо місцями неоптимальне оформлення коду і неймінги (dto великі назви)
додатковий функціонал для user і task

але основна задача виконана, сподіваюсь все ок
