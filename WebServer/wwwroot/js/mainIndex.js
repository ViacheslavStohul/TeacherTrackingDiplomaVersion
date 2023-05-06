import * as Toasties from "../js/common.js";
const f_name = document.querySelector('#first_name');
const s_name = document.querySelector('#second_name');
const m_name = document.querySelector('#middle_name');
const phone = document.querySelector('#phone');
const email = document.querySelector('#email');
document.querySelector('#saveBtn').addEventListener('click', onSaveClick);
document.querySelector('#reloadBtn').addEventListener('click', function () {
    window.location.reload();
})




async function onSaveClick() {
    let data = JSON.stringify({
        "firstName": f_name.value,
        "secondName": s_name.value,
        "middleName": m_name.value,
        "phone": phone.value,
        "email": email.value
    });
    let url = 'https://localhost:7113/User/ChangeUserBasic';
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: data
    })
        .then(function(response) {
            if (response.ok) {
                Toasties.callToast(true, "Зміни внесено");
            } else {
                response.text()
                    .then(function (errorMessage) {
                        Toasties.callToast(false, errorMessage);
                    });
            }
        });
}
