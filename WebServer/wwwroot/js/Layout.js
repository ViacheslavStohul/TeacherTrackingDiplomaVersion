import * as Cookies from "../js/common.js";
let userInfo = null;
let navop=false;

Cookies.checkCookie();
let exitModal = new bootstrap.Modal(document.getElementById('staticBackdrop'), { backdrop: true, keyboard: true, focus: true });
let loadModal = new bootstrap.Modal(document.getElementById('loadModal'), { backdrop: true, keyboard: true, focus: true });

document.getElementById('exitbtn').addEventListener("click", function (e) {
    exitModal.toggle();
});

document.getElementById('totaySuperPuperExitBtn').addEventListener("click", function (e) {
    exitModal.toggle();
     let id = JSON.parse(Cookies.getCookie("user")).idUserInfo
     Cookies.deleteCookie("user");
     window.location = `Exit?id=${id}`;
});
try {
    document.getElementById('userbtn').addEventListener("click", function () {
        window.location = "Users";
    });
}
catch { };

try {
    document.getElementById('comissionbtn').addEventListener("click", function () {
        window.location = "Commissions";
    });
}
catch { };

try {
    document.getElementById('departbtn').addEventListener("click", function () {
        window.location = "Departments";
    });
}
catch { };

try {
    document.getElementById('chairbtn').addEventListener("click", function () {
        window.location = "Chairs";
    });
}
catch { };
//sidebar

document.querySelector('#mainbtn').addEventListener("click", function() {
    window.location = "Index";
})

document.querySelector('body,html').addEventListener("click", function (e) {
    if (navop === true) {
        document.querySelector('.menu').classList.remove('menu_active');
        document.querySelector('.menu-btn span').classList.remove('activabutt');
        document.getElementById("logoimg").style.position = "sticky";
        navop = false;
    }
});
document.querySelector('.menu-btn').addEventListener("click", function (e) {
    e.stopPropagation();
    if (!navop) {
        document.querySelector('.menu').classList.add('menu_active');
        navop = true;
    }
    else {
        document.querySelector('.menu').classList.remove('menu_active');
        navop = false;
}

    if (document.getElementById("logoimg").style.position === "fixed") {
        document.querySelector('.menu-btn span').classList.remove('activabutt');
        document.getElementById("logoimg").style.position = "sticky";
    }
    else {
        document.querySelector('.menu-btn span').classList.add('activabutt');
        document.getElementById("logoimg").style.position = "fixed";
    }
});
document.querySelector('.menu').addEventListener("click", function (e) {
    e.stopPropagation();
});

    $(document).ready(function(){
        $('.Smarttable').dataTable({
            "language": {
                "lengthMenu": "",
                "zeroRecords": "Нічого не знайдено",
                "info": "Сторінка _PAGE_ з _PAGES_",
                "infoEmpty": "Даних немає",
                "search": "Пошук",
                "paginate": {
                    "first": "Перший",
                    "last": "Останній",
                    "next": "Далі",
                    "previous": "Назад"
                },
                "infoFiltered": "(Відібрано з _MAX_ записів)"
            },
        });
        document.querySelector("#DataTables_Table_0_wrapper > div:nth-child(1) > div:nth-child(2)").style.display = "flex";
        document.querySelector("#DataTables_Table_0_wrapper > div:nth-child(1) > div:nth-child(2)").style.justifyContent = "right";
        document.querySelector("#DataTables_Table_0_filter > label").style.padding = "0px 20px 0px 0px";
        document.querySelector("#DataTables_Table_0_filter > label").style.width = "300px";
});



