from flask import Flask, request, jsonify

import sqlite3

app = Flask(__name__)

menu = open("content/menu.dot", "r")
menu_content = menu.read()
menu.close()

def init_db():
    conn = sqlite3.connect("users.db")
    cursor = conn.cursor()
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT UNIQUE NOT NULL,
            email TEXT UNIQUE NOT NULL,
            password TEXT NOT NULL
        )
    ''')
    conn.commit()
    conn.close()
    
def init_server_db():
    conn = sqlite3.connect("server.db")
    cursor = conn.cursor()
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS server (
            userscount INTEGER NOT NULL,
            name TEXT UNIQUE NOT NULL,
        )
    ''')
    
init_db() 
init_server_db()


@app.route('/getUsersCount')
def getUsersCount():
    conn = sqlite3.connect("server.db")
    cursor = conn.cursor()
    cursor.execute("SELECT userscount FROM server")
    users_count = cursor.fetchone()
    conn.close()
    
    if users_count:
        return users_count[0]
    else:
        return 'NULL';
    
@app.route('/incrementUsersCount', methods=['POST'])
def increment_users_count():
    conn = sqlite3.connect("server.db")
    cursor = conn.cursor()
    cursor.execute("UPDATE server SET userscount = usercount + 1")
    conn.commit()
    conn.close()

@app.route('/decrementUsersCount', methods=['POST'])
def decrement_users_count():
    conn = sqlite3.connect("server.db")
    cursor = conn.cursor()
    cursor.execute("UPDATE server SET userscount = userscount - 1 WHERE userscount > 0")
    conn.commit()
    conn.close()

@app.route('/getMenu')
def getMenu():
    return menu_content

@app.route('/register', methods=['POST'])
def register():
    data = request.get_json()
    username = data.get('username')
    email = data.get('email')
    password = data.get('password')
    
    if not username or  email or not password:
        return jsonify({"error": "Email and password are required"}), 400
    
    conn = sqlite3.connect("users.db")
    cursor = conn.cursor()
    
    cursor.execute("SELECT * FROM users WHERE username = ? OR email = ?", (username, email))
    if cursor.fetchone():
        return jsonify({"error": "User already exists"}), 400
    
    cursor.execute("INSERT INTO users (username, email, password) VALUES (?, ?, ?)", (username, email, password))
    conn.commit()
    conn.close()
    
    return jsonify({"message": "User registered successfully"}), 201

@app.route('/login', methods=['POST'])
def login():
    data = request.get_json()
    username = data.get('username')
    password = data.get('password')
    
    if not username or not password:
        return jsonify({"error": "Username and password are required"}), 400
    
    conn = sqlite3.connect("users.db")
    cursor = conn.cursor()
    
    cursor.execute("SELECT * FROM users WHERE username = ? AND password = ?", (username, password))
    user = cursor.fetchone()
    conn.close()
    
    if user:
        return jsonify({"message": "Login successful"}), 200
    else:
        return jsonify({"error": "Invalid email or password"}), 401

if __name__ == '__main__':
    app.run(debug=True, port=5001)