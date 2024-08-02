//document.addEventListener('DOMContentLoaded', () => {
//    const provinceSelect = document.getElementById('province');
//    const districtSelect = document.getElementById('district');

//    const data = {
//        "An Giang": ["Thành phố Long Xuyên", "Thành phố Châu Đốc", "Huyện An Phú"],
//        "Bà Rịa - Vũng Tàu": ["Thành phố Vũng Tàu", "Thành phố Bà Rịa", "Huyện Châu Đức"],
//        "Bắc Giang": ["Thành phố Bắc Giang", "Huyện Yên Thế", "Huyện Tân Yên"],
//        "Bắc Kạn": ["Thành phố Bắc Kạn", "Huyện Ba Bể", "Huyện Bạch Thông"],
//        "Bạc Liêu": ["Thành phố Bạc Liêu", "Huyện Đông Hải", "Huyện Hòa Bình"],
//        "Bắc Ninh": ["Thành phố Bắc Ninh", "Huyện Gia Bình", "Huyện Lương Tài"],
//        "Bến Tre": ["Thành phố Bến Tre", "Huyện Ba Tri", "Huyện Bình Đại"],
//        "Bình Định": ["Thành phố Quy Nhơn", "Huyện An Lão", "Huyện Hoài Ân"],
//        "Bình Dương": ["Thành phố Thủ Dầu Một", "Thị xã Bến Cát", "Thị xã Tân Uyên"],
//        "Bình Phước": ["Thành phố Đồng Xoài", "Thị xã Bình Long", "Thị xã Phước Long"],
//        "Bình Thuận": ["Thành phố Phan Thiết", "Thị xã La Gi", "Huyện Bắc Bình"],
//        "Cà Mau": ["Thành phố Cà Mau", "Huyện Cái Nước", "Huyện Đầm Dơi"],
//        "Cần Thơ": ["Quận Ninh Kiều", "Quận Bình Thủy", "Quận Cái Răng"],
//        "Cao Bằng": ["Thành phố Cao Bằng", "Huyện Bảo Lạc", "Huyện Bảo Lâm"],
//        "Đà Nẵng": ["Quận Hải Châu", "Quận Thanh Khê", "Quận Sơn Trà"],
//        "Đắk Lắk": ["Thành phố Buôn Ma Thuột", "Thị Xã Buôn Hồ", "Huyện Cư Kuin"],
//        "Đắk Nông": ["Thành phố Gia Nghĩa", "Huyện Cư Jút", "Huyện Đắk Glong"],
//        "Điện Biên": ["Thành phố Điện Biên Phủ", "Thị Xã Mường Lay", "Huyện Điện Biên"],
//        "Đồng Nai": ["Thành phố Biên Hòa", "Thành phố Long Khánh", "Huyện Cẩm Mỹ"],
//        "Đồng Tháp": ["Thành phố Cao Lãnh", "Thành phố Sa Đéc", "Thành phố Hồng Ngự"],
//        "Gia Lai": ["Thành phố Pleiku", "Thị xã An Khê", "Thị xã Ayun Pa"],
//        "Hà Giang": ["Thành phố Hà Giang", "Huyện Bắc Mê", "Huyện Bắc Quang"],
//        "Hà Nam": ["Thành phố Phủ Lý", "Thị xã Duy Tiên", "Huyện Bình Lục"],
//        "Hà Nội": ["Quận Ba Đình", "Quận Hoàn Kiếm", "Quận Tây Hồ"],
//        "Hà Tĩnh": ["Thành phố Hà Tĩnh", "Thị xã Hồng Lĩnh", "Thị xã Kỳ Anh"],
//        "Hải Dương": ["Thành phố Hải Dương", "Thị xã Kinh Môn", "Huyện Bình Giang"],
//        "Hải Phòng": ["Quận Hồng Bàng", "Quận Ngô Quyền", "Quận Lê Chân"],
//        "Hậu Giang": ["Thành phố Vị Thanh", "Thành phố Ngã Bảy", "Huyện Châu Thành"],
//        "Hòa Bình": ["Thành phố Hòa Bình", "Huyện Cao Phong", "Huyện Đà Bắc"],
//        "Hưng Yên": ["Thành phố Hưng Yên", "Thị xã Mỹ Hào", "Huyện Ân Thi"],
//        "Khánh Hòa": ["Thành phố Nha Trang", "Thành phố Cam Ranh", "Thị xã Ninh Hòa"],
//        "Kiên Giang": ["Thành phố Rạch Giá", "Thành phố Hà Tiên", "Huyện An Biên"],
//        "Kon Tum": ["Thành phố Kon Tum", "Huyện Đắk Glei", "Huyện Đắk Hà"],
//        "Lai Châu": ["Thành phố Lai Châu", "Huyện Mường Tè", "Huyện Nậm Nhùn"],
//        "Lâm Đồng": ["Thành phố Đà Lạt", "Thành phố Bảo Lộc", "Huyện Bảo Lâm"],
//        "Lạng Sơn": ["Thành phố Lạng Sơn", "Huyện Bắc Sơn", "Huyện Bình Gia"],
//        "Lào Cai": ["Thành phố Lào Cai", "Thị xã Sa Pa", "Huyện Bảo Thắng"],
//        "Long An": ["Thành phố Tân An", "Thị xã Kiến Tường", "Huyện Bến Lức"],
//        "Nam Định": ["Thành phố Nam Định", "Huyện Giao Thủy", "Huyện Hải Hậu"],
//        "Nghệ An": ["Thành phố Vinh", "Thị xã Cửa Lò", "Thị xã Hoàng Mai"],
//        "Ninh Bình": ["Thành phố Ninh Bình", "Thành phố Tam Điệp", "Huyện Gia Viễn"],
//        "Ninh Thuận": ["Thành phố Phan Rang-Tháp Chàm", "Huyện Bác Ái", "Huyện Ninh Hải"],
//        "Phú Thọ": ["Thành phố Việt Trì", "Thị xã Phú Thọ", "Huyện Cẩm Khê"],
//        "Phú Yên": ["Thành phố Tuy Hòa", "Thị xã Sông Cầu", "Huyện Đông Hòa"],
//        "Quảng Bình": ["Thành phố Đồng Hới", "Thị xã Ba Đồn", "Huyện Bố Trạch"],
//        "Quảng Nam": ["Thành phố Tam Kỳ", "Thành phố Hội An", "Thị xã Điện Bàn"],
//        "Quảng Ngãi": ["Thành phố Quảng Ngãi", "Thị xã Đức Phổ", "Huyện Ba Tơ"],
//        "Quảng Ninh": ["Thành phố Hạ Long", "Thành phố Móng Cái", "Thành phố Cẩm Phả"],
//        "Quảng Trị": ["Thành phố Đông Hà", "Thị xã Quảng Trị", "Huyện Cam Lộ"],
//        "Sóc Trăng": ["Thành phố Sóc Trăng", "Thị xã Vĩnh Châu", "Thị xã Ngã Năm"],
//        "Sơn La": ["Thành phố Sơn La", "Huyện Bắc Yên", "Huyện Mai Sơn"],
//        "Tây Ninh": ["Thành phố Tây Ninh", "Thị xã Hòa Thành", "Thị xã Trảng Bàng"],
//        "Thái Bình": ["Thành phố Thái Bình", "Huyện Đông Hưng", "Huyện Hưng Hà"],
//        "Thái Nguyên": ["Thành phố Thái Nguyên", "Thành phố Sông Công", "Thị xã Phổ Yên"],
//        "Thanh Hóa": ["Thành phố Thanh Hóa", "Thị xã Bỉm Sơn", "Thị xã Nghi Sơn"],
//        "Thừa Thiên Huế": ["Thành phố Huế", "Thị xã Hương Thủy", "Thị xã Hương Trà"],
//        "Tiền Giang": ["Thành phố Mỹ Tho", "Thị xã Cai Lậy", "Thị xã Gò Công"],
//        "Trà Vinh": ["Thành phố Trà Vinh", "Thị xã Duyên Hải", "Huyện Càng Long"],
//        "Tuyên Quang": ["Thành phố Tuyên Quang", "Huyện Chiêm Hóa", "Huyện Hàm Yên"],
//        "Vĩnh Long": ["Thành phố Vĩnh Long", "Thị xã Bình Minh", "Huyện Bình Tân"],
//        "Vĩnh Phúc": ["Thành phố Vĩnh Yên", "Thành phố Phúc Yên", "Huyện Bình Xuyên"],
//        "Yên Bái": ["Thành phố Yên Bái", "Thị xã Nghĩa Lộ", "Huyện Lục Yên"]
//    };

//    for (const province in data) {
//        const option = document.createElement('option');
//        option.value = province;
//        option.textContent = province;
//        provinceSelect.appendChild(option);
//    }

//    provinceSelect.addEventListener('change', (event) => {
//        const selectedProvince = event.target.value;
//        districtSelect.innerHTML = '<option value="">--Chọn Huyện--</option>';

//        if (selectedProvince) {
//            districtSelect.disabled = false;
//            data[selectedProvince].forEach(district => {
//                const option = document.createElement('option');+
//                option.value = district;
//                option.textContent = district;
//                districtSelect.appendChild(option);
//            });
//        } else {
//            districtSelect.disabled = true;
//        }
//    });
//});
