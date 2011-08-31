$(document).ready(function () {
    $(".draggable").draggable({
        revert: "invalid",
        cursorAt: { cursor: "crosshair", top: -5, left: -5 },
        start: function () {
            $(this).addClass("being-dragged");   // change the dimensions to make the draggable fit
        },
        stop: function () {
            $(this).removeClass("being-dragged");
        }
    });
    $(".droppable").droppable({
        accept: ".draggable",
        drop: function (event, ui) {
            // here we assing the story to a release

            var storyId = $(ui.draggable).attr('storyid');
            var releaseId = $(this).attr('releaseid');

            $.ajax({
                type: 'POST',
                url: '/Release/Ajax',
                data: { 'releaseId': releaseId, 'storyId': storyId },
                dataType: 'json',
                success: function (data) {
                    $(ui.draggable).remove();
                },
                error: function () {
                    alert('error');
                }
            });

        }
    });
});