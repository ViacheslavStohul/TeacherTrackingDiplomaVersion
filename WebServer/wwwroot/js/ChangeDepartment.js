import * as Toasties from "../js/common.js";
const name = document.querySelector('#name');
const abbr = document.querySelector('#abbreviation');
const director = document.querySelector('#director');
const paramsString = document.location.search; // ?page=4&limit=10&sortby=desc
const searchParams = new URLSearchParams(paramsString);
document.querySelector('#saveBtn').addEventListener('click', onSaveClick);
document.querySelector('#reloadBtn').addEventListener('click', function () {
    window.location.reload();
})

let id = searchParams.get("id"); // 4
let modal = new bootstrap.Modal(document.getElementById('operation_success'), { backdrop: true, keyboard: true, focus: true });

document.getElementById('operation_success').addEventListener('hidden.bs.modal', function (event) {
    window.location = "Departments";
})

document.getElementById('confirmBtn').addEventListener('click', function () {
    modal.toggle();
    window.location = "Departments";
})

async function onSaveClick() {
    let data = JSON.stringify({
        "id": id,
        "name": name.value,
        "abbreviation": abbr.value,
        "head": director.value
    });
    if (id !== "-1") {
        let url = 'https://localhost:7113/Department/UpdateDepartment';
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
        let url = 'https://localhost:7113/Department/AddDepartment';
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
