--ENGLISH README--
Chatbot chat window (divs) where people can ask Gemini AI with knowledge of the company using it, about information about it.

How to use:
 - Copy the .env.copy rename it to .env and fill the environment config file with user key and wanted model
 - Install required dependencies with: "npm install express socket.io cors node dotenv @google/genai"
 - Install PM2 globally with: "npm install -g pm2"
 - Create SSL certificate files ss.key and ss.crt in the project root (self‑signed certificates can be generated with OpenSSL)
 - Run the program with: "pm2 start ecosystem.config.js" – this API between AI and website is set on port 3000.
 - Port can be changed in "service.js" and "public/chatbot.js" files.
 - Create a database_NAME.md file that will store information about the company.
 - Create a prompt.md file that defines the system instruction for the AI (fallback is used if missing).
 - For website to use running chatbot service on it use "<script>window.CHATBOT_CONTEXT = "NAME";</script>" and "<script src="https://localhost:3000/chatbot.js"></script>" addition to the HTML of the site. To the CHATBOT_CONTEXT assign the end of company data file name database_NAME.md (for 2 projects in this repository it's database_coffee.md and database_techzone.md).
 - The project automatically creates a "history" folder where conversation logs are stored.


--POLISH README--

Chatbotowe okno czatu (divy), w którym osoby mogą zadawać pytania Gemini AI z wiedzą o firmie, która go używa.

Jak używać:
 - Skopiuj plik .env.copy, zmień jego nazwę na .env i wypełnij plik konfiguracyjny środowiska kluczem użytkownika oraz
 wybranym modelem
 - Zainstaluj wymagane zależności poleceniem: "npm install express socket.io cors node dotenv @google/genai"
 - Zainstaluj PM2 globalnie poleceniem: "npm install -g pm2"
 - Utwórz pliki certyfikatów SSL ss.key oraz ss.crt w katalogu projektu (można wygenerować samopodpisane certyfikaty przez    OpenSSL)
 - Uruchom program poleceniem: "pm2 start ecosystem.config.js" – to API pomiędzy AI a stroną działa na porcie 3000.
 - Port można zmienić w plikach "service.js" oraz "public/chatbot.js".
 - Utwórz plik database_NAME.md, który będzie przechowywał informacje o firmie.
 - Utwórz plik prompt.md, który definiuje instrukcję systemową dla AI (jeśli go nie ma, używana jest domyślna).
 - Aby strona mogła używać działającej usługi chatbota, dodaj do HTML strony: "<script>window.CHATBOT_CONTEXT = "NAME";</script>" oraz "<script src="https://localhost:3000/chatbot.js"></script>". Do CHATBOT_CONTEXT przypisz końcówkę nazwy pliku danych firmy database_NAME.md (dla 2 projektów w tym repozytorium są to database_coffee.md i database_techzone.md).
 - Projekt automatycznie tworzy folder "history", w którym zapisywane są logi rozmów.