function createbtn(id) {
    console.log(id);
    $.ajax({
        type: "Get",
        url: "/Admin/GroupProduct/Create/" + id
    }).done(function (res) {
        $("#MyModal").modal();
        $("#modalbody").html(res);
    });
}

function Editbtn(id) {
    $.ajax({
        type: "Get",
        url: "/Admin/GroupProduct/Edit/" + id
    }).done(function (res) {
        $("#MyModal").modal();
        $("#modalbody").html(res);
    });
}

function deletebtn(slug) {
    $.ajax({
        type: "Get",
        url: "/Admin/GroupProduct/Delete/" + slug

    }).done(function (res) {
        $("#MyModal").modal();
        $("#modalbody").html(res);
    });
}