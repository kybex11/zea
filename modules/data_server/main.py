from flask import Flask, request, jsonify

import sqlite3

app = Flask(__name__)

def init_db():
    conn = sqlite3.connect("public.db")
    cursor = conn.cursor()
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS servers (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT UNIQUE NOT NULL,
            ip TEXT NOT NULL,
            description TEXT NOT NULL
        )
    ''')
    conn.commit()
    conn.close()
    
init_db()

@app.route('/getServers')
def get_servers():
    conn = sqlite3.connect("public.db")
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM servers")
    servers = cursor.fetchall()
    conn.close()
    
    return jsonify(servers)

@app.route('/addServerLiteSqliteDatabaseStorageListMainOfDataServer', methods=['POST'])
def add_server():
    data = request.get_json()
    conn = sqlite3.connect("public.db")
    cursor = conn.cursor()
    
    cursor.execute('''
        INSERT INTO servers (name, ip, description) VALUES (?, ?, ?)               
    ''', (data['name'], data['ip'], data['description']))
    
