const express = require('express');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcryptjs');
const db = require('./db'); // Kết nối cơ sở dữ liệu SQLite
//const adduser = require('./adduser');


const app = express();
app.use(express.json());
//app.use(express.urlencoded({ extended: true }));

// Mã bí mật cho JWT
const SECRET_KEY = 'k_7979';
let blacklistedTokens = []; // Dùng để lưu các token đã bị logout

const get = (username) => {
    return new Promise((resolve, reject) => {
        const query = username;

        db.all(query, (err, row) => {
            if (err) {
                console.error('Error executing query:', err);
                reject(err);
            } else if (!row) {
                reject(new Error('error'));
            } else {
                resolve(row);
            }
        });
    });
};

const run = (query) => {
    console.log('aa');
    db.run(query);
};

app.listen('3001', '0.0.0.0', () => {
    console.log(`Server is running on http://0.0.0.0:${3001}`);
}); 

app.post('/get-function', (req, res) => {
    const { oldPassword, newPassword } = req.body;

    //console.log(oldPassword);

    get(oldPassword)
    .then(records => {
        console.log('All Records:', records);
        res.json({ status: 'success', receivedMessage: records }); // Phản hồi lại client
    })
    .catch(err => {
        console.error('Error:', err.message);
        res.json({ status: 'error', receivedMessage: err.message }); // Phản hồi lại client
    });
});


app.post('/run-function', (req, res) => {
    const { oldPassword, newPassword } = req.body;

    console.log(oldPassword);

    run(oldPassword)
    //.then(records => {
    //    console.log('All Records:', records);
    //    res.json({ status: 'success', receivedMessage: records }); // Phản hồi lại client
    //})
    //.catch(err => {
    //    console.error('Error:', err.message);
    //    res.json({ status: 'error', receivedMessage: err.message }); // Phản hồi lại client
    //});
});

//app.post('/call-function', (req, res) => {
    //const password = '123456'; // Mật khẩu mẫu
    //const hashedPassword = await bcrypt.hash(password, 10);
    //
    //db.run(`
    //    INSERT INTO users (username, password, role) VALUES (?, ?, ?)
    //`, [username, hashedPassword, role], function (err) {
    //    if (err) {
    //        console.error("Error inserting user", err);
    //    } else {
    //        console.log("User inserted with ID:", this.lastID);
    //    }
    //});
    //console.log({message: res});
    //res.status(200).json({ message: "Function executed!" });
//});

// **1. Đăng nhập (login)**
app.post('/login', async (req, res) => {
    const { username, password } = req.body;
    // Kiểm tra người dùng trong cơ sở dữ liệu
    db.get('SELECT * FROM users WHERE username = ?', [username], async (err, user) => {
        if (err) {
            return res.status(500).json({ message: 'Database error', code: -1 });
        }
        if (!user) {
            return res.status(401).json({ message: 'Không tìm thấy username!', code: -2 });
        }
        // Kiểm tra mật khẩu
        const isMatch = await bcrypt.compare(password, user.password);
        if (!isMatch) {
            return res.status(401).json({ message: 'Sai mật khẩu!', code: -3 });
        }

        // Tạo JWT token
        const token = jwt.sign(
            { id: user.id, username: user.username },
            SECRET_KEY,
            { expiresIn: '10d' } // Token hết hạn sau 1 ngày
        );
        res.json({ accessToken: token, 'role': user.role });
    });
});
// **2. Middleware để xác thực token**
const authenticateToken = (req, res, next) => {
    //console.log(req.header('Authorization'))
    const token = req.header('Authorization')?.split(' ')[1] // Lấy token từ header Authorization
    //console.log(token);
    if (!token) return res.status(401).json({ message: 'Access token required' });
    // Kiểm tra token có nằm trong blacklist không
    if (blacklistedTokens.includes(token)) {
        return res.status(401).json({ message: 'Token đã hết hạn hoặc bị thu hồi' });
    }
    jwt.verify(token, SECRET_KEY, (err, user) => {
        if (err) return res.status(403).json({ message: 'Invalid or expired token' });
        req.user = user; // Gắn thông tin user vào request
        next();
    });
};
//** logout
app.post('/logout', authenticateToken, (req, res) => {
    const token = req.header('Authorization')?.split(' ')[1];
    // Thêm token vào blacklist
    blacklistedTokens.push(token);
    res.status(200).json({ message: 'Đăng xuất thành công' });
});
//**1b thêm tài khoản đăng nhập
app.post('/add-account', authenticateToken, async (req, res) => {
    const { userName, pass, role } = req.body;
    if (!userName || !pass || !role) {
        return res.status(400).json({ message: 'Missing required fields', code: -1 });
    }
    if (userName.length < 1) {
        return res.status(400).json({ message: 'Tên quá ngắn', code: -2 });
    }
    if (pass.length < 1) {
        return res.status(400).json({ message: 'Kiểm tra lại ngày tháng', code: -2 });
    }
    db.run('INSERT INTO users (username,password,role) VALUES (?,?,?)', [userName, pass, role], (updateErr) => {
        if (updateErr) {
            return res.status(500).json({ message: 'Có lỗi xảy ra!', code: -3 });
        }
        res.status(200).json({ message: `Thêm thành công ${userName}`, code: 0 });
    });
});
//**3 thêm thành viên: tên, tuổi, địa chỉ
app.post('/add-mem', authenticateToken, async (req, res) => {
    //const { username } = req.user; // Lấy username từ token
    const { name, date_of_birth, address } = req.body;
    if (!name || !date_of_birth || !address) {
        return res.status(400).json({ message: 'Missing required fields', code: -1 });
    }
    if (name.length < 1) {
        return res.status(400).json({ message: 'Tên quá ngắn', code: -2 });
    }
    if (date_of_birth.length < 1) {
        return res.status(400).json({ message: 'Kiểm tra lại ngày tháng', code: -2 });
    }
    db.run('INSERT INTO members (name,date_of_birth,address) VALUES (?,?,?)', [name, date_of_birth, address], (updateErr) => {
        if (updateErr) {
            return res.status(500).json({ message: 'Có lỗi xảy ra!', code: -3 });
        }
        res.status(200).json({ message: `Thêm thành công ${name}`, code: 0 });
    });
});
//**5 đổi password
app.post('/change-password', authenticateToken, async (req, res) => {
    const { username } = req.user; // Lấy username từ token
    const { oldPassword, newPassword } = req.body;
    if (!oldPassword || !newPassword) {
        return res.status(400).json({ message: 'Missing required fields', code: -1 });
    }
    if (oldPassword == newPassword) {
        return res.status(400).json({ message: 'Mật khẩu mới phải khác mật khẩu cũ', code: -1 });
    }
    db.get('SELECT password FROM users WHERE username = ?', [username], async (err, row) => {
        if (err) {
            return res.status(500).json({ message: 'Database error', code: -3 });
        }
        if (!row) {
            return res.status(404).json({ message: 'Không tìm thấy user', code: -4 });
        }
        const match = await bcrypt.compare(oldPassword, row.password);
        if (!match) {
            return res.status(401).json({ message: 'Mật khẩu cũ không đúng', code: -5 });
        }
        if (newPassword.length < 6) {
            return res.status(400).json({ message: 'Độ dài tối thiểu 6 kí tự', code: -2 });
        }
        const hashedPassword = await bcrypt.hash(newPassword, 10);
        db.run('UPDATE users SET password = ? WHERE username = ?', [hashedPassword, username], (updateErr) => {
            if (updateErr) {
                return res.status(500).json({ message: 'Có lỗi, không thể cập nhật!', code: -3 });
            }

            res.status(200).json({ message: 'Đổi thành công', code: 0 });
        });
    });
});
//**6 đổi tên
app.post('/change-name', authenticateToken, async (req, res) => {
    const { id, newName } = req.body;
    if (!id || !newName) {
        return res.status(400).json({ message: 'Missing required fields', code: -1 });
    }
    db.get('SELECT name FROM members WHERE id = ?', [id], async (err, user) => {
        if (err) {
            return res.status(500).json({ message: 'Database error', code: -3 });
        }
        if (!user) {
            return res.status(404).json({ message: 'Không tìm thấy user id = ' + id, code: -4 });
        }
        if (newName.length < 1) {
            return res.status(400).json({ message: 'Độ dài tối thiểu 1 kí tự', code: -2 });
        }
        db.run('UPDATE members SET name = ? WHERE id = ?', [newName, id], (updateErr) => {
            if (updateErr) {
                return res.status(500).json({ message: 'Có lỗi, không thể cập nhật!', code: -3 });
            }
            res.status(200).json({ message: `Đổi thành công ${user.name} -> ${newName}`, code: 0 });
        });
    });
});
//**6 đổi năm sinh
app.post('/change-dob', authenticateToken, async (req, res) => {
    const { id, newDayOfBirth } = req.body;
    if (!id || !newDayOfBirth) {
        return res.status(400).json({ message: 'Missing required fields', code: -1 });
    }
    db.get('SELECT date_of_birth FROM members WHERE id = ?', [id], async (err, user) => {
        if (err) {
            return res.status(500).json({ message: 'Database error', code: -3 });
        }
        if (!user) {
            return res.status(404).json({ message: 'Không tìm thấy user id = ' + id, code: -4 });
        }
        db.run('UPDATE members SET date_of_birth = ? WHERE id = ?', [newDayOfBirth, id], (updateErr) => {
            if (updateErr) {
                return res.status(500).json({ message: 'Có lỗi, không thể cập nhật!', code: -3 });
            }
            res.status(200).json({ message: `Đổi thành công ${user.date_of_birth} -> ${newDayOfBirth}`, code: 0 });
        });
    });
});
//**3 đổi địa chỉ
app.post('/change-address', authenticateToken, async (req, res) => {
    const { id, newAddress } = req.body;
    if (!id || !newAddress) {
        return res.status(400).json({ message: 'Missing required fields', code: -1 });
    }
    db.get('SELECT address FROM members WHERE id = ?', [id], async (err, user) => {
        if (err) {
            return res.status(500).json({ message: 'Database error', code: -3 });
        }
        if (!user) {
            return res.status(404).json({ message: 'Không tìm thấy user id = ' + id, code: -4 });
        }
        db.run('UPDATE members SET address = ? WHERE id = ?', [newAddress, id], (updateErr) => {
            if (updateErr) {
                return res.status(500).json({ message: 'Có lỗi, không thể cập nhật!', code: -3 });
            }
            res.status(200).json({ message: `Đổi thành công ${user.address} -> ${newAddress}`, code: 0 });
        });
    });
});
//**3 xoá thành viên
app.delete('/del-members/:id', authenticateToken, (req, res) => {
    const userId = req.params.id;
    // Kiểm tra xem user có tồn tại không
    db.get('SELECT * FROM members WHERE id = ?', [userId], (err, user) => {
        if (err) {
            return res.status(500).json({ message: 'Lỗi cơ sở dữ liệu', error: err.message });
        }
        if (!user) {
            return res.status(404).json({ message: 'User không tồn tại' });
        }
        res.status(200).json({ message: `Đã xóa user với ID ${userId}`, affectedRows: this.changes });
        // Xóa user
        db.run('DELETE FROM members WHERE id = ?', [userId], function (err) {
            if (err) {
                return res.status(500).json({ message: 'Không thể xóa user', error: err.message });
            }
        });
    });
});
//** lấy danh sách thành viên
app.get('/members', authenticateToken, (req, res) => {
    db.all('SELECT * FROM members ', [], (err, users) => {
        if (err) {
            return res.status(500).json({ message: 'Lỗi cơ sở dữ liệu', error: err.message });
        }
        res.status(200).json({ count: users.length, members: users });
    });
});

// Khởi động server
const PORT = 3001;
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
