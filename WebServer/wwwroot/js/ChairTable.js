function AddButton() {
    window.location = `ChangeChair?id=${-1}`;
}
let deleteModal = new bootstrap.Modal(document.getElementById('deleteBackDrop'), { backdrop: true, keyboard: true, focus: true });

let _Id = 0;

function DeleteButton(id) {
    _Id = id;
    deleteModal.toggle();
}

document.getElementById('confirmDeleteBtn').addEventListener("click", function (e) {
    DeleteCommission();
});

async function DeleteCommission() {
    deleteModal.toggle();
    const query = await fetch(`https://localhost:7113/Chair/DeleteChair?id=${_Id}`);
    if (query.status !== 200) {
        const response = await query.text();
        callToast(false, response);
    }
    else
        window.location.reload();
}
function EditButton(id) {
    window.location = `ChangeChair?id=${id}`;
}

function callToast(headerresult, message) {
    let toastget = "<div class=\"toast \" style=\"background-color: transparent; backdrop-filter: blur(25px); border-radius:20px; box-shadow: 0px 0px 3px 0 white;\">" +
      "<div class=\"toast-header text-white \" style=\"background-color: transparent;\">" +
      "<p style=\"margin-bottom: 0;padding-top:0.25rem;padding-bottom:0.25rem;\">" + ((headerresult === true) ? "Операція успішна" : "Виникла помилка") + "</p>" +
      "<button type=\"button\" class=\"btn-close btn-close-white me-2 m-auto\" data-bs-dismiss=\"toast\" aria-label=\"Close\"></button>" +
      "</div>" +
      "<div class=\"toast-body\" style=\"color: white;\">" + message +
      "</div></div>";
    document.querySelector('.toast-container').innerHTML = toastget;
    new bootstrap.Toast(document.querySelector('.toast'), { delay: 2000, animation: true, autohide: true }).show();
    
    if (headerresult === true) {
      document.querySelector('.toast').style.boxShadow = '0px 0px 10px green';
    } else {
      document.querySelector('.toast').style.boxShadow = '0px 0px 10px red';
    }
}
