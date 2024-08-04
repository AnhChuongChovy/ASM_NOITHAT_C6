// Lưu thông tin người dùng vào localStorage
localStorage.setItem('user', JSON.stringify(user));

// Lấy thông tin người dùng từ localStorage
const user = JSON.parse(localStorage.getItem('user'));

// Xóa thông tin người dùng khỏi localStorage
localStorage.removeItem('user');

//function saveUser(user) {
//    localStorage.setItem('user', JSON.stringify(user));
//}

//function getUser() {
//    return JSON.parse(localStorage.getItem('user'));
//}

//function clearUser() {
//    localStorage.removeItem('user');
//}