(function () {
    if (document.getElementById('chatbot-widget-container')) return;

    const socketScript = document.createElement('script');
    socketScript.src = 'https://localhost:3000/socket.io/socket.io.js';
    socketScript.onload = initChatbot;
    document.head.appendChild(socketScript);

    function initChatbot() {
        const SERVER_URL = 'https://localhost:3000';

        function generateUUID() {
             return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
                 var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                 return v.toString(16);
             });
        }

        function setCookie(name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + (value || "") + expires + "; path=/; Secure; SameSite=Strict";
        }

        function getCookie(name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for(var i=0;i < ca.length;i++) {
                var c = ca[i];
                while (c.charAt(0)==' ') c = c.substring(1,c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
            }
            return null;
        }

        let sessionId = getCookie('chat_session_id');
        if (!sessionId) {
            sessionId = generateUUID();
            setCookie('chat_session_id', sessionId, 365);
        }

        // --- Styles ---
        const style = document.createElement('style');
        style.textContent = `
            #chatbot-widget-container{--bg:#071025;--panel:#0b1220;--muted:#9aa6b2;--accent:#4f7bff}
            #chatbot-widget-container { position: fixed; bottom: 20px; right: 20px; z-index: 10000; font-family: Inter, 'Segoe UI', Tahoma, Arial; }
            #chatbot-toggle-btn { background:var(--panel); color:var(--muted); border:1px solid rgba(255,255,255,0.03); border-radius:50%; width:60px; height:60px; cursor:pointer; box-shadow:0 10px 30px rgba(2,6,23,0.6); display:flex;align-items:center;justify-content:center;font-size:22px }
            #chatbot-toggle-btn:focus{outline:2px solid rgba(79,123,255,0.12)}
            #chatbot-window{display:none;flex-direction:column;width:380px;height:520px;background:var(--panel);border-radius:12px;box-shadow:0 18px 50px rgba(2,6,23,0.7);position:absolute;bottom:88px;right:0;overflow:hidden;color:#e6eef8}
            @media(max-width:420px){#chatbot-window{right:12px;left:12px;width:auto}}
            #chatbot-header{background:transparent;color:#e6eef8;padding:12px 14px;display:flex;align-items:center;gap:12px;border-bottom:1px solid rgba(255,255,255,0.03)}
            .chat-logo{font-weight:700;font-size:16px;color:#e6eef8}
            #chatbot-header .title{font-weight:700}
            #chatbot-close{background:transparent;border:none;color:#e6eef8;cursor:pointer;font-size:18px}
            #chatbot-messages{flex:1;padding:14px;background:transparent;overflow-y:auto;display:flex;flex-direction:column;gap:12px}
            .chat-row{display:flex;align-items:flex-end;gap:10px}
            .chat-row.user{justify-content:flex-end}
            .avatar{width:36px;height:36px;border-radius:8px;flex:0 0 36px;display:flex;align-items:center;justify-content:center;font-weight:700;color:#071025}
            .avatar.bot{background:linear-gradient(90deg,rgba(255,255,255,0.06),rgba(255,255,255,0.02));color:#e6eef8}
            .avatar.user{background:var(--accent);color:white}
            .bubble{max-width:78%;padding:10px 12px;border-radius:10px;background:rgba(255,255,255,0.04);color:#e6eef8;font-size:14px}
            .bubble.user{background:linear-gradient(90deg,var(--accent),#7b6bff);color:#fff}
            .meta{font-size:11px;color:var(--muted);margin-top:6px}
            #chatbot-input-area{display:flex;gap:10px;padding:12px;border-top:1px solid rgba(255,255,255,0.03);background:transparent}
            #chatbot-input{flex:1;padding:10px 12px;border-radius:10px;border:1px solid rgba(255,255,255,0.04);background:transparent;color:#e6eef8;font-size:14px}
            #chatbot-input::placeholder{color:rgba(230,238,248,0.45)}
            #chatbot-send{background:var(--accent);color:#fff;border:none;padding:10px 12px;border-radius:10px;cursor:pointer;font-weight:700}
            .typing{height:18px;width:44px;border-radius:12px;background:rgba(255,255,255,0.03);display:flex;align-items:center;gap:4px;padding:4px}
            .dot{width:6px;height:6px;background:#9aa6ff;border-radius:50%;opacity:.9;animation:blink 1.2s infinite}
            .dot:nth-child(2){animation-delay:.15s}.dot:nth-child(3){animation-delay:.3s}
            @keyframes blink{0%{opacity:.15}50%{opacity:1}100%{opacity:.15}}
        `;
        document.head.appendChild(style);

        const container = document.createElement('div');
        container.id = 'chatbot-widget-container';
        container.innerHTML = `
            <div id="chatbot-window" role="dialog" aria-label="Chatbot pomoc techniczna">
                <div id="chatbot-header">
                    <div style="display:flex;align-items:center;gap:10px">
                            <div class="chat-logo">${window.CHATBOT_CONTEXT === 'coffee' ? 'CoffeeShop' : window.CHATBOT_CONTEXT === 'techzone' ? 'TechZone' : 'NanoCore'}</div>
                        </div>
                    <button id="chatbot-close" aria-label="Zamknij czat">✕</button>
                </div>
                <div id="chatbot-messages" aria-live="polite">
                    <div class="chat-row bot"><div class="bubble">${window.CHATBOT_CONTEXT === 'coffee' ? 'Witaj! Jestem asystentem CoffeeShop — jak mogę pomóc?' : window.CHATBOT_CONTEXT === 'techzone' ? 'Witaj! Jestem asystentem TechZone — jak mogę pomóc?' : 'Witaj! Jestem NanoCore Asystent — jak mogę pomóc?'}</div><div class="meta">Teraz</div></div>
                </div>
                <form id="chatbot-input-area">
                    <input type="text" id="chatbot-input" placeholder="Zadaj pytanie o produkty lub poproś o demo..." autocomplete="off" aria-label="Wiadomość" />
                    <button type="submit" id="chatbot-send" aria-label="Wyślij">Wyślij</button>
                </form>
            </div>
            <button id="chatbot-toggle-btn" aria-label="Otwórz czat">💬</button>
        `;
        document.body.appendChild(container);

        const context = window.CHATBOT_CONTEXT || 'default';
        const socket = io("https://localhost:3000", {
            query: {
                sessionId: sessionId, 
                context: context
            }
        });

        const toggleBtn = document.getElementById('chatbot-toggle-btn');
        const chatWindow = document.getElementById('chatbot-window');
        const closeBtn = document.getElementById('chatbot-close');
        const form = document.getElementById('chatbot-input-area');
        const input = document.getElementById('chatbot-input');
        const messagesDiv = document.getElementById('chatbot-messages');

        toggleBtn.addEventListener('click', () => {
            chatWindow.style.display = chatWindow.style.display === 'flex' ? 'none' : 'flex';
            if (chatWindow.style.display === 'flex') input.focus();
        });

        closeBtn.addEventListener('click', () => {
            chatWindow.style.display = 'none';
        });

        function appendMessage(text, sender, meta) {
            const row = document.createElement('div');
            row.className = 'chat-row ' + (sender === 'user' ? 'user' : 'bot');
            const bubble = document.createElement('div');
            bubble.className = 'bubble' + (sender === 'user' ? ' user' : '');
            bubble.textContent = text;
            const metaDiv = document.createElement('div');
            metaDiv.className = 'meta';
            metaDiv.textContent = meta || new Date().toLocaleTimeString();
            row.appendChild(bubble);
            row.appendChild(metaDiv);
            messagesDiv.appendChild(row);
            messagesDiv.scrollTop = messagesDiv.scrollHeight;
        }

        // Send message
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            const msg = input.value.trim();
            if (msg) {
                appendMessage(msg, 'user'); // Show instantly
                socket.emit('chat message', msg);
                input.value = '';
                input.focus();
            }
        });

        // Receive message
        socket.on('chat message', (msg) => {
            appendMessage(msg, 'bot'); 
        });

        // Accessibility: close on Escape
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape') chatWindow.style.display = 'none';
        });
    }
})();
