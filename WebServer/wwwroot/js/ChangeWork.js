import * as Toasties from "../js/common.js";
const organizationType = document.querySelector('#organizationType');
const name = document.querySelector('#name');
const description = document.querySelector('#description');
const date = document.querySelector('#date');
const paramsString = document.location.search; // ?page=4&limit=10&sortby=desc
const searchParams = new URLSearchParams(paramsString);
document.querySelector('#saveBtn').addEventListener('click', onSaveClick);
document.querySelector('#reloadBtn').addEventListener('click', function () {
    window.location = '';
})

let id = searchParams.get("id"); // 4
let userId = searchParams.get("user"); // 4
let modal = new bootstrap.Modal(document.getElementById('operation_success'), { backdrop: true, keyboard: true, focus: true });

document.getElementById('operation_success').addEventListener('hidden.bs.modal', function (event) {
    window.location = `Works?id=${userId}`;
})

document.getElementById('confirmBtn').addEventListener('click', function () {
    modal.toggle();
    window.location = `Works?id=${userId}`;
})

async function onSaveClick() {
    let data = JSON.stringify({
        "id": id,
        "organizationType": organizationType.value,
        "name": name.value,
        "description": description.value,
        "date": date.value,
        "user": userId
    });
    if (id !== "-1") {
        let url = 'https://localhost:7113/Work/UpdateWork';
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
        let url = 'https://localhost:7113/Work/AddWork';
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
