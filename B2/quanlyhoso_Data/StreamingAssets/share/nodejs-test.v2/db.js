const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database('./database.db', (err) => {
    if (err) {
        console.error("Error opening database", err);
    } else {
        console.log("Connected to SQLite database");
    }
});

const getUser = (username) => {
    return new Promise((resolve, reject) => {
        const query = 'SELECT * FROM users WHERE username = \'le tam\'';

        db.get(query, [username], (err, row) => {
            if (err) {
                console.error('Error executing query:', err);
                reject(err);
            } else if (!row) {
                reject(new Error('User not found'));
            } else {
                resolve(row); // Trả về kết quả từ database
            }
        });
    });
};

// Tạo bảng users nếu chưa có
db.serialize(() => {
    db.run(`
        CREATE TABLE IF NOT EXISTS users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL UNIQUE,
            password TEXT NOT NULL,
            role TEXT NOT NULL DEFAULT 'viewer'
        )
    `);
    db.run(`
        CREATE TABLE IF NOT EXISTS members (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            date_of_birth DATE,
            address TEXT  
        )
    `);
});


module.exports = db;
