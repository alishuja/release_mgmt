$(document).ready(function () {
    function bindRemove() {
        $('a.removelink').click(function () {
            var $t = $(this);
            var storyId = $(this).parent().attr('storyid');
            $.ajax({
                type: 'POST',
                url: '/Release/Ajax',
                data: { 'releaseId': -1, 'storyId': storyId },
                dataType: 'json',
                success: function (data) {
                    $('#storypool').append($t.parent().addClass('draggable'));
                    $t.remove();
                    bindDraggable();
                },
                error: function () {
                    alert('error');
                    return;
                }
            });
        });
    }

    function bindDraggable() {
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
    }

    $(".droppable").droppable({
        accept: ".draggable",
        drop: function (event, ui) {
            // here we assing the story to a release

            var storyId = $(ui.draggable).attr('storyid');
            var storyName = $(ui.draggable, 'h2');
            var storyDesc = $(ui.draggable, 'p');
            console.log(storyName.html());
            var releaseId = $(this).attr('releaseid');

            $.ajax({
                type: 'POST',
                url: '/Release/Ajax',
                data: { 'releaseId': releaseId, 'storyId': storyId },
                dataType: 'json',
                success: function (data) {
                    $('[releaseid=' + releaseId + '] ul.inner-list').append('<li storyid="' + storyId + '">' + storyName.html() + '<a href="#" class="removelink" onclick="return false;">X</a></li>');
                    $(ui.draggable).remove();

                    //rebind
                    bindRemove();
                    bindDraggable();
                },
                error: function () {
                    alert('error');
                }
            });

        }
    });

    bindRemove();
    bindDraggable();
});