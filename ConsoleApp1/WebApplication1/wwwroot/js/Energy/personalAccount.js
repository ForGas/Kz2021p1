const accountLink = document.getElementsByClassName('account-link');
const accpuntLinkText = document.getElementsByClassName('accpunt-link-text');
const debt = document.getElementById('balance-id');
const url = window.location.origin;
var data = [];


document.addEventListener('DOMContentLoaded', debtChanged, false);
debt.addEventListener('DOMSubtreeModified', debtChanged, false);

Array.from(accountLink).forEach(element =>
    element.addEventListener('click', function (e) {

        Array.from(accountLink).forEach(
            element => element.classList.remove('active'),
        );

        this.classList.add('active');

        getPersonalAccount(e.target.textContent);
        changeAttribute(e.target.textContent);
    })
);

function changeAttribute(textContent) {
    const bDelete = document.getElementById('b-delete');
    let fullPathAction = bDelete.getAttribute('href');

    let accountNumber = textContent.replace(/[^\d]/g, '');
    let attribute = fullPathAction.replace(/[1-9][0-9]*/, '');

    bDelete.setAttribute('href', attribute + accountNumber);

    console.log(bDelete);
}

function debtChanged() {
    const balanceDetails = document.getElementById('balance-details');

    if (!(Number.isNaN(debt.textContent))) {
        let balance = Number.parseInt(debt.textContent);

        if (balance < 0) {
            balanceDetails.innerHTML = `Задолженность:`;
        } else if (balance > 0) {
            balanceDetails.innerHTML = `Переплата:`;
        } else {
            balanceDetails.innerHTML = '';
        }
    }

}

function getPersonalAccount(textContent) {

    let accountNumber = textContent.replace(/[^\d]/g, '');
    console.log(accountNumber);

    fetch(`${url}/api/PersonalAccountApi`,
    {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify(accountNumber)
    })
    .then(responce => responce.ok ? responce.json() : Promise.reject(responce))
    .then((result) => {
        data = result;
        console.log(data);
        updatePersonalAccount(data);
    })
    .catch((result) => {
        console.log(result);
        console.log(result.status);
    })

}

function updatePersonalAccount(data) {
    const balance = document.getElementById('balance-id');
    const tariff = document.getElementById('tariff');
    const address = document.getElementById('address');
    const minTariff = 1;
    const manTariff = 2;

    balance.innerHTML = `${data.debt}`;
    address.innerHTML = `ул. ${data.address}`;

    if (data.rate == minTariff) {
        tariff.innerHTML = `Min`;
    } else if (data.rate == manTariff) {
        tariff.innerHTML = `Max`;
    } else {
        tariff.innerHTML = '';
    }
}