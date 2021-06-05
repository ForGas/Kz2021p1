const url = window.location.origin;
var data = [];

function handleChange() {
    const selectObject = document.getElementById('select_district').value;
    console.log(selectObject);

    if (selectObject == 'All districts') {

        fetch(`${url}/api/BuildingApi`,
            { method: 'GET' }
        )
            .then(responce => responce.ok ? responce.json() : Promise.reject(responce))
            .then((result) => {
                data = result;
                console.log(data);
                updateTable(data);
            })
            .catch((result) => {
                console.log(result);
                console.log(result.status);
            })

    } else {

        fetch(`${url}/api/BuildingApi`,
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify(selectObject)
            })
            .then(responce => responce.ok ? responce.json() : Promise.reject(responce))
            .then((result) => {
                data = result;
                console.log(data);
                updateTable(data);
            })
            .catch((result) => {
                console.log(result);
                console.log(result.status);
            })
    }

};


function updateTable(data) {
    let table = document.getElementById('table');
    let tbody = table.children[0];
    let rowCount = table.rows.length;

    console.log(data);

    for (let i = 0; i + 1 < rowCount; i++) {
        table.deleteRow(0 + 1);
    }

    for (let i = 0; i < data.length; i++) {
        console.log("for")
        tbody.innerHTML += `<tr>
                <th scope="row">${data[i].id}</th>
                <td>${data[i].houseNumber}</td>
                <td>${data[i].street}</td>
                <td>${data[i].floorCount}</td>
                <td>${data[i].districtName}</td>
                <td>${data[i].totalArea}</td>
                <td>${data[i].consumption}</td>
                <td>${data[i].totalDebt}</td>
            </tr>`;
    }

}