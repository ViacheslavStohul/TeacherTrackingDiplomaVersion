import * as Toasties from "../js/common.js";
const surname = document.querySelector('#second_name');
const name = document.querySelector('#first_name');
const middlename = document.querySelector('#middle_name');
const email = document.querySelector('#email');
const phone = document.querySelector('#phone');
const rank = document.querySelector('#rank');
const commission = document.querySelector('#commission');
const chair = document.querySelector('#chair');
const access = document.querySelector('#category_access');
const workType = document.querySelector('#workType');
const paramsString = document.location.search; // ?page=4&limit=10&sortby=desc
const searchParams = new URLSearchParams(paramsString);
document.querySelector('#saveBtn').addEventListener('click', onSaveClick);
document.querySelector('#reloadBtn').addEventListener('click', function () {
    window.location = '';
})

let id = searchParams.get("id"); // 4
let modal = new bootstrap.Modal(document.getElementById('operation_success'), { backdrop: true, keyboard: true, focus: true });

document.getElementById('operation_success').addEventListener('hidden.bs.modal', function (event) {
    window.location = "Users";
})

document.getElementById('confirmBtn').addEventListener('click', function () {
    modal.toggle();
    window.location = "Users";
})

async function onSaveClick() {
    let data = JSON.stringify({
        "id": id,
        "firstName": name.value,
        "secondName": surname.value,
        "middleName": middlename.value,
        "email": email.value,
        "phone": phone.value,
        "workType": workType.value,
        "rank": rank.value,
        "chair": chair.value,
        "comission": commission.value,
        "department": "dep",
        "accessLevel": access.value
    });
    if (id !== "-1") {
        let url = 'https://localhost:7113/User/ChangeUserAdmin';
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: data
        })
            .then(function(response) {
                if (response.ok) {
                    modal.toggle();
                } else {
                    response.text()
                        .then(function (errorMessage) {
                            Toasties.callToast(false, errorMessage);
                        });
                }
            });
    }
    else {
        let url = 'https://localhost:7113/User/AddUser';
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: data
        })
            .then(function(response) {
                if (response.ok) {
                    modal.toggle();
                } else {
                    response.text()
                        .then(function (errorMessage) {
                            Toasties.callToast(false, errorMessage);
                        });
                }
            });
    }
}
