

function setModal(projectId) {
    $.ajax({                
        url: "Project/Update",
        data: { id: projectId }
    })
            .success(function (result) {
                $("#modal").html(result);
                $("#myModal").modal();
            })
            .error(function (error) {
                alert("Error");
            })
}

function setActive(selector,e) {
    $(".task").removeClass("active");
    selector.addClass("active");
    var target = $(e.target);
    if (target.hasClass("fa-to-be-deleted")) {
        $(".task.active").empty();
    }

}

function addTask(e) {
    e.preventDefault();
    var target = $(e.target);
    if (target.hasClass("fa fa-plus"))
    {
        $("#rowTask").append($("#newTask").html());
        $(e.target).removeClass();
        $(e.target).addClass("btn btn-primary btn-task fa fa-times");
    }
    else {
        $(e.target).addClass("btn btn-primary btn-task fa fa-to-be-deleted");
    }

}