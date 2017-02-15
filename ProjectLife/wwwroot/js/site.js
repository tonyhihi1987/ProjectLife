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
    $("div.task").removeClass("active");
    selector.addClass("active");
    var target = $(e.target);
    if (target.hasClass("fa-to-be-deleted")) {
        $("div.task.active").remove();
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
function saveNewProject(e) {

    e.preventDefault();

    var formData = initFormData();

    $.ajax({
        contentType: false,
        processData: false,
        method: "POST",
        url: "Project/Add",
        data: formData
    })
            .success(function (result) {
                window.location.href = result;
            })
            .error(function (error) {
                alert("Error");
            })
}

function updateProject(e) {

    e.preventDefault();

    var formData = initFormData(true);

    $.ajax({
        contentType: false,
        processData: false,
        method: "POST",
        url: "Project/Update",
        data: formData
    })
            .success(function (result) {
                window.location.href = result;
            })
            .error(function (error) {
                alert("Error");
            })

}

function initFormData(update){

    var formData = new FormData($("#project-form")[0]);
    formData.append("File", $("#file").get(0).files[0]);    
    var tasks = [];
    var i = 0;
    $("div.task").each(function () {
        
        var Name = $(this).find(".col-md-6 input").val();
        if (Name.trim() !== "") {
            var Id = $(this).find("#item_Id").val() ? $(this).find("#item_Id").val() : 0;
            var ProjectId = $("#Id").val();
            var IsDone = !$(this).find(".col-md-4 input").prop('checked');
            formData.append('Tasks[' + i + '].Id', Id);

            formData.append('Tasks[' + i + '].Name', Name);
            formData.append('Tasks[' + i + '].ProjectId', ProjectId);
            formData.append('Tasks[' + i + '].IsDone', IsDone);
        }
        i++;

    })
    return formData;
}