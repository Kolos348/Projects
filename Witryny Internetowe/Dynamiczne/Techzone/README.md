--ENGLISH README--
Techzone is a Django-based e-commerce website for selling tech products. It includes features like product listings, shopping cart, and user management.

Requirements:
- Python 3.x
- Django (version specified in the project)
- A database system (e.g., SQLite for development, PostgreSQL for production)

Installation:
1. Clone the repository to your local machine.
2. Navigate to the `Techzone` directory.
3. Install the required Python packages (if a `requirements.txt` file exists, run `pip install -r requirements.txt`; otherwise, install Django manually).

Database Setup:
This project requires a database to store data such as products, users, and orders. The database migration templates are located in the `Techzone/store/migrations/` directory.

To set up and apply the database migrations:
1. Ensure your database is configured in `Techzone/techzone/settings.py` (e.g., DATABASES setting).
2. Run the following command from the `Techzone` directory to apply migrations:
   ```
   python manage.py migrate
   ```
   This will create the necessary database tables based on the migration files.

Running the Server:
To start the development server:
```
python manage.py runserver
```
The website will be available at `http://127.0.0.1:8000/`.

--POLISH README--

Techzone to strona internetowa e-commerce oparta na Django, przeznaczona do sprzedaży produktów technologicznych. Zawiera funkcje takie jak lista produktów, koszyk zakupów i zarządzanie użytkownikami.

Wymagania:
- Python 3.x
- Django (wersja określona w projekcie)
- System bazy danych (np. SQLite do rozwoju, PostgreSQL do produkcji)

Instalacja:
1. Sklonuj repozytorium na swój lokalny komputer.
2. Przejdź do katalogu `Techzone`.
3. Zainstaluj wymagane pakiety Python (jeśli istnieje plik `requirements.txt`, uruchom `pip install -r requirements.txt`; w przeciwnym razie zainstaluj Django ręcznie).

Konfiguracja Bazy Danych:
Projekt wymaga bazy danych do przechowywania danych takich jak produkty, użytkownicy i zamówienia. Szablony migracji bazy danych znajdują się w katalogu `Techzone/store/migrations/`.

Aby skonfigurować i zastosować migracje bazy danych:
1. Upewnij się, że baza danych jest skonfigurowana w `Techzone/techzone/settings.py` (np. ustawienie DATABASES).
2. Uruchom następujące polecenie z katalogu `Techzone`, aby zastosować migracje:
   ```
   python manage.py migrate
   ```
   To utworzy niezbędne tabele bazy danych na podstawie plików migracji.

Uruchamianie Serwera:
Aby uruchomić serwer deweloperski:
```
python manage.py runserver
```
Strona będzie dostępna pod adresem `http://127.0.0.1:8000/`.