const express = require('express');
const https = require('https');
const fs = require('fs');
const socketIo = require('socket.io');
const cors = require('cors');
const path = require('path');
const dotenv = require('dotenv');
dotenv.config();

const { GoogleGenAI } = require("@google/genai");

const app = express();
app.use(cors());

app.use(express.static(path.join(__dirname, 'public')));

const HISTORY_DIR = path.join(__dirname, 'history');
if (!fs.existsSync(HISTORY_DIR)) {
    fs.mkdirSync(HISTORY_DIR);
}

function loadKnowledgeBase(context) {
    const safeContext = (context || '').replace(/[^a-zA-Z0-9_]/g, '');
    const contextPath = `database_${safeContext}.md`;
    try {
        return fs.readFileSync(contextPath, 'utf8');
    } catch {
        console.warn(`Nie znaleziono ${contextPath}, używam domyślnej bazy.`);
        try {
            return fs.readFileSync('database.md', 'utf8');
        } catch (err) {
            console.error("Błąd odczytu database.md:", err);
            return "Baza wiedzy jest niedostępna.";
        }
    }
}

// Read System Prompt
let systemInstruction = '';
try {
    systemInstruction = fs.readFileSync('prompt.md', 'utf8');
} catch (err) {
    console.error("Błąd odczytu prompt.md:", err);
    systemInstruction = "Jesteś pomocnym asystentem AI.";
}

const GEM_MODEL = process.env.GEM_MODEL || "gemini-2.5-flash";
const ai = new GoogleGenAI({ apiKey: process.env.GEM_KEY });

const options = {
    key: fs.readFileSync('ss.key'),
    cert: fs.readFileSync('ss.crt')
};

const server = https.createServer(options, app);
const io = socketIo(server, {
    cors: {
        origin: "*",
        methods: ["GET", "POST"]
    }
});

function getTimestamp() {
    const now = new Date();
    const pad = (num) => (num < 10 ? '0' + num : num);
    return (
        now.getFullYear().toString() +
        pad(now.getMonth() + 1) +
        pad(now.getDate()) +
        '_' +
        pad(now.getHours()) +
        pad(now.getMinutes()) +
        pad(now.getSeconds())
    );
}

io.on('connection', (socket) => {
    const sessionId = socket.handshake.query.sessionId || 'unknown_session';
    const context = socket.handshake.query.context || 'default';
    const knowledgeBase = loadKnowledgeBase(context);
    console.log(`New client connected: ${socket.id}, Session ID: ${sessionId}`);

    const sessionFileBase = `${getTimestamp()}_${sessionId}.json`;
    const sessionFilePath = path.join(HISTORY_DIR, sessionFileBase);
    
    let conversationHistory = [];

    saveHistory(sessionFilePath, conversationHistory);

    socket.on('chat message', async (msg) => {
        console.log(`[${sessionId}] User:`, msg);

        conversationHistory.push({ role: 'user', content: msg });

        saveHistory(sessionFilePath, conversationHistory);

        const historyText = conversationHistory
            .map(entry => `${entry.role === 'user' ? 'UŻYTKOWNIK' : 'AI'}: ${entry.content}`)
            .join('\n');

        try {
            const prompt = `${systemInstruction}
            
            DATABASE:
            ${knowledgeBase}

            HISTORIY:
            ${historyText}
            
            ANSWER AS ASSISTANT:`;

            const response = await ai.models.generateContent({
                model: GEM_MODEL,
                contents: prompt,
            });
            
            const aiResponseText = response.text || "Przepraszam, nie udało się wygenerować odpowiedzi.";

            console.log(`[${sessionId}] AI:`, aiResponseText);

            conversationHistory.push({ role: 'assistant', content: aiResponseText });
            saveHistory(sessionFilePath, conversationHistory);

            socket.emit('chat message', aiResponseText);

        } catch (error) {
            console.error("Gemini Error:", error);
            const errorMsg = "Przepraszam, wystąpił błąd podczas generowania odpowiedzi.";
            socket.emit('chat message', errorMsg);
        }
    });

    socket.on('disconnect', () => {
        console.log(`Client disconnected: ${socket.id}`);
    });
});

function saveHistory(filePath, history) {
    fs.writeFile(filePath, JSON.stringify(history, null, 2), (err) => {
        if (err) console.error("Error saving history:", err);
    });
}

const PORT = 3000;
server.listen(PORT, () => {
    console.log(`Server running on https://localhost:${PORT}`);
});
