
function AddButton() {
    window.location = `ChangeUser?id=${-1}`;
}
let deleteModal = new bootstrap.Modal(document.getElementById('deleteBackDrop'), { backdrop: true, keyboard: true, focus: true });
let restoreModal = new bootstrap.Modal(document.getElementById('restoreModal'), { backdrop: true, keyboard: true, focus: true });
let Id = 0;

document.getElementById('confirmDeleteBtn').addEventListener("click", function (e) {
    DeleteUser();
});

document.getElementById('restoreConfirm').addEventListener("click", function (e) {
    ActivateUser();
});


//document.getElementById('deleteBackDrop').addEventListener('hidden.bs.modal', function (event) {
//    alert("Закриття");
//})

async function DeleteUser() {
    deleteModal.toggle();
    const query = await fetch(`https://localhost:7113/User/DeleteUser?id=${Id}`);
    if (query.status !== 200) {
        const response = await query.text();
        callToast(false, response);
    }
    else
        window.location.reload();
}
async function ActivateUser() {
    const query = await fetch(`https://localhost:7113/User/ActivateUser?id=${Id}&login=${document.querySelector("#username").value}&password=${document.querySelector("#password").value}`);
    restoreModal.toggle();
    if (query.status !== 200) {
        const response = await query.text();
        callToast(false, response);
    }
    else
        window.location.reload();
}
function EditButton(id) {
    window.location = `ChangeUser?id=${id}`;
}
const DeleteButton = id => {
    Id = id;
    deleteModal.toggle();
};
function RestoreButton(id) {
    Id = id;
    restoreModal.toggle();
}

function callToast(headerresult, message) {
    let toastget = "<div class=\"toast \" style=\"background-color:white;border-radius:13px;\">" +
        "<div class=\"toast-header text-white \" style=\"border-radius:10px 10px 0 0;background-color:" + ((headerresult === true) ? "#2C9AD8" : "#DC3545") + "\">" +
        "<p style=\"margin-bottom: 0;padding-top:0.25rem;padding-bottom:0.25rem;\">" + ((headerresult === true) ? "Операція успішна" : "Виникла помилка") + "</p>" +
        "<button type=\"button\" class=\"btn-close btn-close-white me-2 m-auto\" data-bs-dismiss=\"toast\" aria-label=\"Close\"></button>" +
        "</div>" +
        "<div class=\"toast-body\">" + message +
        "</div></div>";
    document.querySelector('.toast-container').innerHTML = toastget;
    new bootstrap.Toast(document.querySelector('.toast'), { delay: 2000, animation: true, autohide: true }).show();

}
