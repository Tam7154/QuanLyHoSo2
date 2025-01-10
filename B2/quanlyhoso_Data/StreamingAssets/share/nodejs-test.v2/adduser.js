const bcrypt = require('bcryptjs');
const db = require('./db');

// Mã hóa mật khẩu và thêm người dùng mẫu vào cơ sở dữ liệu
const createUser = async (username = '1', role = 'viewer') => {
    const password = '123456'; // Mật khẩu mẫu
    const hashedPassword = await bcrypt.hash(password, 10);

    db.run(`
        INSERT INTO users (username, password, role) VALUES (?, ?, ?)
    `, [username, hashedPassword, role], function (err) {
        if (err) {
            console.error("Error inserting user", err);
        } else {
            console.log("User inserted with ID:", this.lastID);
        }
    });
};

//createUser('admin');
createUser('le tam');